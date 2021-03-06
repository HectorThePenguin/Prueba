USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_PesoCompraDirecta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_PesoCompraDirecta]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_PesoCompraDirecta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Ricardo.Lopez
-- Create date: 2013/12/20
-- Description: SP para Para actualizar peso llegada y origen de los animales para compras directas
-- Origen     : APInterfaces
-- EXEC CorteGanado_PesoCompraDirecta 1,1
-- =============================================

CREATE PROCEDURE [dbo].[CorteGanado_PesoCompraDirecta]
@CorralOrigen INT,
@OrganizacionID INT,
@LoteID INT
AS
BEGIN

SET NOCOUNT ON;

DECLARE @TOTAL_FOLIOS AS INT
DECLARE @PesoLlegada AS INT;
DECLARE @PesoOrigen AS INT;
DECLARE @PesoCorte AS INT;
DECLARE @divPesoLlegada AS float;
DECLARE @divPesoOrigen AS float;
DECLARE @FolioEntradaActual AS INT;
DECLARE @TipoOrganizacionOrigenActual AS INT;
DECLARE @IdProcesado AS INT;
DECLARE @PesoOrigenTotal AS float;


DECLARE @FOLIOS_PARTIDA TABLE(
		ID_FOLIO_ENTRADA INT IDENTITY(1,1),
        FOLIO_ENTRADA INT ,
		TipoOrganizacionOrigenID INT );
		
	INSERT INTO @FOLIOS_PARTIDA
	SELECT EG.FolioEntrada ,OO.TipoOrganizacionID
	FROM EntradaGanado EG
	INNER JOIN Organizacion OO ON EG.OrganizacionOrigenID = OO.OrganizacionID 
	WHERE EG.Activo = 1
	AND EG.CorralID = @CorralOrigen
	AND EG.LoteID = @LoteID
	
	 SELECT @TOTAL_FOLIOS = COUNT(*) FROM @FOLIOS_PARTIDA
        SET @IdProcesado = 1;
        WHILE(@IdProcesado <= @TOTAL_FOLIOS)
        BEGIN
			
		SELECT @FolioEntradaActual = FOLIO_ENTRADA , @TipoOrganizacionOrigenActual = TipoOrganizacionOrigenID
		  FROM @FOLIOS_PARTIDA 
		 WHERE ID_FOLIO_ENTRADA = @IdProcesado;
			
			IF (  @TipoOrganizacionOrigenActual = 3)
				BEGIN
					--ORIGEN COMPRA DIRECTA
					SET @PesoLlegada = (SELECT SUM(ED.PesoOrigen) 
										  FROM EntradaGanado EGT 
									INNER JOIN EntradaGanadoCosteo EGC (NOLOCK) ON EGC.EntradaGanadoID = EGT.EntradaGanadoID AND EGC.Activo = 1
									INNER JOIN EntradaDetalle ED (NOLOCK) ON EGC.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID
										 WHERE EGT.FolioEntrada = @FolioEntradaActual 
										   AND EGT.Activo = 1 
										   AND EGT.organizacionId = @OrganizacionID);
										   
					SET @PesoCorte = (SELECT SUM(ANMT.Peso) AS PesoCorte
									   FROM animal ANT(NOLOCK)
									  INNER JOIN AnimalMovimiento ANMT(NOLOCK) ON ANT.AnimalId = ANMT.AnimalId  
									  WHERE ANT.FolioEntrada = @FolioEntradaActual 
									    AND ANT.OrganizacionIDEntrada = @OrganizacionID
										AND ANMT.tipoMovimientoID IN ( 5, 7 ,8) 
										AND ANT.Activo = 1 
										AND ANMT.Activo = 1);
						--Dividimos el peso de llegada entre la sumatoria del peso corte
						SET @divPesoLlegada = (SELECT CAST ( CAST(@PesoLlegada as float) / CAST(@PesoCorte as float) AS float))
						
						UPDATE A SET   PesoCompra = CAST( COALESCE(ROUND(B.peso * @divPesoLlegada ,0),0) AS INT),
									   PesoLlegada = CAST( COALESCE(ROUND(B.Peso * ((EG.PesoBruto-EG.PesoTara)/ @PesoCorte), 0),0) AS INT)
						FROM Animal A(NOLOCK)
						INNER JOIN AnimalMovimiento B(NOLOCK) ON A.AnimalID = B.AnimalID
						INNER JOIN Organizacion O ON A.OrganizacionIDEntrada = O.OrganizacionID /* AND O.TipoOrganizacionID=3 */
						INNER JOIN EntradaGanado EG ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1
						WHERE EG.CorralID = @CorralOrigen 
						AND  EG.LoteID = @LoteID 
						AND EG.FolioEntrada= @FolioEntradaActual
						AND B.OrganizacionID = @OrganizacionID
						AND A.Activo = 1 AND B.Activo = 1 AND B.TipoMovimientoID IN ( 5, 7 ,8);
				END
			ELSE
				BEGIN		
					SET @PesoOrigenTotal = (SELECT SUM(AM.Peso)
											  FROM Animal A(NOLOCK)
											  INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
											 WHERE A.FolioEntrada = @FolioEntradaActual
											   AND A.OrganizacionIDEntrada = @OrganizacionID
											   AND A.Activo = 1 AND AM.Activo = 1 AND AM.TipoMovimientoID IN ( 5, 7 ,8));
											
					--ORIGEN NUESTRO
					UPDATE A SET -- PesoCompra = CAST( COALESCE(ROUND(ISA.PesoCompra, 0),0) as int), 
								PesoLlegada = CAST( COALESCE(ROUND(B.Peso * ((EG.PesoBruto-EG.PesoTara)/ @PesoOrigenTotal ), 0),0) AS INT)
					FROM Animal A(NOLOCK)
					INNER JOIN AnimalMovimiento B(NOLOCK) ON A.AnimalID = B.AnimalID
					INNER JOIN Organizacion O ON A.OrganizacionIDEntrada=O.OrganizacionID AND O.TipoOrganizacionID<>3
					INNER JOIN InterfaceSalidaAnimal ISA ON A.Arete= ISA.Arete
					INNER JOIN EntradaGanado EG ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1
					WHERE EG.CorralID = @CorralOrigen
					AND  EG.LoteID = @LoteID
					AND B.OrganizacionID = @OrganizacionID
					AND A.Activo = 1 AND B.Activo = 1 AND B.TipoMovimientoID IN ( 5, 7 ,8);
				END
				
			SET @IdProcesado = @IdProcesado  + 1;

		END

RETURN 1

END
GO

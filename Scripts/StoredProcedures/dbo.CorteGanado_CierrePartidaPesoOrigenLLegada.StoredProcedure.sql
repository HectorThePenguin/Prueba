USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_CierrePartidaPesoOrigenLLegada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_CierrePartidaPesoOrigenLLegada]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_CierrePartidaPesoOrigenLLegada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		C�sar Valdez
-- Create date: 2014/12/03
-- Description: SP para actualizar peso llegada y origen de los animales para las partidas recien cerradas
-- Origen     : APInterfaces
-- sp_helptext CorteGanado_CierrePartidaPesoOrigenLLegada 967,32684, 1
--001 Jorge Luis Velazquez Araujo ** Se corrige el Sp para las partidas de Ruteo que se este borrando la tabla de animales en cada iteracion del Cursor
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_CierrePartidaPesoOrigenLLegada]
	@CorralID INT,
	@LoteID INT,
	@OrganizacionID INT
AS
BEGIN
SET NOCOUNT ON;
	DECLARE @FolioEntrada AS INT;
	DECLARE @TipoOrganizacionOrigen AS INT;
	DECLARE @TipoOrganizacionCompraDirecta AS INT = 3;
	DECLARE @PesoOrigenTotal AS DECIMAL(18,2);
	DECLARE @AretesCorte AS INT;
	DECLARE @PesoTotalCorte AS DECIMAL(18,2);
	DECLARE @PesoPromedioCorte AS INT;
	DECLARE @CabezasMuertas AS INT;
	DECLARE @PesoCabezasMuertas AS INT;
	CREATE TABLE #AnimalesCortados
	(
		[AnimalID] BIGINT
		, [Peso] INT
	);
	/* Cursor para las entradas de ganado a cerrar */	   
	/* Se llena la tabla temporal de las entradas */
	DECLARE curEntradas CURSOR LOCAL FAST_FORWARD FOR
	SELECT EG.FolioEntrada, OO.TipoOrganizacionID
      FROM EntradaGanado EG 
	 INNER JOIN Organizacion OO ON OO.OrganizacionID = EG.OrganizacionOrigenID
     INNER JOIN TipoOrganizacion Tipo ON Tipo.TipoOrganizacionID = OO.TipoOrganizacionID
     WHERE EG.OrganizacionID = @OrganizacionID
	   AND EG.LoteID = @LoteID
	   AND EG.CorralID = @CorralID
	   AND EG.Activo = 1;
	-- Apertura del cursor
	OPEN curEntradas
	-- Lectura de la primera fila del cursor
		FETCH NEXT FROM curEntradas INTO @FolioEntrada, @TipoOrganizacionOrigen
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
				/* Si es una compra directa obtener el peso origen total*/
				--IF (@TipoOrganizacionOrigen = @TipoOrganizacionCompraDirecta)
					--BEGIN
						/* Se obtienen los pesos PesoOrigenTotal */
						SELECT @PesoOrigenTotal = SUM(ED.PesoOrigen)
						  FROM EntradaGanado EGT 
						 INNER JOIN EntradaGanadoCosteo EGC (NOLOCK) ON EGC.EntradaGanadoID = EGT.EntradaGanadoID AND EGC.Activo = 1
						 INNER JOIN EntradaDetalle ED (NOLOCK) ON EGC.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID
						 WHERE EGT.FolioEntrada = @FolioEntrada 
						   AND EGT.organizacionId = @OrganizacionID
						   AND EGT.Activo = 1 ;
					--END
				/* Obtener animales cortados de la partida */
				-- Insertar animales con Su primer movimiento en el inventario ---> Corte, Muerte, EntradaEnfermeria, etc...
				INSERT INTO #AnimalesCortados 
				SELECT A.AnimalID, AM.Peso
				  FROM Animal A (NOLOCK)
				 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimaLID 
				 WHERE AM.AnimalMovimientoID IN (
							SELECT MIN(AMi.AnimalMovimientoID) AnimalMovimientoID 
							  FROM Animal Ai(NOLOCK)
							 INNER JOIN AnimalMovimiento AMi(NOLOCK) ON Ai.AnimalID = AMi.AnimaLID 
							 WHERE Ai.OrganizacionIDEntrada = @OrganizacionID
							   AND Ai.FolioEntrada IN ( @FolioEntrada )
							 GROUP BY AMi.AnimalID);
				/* Se obtiene al peso total Corte(El primer movimiento en el inventario --> Corte, Muerte, EntradaEnfermeria, etc...) */
				SELECT @PesoTotalCorte = SUM(AC.Peso), 
				       @AretesCorte = COUNT(1)
				  FROM #AnimalesCortados AC;  
				-- validar si la partida tuvo cabezas muertas				  
				SELECT @CabezasMuertas = COUNT(EGM.Arete)
				  FROM EntradaGanado EG 
				 INNER JOIN EntradaGanadoMuerte EGM (NOLOCK) ON EGM.EntradaGanadoID = EG.EntradaGanadoID
				 WHERE EG.FolioEntrada = @FolioEntrada 
				   AND EG.organizacionId = @OrganizacionID
				   AND EG.Activo = 1 ;
				/* Si se tienen cabezas muertas */
				IF (COALESCE(@CabezasMuertas,0) > 0)
					BEGIN
						/* Se promedia el peso corte*/
						SET @PesoPromedioCorte = @PesoTotalCorte / @AretesCorte;
						/* Se calcula el peso de las cabezas muertas*/
						SET @PesoCabezasMuertas = @CabezasMuertas * COALESCE(@PesoPromedioCorte,0);
						/* Se calcula el peso total corte*/
						SET @PesoTotalCorte = @PesoTotalCorte + @PesoCabezasMuertas;
					END
				/* Se actualizan los pesos Origen y llegada de los animales*/
				UPDATE A 
				   SET PesoCompra = 
						CASE WHEN Oo.TipoOrganizacionID=@TipoOrganizacionCompraDirecta 
						  THEN CAST(COALESCE(ROUND((@PesoOrigenTotal/@PesoTotalCorte)*AC.Peso ,0),0) AS INT)
						  ELSE A.PesoCompra 
						END,
					   PesoLlegada = 
					    CAST(COALESCE(ROUND(((EG.PesoBruto-EG.PesoTara)/@PesoTotalCorte)*AC.Peso, 0),0) AS INT)
				  FROM Animal A(NOLOCK)
				 INNER JOIN #AnimalesCortados AC ON A.AnimalId = AC.AnimalID
				 INNER JOIN EntradaGanado EG ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1 and eg.OrganizacionID = @OrganizacionID
				 INNER JOIN Organizacion Oo ON EG.OrganizacionOrigenID = Oo.OrganizacionID;    
				 --Inicia 001
				 declare @PesoCompraFaltante int
				 declare @AnimalesPesoCompra int
				 declare @UltimoAnimalID int
				 declare @PesoCompraPromedio int
				 declare @PesoCompraPromedioFinal int
				 set @PesoCompraFaltante = (
				  select 				 
					@PesoOrigenTotal - sum(a.PesoCompra) 				 
				  FROM Animal A(NOLOCK)
				 INNER JOIN EntradaGanado EG (nolock) ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1 and eg.OrganizacionID = @OrganizacionID
				 INNER JOIN Organizacion Oo (nolock) ON EG.OrganizacionOrigenID = Oo.OrganizacionID  
				 where EG.FolioEntrada = @FolioEntrada
				 group by Eg.Folioentrada, eg.PesoBruto, eg.PesoTara
				 )
				 	 set @AnimalesPesoCompra = (
				  select 				 
					count(a.AnimalID)					 				 
				  FROM Animal A(NOLOCK)
				 INNER JOIN EntradaGanado EG (nolock) ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1 and eg.OrganizacionID = @OrganizacionID
				 INNER JOIN Organizacion Oo (nolock) ON EG.OrganizacionOrigenID = Oo.OrganizacionID  
				 where EG.FolioEntrada = @FolioEntrada
				 and a.PesoCompra = 0
				 group by Eg.Folioentrada, eg.PesoBruto, eg.PesoTara
				 )
				  set @UltimoAnimalID = (
				  select 				 
					top 1 a.AnimalID
				  FROM Animal A(NOLOCK)
				 INNER JOIN EntradaGanado EG (nolock) ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1 and eg.OrganizacionID = @OrganizacionID
				 INNER JOIN Organizacion Oo (nolock) ON EG.OrganizacionOrigenID = Oo.OrganizacionID  
				 where EG.FolioEntrada = @FolioEntrada
				 and a.PesoCompra = 0
				 order by a.AnimalID desc
				 )
				 set @PesoCompraPromedio = (@PesoCompraFaltante / @AnimalesPesoCompra)
				 set @PesoCompraPromedioFinal = @PesoCompraPromedio + (@PesoCompraFaltante % @AnimalesPesoCompra)
				 if @AnimalesPesoCompra is not null 
				 begin 
					UPDATE A 
				   SET PesoCompra = 	@PesoCompraPromedio					
				  FROM Animal A(NOLOCK)
				 INNER JOIN #AnimalesCortados AC (NOLOCK) ON A.AnimalId = AC.AnimalID
				 INNER JOIN EntradaGanado EG (NOLOCK) ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1 and eg.OrganizacionID = @OrganizacionID
				 INNER JOIN Organizacion Oo (NOLOCK) ON EG.OrganizacionOrigenID = Oo.OrganizacionID
				 where a.PesoCompra = 0
				 and a.AnimalID <> @UltimoAnimalID
				 		UPDATE A 
				   SET PesoCompra = 	@PesoCompraPromedioFinal					
				  FROM Animal A(NOLOCK)
				 INNER JOIN #AnimalesCortados AC (NOLOCK) ON A.AnimalId = AC.AnimalID
				 INNER JOIN EntradaGanado EG (NOLOCK) ON EG.FolioEntrada= A.FolioEntrada AND EG.Activo = 1 and eg.OrganizacionID = @OrganizacionID
				 INNER JOIN Organizacion Oo (NOLOCK) ON EG.OrganizacionOrigenID = Oo.OrganizacionID
				 where a.PesoCompra = 0
				 and a.AnimalID = @UltimoAnimalID
				 end
				 delete #AnimalesCortados 
				 -- Finaliza 001
				FETCH NEXT FROM curEntradas INTO @FolioEntrada, @TipoOrganizacionOrigen
			END
		-- Cierre del cursor
		CLOSE curEntradas
	-- Liberar los recursos
	DEALLOCATE curEntradas
END

GO

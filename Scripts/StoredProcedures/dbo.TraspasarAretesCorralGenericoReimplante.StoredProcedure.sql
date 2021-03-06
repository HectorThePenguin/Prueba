USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasarAretesCorralGenericoReimplante]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasarAretesCorralGenericoReimplante]
GO
/****** Object:  StoredProcedure [dbo].[TraspasarAretesCorralGenericoReimplante]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2013/12/18
-- Description: SP para Crear un registro en la tabla de AnimalMovimiento
-- Origen     : APInterfaces
-- EXEC [dbo].[TraspasarAretesCorralGenericoReimplante] 1
-- =============================================
CREATE PROCEDURE [dbo].[TraspasarAretesCorralGenericoReimplante]
	@OrganizacionID INT
AS
BEGIN
   DECLARE @CorralID INT;
   DECLARE @LoteID INT;
   DECLARE @LoteIDOrigen INT;
   DECLARE @AnimalID BIGINT;
   DECLARE @Peso INT;
   DECLARE @Temperatura INT;
   DECLARE @TrampaID INT;
   DECLARE @OperadorID INT;
   DECLARE @MovTraspasoDeGanado INT = 17;
   DECLARE @UsuarioCreacionID INT = 1;
   DECLARE @TmpAretesErrorReimplanteID INT;
	/* Se obtenen los aretes a procesar y cambiar de corral*/
   DECLARE curAretes CURSOR FOR 		
	SELECT TMP.TmpAretesErrorReimplanteID, A.AnimalID, AM.Peso, AM.Temperatura, AM.TrampaID, AM.OperadorID, AM.LoteID
	  FROM TmpAretesErrorReimplante TMP
	 INNER JOIN Animal A ON A.Arete = tmp.Arete
	 INNER JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID AND AM.Activo = 1
	 WHERE 1 = 1
	   AND AM.OrganizacionID = @OrganizacionID
	   AND CONVERT(BIGINT,TMP.Arete) > 0
	   AND TMP.Activo = 1;	
	/* Se obtienen los Datos del corral Destino*/
	SELECT TOP 1 @CorralID = C.CorralID, @LoteID = L.LoteID
	  FROM Lote L 
	 INNER JOIN Corral C ON C.CorralID = L.CorralID
	 WHERE 1 = 1 
	   AND L.Activo = 1
	   AND C.Codigo = 'R99'
	   AND L.OrganizacionID = @OrganizacionID;
	-- Apertura del cursor
	OPEN curAretes
	-- Lectura de la primera fila del cursor
		FETCH NEXT FROM curAretes INTO @TmpAretesErrorReimplanteID, @AnimalID, @Peso, @Temperatura, @TrampaID, @OperadorID, @LoteIDOrigen
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
				/* Se guarda el movimiento de transpaso */
				EXECUTE AnimalMovimiento_Guardar @AnimalID, @OrganizacionID, @CorralID, @LoteID, @Peso, @Temperatura, @MovTraspasoDeGanado, @TrampaID, @OperadorID, '', 1, @UsuarioCreacionID;
				/* Decrementar lote Origen */
				UPDATE Lote
				SET Cabezas = Cabezas - 1,
					Activo = CASE WHEN (Cabezas - 1) = 0 THEN 0 ELSE 1 END,
					FechaSalida = GETDATE(),
					UsuarioModificacionID = @UsuarioCreacionID,
					FechaModificacion = GETDATE()
				WHERE LoteID = @LoteIDOrigen;
				/* Incrementar lote Destino */
				UPDATE Lote
				SET Cabezas = Cabezas + 1,
					CabezasInicio = CabezasInicio + 1,
					UsuarioModificacionID = @UsuarioCreacionID,
					FechaModificacion = GETDATE()
				WHERE LoteID = @LoteID;
				UPDATE TmpAretesErrorReimplante
				   SET Observaciones = 'OK', Activo = 0, FechaModificacion = GETDATE()
				 WHERE TmpAretesErrorReimplanteID = @TmpAretesErrorReimplanteID;
				FETCH NEXT FROM curAretes INTO @TmpAretesErrorReimplanteID, @AnimalID, @Peso, @Temperatura, @TrampaID, @OperadorID, @LoteIDOrigen
			END
		-- Cierre del cursor
		CLOSE curAretes
	-- Liberar los recursos
	DEALLOCATE curAretes
	/* Se actualizan los que no se pueden procesar */ 
	UPDATE TMP
	   SET Observaciones = 'No Existe / Es Nuevo', Activo = 0, FechaModificacion = GETDATE()
	  FROM TmpAretesErrorReimplante TMP
	  LEFT JOIN Animal A ON A.Arete = tmp.Arete
	  LEFT JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID AND AM.Activo = 1
	 WHERE 1 = 1
	   AND CONVERT(BIGINT,TMP.Arete) > 0
	   AND TMP.Activo = 1
	   AND A.Arete IS NULL;
END

GO

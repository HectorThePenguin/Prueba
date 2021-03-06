USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_GuardarDetalleOrden]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_GuardarDetalleOrden]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_GuardarDetalleOrden]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/02/27
-- Description: SP para guardar el detalle de una orden se sacrificio
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_GuardarDetalleOrden 1,0,1,1,'<ROOT>
--  <DetallOrdenSacrificio>
--    <OrdenSacrificioDetalleID>1</OrdenSacrificioDetalleID>
--    <CorraletaID>2</CorraletaID>
--    <LoteID>1</LoteID>
--    <CabezasLote>12</CabezasLote>
--    <DiasEngordaGrano>0</DiasEngordaGrano>
--    <DiasRetiro>-14</DiasRetiro>
--    <CabezasSacrificio>10</CabezasSacrificio>
--    <Activo>1</Activo>
--  </DetallOrdenSacrificio>
--</ROOT>'
--001 Jorge Luis Velazquez Araujo 22/05/2015 ***Se modifica para grabar el TipoGanadoID en la tabla OrdenSacrificioDetalle
--002 Jorge Luis Velazquez Araujo 05/11/2015 ***Se modifica para que las cabezas de los lotes tome en cuenta todas las ordenes se sacrificio
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_GuardarDetalleOrden]
    @OrdenSacrificioID INT,
	@FolioSalida INT,
	@Activo INT,
	@UsuarioCreacionID INT,
	@DetalleOrdenSacrificio XML
	
AS
BEGIN
    DECLARE @IdentityID BIGINT;
	DECLARE @tmpDetalleOrden AS TABLE
	(
	    OrdenSacrificioDetalleID INT,
		CorraletaID INT,
		CorraletaCodigo VARCHAR(10),
		Proveedor VARCHAR(100),
		Clasificacion VARCHAR(100),
		LoteID INT,
		CabezasLote INT,
		DiasEngordaGrano INT,
		DiasRetiro INT,
		CabezasSacrificio INT,
		Turno INT,
        Activo INT,
		Orden INT,
		TipoGanadoID INT --001
	)
	
	INSERT @tmpDetalleOrden(
			OrdenSacrificioDetalleID,
			CorraletaID,
			CorraletaCodigo,
			Proveedor,
			Clasificacion,
			LoteID,
			CabezasLote,
			DiasEngordaGrano,
			DiasRetiro,
			CabezasSacrificio,
			Turno,
			Activo,
			Orden,
			TipoGanadoID --001
		)
	SELECT 
		OrdenSacrificioDetalleID = T.item.value('./OrdenSacrificioDetalleID[1]', 'INT'),
		CorraletaID = T.item.value('./CorraletaID[1]', 'INT'),
		CorraletaCodigo = T.item.value('./CorraletaCodigo[1]', 'VARCHAR(10)'),
		Proveedor = T.item.value('./Proveedor[1]', 'VARCHAR(100)'),
		Clasificacion = T.item.value('./Clasificacion[1]', 'VARCHAR(100)'),
		LoteID = T.item.value('./LoteID[1]', 'INT'),
		CabezasLote = T.item.value('./CabezasLote[1]', 'INT'),
		DiasEngordaGrano = T.item.value('./DiasEngordaGrano[1]', 'INT'),
		DiasRetiro = T.item.value('./DiasRetiro[1]', 'INT'),
		CabezasSacrificio = T.item.value('./CabezasSacrificio[1]', 'INT'),
		Turno = T.item.value('./Turno[1]', 'INT'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		Orden = T.item.value('./Orden[1]', 'INT'),
		TipoGanadoID = T.item.value('./TipoGanadoID[1]', 'INT')--001
	FROM  @DetalleOrdenSacrificio.nodes('ROOT/DetallOrdenSacrificio') AS T(item)
	
			SELECT @FolioSalida = MAX(FolioSalida) from OrdenSacrificioDetalle
	
			/* Se crea registro en la tabla de Orden sacrificio*/
			INSERT INTO OrdenSacrificioDetalle (
				OrdenSacrificioID,
				FolioSalida,
				CorraletaID,
				CorraletaCodigo,
				Proveedor,
				Clasificacion,
				LoteID,
				CabezasLote,
				DiasEngordaGrano,
				DiasRetiro,
				CabezasSacrificio,
				Turno,
				Activo,
				FechaCreacion,
				UsuarioCreacion,
				Orden,
				TipoGanadoID--001
				)
			SELECT @OrdenSacrificioID,
				   @FolioSalida + (ROW_NUMBER() over (order by Turno)),
				   CorraletaID,
				   CorraletaCodigo,
				   Proveedor,
				   Clasificacion,
				   LoteID,
				   CabezasLote,
				   DiasEngordaGrano,
				   DiasRetiro,
				   CabezasSacrificio,
				   Turno,
				   @Activo,
				   GETDATE(),
				   @UsuarioCreacionID,
				   Orden,
				   TipoGanadoID --001
			FROM @tmpDetalleOrden
			WHERE OrdenSacrificioDetalleID = 0
			
			
			UPDATE OrdenSacrificioDetalle
			   SET 
				   CorraletaID = TMP.CorraletaID,
				   CorraletaCodigo = TMP.CorraletaCodigo,
				   Proveedor = TMP.Proveedor,
				   Clasificacion = TMP.Clasificacion,
				   LoteID = TMP.LoteID,
				   CabezasLote = TMP.CabezasLote,
				   DiasEngordaGrano = TMP.DiasEngordaGrano,
				   DiasRetiro = TMP.DiasRetiro,
				   CabezasSacrificio = TMP.CabezasSacrificio,
				   Turno = TMP.Turno,
				   Activo = TMP.Activo,
				   Orden=TMP.Orden,
				   UsuarioModificacion = @UsuarioCreacionID,
				   FechaModificacion = GETDATE()--,
				   --TipoGanadoID = tmp.TipoGanadoID --001
		    FROM OrdenSacrificioDetalle OSDTMP
			INNER JOIN  @tmpDetalleOrden TMP ON TMP.OrdenSacrificioDetalleID = OSDTMP.OrdenSacrificioDetalleID
			AND TMP.OrdenSacrificioDetalleID > 0
			
				   
				   
			UPDATE Lote
			   SET Activo = 0
			  FROM Lote L
			 INNER JOIN @tmpDetalleOrden TMP ON TMP.LoteID=L.LoteID			 
			 AND (L.Cabezas - (SELECT SUM(osd.CabezasSacrificio) FROM OrdenSacrificioDetalle osd WHERE Activo = @Activo AND osd.LoteID=L.LoteID)  ) = 0 --002
			
			SELECT 
				OrdenSacrificioDetalleID,
				OrdenSacrificioID,
				FolioSalida,
				CorraletaID,
				CorraletaCodigo,
				Proveedor,
				Clasificacion,
				LoteID,
				CabezasLote,
				DiasEngordaGrano,
				DiasRetiro,
				CabezasSacrificio,
				Turno,
				Orden,
				UsuarioCreacion,
				TipoGanadoID, --001
				0 AS CabezasActuales
		     FROM OrdenSacrificioDetalle 
			WHERE OrdenSacrificioID = @OrdenSacrificioID
			  AND Activo= @Activo
			ORDER BY Orden

END
GO

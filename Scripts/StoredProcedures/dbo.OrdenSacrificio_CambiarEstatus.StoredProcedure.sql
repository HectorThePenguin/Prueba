USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CambiarEstatus]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_CambiarEstatus]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CambiarEstatus]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/01/03
-- Description: SP para cambiar el estatus de los detalles de una orden de sacrificio
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_CambiarEstatus 1,'<ROOT>
--  <DetallOrdenSacrificio>
--    <OrdenSacrificioDetalleID>1</OrdenSacrificioDetalleID>
--    <Activo>1</Activo>
--  </DetallOrdenSacrificio>
--</ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_CambiarEstatus]
	@UsuarioModificacion INT,
	@DetalleOrdenSacrificio XML
AS
BEGIN
	DECLARE @tmpEstatusDetalleOrden AS TABLE
	(
	    OrdenSacrificioDetalleID INT,
        Activo INT	
	)
	INSERT @tmpEstatusDetalleOrden(
			OrdenSacrificioDetalleID,
			Activo
		)
	SELECT 
		OrdenSacrificioDetalleID = T.item.value('./OrdenSacrificioDetalleID[1]', 'INT'),
		Activo  = T.item.value('./Activo[1]', 'INT')
	FROM  @DetalleOrdenSacrificio.nodes('ROOT/DetallOrdenSacrificio') AS T(item)
	UPDATE OrdenSacrificioDetalle
	   SET 
		   Activo = TMP.Activo,
		   UsuarioModificacion = @UsuarioModificacion,
		   FechaModificacion = GETDATE()
	FROM OrdenSacrificioDetalle OSDTMP
	INNER JOIN  @tmpEstatusDetalleOrden TMP ON TMP.OrdenSacrificioDetalleID = OSDTMP.OrdenSacrificioDetalleID
	AND TMP.OrdenSacrificioDetalleID>0
	UPDATE Lote
	   SET 
		   Activo = 1,
		   UsuarioModificacionID = @UsuarioModificacion,
		   FechaModificacion = GETDATE()
	FROM Lote L
	INNER JOIN OrdenSacrificioDetalle OSD  ON OSD.LoteID=L.LoteID
	INNER JOIN  @tmpEstatusDetalleOrden tmp ON tmp.OrdenSacrificioDetalleID = OSD.OrdenSacrificioDetalleID
	WHERE tmp.Activo = 0
END

GO

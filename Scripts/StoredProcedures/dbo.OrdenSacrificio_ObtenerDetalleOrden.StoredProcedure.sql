USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_ObtenerDetalleOrden]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_ObtenerDetalleOrden]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_ObtenerDetalleOrden]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/02/27
-- Description: SP para obtener el detalle de una orden se sacrificio
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_ObtenerDetalleOrden 1,1
-- 001 Jorge Luis Velazquez 04/03/2016 **Se agrega para que regrese las cabezas actuales del lote
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_ObtenerDetalleOrden]
    @OrdenSacrificioID INT,
	@Activo INT
AS
BEGIN
		SELECT 
			osd.OrdenSacrificioDetalleID,
			osd.OrdenSacrificioID,
			osd.FolioSalida,
			osd.CorraletaID,
			osd.LoteID,
			osd.CorraletaCodigo,
			osd.CabezasLote,
			osd.DiasEngordaGrano,
			osd.DiasRetiro,
			osd.CabezasSacrificio,
			osd.Turno,
			osd.Proveedor,
			osd.Clasificacion,
			osd.Orden,
			osd.UsuarioCreacion,
			(select count(am.AnimalID) from AnimalMovimiento am (nolock) 							 
				where am.LoteID = lo.LoteID 
				and am.Activo = 1 
				and am.TipoMovimientoID not in (8,11,16)) As CabezasActuales --001
		FROM OrdenSacrificioDetalle osd
		inner join Lote lo on osd.LoteID = lo.LoteID --001
		WHERE OrdenSacrificioID = @OrdenSacrificioID
		AND osd.Activo = @Activo
		ORDER BY Orden
END

GO

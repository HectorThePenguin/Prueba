USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoDetalle_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProductoDetalle_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProductoDetalle_ObtenerPorID]
@SolicitudProductoDetalleID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		SolicitudProductoDetalleID,
		SolicitudProductoID,
		ProductoID,
		Cantidad,
		CamionRepartoID,
		EstatusID,
		Activo
	FROM SolicitudProductoDetalle
	WHERE SolicitudProductoDetalleID = @SolicitudProductoDetalleID
	SET NOCOUNT OFF;
END

GO

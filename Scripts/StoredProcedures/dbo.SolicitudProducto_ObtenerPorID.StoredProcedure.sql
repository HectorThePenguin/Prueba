USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProducto_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProducto_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProducto_ObtenerPorID]
@SolicitudProductoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		SolicitudProductoID,
		OrganizacionID,
		FolioSolicitud,
		FechaSolicitud,
		UsuarioIDSolicita,
		EstatusID,
		UsuarioIDAutoriza,
		FechaAutorizado,
		UsuarioIDEntrega,
		FechaEntrega,
		CentroCostoID,
		AlmacenID,
		AlmacenMovimientoID,
		ObservacionUsuarioEntrega,
		ObservacionUsuarioAutoriza,
		Activo
	FROM SolicitudProducto
	WHERE SolicitudProductoID = @SolicitudProductoID
	SET NOCOUNT OFF;
END

GO

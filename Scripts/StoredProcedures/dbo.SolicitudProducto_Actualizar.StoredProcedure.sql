USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProducto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProducto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProducto_Actualizar]
@SolicitudProductoID int,
@OrganizacionID int,
@FolioSolicitud int,
@FechaSolicitud smalldatetime,
@UsuarioIDSolicita int,
@EstatusID int,
@UsuarioIDAutoriza int,
@FechaAutorizado smalldatetime,
@UsuarioIDEntrega int,
@FechaEntrega smalldatetime,
@CentroCostoID int,
@AlmacenID int,
@AlmacenMovimientoID bigint,
@ObservacionUsuarioEntrega varchar(255),
@ObservacionUsuarioAutoriza varchar(255),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE SolicitudProducto SET
		OrganizacionID = @OrganizacionID,
		FolioSolicitud = @FolioSolicitud,
		FechaSolicitud = @FechaSolicitud,
		UsuarioIDSolicita = @UsuarioIDSolicita,
		EstatusID = @EstatusID,
		UsuarioIDAutoriza = @UsuarioIDAutoriza,
		FechaAutorizado = @FechaAutorizado,
		UsuarioIDEntrega = @UsuarioIDEntrega,
		FechaEntrega = @FechaEntrega,
		CentroCostoID = @CentroCostoID,
		AlmacenID = @AlmacenID,
		AlmacenMovimientoID = CASE WHEN @AlmacenMovimientoID > 0 THEN @AlmacenMovimientoID ELSE NULL END,
		ObservacionUsuarioEntrega = @ObservacionUsuarioEntrega,
		ObservacionUsuarioAutoriza = @ObservacionUsuarioAutoriza,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE SolicitudProductoID = @SolicitudProductoID
	SET NOCOUNT OFF;
END

GO

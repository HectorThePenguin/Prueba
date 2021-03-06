USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProducto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProducto_Crear
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProducto_Crear]
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
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT SolicitudProducto (
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
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@SolicitudProductoID,
		@OrganizacionID,
		@FolioSolicitud,
		@FechaSolicitud,
		@UsuarioIDSolicita,
		@EstatusID,
		@UsuarioIDAutoriza,
		@FechaAutorizado,
		@UsuarioIDEntrega,
		@FechaEntrega,
		@CentroCostoID,
		@AlmacenID,
		@AlmacenMovimientoID,
		@ObservacionUsuarioEntrega,
		@ObservacionUsuarioAutoriza,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

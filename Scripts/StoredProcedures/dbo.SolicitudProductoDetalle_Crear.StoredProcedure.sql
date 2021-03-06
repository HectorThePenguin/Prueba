USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProductoDetalle_Crear
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProductoDetalle_Crear]
@SolicitudProductoDetalleID int,
@SolicitudProductoID int,
@ProductoID int,
@Cantidad decimal(18,2),
@CamionRepartoID int,
@EstatusID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT SolicitudProductoDetalle (
		SolicitudProductoDetalleID,
		SolicitudProductoID,
		ProductoID,
		Cantidad,
		CamionRepartoID,
		EstatusID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@SolicitudProductoDetalleID,
		@SolicitudProductoID,
		@ProductoID,
		@Cantidad,
		@CamionRepartoID,
		@EstatusID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

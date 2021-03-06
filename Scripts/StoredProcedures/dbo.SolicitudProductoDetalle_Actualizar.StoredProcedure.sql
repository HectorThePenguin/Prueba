USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProductoDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProductoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SolicitudProductoDetalle_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[SolicitudProductoDetalle_Actualizar]
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
	UPDATE SolicitudProductoDetalle SET
		SolicitudProductoID = @SolicitudProductoID,
		ProductoID = @ProductoID,
		Cantidad = @Cantidad,
		CamionRepartoID = @CamionRepartoID,
		EstatusID = @EstatusID,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE SolicitudProductoDetalleID = @SolicitudProductoDetalleID
	SET NOCOUNT OFF;
END

GO

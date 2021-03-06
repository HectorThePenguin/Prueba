USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaPrecio_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaPrecio_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CausaPrecio_Actualizar]
@CausaPrecioID int,
@CausaSalidaID int,
@OrganizacionID int,
@Precio decimal(18,2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CausaPrecio SET
		OrganizacionID = @OrganizacionID,
		CausaSalidaID = @CausaSalidaID,
		Precio = @Precio,		
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CausaPrecioID = @CausaPrecioID
	SET NOCOUNT OFF;
END

GO

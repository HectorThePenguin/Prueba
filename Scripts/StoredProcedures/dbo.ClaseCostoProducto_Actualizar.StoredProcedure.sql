USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ClaseCostoProducto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_Actualizar]
@ClaseCostoProductoID int,
@AlmacenID int,
@ProductoID int,
@CuentaSAPID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ClaseCostoProducto SET
		AlmacenID = @AlmacenID,
		ProductoID = @ProductoID,
		CuentaSAPID = @CuentaSAPID,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ClaseCostoProductoID = @ClaseCostoProductoID
	SET NOCOUNT OFF;
END

GO

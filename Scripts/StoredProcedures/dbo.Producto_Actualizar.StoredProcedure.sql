USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Producto_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Producto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Producto_Actualizar]
@ProductoID int,
@Descripcion varchar(50),
@SubFamiliaID int,
@UnidadID int,
@ManejaLote bit,
@MaterialSAP varchar(18),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Producto SET
		Descripcion = @Descripcion,
		SubFamiliaID = @SubFamiliaID,
		UnidadID = @UnidadID,
		ManejaLote = @ManejaLote,
		MaterialSAP = @MaterialSAP,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProductoID = @ProductoID
	SET NOCOUNT OFF;
END

GO

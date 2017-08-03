IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[PrecioProducto_Actualizar]'))
	DROP PROCEDURE [dbo].[PrecioProducto_Actualizar]; 
GO
--======================================================
-- Author     : Eric Roberto García Félix
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : PrecioProducto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[PrecioProducto_Actualizar]
@PrecioProductoID int,
@ProductoID int,
@PrecioMaximo decimal (10,2),
@OrganizacionID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CatPrecioProducto SET
		ProductoID = @ProductoID,
		PrecioMaximo = @PrecioMaximo,
		OrganizacionID = @OrganizacionID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProductoID = @ProductoID 
				AND OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

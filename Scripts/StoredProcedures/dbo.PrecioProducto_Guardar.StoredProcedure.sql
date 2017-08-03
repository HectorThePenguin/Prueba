IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[PrecioProducto_Guardar]'))
	DROP PROCEDURE [dbo].[PrecioProducto_Guardar]; 
GO
--======================================================
-- Author     : Eric García
-- Create date: 14/10/2015 12:00:00 a.m.
-- Description: Crea precio de producto
-- SpName     : EXEC PrecioProducto_Guardar 1,1.00,1,1,1
--======================================================
CREATE PROCEDURE [dbo].[PrecioProducto_Guardar]
@ProductoID int,
@PrecioMaximo decimal (10,2),
@OrganizacionID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	
		DECLARE @IdentityID INT;
		SET @IdentityID = (SELECT ProductoID 
													FROM CatPrecioProducto 
																WHERE ProductoID = @ProductoID AND OrganizacionID = @OrganizacionID)

IF (  @IdentityID > 0 )
		BEGIN
			--Se actualiza el precio del producto
			UPDATE CatPrecioProducto 
				SET PrecioMaximo = @PrecioMaximo,
						Activo = @Activo,
						UsuarioModificacionID = @UsuarioCreacionID,
						FechaModificacion = GETDATE()
			WHERE ProductoID = @IdentityID
			
		END
	ELSE
		BEGIN		
			/* Se crea registro en la tabla de CatPrecioProducto*/
			INSERT CatPrecioProducto (
							ProductoID,
							PrecioMaximo,
							OrganizacionID,
							Activo,
							UsuarioCreacionID,
							FechaCreacion,
							FechaModificacion
						)
						VALUES(
							@ProductoID,
							@PrecioMaximo,
							@OrganizacionID,
							@Activo,
							@UsuarioCreacionID,
							GETDATE(),
							GETDATE()
						)
		END

	SET NOCOUNT OFF;
END

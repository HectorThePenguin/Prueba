--======================================================  
-- Author     : Edwin Martin Angulo Juarez
-- Create date: 22-10-20146
-- Description: sp para regresar los almacenes con inventario y activos que contienene un producto
-- SpName     : AlmacenInventarioLote_ObtenerLotesProducto 1,4,81,0
--======================================================  
IF EXISTS (SELECT  object_id FROM    sys.objects WHERE   object_id = OBJECT_ID(N'AbastoEnvioAlimento_ObtenerAlmacenProducto') AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE AbastoEnvioAlimento_ObtenerAlmacenProducto
END
GO
CREATE PROCEDURE [dbo].[AbastoEnvioAlimento_ObtenerAlmacenProducto]
	 @UsuarioID INT
	,@ProductoID INT
	,@Cantidad BIT
	,@Activo BIT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		Distinct
		ta.AlmacenID,
		ta.Descripcion
	FROM Almacen ta 
		INNER JOIN AlmacenInventario tai ON ta.AlmacenID = tai.AlmacenID
		INNER JOIN Usuario usr ON usr.OrganizacionID = ta.OrganizacionID
	WHERE tai.ProductoID = @ProductoID
		AND ( @Cantidad = 0 OR (@Cantidad = 1 AND tai.Cantidad > 0))
		AND usr.UsuarioID = @UsuarioID
		AND ta.Activo = @Activo
END




	



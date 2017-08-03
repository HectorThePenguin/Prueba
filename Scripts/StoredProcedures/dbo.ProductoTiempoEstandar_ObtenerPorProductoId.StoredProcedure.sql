IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ProductoTiempoEstandar_ObtenerPorProductoId]'))
BEGIN
 DROP PROCEDURE [dbo].[ProductoTiempoEstandar_ObtenerPorProductoId]
END
GO

--=============================================
-- Author     : Daniel Benitez
-- Create date: 2017/02/20
-- Description: 
-- ProductoTiempoEstandar_ObtenerPorProductoId 100
--=============================================
CREATE PROCEDURE [dbo].[ProductoTiempoEstandar_ObtenerPorProductoId]
@ProductoId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT pte.ProductoTiempoEstandarID, pte.ProductoID, p.Descripcion, 
	CONVERT(VARCHAR(8),Tiempo,126) Tiempo, 
	pte.Activo 
	FROM ProductoTiempoEstandar pte
	INNER JOIN Producto p ON pte.ProductoID = p.ProductoID
	WHERE pte.ProductoId = @ProductoId

	SET NOCOUNT OFF;
END

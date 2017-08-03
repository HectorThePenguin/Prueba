-- =============================================
-- Author: Gilberto Carranza
-- Create date: 11/08/2014
-- Description:  Obtiene los productos por almacen
-- Modificado: 28/10/2016
-- Modifico: Edwin M. Angulo
-- Descripcion: Se Agrego el estatus Activo, dependiendo de los estatus de los registros
-- ClaseCostoProducto_ObtenerPorProductoAlmacen 1, 100
-- =============================================
IF EXISTS (SELECT  object_id FROM    sys.objects WHERE   object_id = OBJECT_ID(N'ClaseCostoProducto_ObtenerPorProductoAlmacen') AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE ClaseCostoProducto_ObtenerPorProductoAlmacen
END
GO
CREATE PROCEDURE dbo.ClaseCostoProducto_ObtenerPorProductoAlmacen
	@AlmacenID INT,
	@ProductoID INT
AS
BEGIN 
	SET NOCOUNT ON
		SELECT ClaseCostoProductoID
			,AlmacenID
			,ProductoID
			,C.CuentaSAPID
			,C.Descripcion AS CuentaSAPDescripcion
			,C.CuentaSAP AS CuentaSAP
			,CP.Activo
			,C.Activo [CuentaSAPActivo]
		FROM ClaseCostoProducto CP
			INNER JOIN CuentaSAP C ON (CP.CuentaSAPID = C.CuentaSAPID)
		WHERE AlmacenID = @AlmacenID AND ProductoID = @ProductoID
	SET NOCOUNT OFF
END


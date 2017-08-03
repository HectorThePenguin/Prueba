IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ProductoTiempoEstandar_ObtenerPorPagina]'))
BEGIN
 DROP PROCEDURE [dbo].[ProductoTiempoEstandar_ObtenerPorPagina]
END
GO

--=============================================
-- Author     : Daniel Benitez
-- Create date: 2017/02/20
-- Description: 
-- ProductoTiempoEstandar_ObtenerPorPagina 0, 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[ProductoTiempoEstandar_ObtenerPorPagina]
@ProductoId INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
	pte.ProductoTiempoEstandarID, pte.ProductoID, p.Descripcion, 
	CONVERT(VARCHAR(8),Tiempo,126) Tiempo, 
	pte.Activo 
	INTO #tiempos
	FROM ProductoTiempoEstandar pte
	INNER JOIN Producto p ON pte.ProductoID = p.ProductoID
	WHERE @ProductoId = CASE WHEN @ProductoId = 0 THEN 0 ELSE pte.ProductoId END AND pte.activo = @Activo

	SELECT 
	ProductoTiempoEstandarID, ProductoID, Descripcion, 
	Tiempo, 
	Activo 
	FROM #tiempos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	
	SELECT
	COUNT(ProductoTiempoEstandarID) AS [TotalReg]
	FROM #tiempos
	
	DROP TABLE #tiempos
	SET NOCOUNT OFF;
END

IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[PrecioProducto_ObtenerPorPagina]'))
	DROP PROCEDURE [dbo].[PrecioProducto_ObtenerPorPagina]; 
GO
--=============================================
-- Author      Eric García
-- Create date 20151014
-- Description 
-- Name  EXEC PrecioProducto_ObtenerPorPagina 1,'',1,1,5
--=============================================
CREATE PROCEDURE [dbo].[PrecioProducto_ObtenerPorPagina]
@OrganizacionID INT,
@ProductoDescripcion VARCHAR (50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY O.Descripcion ASC) AS RowNum,
				PP.ProductoID,
				PP.OrganizacionID,
				O.Descripcion AS DescripcionOrganizacion,
				P.Descripcion AS DescripcionProducto,
				CAST(PP.PrecioMaximo AS DECIMAL(14,4)) AS PrecioMaximo,
				PP.Activo,
				PP.UsuarioCreacionID,
				PP.FechaCreacion 
		INTO #Datos
		FROM CatPrecioProducto PP
		INNER JOIN Organizacion O ON O.OrganizacionID=PP.OrganizacionID
		INNER JOIN Producto P ON P.ProductoID = PP.ProductoID AND P.Activo = 1
			WHERE ((O.OrganizacionID = @OrganizacionID)OR @OrganizacionID=0) 
			AND (P.Descripcion like '%' + @ProductoDescripcion + '%' OR @ProductoDescripcion = '')
			AND PP.Activo = @Activo
	SELECT
				OrganizacionID,
				DescripcionOrganizacion,
				ProductoID,
				DescripcionProducto,
				PrecioMaximo,
				Activo,
				UsuarioCreacionID,
				FechaCreacion 
	FROM #Datos 
	WHERE RowNum BETWEEN @Inicio AND @Limite


	SELECT 
		COUNT(ProductoID)AS TotalReg
	From #Datos


	DROP TABLE #Datos
	SET NOCOUNT OFF;
END
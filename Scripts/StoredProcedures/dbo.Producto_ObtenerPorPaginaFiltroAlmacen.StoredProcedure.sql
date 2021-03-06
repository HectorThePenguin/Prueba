USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaFiltroAlmacen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorPaginaFiltroAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaFiltroAlmacen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 17/07/2014
-- Description: Producto_ObtenerPorPaginaFiltroAlmacen 0, '', 1, 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorPaginaFiltroAlmacen]
@ProductoID INT,
@Descripcion varchar(50),
@AlmacenID INT,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY pr.Descripcion ASC) AS RowNum,
		pr.ProductoID,
		sf.SubFamiliaID,
		sf.Descripcion [SubFamilia],
		fa.FamiliaID,
		fa.Descripcion [Familia],
		pr.UnidadID,
		pr.Descripcion,
		pr.Activo
		INTO #Datos
	FROM Producto pr
	INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia fa ON sf.FamiliaID = fa.FamiliaID
	INNER JOIN AlmacenInventario ai ON ai.ProductoID = pr.ProductoID
	INNER JOIN Almacen Al ON Al.AlmacenID = ai.AlmacenID
	WHERE (pr.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND pr.Activo = @Activo
	AND Al.Activo = @Activo
	AND @ProductoID in (pr.ProductoID, 0)
	AND Al.AlmacenID = @AlmacenID
	SELECT
		ProductoID,
		SubFamiliaID,
		SubFamilia,
		FamiliaID,
		Familia,
		UnidadID,
		Descripcion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(ProductoID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

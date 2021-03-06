USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaSubFamilia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorPaginaSubFamilia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorPaginaSubFamilia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 17/01/2014
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorPaginaSubFamilia]
@ProductoID INT,
@Descripcion varchar(50),
@SubFamiliaID INT,
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
	WHERE (pr.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND pr.Activo = @Activo
	AND pr.SubFamiliaID = @SubFamiliaID 
	AND @ProductoID in (pr.ProductoID, 0)
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

IF object_id('dbo.ProductoCentros_ObtenerPorPaginaSubFamilia', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.ProductoCentros_ObtenerPorPaginaSubFamilia
END
GO
--=================================================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 14/11/2015
-- Description: Obtener listado de productos disponibles para los centros de acopio  
-- ProductoCentros_ObtenerPorPaginaSubFamilia 19, 0, '', 18, 1 ,1 ,15  
--=================================================================================  
CREATE PROCEDURE [dbo].[ProductoCentros_ObtenerPorPaginaSubFamilia]
@OrganizacionID INT,  
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
	FROM Sukarne.dbo.CatProducto pr  
	INNER JOIN Sukarne.dbo.CatSubFamilia sf 
		ON pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Sukarne.dbo.CatFamilia fa 
		ON sf.FamiliaID = fa.FamiliaID  
	WHERE (pr.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')  
	AND pr.Activo = @Activo  
	AND pr.SubFamiliaID = @SubFamiliaID   
	AND @ProductoID in (pr.ProductoID, 0)
	AND pr.OrganizacionID = @OrganizacionID
  
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
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorDescricionPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorDescricionPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorDescricionPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 2014/06/25
-- Description: Procedimiento para obtener los productos
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorDescricionPaginado]
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT,
@FamiliaID INT,
@SubFamiliaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY Pro.Descripcion ASC
			) AS RowNum, 
		   Pro.ProductoID
		,  Pro.Descripcion	AS Producto
		,  sb.Descripcion	AS SubFamilia
	INTO #Productos
	FROM Producto Pro
	INNER JOIN SubFamilia SB
		ON (Pro.SubFamiliaID = sb.SubFamiliaID)
	INNER JOIN Familia F
		ON (SB.FamiliaID = F.FamiliaID)
	WHERE SB.Activo = @Activo 
				AND Pro.Activo = @Activo
				AND F.FamiliaID = @FamiliaID
				AND Pro.SubFamiliaID = @SubFamiliaID
				AND (@Descripcion = '' OR Pro.Descripcion LIKE '%' + @Descripcion + '%')
	SELECT ProductoID
		,  Producto
		,  SubFamilia
	FROM #Productos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(ProductoID) AS TotalReg
	FROM #Productos
	DROP TABLE #Productos
	SET NOCOUNT OFF;
END

GO

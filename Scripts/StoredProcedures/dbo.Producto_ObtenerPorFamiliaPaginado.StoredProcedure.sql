USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFamiliaPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorFamiliaPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFamiliaPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/06/25
-- Description: Procedimiento para obtener los productos por la familia
-- Producto_ObtenerPorFamiliaPaginado 1, '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorFamiliaPaginado]
@FamiliaID INT,
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
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
		ON (SB.FamiliaID = F.FamiliaID
			AND (@FamiliaID = 0 OR F.FamiliaID = @FamiliaID))
	WHERE SB.Activo = @Activo 
				AND F.Activo = @Activo
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

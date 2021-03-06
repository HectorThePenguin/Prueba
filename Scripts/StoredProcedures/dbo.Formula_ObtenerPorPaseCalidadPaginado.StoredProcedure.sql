USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorPaseCalidadPaginado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerPorPaseCalidadPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorPaseCalidadPaginado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener folios de pase a proceso
-- Formula_ObtenerPorPaseCalidadPaginado '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Formula_ObtenerPorPaseCalidadPaginado]
@DescripcionFormula VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY F.Descripcion ASC
			) AS RowNum, 
		   F.FormulaID
		,  F.Descripcion	AS Formula
		,  sb.Descripcion	AS SubFamilia
		,  Pro.ProductoID
	INTO #DatosFormula
	FROM Formula F
	INNER JOIN Producto Pro
		ON (F.ProductoID = Pro.ProductoID)
	INNER JOIN SubFamilia SB
		ON (Pro.SubFamiliaID = sb.SubFamiliaID)
	WHERE @DescripcionFormula = '' OR F.Descripcion LIKE '%' + @DescripcionFormula + '%'
	SELECT FormulaID
		,  Formula
		,  SubFamilia
		,  ProductoID
	FROM #DatosFormula
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(FormulaID) AS TotalReg
	FROM #DatosFormula
	DROP TABLE #DatosFormula
	SET NOCOUNT OFF;
END

GO

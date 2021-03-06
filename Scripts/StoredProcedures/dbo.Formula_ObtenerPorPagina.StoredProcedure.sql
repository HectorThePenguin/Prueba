USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Formula_ObtenerPorPagina 1,'',1,1,10
--======================================================
CREATE PROCEDURE [dbo].[Formula_ObtenerPorPagina]
@FormulaID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY F.Descripcion ASC) AS [RowNum],
		F.FormulaID,
		F.Descripcion,
		F.TipoFormulaID,
		F.ProductoID,
		F.Activo,
		F.FechaCreacion,
		TF.Descripcion AS TipoFormula,
		P.Descripcion AS Producto
	INTO #Formula
	FROM Formula F
	INNER JOIN TipoFormula TF
		ON (F.TipoFormulaID = TF.TipoFormulaID)
	INNER JOIN Producto P
		ON (F.ProductoID = P.ProductoID)
	WHERE (@FormulaID = 0 OR F.FormulaID = @FormulaID) 
	AND (F.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND F.Activo = @Activo
	SELECT
		FormulaID,
		Descripcion,
		TipoFormulaID,
		ProductoID,
		Activo,
		FechaCreacion,
		TipoFormula,
		Producto
	FROM #Formula
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(FormulaID) AS [TotalReg]
	FROM #Formula
	DROP TABLE #Formula
	SET NOCOUNT OFF;
END

GO

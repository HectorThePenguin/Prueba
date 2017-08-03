USE [SIAP]
GO

DROP PROCEDURE [dbo].[Ingrediente_ObtenerPorPaginaFormulaOrganizacion]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : [Ingrediente_ObtenerPorPaginaFormulaOrganizacion] 1,1,318,22,1,1,100
-- [Ingrediente_ObtenerPorPaginaFormulaOrganizacion] 0,0,0, 1,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerPorPaginaFormulaOrganizacion]
@OrganizacionID INT,
@TipoFormulaID INT,
@ProductoID int,
@FormulaID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #FORMULASTODAS
	(
		OrganizacionID INT,
		OrganizacionDesc VARCHAR(50),
		FormulaID INT,
		Formula varchar(50),		
		TipoFormulaID INT,
		TipoFormula varchar(50),
		ProductoID INT,
		Producto varchar(50)
	)
	
	CREATE TABLE #FORMULAS
	(
		OrganizacionID INT,
		OrganizacionDesc VARCHAR(50),
		FormulaID INT,
		Formula varchar(50),		
		TipoFormulaID INT,
		TipoFormula varchar(50),
		ProductoID INT,
		Producto varchar(50),
		RowNum INT IDENTITY
	)
	INSERT INTO #FORMULASTODAS	
	SELECT	
		o.OrganizacionID, 
		o.Descripcion,
		fo.FormulaID,
		fo.Descripcion AS Formula,
		tf.TipoFormulaID,
		tf.Descripcion AS TipoFormula,		
		pr.ProductoID,
		pr.Descripcion AS Producto
	FROM Formula fo
	INNER JOIN TipoFormula tf on fo.TipoFormulaID = tf.TipoFormulaID
	inner join Producto pr on fo.ProductoID = pr.ProductoID
	inner join Ingrediente i on i.FormulaID = fo.FormulaID 
	inner join Organizacion o on i.OrganizacionID = o.OrganizacionID 
	WHERE fo.Activo = @Activo
	AND tf.TipoFormulaID = CASE WHEN @TipoFormulaID = 0 THEN tf.TipoFormulaID ELSE @TipoFormulaID END
	AND pr.ProductoID = CASE WHEN @ProductoID = 0 THEN pr.ProductoID ELSE @ProductoID END
	AND fo.FormulaID = CASE WHEN @FormulaID = 0 THEN fo.FormulaID ELSE @FormulaID END
	
	
	INSERT INTO #FORMULAS
	SELECT distinct OrganizacionID, OrganizacionDesc, FormulaID, Formula, TipoFormulaID,
		TipoFormula, ProductoID, Producto
	FROM #FORMULASTODAS
	WHERE OrganizacionID = CASE WHEN @OrganizacionID = 0 THEN OrganizacionID ELSE @OrganizacionID END
		
	SELECT
	F.OrganizacionID,
	F.OrganizacionDesc,
	F.FormulaID,
		F.Formula,
		F.TipoFormulaID,
		F.TipoFormula,		
		F.ProductoID,
		F.Producto
	FROM #FORMULAS F
	WHERE RowNum BETWEEN @Inicio AND @Limite
	
	SELECT
	COUNT(RowNum) AS [TotalReg]
	FROM #FORMULAS
	SELECT
		i.IngredienteID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		fo.FormulaID,
		fo.Formula,
		pr.ProductoID,
		pr.Descripcion AS Producto,
		i.PorcentajeProgramado,
		i.Activo
	FROM Ingrediente i 
	INNER JOIN #FORMULAS fo on i.FormulaID = fo.FormulaID and i.OrganizacionID = fo.OrganizacionID
	inner join Producto pr on i.ProductoID = pr.ProductoID
	inner join Organizacion o on i.OrganizacionID = o.OrganizacionID
	WHERE o.OrganizacionID = CASE WHEN @OrganizacionID = 0 THEN o.OrganizacionID ELSE @OrganizacionID END
		AND RowNum BETWEEN @Inicio AND @Limite
	DROP TABLE #FORMULAS
	SET NOCOUNT OFF;
	
	
END

GO

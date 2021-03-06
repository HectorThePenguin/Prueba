USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorPaginaFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_ObtenerPorPaginaFormula]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorPaginaFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Ingrediente_ObtenerPorPaginaFormula 0,1,1,1,30
--======================================================
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerPorPaginaFormula]
@IngredienteID int,
@OrganizacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #FORMULAS
	(
		FormulaID INT,
		Formula varchar(50),		
		TipoFormulaID INT,
		TipoFormula varchar(50),
		ProductoID INT,
		Producto varchar(50),
		RowNum INT IDENTITY
	)
	INSERT INTO #FORMULAS	
	SELECT	
		fo.FormulaID,
		fo.Descripcion AS Formula,
		tf.TipoFormulaID,
		tf.Descripcion AS TipoFormula,		
		pr.ProductoID,
		pr.Descripcion AS Producto
	FROM Formula fo
	INNER JOIN TipoFormula tf on fo.TipoFormulaID = tf.TipoFormulaID
	inner join Producto pr on fo.ProductoID = pr.ProductoID
	WHERE fo.Activo = @Activo
	SELECT
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
	INNER JOIN #FORMULAS fo on i.FormulaID = fo.FormulaID
	inner join Producto pr on i.ProductoID = pr.ProductoID
	inner join Organizacion o on i.OrganizacionID = o.OrganizacionID
	WHERE o.OrganizacionID = @OrganizacionID
		AND RowNum BETWEEN @Inicio AND @Limite
	DROP TABLE #FORMULAS
	SET NOCOUNT OFF;
END

GO

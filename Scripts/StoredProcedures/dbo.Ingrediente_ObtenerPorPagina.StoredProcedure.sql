USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Ingrediente_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerPorPagina]
@IngredienteID int,
@OrganizacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY IngredienteID ASC) AS [RowNum],
		IngredienteID,
		OrganizacionID,
		FormulaID,
		ProductoID,
		PorcentajeProgramado,
		Activo
	INTO #Ingrediente
	FROM Ingrediente
	WHERE Activo = @Activo
	AND OrganizacionID = @OrganizacionID
	SELECT
		i.IngredienteID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		fo.FormulaID,
		fo.Descripcion AS Formula,
		pr.ProductoID,
		pr.Descripcion AS Producto,
		i.PorcentajeProgramado,
		i.Activo
	FROM #Ingrediente i 
	INNER JOIN Formula fo on i.FormulaID = fo.FormulaID
	inner join Producto pr on i.ProductoID = pr.ProductoID
	inner join Organizacion o on i.OrganizacionID = o.OrganizacionID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(IngredienteID) AS [TotalReg]
	FROM #Ingrediente
	DROP TABLE #Ingrediente
	SET NOCOUNT OFF;
END

GO

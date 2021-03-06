USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Ingrediente_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Ingrediente_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Ingrediente_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Ingrediente_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
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
	FROM Ingrediente i 
	INNER JOIN Formula fo on i.FormulaID = fo.FormulaID
	inner join Producto pr on i.ProductoID = pr.ProductoID
	inner join Organizacion o on i.OrganizacionID = o.OrganizacionID
	WHERE i.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

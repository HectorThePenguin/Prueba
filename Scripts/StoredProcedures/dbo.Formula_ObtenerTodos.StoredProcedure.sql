USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Formula_ObtenerTodos 1
--======================================================
CREATE PROCEDURE [dbo].[Formula_ObtenerTodos] @Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT f.FormulaID
		,f.Descripcion
		,f.TipoFormulaID
		,tf.Descripcion AS [TipoFormula]
		,f.ProductoID
		,p.Descripcion AS [Producto]
		,f.Activo
	FROM Formula f
	INNER JOIN TipoFormula tf ON tf.TipoFormulaID = f.TipoFormulaID
	INNER JOIN Producto p ON p.ProductoID = f.ProductoID
	WHERE f.Activo = @Activo
		OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

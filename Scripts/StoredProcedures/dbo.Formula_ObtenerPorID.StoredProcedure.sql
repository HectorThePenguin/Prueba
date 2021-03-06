USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Formula_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[Formula_ObtenerPorID]
@FormulaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		f.FormulaID,
		f.Descripcion,
		f.TipoFormulaID,
		tf.Descripcion as [TipoFormula],
		f.ProductoID,
		p.Descripcion as [Producto],
		f.Activo
	FROM Formula f
	inner join TipoFormula tf on tf.TipoFormulaID = f.TipoFormulaID
	inner join Producto p on p.ProductoID = f.ProductoID
	WHERE FormulaID = @FormulaID
	SET NOCOUNT OFF;
END

GO

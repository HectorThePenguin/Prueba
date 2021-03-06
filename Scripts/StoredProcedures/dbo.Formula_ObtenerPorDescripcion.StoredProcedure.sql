USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Formula_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Formula_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		F.FormulaID,
		F.Descripcion,
		F.TipoFormulaID,
		F.ProductoID,
		F.Activo,
		F.FechaCreacion
		, TF.Descripcion AS TipoFormula
		, P.ProductoID
		, P.Descripcion AS Producto
	FROM Formula F
	INNER JOIN TipoFormula TF
		ON (F.TipoFormulaID = TF.TipoFormulaID)
	INNER JOIN Producto P
		ON (F.ProductoID = P.ProductoID)
	WHERE F.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO

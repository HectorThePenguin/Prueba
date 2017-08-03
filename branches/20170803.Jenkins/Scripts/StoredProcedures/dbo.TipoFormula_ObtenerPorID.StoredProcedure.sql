USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoFormula_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoFormula_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoFormula_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoFormula_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoFormula_ObtenerPorID]
@TipoFormulaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoFormulaID,
		Descripcion,
		Activo
	FROM TipoFormula
	WHERE TipoFormulaID = @TipoFormulaID
	SET NOCOUNT OFF;
END

GO

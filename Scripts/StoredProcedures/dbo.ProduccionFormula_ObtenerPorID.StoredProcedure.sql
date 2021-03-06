USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionFormula_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerPorID]
@ProduccionFormulaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		pf.ProduccionFormulaID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		pf.FolioFormula,
		fo.FormulaID,
		fo.Descripcion AS Formula,
		pf.CantidadProducida,
		pf.FechaProduccion,
		pf.AlmacenMovimientoEntradaID,
		pf.AlmacenMovimientoSalidaID,
		pf.Activo
	FROM ProduccionFormula pf
	INNER JOIN Organizacion o on pf.OrganizacionID = o.OrganizacionID
	INNER JOIN Formula fo on pf.FormulaID = fo.FormulaID
	WHERE ProduccionFormulaID = @ProduccionFormulaID
	SET NOCOUNT OFF;
END

GO

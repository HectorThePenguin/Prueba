USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorFolioFormula]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerPorFolioFormula]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorFolioFormula]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionFormula_ObtenerPorFolioFormula
-- 001 Jorge Luis Velazquez Araujo 28/10/2015 **se cambia el Folio del Movimiento en vez del Folio de Formula
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerPorFolioFormula]
@OrganizacionID int
,@FolioMovimiento int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		pf.ProduccionFormulaID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		pf.FolioFormula,
		am.FolioMovimiento,--001
		fo.FormulaID,
		fo.Descripcion AS Formula,
		pf.CantidadProducida,
		pf.FechaProduccion,
		pf.AlmacenMovimientoEntradaID,
		pf.AlmacenMovimientoSalidaID,
		pf.Activo
	FROM ProduccionFormula pf
	INNER JOIN Organizacion o on pf.OrganizacionID = o.OrganizacionID
	inner join Formula fo on pf.FormulaID = fo.FormulaID
	INNER JOIN AlmacenMovimiento am on pf.AlmacenMovimientoEntradaID = am.AlmacenMovimientoID --001
	WHERE pf.Activo = 1
	AND pf.OrganizacionID = @OrganizacionID
	AND am.FolioMovimiento = @FolioMovimiento --001
	SET NOCOUNT OFF;
END

GO

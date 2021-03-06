USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorIDCompleto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerPorIDCompleto]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorIDCompleto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionFormula_ObtenerPorIDCompleto 1009
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerPorIDCompleto]
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
		pro.ProductoID,
		pro.Descripcion AS Producto,
		pf.CantidadProducida,
		pf.FechaProduccion,
		pf.AlmacenMovimientoEntradaID,
		pf.AlmacenMovimientoSalidaID,
		pf.Activo
	FROM ProduccionFormula pf
	INNER JOIN Organizacion o on pf.OrganizacionID = o.OrganizacionID
	INNER JOIN Formula fo on pf.FormulaID = fo.FormulaID
	inner join Producto pro on fo.ProductoID = pro.ProductoID
	WHERE pf.ProduccionFormulaID = @ProduccionFormulaID
	AND pf.Activo = 1
	SELECT
	pfd.ProduccionFormulaDetalleID,
		pfd.ProduccionFormulaID,		
		pro.ProductoID,
		pro.Descripcion AS Producto,
		um.UnidadID,
		um.ClaveUnidad,
		pfd.CantidadProducto,
		pfd.IngredienteID	
		, AMD.AlmacenInventarioLoteID	
	FROM ProduccionFormulaDetalle pfd
	INNER JOIN ProduccionFormula pf on pfd.ProduccionFormulaID = pf.ProduccionFormulaID		
	inner join Producto pro on pfd.ProductoID = pro.ProductoID
	inner join UnidadMedicion um on pro.UnidadID = um.UnidadID
	INNER JOIN AlmacenMovimientoDetalle AMD
		ON (PF.AlmacenMovimientoSalidaID = AMD.AlmacenMovimientoID)
	WHERE pf.ProduccionFormulaID = @ProduccionFormulaID
	AND pf.Activo = 1
	AND pfd.Activo = 1
	SET NOCOUNT OFF;
END

GO

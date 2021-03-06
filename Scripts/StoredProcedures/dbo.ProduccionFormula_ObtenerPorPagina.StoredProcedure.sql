USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionFormula_ObtenerPorPagina 1,'',1,1,15
-- 001 Jorge Luis Velazquez Araujo 28/10/2015 **se cambia el Folio del Movimiento en vez del Folio de Formula
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ObtenerPorPagina]
@OrganizacionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY fo.Descripcion ASC) AS [RowNum],
		pf.ProduccionFormulaID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		pf.FolioFormula,
		am.FolioMovimiento, --001
		fo.FormulaID,
		fo.Descripcion AS Formula,
		pf.CantidadProducida,
		pf.FechaProduccion,
		pf.AlmacenMovimientoEntradaID,
		pf.AlmacenMovimientoSalidaID,
		pf.Activo
	INTO #ProduccionFormula
	FROM ProduccionFormula pf
	INNER JOIN Organizacion o on pf.OrganizacionID = o.OrganizacionID
	INNER JOIN Formula fo on pf.FormulaID = fo.FormulaID
	INNER JOIN AlmacenMovimiento am on pf.AlmacenMovimientoEntradaID = am.AlmacenMovimientoID --001
	WHERE (fo.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND o.OrganizacionID = @OrganizacionID
	AND pf.Activo = @Activo
	SELECT
		ProduccionFormulaID,
		OrganizacionID,
		Organizacion,
		FolioFormula,
		FolioMovimiento,--001
		FormulaID,
		Formula,
		CantidadProducida,
		FechaProduccion,
		AlmacenMovimientoEntradaID,
		AlmacenMovimientoSalidaID,
		Activo
	FROM #ProduccionFormula
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ProduccionFormulaID) AS [TotalReg]
	FROM #ProduccionFormula
	DROP TABLE #ProduccionFormula
	SET NOCOUNT OFF;
END

GO

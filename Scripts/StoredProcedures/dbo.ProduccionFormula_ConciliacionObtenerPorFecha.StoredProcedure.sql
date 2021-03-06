USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ConciliacionObtenerPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_ConciliacionObtenerPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_ConciliacionObtenerPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 06/11/2014
-- Description: 
-- SpName     : ProduccionFormula_ConciliacionObtenerPorFecha '20141001', '20141030'
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_ConciliacionObtenerPorFecha]
@FechaInicial DATE
, @FechaFinal DATE
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		CREATE TABLE #tProduccionFormula
		(
			ProduccionFormulaID			INT,
			OrganizacionID				INT,
			Organizacion				VARCHAR(100),
			FolioFormula				INT,
			FormulaID					INT,
			Formula						VARCHAR(100),
			ProductoID					INT,
			Producto					VARCHAR(100),
			CantidadProducida			DECIMAL,
			FechaProduccion				DATETIME,
			AlmacenMovimientoEntradaID	BIGINT,
			AlmacenMovimientoSalidaID	BIGINT,
			Activo						BIT
		)
		INSERT INTO #tProduccionFormula
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
		INNER JOIN Producto pro on fo.ProductoID = pro.ProductoID
		WHERE CAST(pf.FechaProduccion AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			AND pf.Activo = 1
			AND PF.OrganizacionID = @OrganizacionID
		SELECT ProduccionFormulaID			,
				OrganizacionID				,
				Organizacion				,
				FolioFormula				,
				FormulaID					,
				Formula						,
				ProductoID					,
				Producto					,
				CantidadProducida			,
				FechaProduccion				,
				AlmacenMovimientoEntradaID	,
				AlmacenMovimientoSalidaID	,
				Activo						
		FROM #tProduccionFormula
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
		INNER JOIN #tProduccionFormula pf on pfd.ProduccionFormulaID = pf.ProduccionFormulaID		
		INNER JOIN Producto pro on pfd.ProductoID = pro.ProductoID
		INNER JOIN UnidadMedicion um on pro.UnidadID = um.UnidadID
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (PF.AlmacenMovimientoSalidaID = AMD.AlmacenMovimientoID)
		WHERE pfd.Activo = 1
		DROP TABLE #tProduccionFormula
	SET NOCOUNT OFF;
END

GO

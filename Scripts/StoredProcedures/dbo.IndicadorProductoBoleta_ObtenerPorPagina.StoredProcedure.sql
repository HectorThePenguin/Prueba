USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 24/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoBoleta_ObtenerPorPagina 0, 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_ObtenerPorPagina]
@IndicadorID INT,
@ProductoID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY I.Descripcion ASC) AS [RowNum],
		IPB.IndicadorProductoBoletaID,
		IPB.IndicadorProductoID,
		IPB.OrganizacionID,
		IPB.RangoMinimo,
		IPB.RangoMaximo,
		IPB.Activo
		, I.Descripcion
		, I.IndicadorID
		, O.Descripcion AS Organizacion
		, IP.ProductoID
		, P.Descripcion	AS Producto
	INTO #IndicadorProductoBoleta
	FROM IndicadorProductoBoleta IPB
	INNER JOIN IndicadorProducto IP
		ON (IPB.IndicadorProductoID = IP.IndicadorProductoID)
	INNER JOIN Indicador I
		ON (IP.IndicadorID = I.IndicadorID
			AND (@IndicadorID IN (I.IndicadorID, 0)))
	INNER JOIN Organizacion O
		ON (IPB.OrganizacionID = O.OrganizacionID)
	INNER JOIN Producto P
		ON (IP.ProductoID = P.ProductoID)
	WHERE IPB.Activo = @Activo
	AND (@ProductoID IN (IP.ProductoID, 0))
	SELECT
		IndicadorProductoBoletaID,
		IndicadorProductoID,
		OrganizacionID,
		RangoMinimo,
		RangoMaximo,
		Activo
		, Descripcion
		, IndicadorID
		, Organizacion
		, ProductoID
		, Producto
	FROM #IndicadorProductoBoleta
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(IndicadorProductoBoletaID) AS [TotalReg]
	FROM #IndicadorProductoBoleta
	DROP TABLE #IndicadorProductoBoleta
	SET NOCOUNT OFF;
END

GO

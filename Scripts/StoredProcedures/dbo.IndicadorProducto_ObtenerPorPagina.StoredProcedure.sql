USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProducto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProducto_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProducto_ObtenerPorPagina]
@IndicadorProductoID int,
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
		IP.IndicadorProductoID,
		IP.IndicadorID,
		IP.ProductoID,
		IP.Activo
		, P.Descripcion			AS Producto
		, I.Descripcion			AS Indicador
	INTO #IndicadorProducto
	FROM IndicadorProducto IP
	INNER JOIN Producto P
		ON (IP.ProductoID = P.ProductoID)
	INNER JOIN Indicador I
		ON (IP.IndicadorID = I.IndicadorID)
	WHERE (@IndicadorID IN (I.IndicadorID, 0)) 
		AND (@ProductoID IN (IP.ProductoID, 0)) 
		AND IP.Activo = @Activo

	SELECT
		IndicadorProductoID,
		IndicadorID,
		ProductoID,
		Activo
		, Indicador
		, Producto
	FROM #IndicadorProducto
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(IndicadorProductoID) AS [TotalReg]
	FROM #IndicadorProducto

	DROP TABLE #IndicadorProducto

	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoCalidad_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoCalidad_ObtenerPorPagina]
@IndicadorProductoCalidadID INT,
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
		IPC.IndicadorProductoCalidadID,
		IPC.IndicadorID,
		IPC.ProductoID,
		IPC.Activo
		, I.Descripcion					AS Indicador
		, P.Descripcion					AS Producto
	INTO #IndicadorProductoCalidad
	FROM IndicadorProductoCalidad IPC
	INNER JOIN Indicador I
		ON (IPC.IndicadorID = I.IndicadorID)
	INNER JOIN Producto P
		ON (IPC.ProductoID = P.ProductoID)
	WHERE (@IndicadorID IN (IPC.IndicadorID, 0)) 
		AND (@ProductoID IN (IPC.ProductoID, 0)) 
		AND IPC.Activo = @Activo
	SELECT
		IndicadorProductoCalidadID,
		IndicadorID,
		ProductoID,
		Activo
		, Producto
		, Indicador
	FROM #IndicadorProductoCalidad
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(IndicadorProductoCalidadID) AS [TotalReg]
	FROM #IndicadorProductoCalidad
	DROP TABLE #IndicadorProductoCalidad
	SET NOCOUNT OFF;
END

GO

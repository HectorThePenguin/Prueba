USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorObjetivo_ObtenerPorPagina 0,1,0,111,   1,1,15
--======================================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_ObtenerPorPagina]
@IndicadorObjetivoID int,
@OrganizacionID INT,
@IndicadorID INT,
@ProductoID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY iob.IndicadorObjetivoID ASC) AS [RowNum],
		iob.IndicadorObjetivoID,
		ipc.IndicadorProductoCalidadID,
		i.IndicadorID,
		i.Descripcion AS Indicador,
		pr.ProductoID,
		pr.Descripcion AS Producto,		
		toc.TipoObjetivoCalidadID,
		toc.Descripcion As TipoObjetivoCalidad,
		o.OrganizacionID,
		o.Descripcion As Organizacion,
		iob.ObjetivoMinimo,
		iob.ObjetivoMaximo,
		iob.Tolerancia,
		iob.Medicion,
		iob.Activo
	INTO #IndicadorObjetivo
	FROM IndicadorObjetivo iob	
	INNER JOIN IndicadorProductoCalidad ipc on iob.IndicadorProductoCalidadID = ipc.IndicadorProductoCalidadID
	INNER JOIN Indicador i on ipc.IndicadorID = i.IndicadorID
	INNER JOIN Producto pr on ipc.ProductoID = pr.ProductoID
	INNER JOIN TipoObjetivoCalidad toc on iob.TipoObjetivoCalidadID = toc.TipoObjetivoCalidadID
	INNER JOIN Organizacion o on iob.OrganizacionID = o.OrganizacionID
	WHERE iob.Activo = @Activo
	AND iob.OrganizacionID = @OrganizacionID
	AND @IndicadorID in (i.IndicadorID, 0)
	AND @ProductoID in (pr.ProductoID, 0)
	SELECT
		IndicadorObjetivoID,
		IndicadorProductoCalidadID,
		IndicadorID,
		Indicador,
		ProductoID,
		Producto,		
		TipoObjetivoCalidadID,
		TipoObjetivoCalidad,
		OrganizacionID,
		Organizacion,
		ObjetivoMinimo,
		ObjetivoMaximo,
		Tolerancia,
		Medicion,
		Activo
	FROM #IndicadorObjetivo	
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(IndicadorObjetivoID) AS [TotalReg]
	FROM #IndicadorObjetivo
	DROP TABLE #IndicadorObjetivo
	SET NOCOUNT OFF;
END

GO

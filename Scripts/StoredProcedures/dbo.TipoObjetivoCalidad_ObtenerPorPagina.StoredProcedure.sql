USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObjetivoCalidad_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObjetivoCalidad_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TipoObjetivoCalidad_ObtenerPorPagina]
@TipoObjetivoCalidadID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoObjetivoCalidadID,
		Descripcion,
		Activo
	INTO #TipoObjetivoCalidad
	FROM TipoObjetivoCalidad
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoObjetivoCalidadID,
		Descripcion,
		Activo
	FROM #TipoObjetivoCalidad
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoObjetivoCalidadID) AS [TotalReg]
	FROM #TipoObjetivoCalidad
	DROP TABLE #TipoObjetivoCalidad
	SET NOCOUNT OFF;
END

GO

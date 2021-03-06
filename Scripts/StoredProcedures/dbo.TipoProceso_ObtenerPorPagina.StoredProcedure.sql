USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProceso_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProceso_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoProceso_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoProceso_ObtenerPorPagina 0,'',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[TipoProceso_ObtenerPorPagina]
@TipoProcesoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoProcesoID,
		Descripcion,
		Activo
	INTO #TipoProceso
	FROM TipoProceso
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoProcesoID,
		Descripcion,
		Activo
	FROM #TipoProceso
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoProcesoID) AS [TotalReg]
	FROM #TipoProceso
	DROP TABLE #TipoProceso
	SET NOCOUNT OFF;
END

GO

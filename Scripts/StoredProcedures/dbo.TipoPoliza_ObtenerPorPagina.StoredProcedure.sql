USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoPoliza_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoPoliza_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoPoliza_ObtenerPorPagina '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[TipoPoliza_ObtenerPorPagina]
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoPolizaID,
		Descripcion,
		ClavePoliza,
		Activo
	INTO #TipoPoliza
	FROM TipoPoliza
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoPolizaID,
		Descripcion,
		ClavePoliza,
		Activo
	FROM #TipoPoliza
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoPolizaID) AS [TotalReg]
	FROM #TipoPoliza
	DROP TABLE #TipoPoliza
	SET NOCOUNT OFF;
END

GO

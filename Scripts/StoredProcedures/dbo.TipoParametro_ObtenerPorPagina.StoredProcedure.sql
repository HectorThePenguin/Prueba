USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoParametro_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Obtiene una lista de tipos de parametros paginada
-- SpName     : TipoParametro_ObtenerPorPagina '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[TipoParametro_ObtenerPorPagina]
	@Descripcion varchar(50),
	@Activo BIT,
	@Inicio INT,
	@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoParametroID,
		Descripcion,
		Activo
	INTO #TipoParametro
	FROM TipoParametro
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoParametroID,
		Descripcion,
		Activo
	FROM #TipoParametro
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoParametroID) AS [TotalReg]
	FROM #TipoParametro
	DROP TABLE #TipoParametro
	SET NOCOUNT OFF;
END

GO

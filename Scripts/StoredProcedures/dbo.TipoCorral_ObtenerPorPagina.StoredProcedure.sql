USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCorral_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCorral_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoCorral_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCorral_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TipoCorral_ObtenerPorPagina]
@TipoCorralID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoCorralID,
		Descripcion,
		Activo
	INTO #TipoCorral
	FROM TipoCorral
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoCorralID,
		Descripcion,
		Activo
	FROM #TipoCorral
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoCorralID) AS [TotalReg]
	FROM #TipoCorral
	DROP TABLE #TipoCorral
	SET NOCOUNT OFF;
END

GO

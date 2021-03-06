USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Indicador_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Indicador_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Indicador_ObtenerPorPagina]
@IndicadorID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		IndicadorID,
		Descripcion,
		Activo
	INTO #Indicador
	FROM Indicador
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		IndicadorID,
		Descripcion,
		Activo
	FROM #Indicador
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(IndicadorID) AS [TotalReg]
	FROM #Indicador
	DROP TABLE #Indicador
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CondicionJaula_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 22/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CondicionJaula_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[CondicionJaula_ObtenerPorPagina]
@CondicionJaulaID int,
@Descripcion varchar(50),
@Activo BIT, 
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		CondicionJaulaID,
		Descripcion,
		Activo
	INTO #CondicionJaula
	FROM CondicionJaula
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		CondicionJaulaID,
		Descripcion,
		Activo
	FROM #CondicionJaula
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CondicionJaulaID) AS [TotalReg]
	FROM #CondicionJaula
	DROP TABLE #CondicionJaula
	SET NOCOUNT OFF;
END

GO

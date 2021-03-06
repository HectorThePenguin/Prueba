USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_ObtenerPorPagina '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_ObtenerPorPagina]
@GrupoDescripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY G.Descripcion ASC) AS [RowNum]
		, G.GrupoID
		, G.Descripcion AS Grupo
		, G.Activo
	INTO #Grupos
	FROM Grupo G
	WHERE (G.Descripcion like '%' + @GrupoDescripcion + '%' OR @GrupoDescripcion = '')
			AND G.Activo = @Activo
	SELECT
		GrupoID
		, Grupo
		, Activo
	FROM #Grupos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(GrupoID) AS [TotalReg]
	FROM #Grupos
	DROP TABLE #Grupos
	SET NOCOUNT OFF;
END

GO

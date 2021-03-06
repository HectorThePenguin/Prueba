USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Grado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Grado_ObtenerPorPagina '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Grado_ObtenerPorPagina]
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY G.Descripcion ASC) AS [RowNum]
		, G.GradoID
		, G.Descripcion
		, G.NivelGravedad
		, G.Activo
	INTO #Grado
	FROM Grado G
	WHERE (G.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
			AND G.Activo = @Activo
	SELECT
		GradoID
		, Descripcion
		, NivelGravedad
		, Activo
	FROM #Grado
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(GradoID) AS [TotalReg]
	FROM #Grado
	DROP TABLE #Grado
	SET NOCOUNT OFF;
END

GO

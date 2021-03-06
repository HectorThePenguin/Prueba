USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConceptoDeteccion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConceptoDeteccion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ConceptoDeteccion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConceptoDeteccion_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ConceptoDeteccion_ObtenerPorPagina]
@ConceptoDeteccionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		ConceptoDeteccionID,
		Descripcion,
		Activo
	INTO #ConceptoDeteccion
	FROM ConceptoDeteccion
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		ConceptoDeteccionID,
		Descripcion,
		Activo
	FROM #ConceptoDeteccion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ConceptoDeteccionID) AS [TotalReg]
	FROM #ConceptoDeteccion
	DROP TABLE #ConceptoDeteccion
	SET NOCOUNT OFF;
END

GO

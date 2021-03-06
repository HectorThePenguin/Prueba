USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EstadoComedero_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EstadoComedero_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[EstadoComedero_ObtenerPorPagina]
@EstadoComederoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		EstadoComederoID,
		Descripcion,
		DescripcionCorta,
		NoServir,
		AjusteBase,
		Tendencia,
		Activo
	INTO #EstadoComedero
	FROM EstadoComedero
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		EstadoComederoID,
		Descripcion,
		DescripcionCorta,
		NoServir,
		AjusteBase,
		Tendencia,
		Activo
	FROM #EstadoComedero
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(EstadoComederoID) AS [TotalReg]
	FROM #EstadoComedero
	DROP TABLE #EstadoComedero
	SET NOCOUNT OFF;
END

GO

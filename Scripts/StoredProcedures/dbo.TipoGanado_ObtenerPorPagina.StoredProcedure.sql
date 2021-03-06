USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 20/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoGanado_ObtenerPorPagina 0, '',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[TipoGanado_ObtenerPorPagina]
@TipoGanadoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoGanadoID,
		Descripcion,
		Sexo,
		PesoMinimo,
		PesoMaximo,
		PesoSalida,
		Activo
	INTO #TipoGanado
	FROM TipoGanado
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoGanadoID,
		Descripcion,
		Sexo,
		PesoMinimo,
		PesoMaximo,
		PesoSalida,
		Activo
	FROM #TipoGanado
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoGanadoID) AS [TotalReg]
	FROM #TipoGanado
	DROP TABLE #TipoGanado
	SET NOCOUNT OFF;
END

GO

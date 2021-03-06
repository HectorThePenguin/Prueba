USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoTratamiento_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoTratamiento_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TipoTratamiento_ObtenerPorPagina]
@TipoTratamientoID int,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY TipoTratamientoID ASC) AS [RowNum],
		TipoTratamientoID,
		Descripcion,
		Activo	INTO #Datos
	FROM TipoTratamiento
	WHERE Activo = @Activo
	SELECT
		TipoTratamientoID,
		Descripcion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoTratamientoID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

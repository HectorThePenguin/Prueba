USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObservacion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObservacion_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TipoObservacion_ObtenerPorPagina]
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoObservacionID,
		Descripcion	,
		Activo
	INTO #Datos
	FROM TipoObservacion
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '' )
		AND Activo = @Activo
	SELECT
		TipoObservacionID,
		Descripcion
		, Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoObservacionID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

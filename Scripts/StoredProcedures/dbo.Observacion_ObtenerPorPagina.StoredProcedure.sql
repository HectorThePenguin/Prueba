USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Observacion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Observacion_ObtenerPorPagina '', 1 ,  1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Observacion_ObtenerPorPagina]
@Descripcion varchar(50),
@TipoObservacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT		
		ROW_NUMBER() OVER (
			ORDER BY o.Descripcion ASC
			) AS RowNum,
		o.ObservacionID,
		o.Descripcion,
		tob.TipoObservacionID,
		tob.Descripcion [TipoObservacion],
		o.Activo	
		INTO #Datos
	FROM Observacion o
	INNER JOIN TipoObservacion tob 
		on o.TipoObservacionID = tob.TipoObservacionID
					AND @TipoObservacionID IN (Tob.TipoObservacionID, 0)
	WHERE (o.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '' )
		AND o.Activo = @Activo
	SELECT
		ObservacionID,
		Descripcion,
		TipoObservacionID,
		TipoObservacion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ObservacionID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

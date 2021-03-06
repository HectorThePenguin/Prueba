USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoOrganizacion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoOrganizacion_ObtenerPorPagina 0,'',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[TipoOrganizacion_ObtenerPorPagina]
@TipoOrganizacionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoOrganizacionID,
		Descripcion,
		TipoProcesoID,
		Activo
	INTO #TipoOrganizacion
	FROM TipoOrganizacion
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		o.TipoOrganizacionID,
		o.Descripcion,		
		o.TipoProcesoID,
		tp.Descripcion as [TipoProceso],
		o.Activo
	FROM #TipoOrganizacion o
	INNER JOIN TipoProceso tp on tp.TipoProcesoID = o.TipoProcesoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoOrganizacionID) AS [TotalReg]
	FROM #TipoOrganizacion
	DROP TABLE #TipoOrganizacion
	SET NOCOUNT OFF;
END

GO

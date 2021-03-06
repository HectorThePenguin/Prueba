USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAlmacen_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoAlmacen_ObtenerPorPagina 0, '',1,1,10
--======================================================
CREATE PROCEDURE [dbo].[TipoAlmacen_ObtenerPorPagina]
@TipoAlmacenID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoAlmacenID,
		Descripcion,
		Activo
	INTO #TipoAlmacen
	FROM TipoAlmacen
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoAlmacenID,
		Descripcion,
		Activo
	FROM #TipoAlmacen
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoAlmacenID) AS [TotalReg]
	FROM #TipoAlmacen
	DROP TABLE #TipoAlmacen
	SET NOCOUNT OFF;
END

GO

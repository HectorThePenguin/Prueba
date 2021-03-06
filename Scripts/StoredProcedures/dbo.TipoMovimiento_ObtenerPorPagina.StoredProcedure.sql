USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoMovimiento_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoMovimiento_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoMovimiento_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TipoMovimiento_ObtenerPorPagina]
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY TM.Descripcion ASC) AS [RowNum],
		TipoMovimientoID,
		TM.Descripcion,
		EsGanado,
		EsProducto,
		EsEntrada,
		EsSalida,
		ClaveCodigo,
		TM.TipoPolizaID,
		TP.Descripcion TipoPoliza,
		TM.Activo
	INTO #TipoMovimiento
	FROM TipoMovimiento TM
	INNER JOIN TipoPoliza TP 
		ON TP.TipoPolizaID = TM.TipoPolizaID
	WHERE (TM.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND TM.Activo = @Activo
	SELECT
		TipoMovimientoID,
		Descripcion,
		EsGanado,
		EsProducto,
		EsEntrada,
		EsSalida,
		ClaveCodigo,
		TipoPolizaID,
		TipoPoliza,
		Activo
	FROM #TipoMovimiento
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoMovimientoID) AS [TotalReg]
	FROM #TipoMovimiento
	DROP TABLE #TipoMovimiento
	SET NOCOUNT OFF;
END

GO

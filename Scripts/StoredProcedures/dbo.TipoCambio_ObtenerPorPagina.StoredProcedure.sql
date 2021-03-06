USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCambio_ObtenerPorPagina 0,'',0,'19000101',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[TipoCambio_ObtenerPorPagina]
@TipoCambioID int,
@Descripcion varchar(50),
@MonedaID int,
@Fecha DATE,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
declare @FechaMinima DATE
set @FechaMinima = CAST('19000101' AS DATE)
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TipoCambioID,
		MonedaID,
		Descripcion,
		Cambio,
		Fecha,
		Activo
	INTO #TipoCambio
	FROM TipoCambio
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	AND @Fecha in (CAST(Fecha AS DATE),@FechaMinima)
	AND @MonedaID in (MonedaID, 0)
	SELECT
		tc.TipoCambioID,
		tc.Descripcion,
		mo.MonedaID,
		mo.Descripcion AS Moneda,
		tc.Cambio,
		tc.Fecha,
		tc.Activo
	FROM #TipoCambio tc
	LEFT JOIN Moneda mo on tc.MonedaID = mo.MonedaID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoCambioID) AS [TotalReg]
	FROM #TipoCambio
	DROP TABLE #TipoCambio
	SET NOCOUNT OFF;
END

GO

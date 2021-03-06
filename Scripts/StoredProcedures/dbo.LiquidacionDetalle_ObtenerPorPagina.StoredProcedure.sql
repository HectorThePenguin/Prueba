USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LiquidacionDetalle_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LiquidacionDetalle_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[LiquidacionDetalle_ObtenerPorPagina]
@LiquidacionDetalleID int,
--@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY LiquidacionDetalleID ASC) AS [RowNum],
		LiquidacionDetalleID,
		LiquidacionID,
		EntradaProductoID,
		Activo
	INTO #LiquidacionDetalle
	FROM LiquidacionDetalle
	WHERE --(Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	--AND 
	Activo = @Activo
	SELECT
		LiquidacionDetalleID,
		LiquidacionID,
		EntradaProductoID,
		Activo
	FROM #LiquidacionDetalle
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(LiquidacionDetalleID) AS [TotalReg]
	FROM #LiquidacionDetalle
	DROP TABLE #LiquidacionDetalle
	SET NOCOUNT OFF;
END

GO

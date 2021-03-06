USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiariaDetalle_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiariaDetalle_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiariaDetalle_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiariaDetalle_ObtenerPorPagina]
@ProduccionDiariaDetalleID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY ProduccionDiariaDetalleID ASC) AS [RowNum],
		ProduccionDiariaDetalleID,
		ProduccionDiariaID,
		ProductoID,
		PesajeMateriaPrimaID,
		EspecificacionForraje,
		HoraInicial,
		HoraFinal,
		Activo
	INTO #ProduccionDiariaDetalle
	FROM ProduccionDiariaDetalle
	WHERE Activo = @Activo
	SELECT
		ProduccionDiariaDetalleID,
		ProduccionDiariaID,
		ProductoID,
		PesajeMateriaPrimaID,
		EspecificacionForraje,
		HoraInicial,
		HoraFinal,
		Activo
	FROM #ProduccionDiariaDetalle
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ProduccionDiariaDetalleID) AS [TotalReg]
	FROM #ProduccionDiariaDetalle
	DROP TABLE #ProduccionDiariaDetalle
	SET NOCOUNT OFF;
END

GO

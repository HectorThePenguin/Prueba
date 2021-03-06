USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Liquidacion_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerPorPagina]
@LiquidacionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY LiquidacionID ASC) AS [RowNum],
		LiquidacionID,
		ContratoID,
		OrganizacionID,
		TipoCambio,
		Folio,
		Fecha,
		IPRM,
		CuotaEjidal,
		ProEducacion,
		PIEAFES,
		Factura,
		Cosecha,
		FechaInicio,
		FechaFin,
		Activo
	INTO #Liquidacion
	FROM Liquidacion
	WHERE --(Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	--AND 
	Activo = @Activo
	SELECT
		LiquidacionID,
		ContratoID,
		OrganizacionID,
		TipoCambio,
		Folio,
		Fecha,
		IPRM,
		CuotaEjidal,
		ProEducacion,
		PIEAFES,
		Factura,
		Cosecha,
		FechaInicio,
		FechaFin,
		Activo
	FROM #Liquidacion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(LiquidacionID) AS [TotalReg]
	FROM #Liquidacion
	DROP TABLE #Liquidacion
	SET NOCOUNT OFF;
END

GO

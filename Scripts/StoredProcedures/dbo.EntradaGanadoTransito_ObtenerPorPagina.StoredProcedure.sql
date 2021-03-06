USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoTransito_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoTransito_ObtenerPorPagina]
@Lote varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS 
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY L.Lote ASC) AS [RowNum],
		EGT.EntradaGanadoTransitoID,
		EGT.LoteID,
		EGT.Cabezas,
		EGT.Importe,
		EGT.Peso,
		EGT.Activo
		, L.Lote
		, C.Codigo
		, C.CorralID
	INTO #EntradaGanadoTransito
	FROM EntradaGanadoTransito EGT
	INNER JOIN Lote L
		ON (EGT.LoteID = L.LoteID)
	INNER JOIN Corral C
		ON (L.CorralID = C.CorralID)
	WHERE (L.Lote like '%' + @Lote + '%' OR @Lote = '') 
	AND EGT.Activo = @Activo
	SELECT
		EntradaGanadoTransitoID,
		LoteID,
		Cabezas,
		Importe,
		Peso,
		Activo
		, Lote
	FROM #EntradaGanadoTransito
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(EntradaGanadoTransitoID) AS [TotalReg]
	FROM #EntradaGanadoTransito
	DROP TABLE #EntradaGanadoTransito
	SET NOCOUNT OFF;
END

GO

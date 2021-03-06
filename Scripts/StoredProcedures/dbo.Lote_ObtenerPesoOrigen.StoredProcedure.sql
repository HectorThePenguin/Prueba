USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPesoOrigen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPesoOrigen]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPesoOrigen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jorge Luis Velazquez Araujo
-- Fecha: 25/06/2015
-- Descripci�n:	Obtiene el lote del corral
-- EXEC Lote_ObtenerPesoOrigen 12679
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerPesoOrigen]
	@LoteID int	
AS
BEGIN
SET NOCOUNT ON
CREATE TABLE #Pesos(LoteID INT,
PesoCompra INT)
INSERT INTO #Pesos
	SELECT
		am.LoteID,
		a.PesoCompra
	FROM AnimalMovimiento am
	INNER JOIN TipoMovimiento tm on am.TipoMovimientoID = tm.TipoMovimientoID
	INNER JOIN Animal a
		ON am.AnimalID = a.AnimalID
	WHERE am.LoteID = @LoteID
	AND tm.EsGanado = 1
	and tm.EsEntrada = 1
INSERT INTO #Pesos
	SELECT
		am.LoteID,
		a.PesoCompra
	FROM AnimalMovimientoHistorico am
	INNER JOIN TipoMovimiento tm on am.TipoMovimientoID = tm.TipoMovimientoID
	INNER JOIN AnimalHistorico a
		ON am.AnimalID = a.AnimalID
	WHERE am.LoteID = @LoteID
	AND tm.EsGanado = 1
	and tm.EsEntrada = 1
SELECT
	LoteID,
	AVG(PesoCompra) AS PesoCompra
FROM #Pesos
group by LoteID
SET NOCOUNT OFF
END

GO

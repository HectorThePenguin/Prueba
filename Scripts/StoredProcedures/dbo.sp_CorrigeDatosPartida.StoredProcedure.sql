USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_CorrigeDatosPartida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_CorrigeDatosPartida]
GO
/****** Object:  StoredProcedure [dbo].[sp_CorrigeDatosPartida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CorrigeDatosPartida] (@Org int, @Folio int, @Accion int)
AS 
BEGIN

SET NOCOUNT ON

--DECLARE @Org int
--DECLARE @Folio int
--DECLARE @Accion int

--Accion 1 =  'Valida'
--Accion 2 =  'Corrige Pesos'
--Accion 3 =  'Corrige Pesos e Importes'

--SET @Org = 1
--SET @Folio = 1520
--SET @Accion = 0

SELECT va.OrganizacionID, va.FolioEntrada, va.CabezasRecibidas, va.Importe, SUM(ed.PesoOrigen) AS PesoOrigen, ROUND(va.Importe/SUM(ed.PesoOrigen),2) AS PrecioPromedio, 
eg.PesoBruto - eg.PesoTara As PesoLlegada, 0 AS PesoCorte
INTO #DatosPartida
FROM vw_RevisionPartidaAgrupada va
INNER JOIN EntradaGanado eg (NOLOCK)
    ON eg.OrganizacionID = va.OrganizacionID AND eg.FolioEntrada = va.FolioEntrada
INNER JOIN EntradaGanadoCosteo (NOLOCK) egc
    ON egc.EntradaGanadoID = eg.EntradaGanadoID AND egc.Activo = 1
INNER JOIN EntradaDetalle (NOLOCK) ed
    ON ed.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID
WHERE va.OrganizacionID = @Org AND va.FolioEntrada = @Folio
GROUP BY va.OrganizacionID, va.FolioEntrada, va.Importe, va.CabezasRecibidas, eg.PesoTara, eg.PesoBruto

SELECT *
INTO #DatosIndividuales
FROM (
SELECT a.OrganizacionIDEntrada, a.FolioEntrada, a.PesoCompra, a.AnimalID, 'I' AS Ubicacion, ac.AnimalCostoID, ac.Importe, a.PesoLlegada
FROM Animal a (NOLOCK)
LEFT JOIN AnimalCosto ac (NOLOCK) ON ac.AnimalID = a.AnimalID AND ac.CostoID = 1 AND ac.TipoReferencia = 1
WHERE a.OrganizacionIDEntrada = @Org AND a.FolioEntrada = @Folio
--WHERE a.OrganizacionIDEntrada = 1 AND a.FolioEntrada = 2890
UNION ALL
SELECT a.OrganizacionIDEntrada, a.FolioEntrada, a.PesoCompra, a.AnimalID, 'H' AS Ubicacion, ac.AnimalCostoID, ac.Importe, a.PesoLlegada
FROM AnimalHistorico a (NOLOCK)
LEFT JOIN AnimalCostoHistorico ac (NOLOCK) ON ac.AnimalID = a.AnimalID AND ac.CostoID = 1 AND ac.TipoReferencia = 1
WHERE a.OrganizacionIDEntrada = @Org AND a.FolioEntrada = @Folio) x
--WHERE a.OrganizacionIDEntrada = 1 AND a.FolioEntrada = 2890) x

SELECT OrganizacionIDEntrada, FolioEntrada, AnimalID, x.Peso
INTO #DatosCorte
FROM (
SELECT a.OrganizacionIDEntrada, a.FolioEntrada, a.AnimalID, am.Peso
FROM AnimalMovimiento am (NOLOCK)
INNER JOIN Animal a (NOLOCK) ON a.AnimalID = am.AnimalID
INNER JOIN (
SELECT a.AnimalID, MIN(am.AnimalMovimientoID) AS PrimerMovimiento
FROM Animal a (NOLOCK)
INNER JOIN AnimalMovimiento (NOLOCK) am ON am.AnimalID = a.AnimalID 
GROUP BY a.AnimalID
		  ) pm ON pm.PrimerMovimiento = am.AnimalMovimientoID
UNION ALL
SELECT a.OrganizacionIDEntrada, a.FolioEntrada, a.AnimalID, am.Peso
FROM AnimalMovimientoHistorico am (NOLOCK)
INNER JOIN AnimalHistorico a (NOLOCK) ON a.AnimalID = am.AnimalID
INNER JOIN (
SELECT a.AnimalID, MIN(am.AnimalMovimientoID) AS PrimerMovimiento
FROM AnimalHistorico a (NOLOCK)
INNER JOIN AnimalMovimientoHistorico am (NOLOCK) ON am.AnimalID = a.AnimalID 
GROUP BY a.AnimalID
		  ) pm ON pm.PrimerMovimiento = am.AnimalMovimientoID
		  )x
WHERE x.OrganizacionIDEntrada = @Org AND x.FolioEntrada = @Folio

SELECT daa.*, dp.Importe, dp.PesoOrigen, dp.PrecioPromedio, dp.PesoOrigen - daa.PesoCompra AS Diferencia
INTO #DatosValidacion
FROM (
SELECT OrganizacionIDEntrada, FolioEntrada, SUM(PesoCompra) AS PesoCompra, MAX(AnimalID) AS AnimalConejillo
FROM #DatosIndividuales
GROUP BY OrganizacionIDEntrada, FolioEntrada) daa
INNER JOIN #DatosPartida dp ON dp.OrganizacionID = daa.OrganizacionIDEntrada AND dp.FolioEntrada = daa.FolioEntrada

UPDATE #DatosPartida SET PesoCorte = dc.Peso
FROM (
SELECT OrganizacionIDEntrada, FolioEntrada, SUM(Peso) AS Peso
FROM #DatosCorte
GROUP BY OrganizacionIDEntrada, FolioEntrada) dc
INNER JOIN #DatosPartida dp ON dp.OrganizacionID = dc.OrganizacionIDEntrada AND dp.FolioEntrada = dc.FolioEntrada

-------------------------------------------------------------------------------------------------

SELECT dp.OrganizacionID, dp.FolioEntrada, dp.Importe, SUM(di.Importe) AS ImporteNuevo, dp.Importe - SUM(di.Importe) AS DiferenciaImporte, dp.PesoOrigen, 
SUM(di.PesoCompra) AS PesoCompra, dp.PesoOrigen - SUM(di.PesoCompra) AS DiferenciaKilos, dp.CabezasRecibidas, COUNT(di.AnimalID) AS CabezasCosto, 
dp.CabezasRecibidas - COUNT(di.AnimalID) AS DiferenciaCabezas, dp.PesoLlegada, SUM(di.PesoLlegada) AS PesoLlegadaIndividual, 
dp.PesoLlegada - SUM(di.PesoLlegada) AS DiferenciaLlegada, dp.PesoCorte
FROM #DatosIndividuales di
INNER JOIN #DatosPartida dp 
    ON dp.OrganizacionID = di.OrganizacionIDEntrada AND dp.FolioEntrada = di.FolioEntrada
GROUP BY dp.OrganizacionID, dp.FolioEntrada, dp.importe, dp.PesoOrigen, dp.CabezasRecibidas, dp.PesoLlegada, dp.PesoCorte

PRINT 'Actualiza PesoCompra y PesoLlegada'
UPDATE #DatosIndividuales SET PesoCompra = ROUND((dp.PesoOrigen/dp.PesoCorte) * dc.Peso, 0), PesoLlegada = ROUND((dp.PesoLlegada/dp.PesoCorte) * dc.Peso, 0)
--SELECT ROUND((dp.PesoOrigen/dp.PesoCorte) * dc.Peso, 0), ROUND((dp.PesoLlegada/dp.PesoCorte) * dc.Peso, 0), *
FROM #DatosIndividuales di
INNER JOIN #DatosCorte dc 
    ON dc.AnimalID = di.AnimalID
INNER JOIN #DatosPartida dp 
    ON dp.OrganizacionID = di.OrganizacionIDEntrada AND dp.FolioEntrada = di.FolioEntrada   

PRINT 'Actualiza Tabla Costos Temporales'
UPDATE #DatosIndividuales SET Importe = di.Pesocompra * dp.PrecioPromedio
FROM #DatosIndividuales di
INNER JOIN #DatosPartida dp 
    ON dp.OrganizacionID = di.OrganizacionIDEntrada AND dp.FolioEntrada = di.FolioEntrada

DECLARE @DiferenciaImporte decimal(18,2), @DiferenciaPesoOrigen int, @DiferenciaPesoLlegada int, @DiferenciaCabezas int

SELECT @DiferenciaImporte = dp.Importe - SUM(di.Pesocompra * dp.PrecioPromedio), @DiferenciaPesoOrigen = dp.PesoOrigen - SUM(di.PesoCompra), 
@DiferenciaPesoLlegada = dp.PesoLlegada - SUM(di.PesoLlegada)
FROM #DatosIndividuales di
INNER JOIN #DatosPartida dp 
    ON dp.OrganizacionID = di.OrganizacionIDEntrada AND dp.FolioEntrada = di.FolioEntrada
GROUP BY dp.importe, dp.PesoOrigen, dp.PesoLlegada

PRINT 'Actualiza Tabla Diferencias'
UPDATE #DatosIndividuales SET Importe = di.Importe + @DiferenciaImporte, PesoCompra = di.PesoCompra + @DiferenciaPesoOrigen, PesoLlegada = di.PesoLlegada + @DiferenciaPesoLlegada
FROM #DatosValidacion dv
INNER JOIN #DatosIndividuales di ON di.AnimalID = dv.AnimalConejillo

PRINT 'Finaliza'
SELECT dp.OrganizacionID, dp.FolioEntrada, dp.Importe, SUM(di.Importe) AS ImporteNuevo, dp.Importe - SUM(di.Importe) AS DiferenciaImporte, dp.PesoOrigen, 
SUM(di.PesoCompra) AS PesoCompra, dp.PesoOrigen - SUM(di.PesoCompra) AS DiferenciaKilos, dp.CabezasRecibidas, COUNT(di.AnimalID) AS CabezasCosto, 
dp.CabezasRecibidas - COUNT(di.AnimalID) AS DiferenciaCabezas, dp.PesoLlegada, SUM(di.PesoLlegada) AS PesoLlegadaIndividual, 
dp.PesoLlegada - SUM(di.PesoLlegada) AS DiferenciaLlegada, dp.PesoCorte
FROM #DatosIndividuales di
INNER JOIN #DatosPartida dp 
    ON dp.OrganizacionID = di.OrganizacionIDEntrada AND dp.FolioEntrada = di.FolioEntrada
GROUP BY dp.OrganizacionID, dp.FolioEntrada, dp.importe, dp.PesoOrigen, dp.CabezasRecibidas, dp.PesoLlegada, dp.PesoCorte

SET NOCOUNT OFF

SELECT @DiferenciaImporte = dp.Importe - SUM(di.Importe), @DiferenciaPesoOrigen = dp.PesoOrigen - SUM(di.PesoCompra),
@DiferenciaPesoLlegada = dp.PesoLlegada - SUM(di.PesoLlegada), @DiferenciaCabezas = dp.CabezasRecibidas - COUNT(di.AnimalID)
FROM #DatosIndividuales di
INNER JOIN #DatosPartida dp 
    ON dp.OrganizacionID = di.OrganizacionIDEntrada AND dp.FolioEntrada = di.FolioEntrada
GROUP BY dp.importe, dp.PesoOrigen, dp.PesoLlegada, dp.CabezasRecibidas

IF @DiferenciaImporte != 0
BEGIN
    SET @Accion = 1
    SELECT 'El Importe es incorrecto'
END
    
IF @DiferenciaPesoOrigen != 0
BEGIN
    SET @Accion = 1
    SELECT 'El Peso Origen es incorrecto'
END
    
IF @DiferenciaPesoLlegada != 0
BEGIN
    SET @Accion = 1
    SELECT 'El Peso Llegada es incorrecto'
END

IF @DiferenciaCabezas != 0
BEGIN
    SET @Accion = 1
    SELECT 'Las Cabezas son incorrectas'
END

IF (@Accion = 2 OR @Accion = 3)
BEGIN

    PRINT 'Actualiza Animales'

    --SELECT *
    UPDATE Animal SET PesoCompra = di.PesoCompra, PesoLlegada = di.PesoLlegada
    FROM #DatosIndividuales di
    INNER JOIN Animal a ON a.AnimalID = di.AnimalID

    --SELECT *
    UPDATE AnimalHistorico SET PesoCompra = di.PesoCompra, PesoLlegada = di.PesoLlegada
    FROM #DatosIndividuales di
    INNER JOIN AnimalHistorico a ON a.AnimalID = di.AnimalID

END
IF (@Accion = 3)
BEGIN

    PRINT 'Actualiza Costos'

    --SELECT *
    UPDATE AnimalCosto SET Importe = di.Importe
    FROM #DatosIndividuales di
    INNER JOIN AnimalCosto ac (NOLOCK) ON ac.AnimalCostoID = di.AnimalCostoID

    --SELECT *
    UPDATE AnimalCostoHistorico SET Importe = di.Importe
    FROM #DatosIndividuales di
    INNER JOIN AnimalCostoHistorico ac (NOLOCK) ON ac.AnimalCostoID = di.AnimalCostoID

    --Inserta Animales sin costo
    INSERT INTO AnimalCosto
    SELECT AnimalID, CONVERT(varchar, GETDATE(), 112), 1, 1, 0, Importe, GETDATE(), 1, NULL, NULL
    FROM #DatosIndividuales
    WHERE Ubicacion = 'I' AND AnimalCostoID IS NULL

    INSERT INTO AnimalCostoHistorico
    SELECT 0, AnimalID, CONVERT(varchar, GETDATE(), 112), 1, 1, 0, Importe, GETDATE(), 1, NULL, NULL
    FROM #DatosIndividuales
    WHERE Ubicacion = 'H' AND AnimalCostoID IS NULL

END

DROP TABLE #DatosIndividuales
DROP TABLE #DatosPartida
DROP TABLE #DatosValidacion
DROP TABLE #DatosCorte

END
GO

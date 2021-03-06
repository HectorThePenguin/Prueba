USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarios_ObtenerPorFechaRam]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteInventarios_ObtenerPorFechaRam]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarios_ObtenerPorFechaRam]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 15/02/2014
-- Description: Obtiene los datos para el reporte de inventario
-- SpName     : ReporteInventarios_ObtenerPorFechaRam 1,0,'2014-08-30' ,'2014-09-30'
--======================================================
CREATE PROCEDURE [dbo].[ReporteInventarios_ObtenerPorFechaRam]
@OrganizacionId INT
,@TipoProcesoID INT
, @FechaInicio DATE
, @FechaFin	DATE
AS
BEGIN 
--DECLARE @OrganizacionId INT, @TipoProcesoID INT, @FechaInicio DATE, @FechaFin DATE
--SELECT @OrganizacionId = 1, @TipoProcesoID = 0, @FechaInicio = '2014-08-28', @FechaFin = '2014-09-30'

	SET NOCOUNT ON

		    --SET @FechaFin = DATEADD(HH,24, @FechaFin)
			CREATE TABLE #tInventarios
			(
				TipoGanadoID		INT
				, TipoGanado		VARCHAR(200)
				, InventarioInicial	INT
				, Entradas			INT
				, Ventas			INT
				, Muertes			INT
				, Sacrificio		INT
				, InventarioFinal	INT
			)
			
			INSERT INTO #tInventarios
			SELECT TG.TipoGanadoID
				,  TG.Descripcion
				,  0 AS InventarioInicial
				,  0 AS Entradas
				,  0 AS Venta
				,  0 AS Muertes
				,  0 AS Sacrificio
				,  0 AS InventarioFinal
			FROM TipoGanado(NOLOCK) TG
			----- OBTENER EL INVENTARIO INICIAL -----			
			UPDATE t
			SET t.InventarioInicial = ISNULL(Entradas.EntradasAnteriores, 0) - ISNULL(Salidas.SalidasAnteriores, 0)
			FROM #tInventarios t
			LEFT JOIN
			(
				SELECT TG.TipoGanadoID
					,  SUM(ED.Cabezas) AS EntradasAnteriores
				FROM EntradaGanado (NOLOCK) EG
				INNER JOIN Lote (NOLOCK) L
					ON (EG.LoteID = L.LoteID 
						AND CAST(EG.FechaEntrada as DATE) < @FechaInicio
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0))
				INNER JOIN EntradaGanadoCosteo (NOLOCK) EGC
					ON (EG.EntradaGanadoID = EGC.EntradaGanadoID
						AND EGC.Activo = 1)
				INNER JOIN EntradaDetalle (NOLOCK) ED
					ON (EGC.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID)
				INNER JOIN TipoGanado (NOLOCK) TG
					ON (ED.TipoGanadoID = TG.TipoGanadoID)
				WHERE EG.OrganizacionId = @OrganizacionId
				GROUP BY TG.TipoGanadoID
			) Entradas ON (T.TipoGanadoID = Entradas.TipoGanadoID)
			LEFT JOIN
			(
				SELECT S.TipoGanadoID,  COUNT(S.TipoGanadoID) AS SalidasAnteriores
				FROM (
					SELECT AM.AnimalMovimientoID, A.TipoGanadoID
					FROM Animal (NOLOCK) A 
						INNER JOIN AnimalMovimiento (NOLOCK) AM ON (AM.TipoMovimientoID IN (16,11) AND A.AnimalID = AM.AnimalID )
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
					WHERE CAST(AM.FechaMovimiento AS DATE) < @FechaInicio
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND A.Activo = 0
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
					UNION 
					SELECT AM.AnimalMovimientoID, AH.TipoGanadoID
					FROM AnimalHistorico (NOLOCK) AH 
						INNER JOIN AnimalMovimientoHistorico (NOLOCK) AM ON (AM.TipoMovimientoID IN (16,11) AND AH.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
					WHERE CAST(AM.FechaMovimiento AS DATE) < @FechaInicio 
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND AH.Activo = 0 
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
					UNION
					SELECT AM.AnimalMovimientoID, A.TipoGanadoID
					FROM Animal (NOLOCK) A 
						INNER JOIN AnimalMovimiento (NOLOCK) AM ON (AM.TipoMovimientoID = 8 AND A.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
						INNER JOIN Muertes (NOLOCK) M  ON (AM.LoteID = M.LoteID AND A.Arete = M.Arete)
						INNER JOIN Corral (NOLOCK) C ON (C.Codigo <> 'ZZZ' AND C.CorralID = AM.CorralID)
					WHERE CAST(AM.FechaMovimiento AS DATE) < @FechaInicio 
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND A.Activo = 0
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
					UNION 
					SELECT AM.AnimalMovimientoID, AH.TipoGanadoID
					FROM AnimalHistorico (NOLOCK) AH 
						INNER JOIN AnimalMovimientoHistorico (NOLOCK) AM ON (AM.TipoMovimientoID = 8 AND AH.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
						INNER JOIN Muertes (NOLOCK) M  ON (AM.LoteID = M.LoteID AND AH.Arete = M.Arete)
						INNER JOIN Corral (NOLOCK) C ON (C.Codigo <> 'ZZZ' AND C.CorralID = AM.CorralID)
					WHERE CAST(AM.FechaMovimiento AS DATE) < @FechaInicio 
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND AH.Activo = 0 
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
				) S
				GROUP BY S.TipoGanadoID
			) Salidas ON (T.TipoGanadoID = Salidas.TipoGanadoID)

			----- OBTENER EL INVENTARIO INICIAL -----
			----- OBTENER ENTRADAS -----
			UPDATE t
			SET Entradas = E.Entradas
			FROM #tInventarios t
			INNER JOIN (
				SELECT TG.TipoGanadoID
					,  SUM(ED.Cabezas) AS Entradas
				FROM EntradaGanado (NOLOCK) EG
				INNER JOIN EntradaGanadoCalidad (NOLOCK) EGC
				ON (EG.EntradaGanadoID = EGC.EntradaGanadoID)
				INNER JOIN EntradaGanadoCosteo (NOLOCK) EGCosteo
					ON (EGC.EntradaGanadoID = EGCosteo.EntradaGanadoID
						AND EG.EntradaGanadoID = EGCosteo.EntradaGanadoID
						AND EG.OrganizacionID = EGCosteo.OrganizacionID
						AND EGCosteo.Activo = 1)
				INNER JOIN EntradaDetalle (NOLOCK) ED
					ON (EGCosteo.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID)
				INNER JOIN EntradaGanadoCosto (NOLOCK) EGCosto
				ON (EGCosteo.EntradaGanadoCosteoID = EGCosto.EntradaGanadoCosteoID
				AND ED.EntradaGanadoCosteoID = EGCosto.EntradaGanadoCosteoID
					)
				INNER JOIN TipoGanado (NOLOCK) TG
					ON (ED.TipoGanadoID = tg.TipoGanadoID)
				INNER JOIN Lote (NOLOCK) L
					ON (EG.LoteID = L.LoteID
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0))
				WHERE EG.OrganizacionId = @OrganizacionId AND (CAST (EG.FechaEntrada AS DATE) BETWEEN @FechaInicio AND @FechaFin)
				GROUP BY TG.TipoGanadoID
			) E ON (T.TipoGanadoID = E.TipoGanadoID)
			----- OBTENER ENTRADAS -----
			----- SACRIFICIO -----
			UPDATE t
			SET Sacrificio = S.Sacrificio
			FROM #tInventarios t
			INNER JOIN (
				SELECT S.TipoGanadoID, COUNT(S.TipoGanadoID) AS Sacrificio
				FROM (
					SELECT AM.AnimalMovimientoID, A.TipoGanadoID
					FROM  Animal (NOLOCK) A 
						INNER JOIN AnimalMovimiento (NOLOCK) AM ON (AM.TipoMovimientoID = 16 AND A.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
					WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND A.Activo = 0
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
					UNION
					SELECT AM.AnimalMovimientoID, AH.TipoGanadoID
						FROM AnimalHistorico (NOLOCK) AH 
						INNER JOIN AnimalMovimientoHistorico (NOLOCK) AM ON (AM.TipoMovimientoID = 16 AND AH.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
					WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin 
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND AH.Activo = 0 
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
				) S
				GROUP BY S.TipoGanadoID
			) S ON (T.TipoGanadoID = S.TipoGanadoID)
			----- SACRIFICIO ---------- 
			----- OBTENER VENTAS ------
			UPDATE t
			SET Ventas = V.Ventas
			FROM #tInventarios t
			INNER JOIN (
				SELECT S.TipoGanadoID, COUNT(S.TipoGanadoID) AS Ventas
				FROM (
					SELECT AM.AnimalMovimientoID, A.TipoGanadoID
					FROM Animal (NOLOCK) A 
						INNER JOIN AnimalMovimiento (NOLOCK) AM ON (AM.TipoMovimientoID = 11 AND A.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
					WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND A.Activo = 0
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
					UNION
					SELECT AM.AnimalMovimientoID, AH.TipoGanadoID
						FROM AnimalHistorico (NOLOCK) AH 
						INNER JOIN AnimalMovimientoHistorico (NOLOCK) AM ON (AM.TipoMovimientoID = 11 AND AH.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
					WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin 
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND AH.Activo = 0 
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
				) S
				GROUP BY S.TipoGanadoID
			) V ON (T.TipoGanadoID = V.TipoGanadoID)
			----- OBTENER VENTAS -----
			----- OBTENER MUERTES -----
			UPDATE t
			SET Muertes = M.Muertes
			FROM #tInventarios t
			INNER JOIN (
				SELECT S.TipoGanadoID, COUNT(S.TipoGanadoID) AS Muertes
				FROM ( 
					SELECT AM.AnimalMovimientoID, A.TipoGanadoID
					FROM Animal (NOLOCK) A 
						INNER JOIN AnimalMovimiento (NOLOCK) AM ON (AM.TipoMovimientoID = 8 AND A.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
						INNER JOIN Muertes (NOLOCK) M  ON (AM.LoteID = M.LoteID AND A.Arete = M.Arete)
						INNER JOIN Corral (NOLOCK) C ON (C.Codigo <> 'ZZZ' AND C.CorralID = AM.CorralID)
					WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND A.Activo = 0
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
					UNION 
					SELECT AM.AnimalMovimientoID, AH.TipoGanadoID
					FROM AnimalHistorico (NOLOCK) AH 
						INNER JOIN AnimalMovimientoHistorico (NOLOCK) AM ON (AM.TipoMovimientoID = 8 AND AH.AnimalID = AM.AnimalID)
						INNER JOIN Lote (NOLOCK) L ON (AM.LoteID = L.LoteID)
						INNER JOIN Muertes (NOLOCK) M  ON (AM.LoteID = M.LoteID AND AH.Arete = M.Arete)
						INNER JOIN Corral (NOLOCK) C ON (C.Codigo <> 'ZZZ' AND C.CorralID = AM.CorralID)
					WHERE CAST(AM.FechaMovimiento AS DATE) BETWEEN @FechaInicio AND @FechaFin 
						AND AM.OrganizacionID = @OrganizacionID 
						AND AM.Activo = 1
						AND AH.Activo = 0 
						AND (L.TipoProcesoID = @TipoProcesoID OR @TipoProcesoID = 0)
				) S
				GROUP BY S.TipoGanadoID
			) M ON (T.TipoGanadoID = M.TipoGanadoID)

			UPDATE T
			SET InventarioFinal = (T2.InventarioInicial + T2.Entradas) - (T2.Sacrificio + T2.Ventas + T2.Muertes)
			FROM #tInventarios T
				INNER JOIN #tInventarios T2 ON (T.TipoGanadoID = T2.TipoGanadoID)

			DELETE #tInventarios WHERE InventarioFinal = 0

			SELECT TipoGanadoID
				,  TipoGanado
				,  InventarioInicial	
				,  Entradas			
				,  Ventas				
				,  Muertes			
				,  InventarioFinal
				,  Sacrificio
			FROM #tInventarios

			DROP TABLE #tInventarios
	
	SET NOCOUNT OFF
END


GO

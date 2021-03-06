USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarioMateriaPrima_ObtenerRam]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteInventarioMateriaPrima_ObtenerRam]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarioMateriaPrima_ObtenerRam]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gumaro Lugo
-- Create date: 30/07/2014
-- Description:	Obtener informacion del reporte de materia prima
-- ReporteInventarioMateriaPrima_Obtener 1, 100, 6, 0, '2014-09-01', '2014-10-01'
-- =============================================
CREATE PROCEDURE [dbo].[ReporteInventarioMateriaPrima_ObtenerRam]
	@OrganizacionId INT,  
	@ProductoId INT,  
	@AlmacenId INT,   
	@LoteId INT,
	@FechaInicio DATE,
	@FechaFin DATE 
AS
BEGIN
	    DECLARE @CantidadInicialAnterior DECIMAL(18,4)
		DECLARE @CantidadSalidas DECIMAL(18,4)
		DECLARE @CantidadEntradas DECIMAL(18,4)
		DECLARE @CantidadInventario DECIMAL(18,4)
		DECLARE @PrecioPromedioEntrada DECIMAL(18,4)
		DECLARE @PrecioPromedioSalida DECIMAL(18,4)
		DECLARE @CargosAnteriores DECIMAL(18,4)
		DECLARE @AbonosAnteriores DECIMAL(18,4)
		DECLARE @CostoInicialInventario DECIMAL(18,4)
		DECLARE @CostosFletesInicial DECIMAL(18,4)
		DECLARE @PiezasEntradas INT
		DECLARE @PiezasSalidas INT
		DECLARE @ExistenciaPiezasInicial INT

		-- OBTENEMOS LA CANTIDAD ENTREGADA DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @PiezasEntradas = ISNULL(SUM(AMD.Piezas), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsEntrada = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- OBTENEMOS LA CANTIDAD DE SALIDA DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @PiezasSalidas = ISNULL(SUM(AMD.Piezas), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsSalida = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- OBTENEMOS LA CANTIDAD ENTREGADA DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @CantidadEntradas = ISNULL(SUM(AMD.Cantidad), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsEntrada = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- OBTENEMOS LA CANTIDAD DE SALIDA DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @CantidadSalidas = ISNULL(SUM(AMD.Cantidad), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsSalida = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- OBTENEMOS LOS CARGOS DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @CargosAnteriores = ISNULL(SUM(AMD.Cantidad * AMD.Precio), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsEntrada = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- OBTENEMOS LOS ABONOS DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @AbonosAnteriores = ISNULL(SUM(AMD.Cantidad * AMD.Precio), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsSalida = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- OBTENEMOS LOS COSTOS DE LOS MOVIMIENTOS QUE SON ANTES DEL RANGO DE FECHAS
		SELECT @CostosFletesInicial = ISNULL(SUM(AMC.Importe), 0) FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		LEFT JOIN AlmacenMovimientoCosto (NOLOCK) AS AMC ON AMC.AlmacenMovimientoID = AM.AlmacenMovimientoID
		LEFT JOIN Costo (NOLOCK) CST ON CST.CostoId = AMC.CostoId
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		WHERE AMD.ProductoID = @ProductoId AND AM.AlmacenID = @AlmacenId AND TM.EsEntrada = 1 AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) < CAST(@FechaInicio AS DATE)

		-- SE CALCULA LA CANTIDAD INICIAL ANTES DEL RANGO DE FECHAS SELECCIONADO
		SELECT @CostoInicialInventario = @CostosFletesInicial + @CargosAnteriores - @AbonosAnteriores

		SELECT @CantidadInicialAnterior = @CantidadEntradas - @CantidadSalidas

		SELECT @ExistenciaPiezasInicial = @PiezasEntradas - @PiezasSalidas

		-- SE CREA UNA TEMPORAL CON LOS DATOS NECESARIOS PARA EL REPORTE
	    SELECT  P.Descripcion as Producto
				, UM.Descripcion as Unidad
				, AM.FechaMovimiento
				, A.Descripcion AS Almacen
				, AIL.AlmacenInventarioLoteID
				, COALESCE(AIL.Lote, 0) AS Lote
				, TM.TipoMovimientoID
				, TM.ClaveCodigo
				, TM.Descripcion as TipoMovimiento
				, CAST(ISNULL(EP.Folio,AM.FolioMovimiento) AS BIGINT) AS FolioMovimiento
				, COALESCE(EP.PesoOrigen, 0) as PesoOrigen
				, COALESCE((CASE TM.EsEntrada WHEN 1 THEN AMD.Cantidad END),0) as CantidadEntrada
				, COALESCE((CASE TM.EsSalida WHEN 1 THEN AMD.Cantidad END),0) as CantidadSalida
				, AMD.Piezas
				, AMD.Precio
				--, COALESCE((CASE TM.EsEntrada WHEN 1 THEN CAST(AMD.Cantidad * AMD.Precio AS DECIMAL(18,2)) END),0) as Cargos
				--, COALESCE((CASE TM.EsSalida WHEN 1 THEN CAST(AMD.Cantidad * AMD.Precio AS DECIMAL(18,2)) END),0) as Abonos
				, COALESCE((CASE TM.EsEntrada WHEN 1 THEN CAST(AMD.Importe AS DECIMAL(18,2)) END),0) as Cargos
				, COALESCE((CASE TM.EsSalida WHEN 1 THEN CAST(AMD.Importe AS DECIMAL(18,2)) END),0) as Abonos
				, COALESCE((CASE TM.EsEntrada WHEN 1 THEN AMD.Piezas END),0) as PiezasEntrada
				, COALESCE((CASE TM.EsSalida WHEN 1 THEN AMD.Piezas END),0) as PiezasSalida
				, COALESCE(AMD.AlmacenMovimientoDetalleID, 0) AS AlmacenMovimientoDetalleID
				, COALESCE(AMC.AlmacenMovimientoCostoID, 0) AS AlmacenMovimientoCostoID
				, AM.AlmacenMovimientoID
				, AMD.Importe as ImporteDetalle
				, TM.EsEntrada
				, TM.EsSalida
				, COALESCE(AMC.CostoID, 0) AS CostoID
				, COALESCE(AMC.FechaCreacion, '1900-01-01') as FechaCosto
				, COALESCE(AMC.Importe,0) as ImporteCosto
				, COALESCE(AMC.Cantidad,0) as CantidadCosto
				, COALESCE(CST.Descripcion,'') as Costo
				, COALESCE(@CantidadInicialAnterior ,0) AS ExistenciaInicial
				, COALESCE(@CostoInicialInventario ,0) AS CostoInicialInventario
				, COALESCE(@CantidadInicialAnterior, 0) AS ExistenciaFinalMesAnterior
				, COALESCE(@ExistenciaPiezasInicial, 0) AS ExistenciaPiezasInicial
				, COALESCE(@ExistenciaPiezasInicial, 0) AS ExistenciaPiezasMesAnterior
				, COALESCE(@CostoInicialInventario ,0) AS CostoFinalMesAnterior
				INTO #TmpReporteMateriaPrima
		FROM AlmacenMovimiento (NOLOCK) AS AM
		INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AS AMD ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		LEFT JOIN AlmacenMovimientoCosto (NOLOCK) AS AMC ON AMC.AlmacenMovimientoID = AM.AlmacenMovimientoID
		LEFT JOIN Costo (NOLOCK) CST ON CST.CostoId = AMC.CostoId
		INNER JOIN Producto (NOLOCK) AS P ON P.ProductoID = AMD.ProductoID
		INNER JOIN TipoMovimiento (NOLOCK) AS TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
		INNER JOIN UnidadMedicion (NOLOCK) AS UM ON (UM.UnidadID = P.UnidadID)
		INNER JOIN AlmacenInventario (NOLOCK) AI ON (AI.ProductoID = AMD.ProductoID AND AI.AlmacenID = AM.AlmacenID)
		INNER JOIN Almacen (NOLOCK) AS A ON (A.AlmacenID = AM.AlmacenID)
		LEFT JOIN AlmacenInventarioLote (NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
		LEFT JOIN EntradaProducto (NOLOCK) AS EP ON (EP.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		WHERE A.OrganizacionID = @OrganizacionId AND AM.TipoMovimientoID <> 18 --AND AM.Status = 21
		AND AM.AlmacenID = @AlmacenId AND AMD.ProductoID = @ProductoId
		AND (AIL.AlmacenInventarioLoteID = @LoteId OR @LoteId = 0)
		AND CAST(AM.FechaMovimiento as DATE) BETWEEN CAST(@FechaInicio AS DATE) AND CAST(@FechaFin AS DATE)

		-- SUMAMOS EL INVENTARIO DEL MES ANTERIOR Y LO PINTAMOS EN EL MES SIGUIENTE PARA SUMARLO EN EL TOTAL
		SELECT DISTINCT(Almacenmovimientodetalleid),
		DATEADD(month,1,CAST(CAST(YEAR(FechaMovimiento) AS VARCHAR)+RIGHT('00'+CAST(MONTH(FechaMovimiento) AS VARCHAR), 2)+'01' AS DATE)) AS FechaMovimiento
		, CantidadEntrada, CantidadSalida, PiezasEntrada, PiezasSalida, Cargos, Abonos
		INTO #TmpMeses
		FROM #TmpReporteMateriaPrima
		GROUP BY MONTH(FechaMovimiento), YEAR(FechaMovimiento), Almacenmovimientodetalleid, CantidadEntrada, CantidadSalida, PiezasEntrada, PiezasSalida, Cargos, Abonos
		
		SELECT SUM(CantidadEntrada) - SUM(CantidadSalida) AS Existencias, 
			SUM(PiezasEntrada) - SUM(PiezasSalida) AS PiezasExistencia, 
			SUM(Cargos) - SUM(Abonos) AS CostosFinalMes, FechaMovimiento
		INTO #TmpMesesDetalle
		FROM #TmpMeses 
		GROUP BY FechaMovimiento
		--select * from #TmpMesesDetalle
		SELECT
			CAST(CAST(YEAR(a.FechaMovimiento) AS VARCHAR)+RIGHT('00'+CAST(MONTH(a.FechaMovimiento) AS VARCHAR), 2)+'01' AS DATE) AS FechaMovimiento,
			(
			SELECT SUM(b.Existencias)
			FROM #TmpMesesDetalle AS b
			WHERE (YEAR(b.FechaMovimiento) * 100) + MONTH(b.FechaMovimiento) <= (YEAR(a.FechaMovimiento) * 100) + MONTH(a.FechaMovimiento)
			) AS Existencias,
			(
			SELECT SUM(b.CostosFinalMes)
			FROM #TmpMesesDetalle AS b
			WHERE (YEAR(b.FechaMovimiento) * 100) + MONTH(b.FechaMovimiento) <= (YEAR(a.FechaMovimiento) * 100) + MONTH(a.FechaMovimiento)
			) AS CostosFinalMes,
			(
			SELECT SUM(b.PiezasExistencia)
			FROM #TmpMesesDetalle AS b
			WHERE (YEAR(b.FechaMovimiento) * 100) + MONTH(b.FechaMovimiento) <= (YEAR(a.FechaMovimiento) * 100) + MONTH(a.FechaMovimiento)
			) AS PiezasExistencia
		INTO #TmpMesesDetalle2
		FROM
			#TmpMesesDetalle AS a

		-- SE SUMAN LOS COSTOS DE CADA MES PARA SUMARSELO AL MES SIGUIENTE
		SELECT SUM(ImporteCosto) AS ImporteCosto, 
		DATEADD(month,1,CAST(CAST(YEAR(FechaMovimiento) AS VARCHAR)+RIGHT('00'+CAST(MONTH(FechaMovimiento) AS VARCHAR), 2)+'01' AS DATE)) AS FechaMovimiento 
		INTO #TmpMesesCostos
		FROM #TmpReporteMateriaPrima
		GROUP BY MONTH(FechaMovimiento), YEAR(FechaMovimiento)

		SELECT SUM(ImporteCosto) AS ImporteCostoMes, FechaMovimiento
		INTO #TmpMesesCostosDetalle
		FROM #TmpMesesCostos
		GROUP BY FechaMovimiento

		SELECT
			CAST(CAST(YEAR(a.FechaMovimiento) AS VARCHAR)+RIGHT('00'+CAST(MONTH(a.FechaMovimiento) AS VARCHAR), 2)+'01' AS DATE) AS FechaMovimiento,
			(
			SELECT SUM(b.ImporteCostoMes)
			FROM #TmpMesesCostosDetalle AS b
			WHERE (YEAR(b.FechaMovimiento) * 100) + MONTH(b.FechaMovimiento) <= (YEAR(a.FechaMovimiento) * 100) + MONTH(a.FechaMovimiento)
			) AS ImporteCostoMes
		INTO #TmpMesesCostosDetalle2
		FROM #TmpMesesCostosDetalle AS a

		-- ACTUALIZAMOS LAS EXISTENCIAS DEL MES ANTERIOR.
		UPDATE #TmpReporteMateriaPrima SET ExistenciaFinalMesAnterior = ExistenciaFinalMesAnterior + Existencias, 
		ExistenciaPiezasMesAnterior = ExistenciaPiezasMesAnterior + PiezasExistencia,
		CostoFinalMesAnterior = CostoFinalMesAnterior + CostosFinalMes
		FROM #TmpMesesDetalle2 WHERE MONTH(#TmpMesesDetalle2.FechaMovimiento) = MONTH(#TmpReporteMateriaPrima.FechaMovimiento)
		AND YEAR(#TmpMesesDetalle2.FechaMovimiento) = YEAR(#TmpReporteMateriaPrima.FechaMovimiento)

		-- ACTUALIZAMOS LOS COSTOS DEL MES ANTERIOR.
		UPDATE #TmpReporteMateriaPrima SET CostoFinalMesAnterior = CostoFinalMesAnterior + ImporteCostoMes
		FROM #TmpMesesCostosDetalle2 WHERE MONTH(#TmpMesesCostosDetalle2.FechaMovimiento) = MONTH(#TmpReporteMateriaPrima.FechaMovimiento)
		AND YEAR(#TmpMesesCostosDetalle2.FechaMovimiento) = YEAR(#TmpReporteMateriaPrima.FechaMovimiento)

		-- REGRESAMOS LOS MOVIMIENTOS ORDENADOS POR FECHA MOVIMIENTO
		SELECT * FROM #TmpReporteMateriaPrima ORDER BY FechaMovimiento
		
		-- BORRAMOS LAS TEMPORALES
		DROP TABLE #TmpReporteMateriaPrima
		DROP TABLE #TmpMeses
		DROP TABLE #TmpMesesDetalle
		DROP TABLE #TmpMesesCostosDetalle
		DROP TABLE #TmpMesesCostos
END

GO

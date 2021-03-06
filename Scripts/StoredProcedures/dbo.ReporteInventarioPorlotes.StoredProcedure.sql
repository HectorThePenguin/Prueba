USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarioPorlotes]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteInventarioPorlotes]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarioPorlotes]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gumaro Alberto Lugo
-- Create date: 31/07/2014 13:00:00 p.m.
-- Description:  Procedimiento para generar el reporte de Inventario por Lotes
-- SpName     : ReporteInventarioPorlotes  1, 1, '20150807'
--======================================================
CREATE PROCEDURE [dbo].[ReporteInventarioPorlotes]
   @OrganizacionID AS INT ,
   @FamiliaID INT,
   @Fecha DATE
AS
	CREATE TABLE #Lotes (
		AlmacenId INT,
		CodigoAlmacen varchar(10),
		NombreAlmacen varchar(50),
		TipoAlmacenId INT,
		TipoAlmacen varchar(50),
		Lote INT,
		AlmacenInventarioLoteID INT,
		FechaInicioLote DATE,
		PrecioPromedio decimal(16,4),
		CostoPromedio decimal(16,4),
		CantidadInventario decimal(16,4),
		ProductoId INT,
		Producto varchar(50),
		FamiliaId INT,
		Familia varchar(50),
		FechaFin DATE,
		FechaMovimientoPrimerSalida SMALLDATETIME,
		TAMInicialLote decimal(16,4) NOT NULL DEFAULT 0.00,
		TAMEntradas decimal(16,4) NOT NULL DEFAULT 0.00,
		TAMSalidas decimal(16,4) NOT NULL DEFAULT 0.00
	)
	--DECLARE @OrganizacionID AS INT, @FamiliaID INT, @Fecha DATE
	--SELECT @OrganizacionID = 1, @FamiliaID = 1, @Fecha = '2014-09-02'
	INSERT INTO #Lotes (AlmacenId, CodigoAlmacen, NombreAlmacen, TipoAlmacenId, TipoAlmacen, 
						Lote, AlmacenInventarioLoteID, FechaInicioLote, FechaFin, PrecioPromedio, 
					    CostoPromedio, CantidadInventario, 
					    ProductoID, Producto, FamiliaId, Familia, 
					    FechaMovimientoPrimerSalida)
	SELECT AI.AlmacenID, ALM.CodigoAlmacen, ALM.Descripcion, TP.TipoAlmacenID, TP.Descripcion, 
	       AIL.Lote, AIL.AlmacenInventarioLoteID, AIL.FechaInicio, AIL.FechaFin, AIL.PrecioPromedio, 
		   COALESCE((AIL.Cantidad * AIL.PrecioPromedio), 0) AS CostoPromedio, AIL.Cantidad, 
		   AI.ProductoID, P.Descripcion, F.FamiliaID, F.Descripcion, 
		   MIN(CASE TM.EsSalida WHEN 1 THEN FechaMovimiento END) AS FechaMovimientoPrimerSalida 
	FROM AlmacenInventario AI 
	  INNER JOIN AlmacenInventarioLote (NOLOCK) AIL ON AI.AlmacenInventarioID = AIL.AlmacenInventarioID 
	  INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AMD ON AIL.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID 
	  INNER JOIN AlmacenMovimiento (NOLOCK) AM on (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID AND AM.Status not in (41,42,43))
	  INNER JOIN Almacen (NOLOCK) ALM ON AI.AlmacenID = ALM.AlmacenID 
	  INNER JOIN TipoAlmacen (NOLOCK) TP on ALM.TipoAlmacenID = TP.TipoAlmacenID 
	  INNER JOIN TipoMovimiento (NOLOCK) TM ON TM.TipoMovimientoID = AM.TipoMovimientoID 
	  INNER JOIN Producto (NOLOCK) P ON AI.ProductoID = P.ProductoID 
	  INNER JOIN SubFamilia (NOLOCK) SF ON SF.SubFamiliaID = P.SubFamiliaID 
	  INNER JOIN Familia (NOLOCK) F ON F.FamiliaID = SF.FamiliaID 
	WHERE ALM.OrganizacionID = @OrganizacionID 
	  AND CAST(AM.FechaMovimiento AS DATE) <= @Fecha 
	  AND (AIL.FechaFin IS NULL OR @Fecha <= CAST(AIL.FechaFin AS DATE))
	  AND F.FamiliaID = @FamiliaID 
	  AND P.Activo = 1 
	  AND F.Activo = 1 
	  --AND ALM.AlmacenID = 6
	  --AND P.ProductoID = 100
	GROUP BY AI.AlmacenID, ALM.CodigoAlmacen, ALM.Descripcion,Tp.TipoAlmacenID, TP.Descripcion, 
	        AIL.Lote, AIL.AlmacenInventarioLoteID, AIL.FechaInicio, AIL.PrecioPromedio, AIL.Cantidad,
			AI.ProductoID, P.Descripcion, F.FamiliaID, F.Descripcion, AIL.FechaFin
	ORDER BY AlmacenID, AI.ProductoID, AIL.Lote
	--select * from #Lotes
	--Tama�o del lote
	SELECT LTS.AlmacenId ,LTS.TipoAlmacenId, LTS.Lote, LTS.AlmacenInventarioLoteID, LTS.ProductoId, AM.FechaMovimiento, AMD.Cantidad, AM.TipoMovimientoID, 
		TM.EsEntrada, 
		TM.EsSalida, LTS.FechaFin,
		(SELECT AMD.Importe + COALESCE( (SELECT SUM(AMC.importe) 
										 FROM AlmacenMovimientoCosto (NOLOCK) AMC
										 WHERE AMC.AlmacenMovimientoId = AM.AlmacenMovimientoID), 0)) AS ImporteMovimiento
	INTO #LotesMovimientosTotal
	FROM #Lotes LTS
	  INNER JOIN  AlmacenMovimientoDetalle (NOLOCK) AMD ON AMD.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID
	  INNER JOIN  AlmacenMovimiento (NOLOCK) AM ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID AND AM.Status not in (41,42,43))
	  INNER JOIN  TipoMovimiento (NOLOCK) TM ON TM.TipoMovimientoID = AM.TipoMovimientoID
	WHERE AM.TipoMovimientoID <> 18
	ORDER BY LTS.AlmacenInventarioLoteID, AM.FechaMovimiento
	--select * from #LotesMovimientosTotal
	--Movimientos del dia
	/*SELECT LTS.AlmacenId ,LTS.TipoAlmacenId, LTS.Lote, LTS.AlmacenInventarioLoteID, LTS.ProductoId, AM.FechaMovimiento, AMD.Cantidad, AM.TipoMovimientoID, 
		TM.EsEntrada, 
		TM.EsSalida
	INTO #LotesMovimientos
	FROM #Lotes LTS
    INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AMD ON AMD.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID
    INNER JOIN AlmacenMovimiento (NOLOCK) AM ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID) --AM.[Status] = 21 AND 
    INNER JOIN TipoMovimiento (NOLOCK) TM ON (TM.TipoMovimientoID = AM.TipoMovimientoID AND CAST(AM.FechaMovimiento as DATE) = @Fecha) 
    ORDER BY LTS.AlmacenInventarioLoteID, AM.FechaMovimiento*/
	--select * from #LotesMovimientos
   SELECT LTS.AlmacenId, LTS.CodigoAlmacen, LTS.NombreAlmacen, LTS.TipoAlmacenId, LTS.TipoAlmacen, LTS.Lote, 
	   LTS.AlmacenInventarioLoteID, LTS.FechaInicioLote, LTS.FechaFin, LTS.PrecioPromedio, 
	   LTS.CostoPromedio, LTS.CantidadInventario,
	   LTS.ProductoID, LTS.Producto, LTS.FamiliaId, LTS.Familia,
		--Tama�o del lote acumulado a la fecha del reporte
	COALESCE((select sum(#LotesMovimientosTotal.Cantidad) from #LotesMovimientosTotal
		where CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) <= @Fecha 
			AND #LotesMovimientosTotal.EsEntrada = 1 AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID ), 0) as TAMLote,
	--Salidas acumuladas a la fecha del reporte
	COALESCE((select sum(#LotesMovimientosTotal.Cantidad) from #LotesMovimientosTotal
		where CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) <= @Fecha 
			AND #LotesMovimientosTotal.EsSalida = 1 AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID ), 0) as UnidadSalida,
	COALESCE((select sum(#LotesMovimientosTotal.Cantidad) from #LotesMovimientosTotal
		where CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) = @Fecha 
			AND #LotesMovimientosTotal.EsEntrada = 1 AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID), 0) as UnidadEntrada,
	COALESCE((select sum(#LotesMovimientosTotal.Cantidad) from #LotesMovimientosTotal
		where #LotesMovimientosTotal.EsSalida = 1 AND CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) > @Fecha 
			AND (#LotesMovimientosTotal.FechaFin IS NULL OR @Fecha < CAST(#LotesMovimientosTotal.FechaFin as DATE)) 
			AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID ), 0) as UnidadSalidaPosterior,
	COALESCE((select sum(#LotesMovimientosTotal.Cantidad) from #LotesMovimientosTotal
		where #LotesMovimientosTotal.EsEntrada = 1 AND CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) > @Fecha 
			AND (#LotesMovimientosTotal.FechaFin IS NULL OR @Fecha < CAST(#LotesMovimientosTotal.FechaFin as DATE))
			AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID), 0) as UnidadEntradaPosterior,
	COALESCE((select sum(#LotesMovimientosTotal.ImporteMovimiento) from #LotesMovimientosTotal
		where #LotesMovimientosTotal.EsSalida = 1 AND CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) > @Fecha 
			AND (#LotesMovimientosTotal.FechaFin IS NULL OR @Fecha < CAST(#LotesMovimientosTotal.FechaFin as DATE))
			AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID ), 0) as CostoSalidaPosterior,
	COALESCE((select sum(#LotesMovimientosTotal.ImporteMovimiento) from #LotesMovimientosTotal
		where #LotesMovimientosTotal.EsEntrada = 1 AND CAST(#LotesMovimientosTotal.FechaMovimiento as DATE) > @Fecha 
			AND (#LotesMovimientosTotal.FechaFin IS NULL OR @Fecha < CAST(#LotesMovimientosTotal.FechaFin as DATE))
			AND #LotesMovimientosTotal.AlmacenInventarioLoteID = LTS.AlmacenInventarioLoteID ), 0) as CostoEntradaPosterior
   INTO #LotesFinal
   FROM #Lotes LTS
	   --LEFT JOIN #LotesMovimientos LTSM ON LTS.AlmacenInventarioLoteID = LTSM.AlmacenInventarioLoteID
	   LEFT JOIN #LotesMovimientosTotal  LTSTO ON LTS.AlmacenInventarioLoteID = LTSTO.AlmacenInventarioLoteID
   GROUP BY LTS.AlmacenId, LTS.CodigoAlmacen, LTS.NombreAlmacen, LTS.TipoAlmacenId, LTS.TipoAlmacen, LTS.Lote, 
			LTS.AlmacenInventarioLoteID, LTS.FechaInicioLote,  LTS.FechaFin, LTS.PrecioPromedio,
			LTS.CostoPromedio, LTS.CantidadInventario,
			LTS.ProductoID, LTS.Producto, LTS.FamiliaId, LTS.Familia, LTS.FechaMovimientoPrimerSalida
   ORDER BY LTS.ProductoId, LTS.AlmacenId, LTS.Lote
   UPDATE #LotesFinal SET CantidadInventario = CantidadInventario
                                             - UnidadEntradaPosterior + UnidadSalidaPosterior
   UPDATE #LotesFinal SET CostoPromedio = CostoPromedio 
											 - CostoEntradaPosterior + CostoSalidaPosterior
   UPDATE #LotesFinal SET PrecioPromedio = CASE CantidadInventario WHEN 0 THEN 0 ELSE CostoPromedio / CantidadInventario END
   SELECT * 
   FROM #LotesFinal 
   ORDER BY ProductoId, AlmacenId, Lote
   -- Eliminacion de tablas
   DROP TABLE #Lotes
   --DROP TABLE #LotesMovimientos
   DROP TABLE #LotesMovimientosTotal
   DROP TABLE #LotesFinal

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarioMateriaPrima_ObtenerPrueba]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteInventarioMateriaPrima_ObtenerPrueba]
GO
/****** Object:  StoredProcedure [dbo].[ReporteInventarioMateriaPrima_ObtenerPrueba]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Gumaro Lugo
-- Create date: 30/07/2014
-- Description:	Obtener informacion del reporte de materia prima
-- ReporteInventarioMateriaPrima_Obtener 1, 100, 5, 0, '2014-07-01', '2014-07-31'
-- =============================================
CREATE PROCEDURE [dbo].[ReporteInventarioMateriaPrima_ObtenerPrueba]
	@OrganizacionId INT,  
	@ProductoId INT,  
	@AlmacenId INT,   
	@LoteId INT,
	@FechaInicio DATE,
	@FechaFin DATE 
AS  
BEGIN
	    SELECT AMD.AlmacenMovimientoDetalleID
		,COALESCE(ALMC.AlmacenMovimientoCostoID, 0) as AlmacenMovimientoCostoID
		,COALESCE(ALMC.CostoID,0) as CostoID
		,COALESCE(ALMC.FechaCreacion, '1900-01-01') as FechaCosto
		,COALESCE(ALMC.Importe,0) as ImporteCosto
		, COALESCE(ALMC.Cantidad,0) as CantidadCosto
		,COALESCE(CST.Descripcion,'') as Costo
		, AM.AlmacenMovimientoID
		, AIL.AlmacenInventarioLoteID
		, AI.AlmacenInventarioID
		--, (CASE @LoteId WHEN 0 THEN AI.Cantidad ELSE AIL.Cantidad END) as CantidadInventario
		--, (CASE @LoteId WHEN 0 THEN AI.Cantidad * AI.PrecioPromedio ELSE AIL.Cantidad * AIL.PrecioPromedio END) as CostoInventario
		, (CASE @LoteId WHEN 0 THEN
			(SELECT SUM(AlmacenInventarioLote.Cantidad) FROM AlmacenInventarioLote 
				WHERE AlmacenInventarioLote.AlmacenInventarioID = AI.AlmacenInventarioID)
			ELSE

				(SELECT SUM(AlmacenInventarioLote.Cantidad) FROM AlmacenInventarioLote 
				WHERE AlmacenInventarioLote.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID) 
			END) as CantidadInventario

		,(CASE @LoteId WHEN 0 THEN
			(SELECT SUM(AlmacenInventarioLote.Importe) FROM AlmacenInventarioLote 
				WHERE AlmacenInventarioLote.AlmacenInventarioID = AI.AlmacenInventarioID)
			ELSE

				(SELECT SUM(AlmacenInventarioLote.Importe) FROM AlmacenInventarioLote 
				WHERE AlmacenInventarioLote.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID) 
			END) as CostoInventario

		, AIL.Lote
		, AIL.Cantidad as CantidadLote
		, AIL.Importe as ImporteLote
		, AIL.PrecioPromedio
		, AIL.Activo as LoteActivo
		, ALM.Descripcion as Almacen
		, P.Descripcion as Producto
		, UM.Descripcion as Unidad
		,tp.TipoMovimientoID
		, TP.ClaveCodigo
		, tp.Descripcion as TipoMovimiento
		, COALESCE(EP.PesoOrigen, 0) as PesoOrigen,
		AM.FechaMovimiento
		,EP.Folio AS FolioMovimiento
		, AMD.Cantidad
		, AMD.Piezas
		, AMD.Precio
		, AMD.Importe as ImporteDetalle
		, TP.EsEntrada
		, TP.EsSalida
		, COALESCE((CASE TP.EsEntrada WHEN 1 THEN AMD.Cantidad END),0) as CantidadEntrada
		, COALESCE((CASE TP.EsSalida WHEN 1 THEN AMD.Cantidad END),0) as CantidadSalida
		, cast(0.0 as decimal(12,4)) as ExistenciaInicial
		, cast(0.0 as decimal(18,4)) as CostoInicialInventario
		INTO #TmpReporteMateriaPrima
		FROM 
		AlmacenMovimientoDetalle AMD
		INNER JOIN AlmacenMovimiento AM ON AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID
		LEFT JOIN AlmacenMovimientoCosto ALMC ON ALMC.AlmacenMovimientoId = AMD.AlmacenMovimientoId
		LEFT join Costo CST ON CST.CostoId = ALMC.CostoId
		inner join AlmacenInventarioLote AIL ON AMD.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID
		INNER JOIN AlmacenInventario AI ON AIL.AlmacenInventarioID = AI.AlmacenInventarioID
		INNER JOIN TipoMovimiento TP on TP.TipoMovimientoID = am.TipoMovimientoID
		inner join Producto P on AMD.ProductoID = P.ProductoID
		inner join UnidadMedicion UM ON P.UnidadID = UM.UnidadID
		LEFT JOIN EntradaProducto EP ON AM.AlmacenMovimientoID = EP.AlmacenMovimientoID
		INNER JOIN Almacen ALM ON ALM.AlmacenID = AI.AlmacenID
		WHERE 
		ALM.OrganizacionID = @OrganizacionId
		AND AI.AlmacenID = @AlmacenId
		AND AMD.ProductoID = @ProductoId
		AND AIL.Lote = (CASE @LoteId WHEN 0 THEN AIL.Lote ELSE @LoteId END)
		AND (TP.EsEntrada = 1 OR TP.EsSalida = 1)
		AND CAST(AM.FechaMovimiento as DATE) BETWEEN @FechaInicio AND @FechaFin 
		
		DECLARE @CantidadInicial decimal(12,4)
		DECLARE @CantidadInventario decimal(12,4) = (SELECT TOP 1 CantidadInventario FROM #TmpReporteMateriaPrima)
		DECLARE @CantidadEntradas decimal(12,4) = (SELECT SUM(CantidadEntrada) FROM #TmpReporteMateriaPrima WHERE EsEntrada = 1)
		DECLARE @CantidadSalidas decimal(12,4) = (SELECT SUM(CantidadSalida) FROM #TmpReporteMateriaPrima WHERE EsSalida = 1 AND LoteActivo = 1)

		SET @CantidadInicial =(SELECT @CantidadInventario -  COALESCE(@CantidadEntradas, 0) +  COALESCE(@CantidadSalidas, 0))
		
		UPDATE #TmpReporteMateriaPrima SET ExistenciaInicial = @CantidadInicial

		DECLARE @CostoInicial decimal(18,4)
		DECLARE @CostoInventario decimal(18,4) = (SELECT TOP 1 CostoInventario FROM #TmpReporteMateriaPrima)
		DECLARE @CostoEntradas decimal(18,4) = (SELECT SUM(ImporteDetalle) FROM #TmpReporteMateriaPrima WHERE EsEntrada = 1)
		DECLARE @CostoSalidas decimal(18,4) = (SELECT SUM(ImporteDetalle) FROM #TmpReporteMateriaPrima WHERE EsSalida = 1 AND LoteActivo = 1)

		SET @CostoInicial = (SELECT @CostoInventario -  COALESCE(@CostoEntradas, 0) +  COALESCE(@CostoSalidas, 0))
		
		UPDATE #TmpReporteMateriaPrima SET CostoInicialInventario = @CostoInicial

		SELECT * FROM #TmpReporteMateriaPrima
		drop table #TmpReporteMateriaPrima
END

GO

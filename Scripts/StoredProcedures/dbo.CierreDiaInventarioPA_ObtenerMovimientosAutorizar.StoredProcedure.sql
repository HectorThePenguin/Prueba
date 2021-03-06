USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventarioPA_ObtenerMovimientosAutorizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventarioPA_ObtenerMovimientosAutorizar]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventarioPA_ObtenerMovimientosAutorizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 05/07/2014
-- Description:  Obtiene los registros pendientes a autorizar
-- CierreDiaInventarioPA_ObtenerMovimientosAutorizar 23, 18, 20, '20140714'
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventarioPA_ObtenerMovimientosAutorizar] 
	 @AlmacenID INT
	,@TipoMovimientoID INT
	,@EstatusMovimiento INT
	,@FechaMovimiento smalldatetime
AS
BEGIN	
	CREATE TABLE #MOVIMIENTOSPENDIENTES (
		ProductoID INT
		,Producto varchar(50)
		,FolioMovimiento INT
		,FechaMovimiento Smalldatetime
		,Observaciones varchar(255)
		,CostoUnitario decimal(18,2)
		,ManejaLote BIT
		,AlmacenInventarioLoteID INT
		,Lote INT
		,TamanioLote INT
		,InventarioTeorico INT
		,InventarioFisico INT
		)
	--Obtiene los valores para los productos que manejan lote
	INSERT INTO #MOVIMIENTOSPENDIENTES
	SELECT pr.ProductoID
		,pr.Descripcion AS Producto
		,am.FolioMovimiento AS FolioMovimiento
		,am.FechaMovimiento
		,am.Observaciones
		,ail.PrecioPromedio
		,pr.ManejaLote
		,ail.AlmacenInventarioLoteID
		,ail.Lote
		,dbo.ObtenerTamanioLote(ai.AlmacenID, ai.AlmacenInventarioID, ail.Lote) AS TamanioLote
		,Round(ail.Cantidad, 0) AS InventarioTeorico
		,Round(amd.Cantidad, 0) AS InventarioFisico
		--,Round(((Round(ail.Cantidad, 0) - amd.Cantidad) / Round(ail.Cantidad, 0))* 100,2) AS PorcentajeMermaSuperavit
		--,(Round(amd.Cantidad, 0) / dbo.ObtenerTamanioLote(ai.AlmacenID, ai.AlmacenInventarioID, ail.Lote)) * 100 AS PorcentajeLote
	FROM AlmacenMovimiento am
	INNER JOIN AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID  
	INNER JOIN Producto pr on pr.ProductoID = amd.ProductoID	
	INNER JOIN AlmacenInventario ai ON (pr.ProductoID = ai.ProductoID AND ai.AlmacenID = @AlmacenID)
	INNER JOIN AlmacenInventarioLote ail ON (ai.AlmacenInventarioID = ail.AlmacenInventarioID AND ail.AlmacenInventarioLoteID = amd.AlmacenInventarioLoteID)
	WHERE pr.ManejaLote = 1
	AND am.AlmacenID = @AlmacenID
	ANd am.TipoMovimientoID = @TipoMovimientoID
	AND am.Status = @EstatusMovimiento
	AND Convert(varchar,am.FechaMovimiento,112) = Convert(varchar,@FechaMovimiento,112)
	--Obtiene los valores para los productos que no manejan lote
	INSERT INTO #MOVIMIENTOSPENDIENTES
	SELECT pr.ProductoID
		,pr.Descripcion AS Producto
		,am.FolioMovimiento AS FolioMovimiento
		,am.FechaMovimiento
		,am.Observaciones
		,ai.PrecioPromedio
		,pr.ManejaLote
		,0 AS AlmacenInventarioLoteID
		,0 AS Lote
		,0 AS TamanioLote		
		,Round(ai.Cantidad, 0) AS InventarioTeorico
		,Round(amd.Cantidad, 0) AS InventarioFisico
		--,Round(((Round(ai.Cantidad, 0) - amd.Cantidad) / Round(ai.Cantidad, 0))* 100,2) AS PorcentajeMermaSuperavit
		--,0 AS PorcentajeLote
	FROM AlmacenMovimiento am
	INNER JOIN AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID  
	INNER JOIN Producto pr on pr.ProductoID = amd.ProductoID	
	INNER JOIN AlmacenInventario ai ON (pr.ProductoID = ai.ProductoID AND ai.AlmacenID = @AlmacenID)
	WHERE pr.ManejaLote = 0
	AND am.AlmacenID = @AlmacenID
	ANd am.TipoMovimientoID = @TipoMovimientoID
	AND am.Status = @EstatusMovimiento	
	AND Convert(varchar,am.FechaMovimiento,112) = Convert(varchar,@FechaMovimiento,112)
	SELECT ProductoID 
		,Producto
		,FolioMovimiento
		,FechaMovimiento
		,Observaciones
		,CostoUnitario
		,ManejaLote
		,AlmacenInventarioLoteID
		,Lote 
		,TamanioLote
		,InventarioTeorico 
		,InventarioFisico
	FROM #MOVIMIENTOSPENDIENTES
END

GO

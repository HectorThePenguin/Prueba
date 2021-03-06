USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventarioPA_ObtenerDatos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventarioPA_ObtenerDatos]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventarioPA_ObtenerDatos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 30/06/2014
-- Description:  Obtiene la informacion para el Cierre de D�a de Inventario PA
-- CierreDiaInventarioPA_ObtenerDatos 1	,4
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventarioPA_ObtenerDatos] @OrganizacionID INT
	,@AlmacenID INT
AS
BEGIN
	DECLARE @FechaActual DATE
	set @FechaActual = CAST(GETDATE() as DATE)
	CREATE TABLE #MOVIMIENTOSDIA
	(		
		AlmacenInventarioLoteID int,
		ProductoID int,		
	)
	CREATE TABLE #PRODUCTOS (
		ProductoID INT
		,Producto VARCHAR(50)
		,UnidadMedida VARCHAR(50)
		,SubFamiliaID int
		)
	CREATE TABLE #PRODUCTOSDETALLE (
		ProductoID INT
		,Producto varchar(50)
		,ManejaLote BIT
		,AlmacenInventarioLoteID INT
		,Lote INT
		,CostoUnitario DECIMAL(18, 2)
		,TamanioLote INT
		,InventarioTeorico INT
		,PiezasTeoricas int
		,SubFamiliaID int
		)
	INSERT INTO #MOVIMIENTOSDIA
	SELECT 	
	amd.AlmacenInventarioLoteID
	,amd.ProductoID
	FROM AlmacenMovimiento am
	INNER JOIN Almacen a on am.AlmacenID = a.AlmacenID
	INNER JOIN AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	WHERE A.AlmacenID = @AlmacenID
	AND CAST(am.FechaMovimiento AS DATE) = @FechaActual
	GROUP BY amd.AlmacenInventarioLoteID, amd.ProductoID
	INSERT INTO #PRODUCTOS
	SELECT pro.ProductoID
		,pro.Descripcion AS Producto
		,um.Descripcion AS UnidadMedida
		,pro.SubFamiliaID
	FROM AlmacenInventario ai
	INNER JOIN Almacen a ON ai.AlmacenID = a.AlmacenID
	INNER JOIN Producto pro ON ai.ProductoID = pro.ProductoID
	INNER JOIN UnidadMedicion um ON pro.UnidadID = um.UnidadID
	INNER JOIN #MOVIMIENTOSDIA MD ON pro.ProductoID = md.ProductoID
	WHERE a.OrganizacionID = @OrganizacionID
		AND a.AlmacenID = @AlmacenID
		and pro.Activo = 1
		and ai.Cantidad > 0
		group by pro.ProductoID	,pro.Descripcion ,um.Descripcion ,pro.SubFamiliaID
	--Obtiene los valores para los productos que manejan lote
	INSERT INTO #PRODUCTOSDETALLE
	SELECT pro.ProductoID
		,pro.Producto
		,pr.ManejaLote
		,ail.AlmacenInventarioLoteID
		,ail.Lote
		,ail.PrecioPromedio AS CostoUnitario
		,dbo.ObtenerTamanioLote(ai.AlmacenID, ai.AlmacenInventarioID, ail.Lote) AS TamanioLote
		,Round(ail.Cantidad, 0) AS InventarioTeorico
		,ROUND(ail.Piezas,0) AS PiezasTeoricas
		,pr.SubFamiliaID
	FROM #PRODUCTOS pro
	INNER JOIN Producto pr ON pro.ProductoID = pr.ProductoID
	INNER JOIN AlmacenInventario ai ON pro.ProductoID = ai.ProductoID
	INNER JOIN AlmacenInventarioLote ail ON ai.AlmacenInventarioID = ail.AlmacenInventarioID
	INNER JOIN #MOVIMIENTOSDIA md ON pr.ProductoID = md.ProductoID and ail.AlmacenInventarioLoteID = md.AlmacenInventarioLoteID
	WHERE pr.ManejaLote = 1
	AND ai.AlmacenID = @AlmacenID
	and ail.Activo = 1
	and ail.Cantidad > 0
	--Obtiene los valores para los productos que no manejan lote
	INSERT INTO #PRODUCTOSDETALLE
	SELECT pro.ProductoID
		,pro.Producto
		,0
		,0
		,0 AS Lote
		,ai.PrecioPromedio AS CostoUnitario
		,0
		,Round(ai.Cantidad, 0) AS InventarioTeorico
		,0
		,pr.SubFamiliaID
	FROM #PRODUCTOS pro
	INNER JOIN Producto pr ON pro.ProductoID = pr.ProductoID
	INNER JOIN AlmacenInventario ai ON pro.ProductoID = ai.ProductoID
	WHERE pr.ManejaLote = 0
	AND ai.AlmacenID = @AlmacenID
	and ai.Cantidad > 0
	SELECT ProductoID
		,Producto
		,UnidadMedida
		,SubFamiliaID
	FROM #PRODUCTOS
	SELECT ProductoID
		,Producto
		,ManejaLote
		,AlmacenInventarioLoteID
		,Lote
		,CostoUnitario
		,TamanioLote
		,InventarioTeorico
		,PiezasTeoricas
		,SubFamiliaID
	FROM #PRODUCTOSDETALLE
END

GO

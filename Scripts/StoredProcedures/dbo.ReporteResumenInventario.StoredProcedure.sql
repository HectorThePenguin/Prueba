USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteResumenInventario]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteResumenInventario]
GO
/****** Object:  StoredProcedure [dbo].[ReporteResumenInventario]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Gumaro Alberto Lugo Diaz  
-- Create date: 26/07/2014 10:37:00:00 p.m.  
-- Description: Procedimiento almacenado para generar el reporte de resumen de inventario  
-- SpName     : ReporteResumenInventario 5,2,'20150325', '20150325'
--======================================================  
CREATE PROCEDURE [dbo].[ReporteResumenInventario]  
@OrganizacionID AS INT,
@Familia INT,
@FechaInicial DATE,
@FechaFinal DATE 
AS   
BEGIN

	DECLARE @FechaInicio DATE, @FechaFin DATE
	DECLARE @FamiliaIDPremezcla INT 
	set @FamiliaIDPremezcla = 6
	SELECT @FechaInicio = @FechaInicial, @FechaFin = @FechaFinal
	
	CREATE TABLE #TotalCostoEntradas(
		ImporteCosto	decimal(24,2),
		AlmacenID	int,
		ProductoID	int,
		TipoAlmacenID	int
	)
	
	CREATE TABLE #TotalCostoEntradasRango(
		ImporteCosto	decimal(24,2),
		AlmacenID	int,
		ProductoID	int,
		TipoAlmacenID	int
	)

	CREATE TABLE #TotalCostoSalidaSubProductos
	(
		ImporteCosto decimal(24,2),
		AlmacenID	int,
		ProductoID	int,
	)


	--CREATE TABLE #tProductos
	--(
	--	ProductoID	INT
	--)

	--DECLARE @ParametroProductosCosto INT
	--SET @ParametroProductosCosto = 11

	--DECLARE @Productos VARCHAR(400)
	--SELECT @Productos = Valor
	--FROM ParametroGeneral
	--WHERE ParametroGeneralID = @ParametroProductosCosto

	--INSERT INTO #tProductos
	--SELECT Registros
	--FROM dbo.FuncionSplit(@Productos, '|')

	if @Familia =@FamiliaIDPremezcla
	begin 
		insert into #TotalCostoSalidaSubProductos
		select 
		(select sum(amdS.Importe) from AlmacenMovimiento amS 
							inner join AlmacenMovimientoDetalle amdS on amdS.AlmacenMovimientoID = amS.AlmacenMovimientoID
							where ep.AlmacenMovimientoIDSalida = amS.AlmacenMovimientoID) AS ImporteCosto
		,ame.AlmacenID
		,amdE.ProductoID
		from EntradaPremezcla ep
		inner join AlmacenMovimiento amE on ep.AlmacenMovimientoIDEntrada = amE.AlmacenMovimientoID		
		inner join Almacen a on amE.AlmacenID = a.AlmacenID
		inner join AlmacenMovimientoDetalle amdE on amdE.AlmacenMovimientoID = amE.AlmacenMovimientoID		
		where 1=1
		AND CAST(amE.FechaMovimiento as DATE) BETWEEN @FechaInicio AND @FechaFin	
		AND a.OrganizacionID = @OrganizacionID
		order by amdE.ProductoID
		
	end
	
	INSERT INTO #TotalCostoEntradas
	SELECT ImporteCosto = SUM(Importe),SUB.AlmacenId,SUB.ProductoId,SUB.TipoAlmacenId 
	FROM AlmacenMovimientoCosto AMC (NOLOCK)
	LEFT JOIN (
		SELECT 
			A.AlmacenId as AlmacenId, 
			TA.TipoAlmacenID as TipoAlmacenId, 
			AMD.ProductoID as ProductoId,
			AM.AlmacenMovimientoID
		FROM AlmacenMovimientoDetalle AMD (NOLOCK) 
		INNER JOIN AlmacenMovimiento AM (NOLOCK)
			ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID  
		LEFT  JOIN AlmacenMovimientoCosto AMC (NOLOCK)
			ON AMC.AlmacenMovimientoID = AM.AlmacenMovimientoID --AND AMC.Importe > 0 
		INNER JOIN Almacen A (NOLOCK)
			ON A.AlmacenID = AM.AlmacenID  
		INNER JOIN TipoAlmacen TA (NOLOCK)
			ON TA.TipoAlmacenID = A.TipoAlmacenID  
		INNER JOIN TipoMovimiento TM (NOLOCK)
			ON TM.TipoMovimientoID = AM.TipoMovimientoID  
		WHERE  A.OrganizacionID = @OrganizacionID
		AND CAST(AM.FechaMovimiento as DATE) < @FechaInicio
		AND TM.EsEntrada = 1 
		GROUP BY  A.AlmacenId, TA.TipoAlmacenID, AMD.ProductoID,AM.AlmacenMovimientoID 
	) SUB ON AMC.AlmacenMovimientoID = SUB.AlmacenMovimientoID
	WHERE SUB.AlmacenId IS NOT NULL
	GROUP BY SUB.AlmacenId,SUB.ProductoId,SUB.TipoAlmacenId
	ORDER BY SUB.AlmacenId,SUB.TipoAlmacenId,SUB.ProductoId
	
	-- Total entradas hasta la fecha incial 
	SELECT 
		A.AlmacenId as AlmacenId, 
		TA.TipoAlmacenID as TipoAlmacenId, 
		AMD.ProductoID as ProductoId, 
		SUM(AMD.Cantidad) as Cantidad, 
		 (SUM(AMD.Importe) + COALESCE(AMC.ImporteCosto,0) )as Importe 
		INTO #TotalProductosEntradas  
	FROM AlmacenMovimientoDetalle AMD (NOLOCK)
	INNER JOIN AlmacenMovimiento AM (NOLOCK)
		ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID  
	INNER JOIN Almacen A (NOLOCK)
		ON A.AlmacenID = AM.AlmacenID  
	INNER JOIN TipoAlmacen TA (NOLOCK)
		ON TA.TipoAlmacenID = A.TipoAlmacenID  
	LEFT  JOIN #TotalCostoEntradas AMC (NOLOCK)
		ON A.AlmacenID = AMC.AlmacenId AND AMD.ProductoID = AMC.ProductoID AND TA.TipoAlmacenID = AMC.TipoAlmacenID
	INNER JOIN TipoMovimiento TM (NOLOCK)
		ON TM.TipoMovimientoID = AM.TipoMovimientoID  
	WHERE  A.OrganizacionID = @OrganizacionID   
	AND CAST(AM.FechaMovimiento as DATE) < @FechaInicio
	AND TM.EsEntrada = 1 
	GROUP BY  A.AlmacenId, TA.TipoAlmacenID, AMD.ProductoID,AMC.ImporteCosto


	-- Total salidas hasta la fecha incial  
	SELECT 
		A.AlmacenId as AlmacenId, 
		TA.TipoAlmacenID as TipoAlmacenId, 
		AMD.ProductoID as ProductoId, 
		SUM(AMD.Cantidad) as Cantidad,
		(SUM(AMD.Importe) )as Importe  
		INTO #TotalProductosSalidas  
	FROM AlmacenMovimientoDetalle AMD (NOLOCK) 
	INNER JOIN AlmacenMovimiento AM (NOLOCK)
		ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID  
	INNER JOIN Almacen A (NOLOCK)
		ON A.AlmacenID = AM.AlmacenID  
	INNER JOIN TipoAlmacen TA (NOLOCK)
		ON TA.TipoAlmacenID = A.TipoAlmacenID  
	INNER JOIN TipoMovimiento TM (NOLOCK)
		ON TM.TipoMovimientoID = AM.TipoMovimientoID  
	WHERE  A.OrganizacionID = @OrganizacionID   
	AND CAST(AM.FechaMovimiento as DATE) < @FechaInicio
	AND TM.EsSalida = 1  
	GROUP BY  A.AlmacenId, TA.TipoAlmacenID, AMD.ProductoID
	
	INSERT INTO #TotalCostoEntradasRango
	SELECT ImporteCosto = SUM(Importe),SUB.AlmacenId,SUB.ProductoId,SUB.TipoAlmacenId 
	FROM AlmacenMovimientoCosto AMC (NOLOCK)
	LEFT JOIN (
		SELECT 
			A.AlmacenId as AlmacenId, 
			TA.TipoAlmacenID as TipoAlmacenId, 
			AMD.ProductoID as ProductoId,
			AM.AlmacenMovimientoID
		FROM AlmacenMovimientoDetalle AMD (NOLOCK) 
		INNER JOIN AlmacenMovimiento AM (NOLOCK)
			ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID  
		LEFT  JOIN AlmacenMovimientoCosto AMC (NOLOCK)
			ON AMC.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN Almacen A (NOLOCK)
			ON A.AlmacenID = AM.AlmacenID  
		INNER JOIN TipoAlmacen TA (NOLOCK)
			ON TA.TipoAlmacenID = A.TipoAlmacenID  
		INNER JOIN TipoMovimiento TM (NOLOCK)
			ON TM.TipoMovimientoID = AM.TipoMovimientoID  
		WHERE  A.OrganizacionID = @OrganizacionID
		AND CAST(AM.FechaMovimiento as DATE) BETWEEN @FechaInicio AND @FechaFin
		AND TM.EsEntrada = 1 
		GROUP BY  A.AlmacenId, TA.TipoAlmacenID, AMD.ProductoID,AM.AlmacenMovimientoID 
	) SUB ON AMC.AlmacenMovimientoID = SUB.AlmacenMovimientoID
	WHERE SUB.AlmacenId IS NOT NULL
	GROUP BY SUB.AlmacenId,SUB.ProductoId,SUB.TipoAlmacenId
	ORDER BY SUB.AlmacenId,SUB.TipoAlmacenId,SUB.ProductoId


	SELECT 
		A.AlmacenId as AlmacenId, 
		TA.TipoAlmacenID as TipoAlmacenId, 
		AMD.ProductoID as ProductoId, 
		SUM(AMD.Cantidad) as Cantidad, 
		(SUM(AMD.Importe)  + COALESCE(AMC.ImporteCosto,0) )as Importe--
		INTO #TotalProductosEntradasRango 
	FROM AlmacenMovimientoDetalle AMD (NOLOCK) 
	INNER JOIN AlmacenMovimiento AM (NOLOCK)
		ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID  
	INNER JOIN Almacen A (NOLOCK)
		ON A.AlmacenID = AM.AlmacenID  		
	INNER JOIN TipoAlmacen TA (NOLOCK)
		ON TA.TipoAlmacenID = A.TipoAlmacenID  
	LEFT  JOIN #TotalCostoEntradasRango AMC (NOLOCK)
		ON A.AlmacenID = AMC.AlmacenId AND AMD.ProductoID = AMC.ProductoID AND TA.TipoAlmacenID = AMC.TipoAlmacenID
	INNER JOIN TipoMovimiento TM (NOLOCK)
		ON TM.TipoMovimientoID = AM.TipoMovimientoID  
	WHERE  A.OrganizacionID = @OrganizacionID
	AND CAST(AM.FechaMovimiento as DATE) BETWEEN @FechaInicio AND @FechaFin
	AND TM.EsEntrada = 1 
	GROUP BY  A.AlmacenId, TA.TipoAlmacenID, AMD.ProductoID,AMC.ImporteCosto
	
	--Total salidas hasta entre fecha inicial y fecha final  
	SELECT 
		A.AlmacenId as AlmacenId, 
		TA.TipoAlmacenID as TipoAlmacenId, 
		AMD.ProductoID as ProductoId, 
		SUM(AMD.Cantidad) as Cantidad,  
		(SUM(AMD.Importe) + COALESCE(SUM(AMC.Importe),0) )as Importe 
		INTO #TotalProductosSalidasRango 
	FROM AlmacenMovimientoDetalle AMD (NOLOCK)
	INNER JOIN AlmacenMovimiento AM (NOLOCK)
		ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID  
	LEFT  JOIN AlmacenMovimientoCosto AMC (NOLOCK)
		ON AMC.AlmacenMovimientoID = AM.AlmacenMovimientoID  
	INNER JOIN Almacen A (NOLOCK)
		ON A.AlmacenID = AM.AlmacenID  
	INNER JOIN TipoAlmacen TA (NOLOCK)
		ON TA.TipoAlmacenID = A.TipoAlmacenID  
	INNER JOIN TipoMovimiento TM (NOLOCK)
		ON TM.TipoMovimientoID = AM.TipoMovimientoID  
	WHERE  A.OrganizacionID = @OrganizacionID
	AND CAST(AM.FechaMovimiento as DATE) BETWEEN @FechaInicio AND @FechaFin
	AND TM.EsSalida = 1 
	GROUP BY  A.AlmacenId, TA.TipoAlmacenID, AMD.ProductoID
	
	SELECT 
		AI.AlmacenId, 
		F.FamiliaID, 
		F.Descripcion AS Familia, 
		TA.TipoAlmacenID, 
		A.Descripcion AS TipoAlmacen,   
		SF.SubFamiliaID, 
		SF.Descripcion AS SubFamilia,  
		P.ProductoID, 
		P.Descripcion AS Producto,    
		( COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosEntradas   
		WHERE #TotalProductosEntradas.AlmacenId = AI.AlmacenID AND #TotalProductosEntradas.ProductoId = AI.ProductoID),0) -
		COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosSalidas   
		WHERE #TotalProductosSalidas.AlmacenId = AI.AlmacenID AND #TotalProductosSalidas.ProductoId = AI.ProductoID),0) ) as InventarioInicial ,
		COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosEntradasRango   
		WHERE #TotalProductosEntradasRango.AlmacenId = AI.AlmacenID AND #TotalProductosEntradasRango.ProductoId = AI.ProductoID),0) as UnidadEntrada,  
		COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosSalidasRango   
		WHERE #TotalProductosSalidasRango.AlmacenId = AI.AlmacenID AND #TotalProductosSalidasRango.ProductoId = AI.ProductoID),0) as UnidadSalida, 
		( COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosEntradasRango   
		WHERE #TotalProductosEntradasRango.AlmacenId = AI.AlmacenID AND #TotalProductosEntradasRango.ProductoId = AI.ProductoID),0) -
		COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosSalidasRango   
		WHERE #TotalProductosSalidasRango.AlmacenId = AI.AlmacenID AND #TotalProductosSalidasRango.ProductoId = AI.ProductoID),0) ) +
		( COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosEntradas   
		WHERE #TotalProductosEntradas.AlmacenId = AI.AlmacenID AND #TotalProductosEntradas.ProductoId = AI.ProductoID),0) -    
		COALESCE((SELECT SUM(Cantidad) FROM #TotalProductosSalidas   
		WHERE #TotalProductosSalidas.AlmacenId = AI.AlmacenID AND #TotalProductosSalidas.ProductoId = AI.ProductoID),0) ) as CantidadInventario, 
		( COALESCE((SELECT SUM(Importe) FROM #TotalProductosEntradas   
		WHERE #TotalProductosEntradas.AlmacenId = AI.AlmacenID AND #TotalProductosEntradas.ProductoId = AI.ProductoID),0) -    
		COALESCE((SELECT SUM(Importe) FROM #TotalProductosSalidas   
		WHERE #TotalProductosSalidas.AlmacenId = AI.AlmacenID AND #TotalProductosSalidas.ProductoId = AI.ProductoID),0) ) as ImporteInventarioInicial,
		COALESCE((SELECT SUM(Importe) FROM #TotalProductosEntradasRango   
		WHERE #TotalProductosEntradasRango.AlmacenId = AI.AlmacenID AND #TotalProductosEntradasRango.ProductoId = AI.ProductoID),0) as ImporteEntrada,    
		COALESCE((SELECT SUM(Importe) FROM #TotalProductosSalidasRango   
		WHERE #TotalProductosSalidasRango.AlmacenId = AI.AlmacenID AND #TotalProductosSalidasRango.ProductoId = AI.ProductoID),0) as ImporteSalida,  
		(COALESCE((SELECT SUM(Importe) FROM #TotalProductosEntradasRango   
		WHERE #TotalProductosEntradasRango.AlmacenId = AI.AlmacenID AND #TotalProductosEntradasRango.ProductoId = AI.ProductoID),0) -    
		COALESCE((SELECT SUM(Importe) FROM #TotalProductosSalidasRango   
		WHERE #TotalProductosSalidasRango.AlmacenId = AI.AlmacenID AND #TotalProductosSalidasRango.ProductoId = AI.ProductoID),0) ) +
		COALESCE((SELECT SUM(Importe) FROM #TotalProductosEntradas   
		WHERE #TotalProductosEntradas.AlmacenId = AI.AlmacenID AND #TotalProductosEntradas.ProductoId = AI.ProductoID),0) -
		COALESCE((SELECT SUM(Importe) FROM #TotalProductosSalidas   
		WHERE #TotalProductosSalidas.AlmacenId = AI.AlmacenID AND #TotalProductosSalidas.ProductoId = AI.ProductoID),0)  as ImporteInventario
	INTO #tReporte
	FROM AlmacenInventario AI (NOLOCK) 
	INNER JOIN Producto P (NOLOCK)
		ON P.ProductoID = AI.ProductoID  
	INNER JOIN SubFamilia SF (NOLOCK)
		ON SF.SubFamiliaID = P.SubFamiliaID  
	INNER JOIN Familia F (NOLOCK)
		ON F.FamiliaID = SF.FamiliaID  
	INNER JOIN Almacen A (NOLOCK)
		ON A.AlmacenID = AI.AlmacenID  
	INNER JOIN TipoAlmacen TA (NOLOCK)
		ON TA.TipoAlmacenID = A.TipoAlmacenID  
	INNER JOIN Organizacion O (NOLOCK)
		ON O.OrganizacionID = A.OrganizacionID  
	WHERE O.OrganizacionID = @OrganizacionID AND F.FamiliaID = @Familia  
	--AND AI.Cantidad > 0 
	GROUP BY AI.AlmacenId, AI.ProductoId, F.FamiliaID, F.Descripcion,  TA.TipoAlmacenID, A.Descripcion ,
	SF.SubFamiliaID, SF.Descripcion,
	P.ProductoID, P.Descripcion
	ORDER BY AI.AlmacenID 

	if @Familia =@FamiliaIDPremezcla
	BEGIN 
	update r 
	set r.ImporteEntrada = (r.ImporteEntrada + sub.ImporteCosto), r.ImporteInventario = (r.ImporteEntrada + sub.ImporteCosto) - r.ImporteSalida
	from #tReporte r
	inner join #TotalCostoSalidaSubProductos sub on r.AlmacenID = sub.AlmacenID and r.ProductoID = sub.ProductoID
	END

	SELECT * 
	FROM #tReporte tR
	--LEFT OUTER JOIN #tProductos tP
	--	ON (tR.ProductoID = tP.ProductoID)
	--WHERE tP.ProductoID IS NULL

DROP TABLE #TotalProductosEntradasRango  
DROP TABLE #TotalProductosSalidasRango
DROP TABLE #TotalProductosSalidas
DROP TABLE #TotalProductosEntradas
DROP TABLE #TotalCostoEntradas
DROP TABLE #TotalCostoEntradasRango
--DROP TABLE #tProductos
DROP TABLE #tReporte

END
GO

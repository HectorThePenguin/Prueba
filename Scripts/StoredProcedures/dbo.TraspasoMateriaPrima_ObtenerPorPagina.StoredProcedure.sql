USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoMateriaPrima_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoMateriaPrima_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 12/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TraspasoMateriaPrima_ObtenerPorPagina '',1,7,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[TraspasoMateriaPrima_ObtenerPorPagina]
 @DescripcionProducto varchar(50)
 ,@OrganizacionID int
 ,@DiasPermitidos int
 ,@Activo bit
 ,@Inicio int
 ,@Limite int             
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY tmp.TraspasoMateriaPrimaID ASC) AS [RowNum],
		tmp.TraspasoMateriaPrimaID
		,tmp.OrganizacionID
		,tmp.ContratoOrigenID
		,tmp.ContratoDestinoID
		,tmp.FolioTraspaso
		,tmp.AlmacenOrigenID
		,tmp.AlmacenDestinoID
		,tmp.InventarioLoteOrigenID
		,tmp.InventarioLoteDestinoID
		,tmp.CuentaSAPID
		,tmp.Justificacion
		,tmp.AlmacenMovimientoEntradaID
		,tmp.AlmacenMovimientoSalidaID
		,tmp.FechaMovimiento
		,tmp.Activo
		,amd.Cantidad AS CantidadEntrada
		,amd2.Cantidad AS CantidadSalida
		,amd.ProductoID	
		INTO #Datos
	FROM TraspasoMateriaPrima tmp
	INNER JOIN AlmacenMovimiento am on tmp.AlmacenMovimientoEntradaID = am.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	INNER JOIN AlmacenMovimiento am2 on tmp.AlmacenMovimientoSalidaID = am2.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amd2 on am2.AlmacenMovimientoID = amd2.AlmacenMovimientoID
	INNER JOIN Producto pr on amd.ProductoID = pr.ProductoID
	WHERE tmp.OrganizacionID = @OrganizacionID
	AND CAST(tmp.FechaMovimiento AS DATE) > GETDATE() - @DiasPermitidos
	AND (pr.Descripcion like '%' + @DescripcionProducto + '%' OR @DescripcionProducto = '') 
	and tmp.Activo = @Activo	
	select 
		TraspasoMateriaPrimaID
		,OrganizacionID
		,ContratoOrigenID
		,ContratoDestinoID
		,FolioTraspaso
		,AlmacenOrigenID
		,AlmacenDestinoID
		,InventarioLoteOrigenID
		,InventarioLoteDestinoID
		,CuentaSAPID
		,Justificacion
		,AlmacenMovimientoEntradaID
		,AlmacenMovimientoSalidaID
		,FechaMovimiento
		,Activo
		,CantidadEntrada
		,CantidadSalida		
		,ProductoID	
	from #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TraspasoMateriaPrimaID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

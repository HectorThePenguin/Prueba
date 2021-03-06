USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Jesus Alvarez
-- Create date: 26/05/2014
-- Description: Obtiene contratos por pagina
-- Contrato_ObtenerPorPagina 1, 0, '', 0, 0, 0, 0, 0, '', 0, 0, '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorPagina] @ContratoID INT
	,@OrganizacionID INT
	,@Folio INT
	,@ProductoDescripcion VARCHAR(50)
	,@ProductoID INT
	,@TipoContratoID INT
	,@TipoFleteID INT
	,@ProveedorID INT
	,@CodigoSAP VARCHAR(10)
	,@EstatusID INT
	,@TipoCambioID INT
	,@PesoNegociar VARCHAR(10)
	,@Activo BIT
	,@Inicio INT
	,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @TipoMovimientoEntradaCompra int,
	@TipoMovimientoEntradaAlmacen int,
	@TipoMovimientoSalidaAjuste int,
	@TipoMovimientoEntradaAjuste int,
	@TipoMovimientoEntradaBodegaTerceros int
	
	set @TipoMovimientoEntradaCompra = 20
	set @TipoMovimientoEntradaAlmacen = 23
	set @TipoMovimientoEntradaAjuste = 31
	set @TipoMovimientoSalidaAjuste = 32
	set @TipoMovimientoEntradaBodegaTerceros = 21
	
	

	SELECT ROW_NUMBER() OVER (ORDER BY C.Folio ASC) AS RowNum
		,C.ContratoID
		,C.OrganizacionID
		,O.Descripcion AS OrganizacionDescripcion
		,O.TipoOrganizacionID
		,C.Folio
		,C.ProductoID
		,P.Descripcion AS ProductoDescripcion
		,P.SubFamiliaID
		,C.CuentaSAPID
		,CS.CuentaSAP
		,CS.Descripcion AS CuentaDescripcion
		,C.TipoContratoID
		,TC.Descripcion AS TipoContratoDescripcion
		,ISNULL(C.TipoFleteID, 0) AS TipoFleteID
		,ISNULL(TF.Descripcion, '') AS TipoFleteDescripcion
		,C.ProveedorID
		,PR.Descripcion AS ProveedorDescripcion
		,PR.CodigoSAP
		,PR.TipoProveedorID
		,C.Precio
		,ISNULL(C.TipoCambioID, 0) AS TipoCambioID
		,TCB.Descripcion AS TipoCambioDescripcion
		,TCB.Cambio
		,C.EstatusID
		,E.TipoEstatus
		,E.Descripcion AS EstatusDescripcion
		,C.Cantidad
		,C.Tolerancia
		,C.Parcial
		,C.Merma
		,C.PesoNegociar
		,C.Fecha
		,C.FechaVigencia
		,C.CalidadOrigen
		,C.FolioAserca
		,C.FolioCobertura
		,C.CostoSecado
		,C.AplicaDescuento
		,C.Activo
		,C.FechaCreacion
		,C.UsuarioCreacionID
	INTO #Datos
	FROM Contrato C
	INNER JOIN Organizacion O ON (O.OrganizacionID = C.OrganizacionID)
	INNER JOIN Producto P ON (C.ProductoID = P.ProductoID)
	INNER JOIN TipoContrato TC ON (C.TipoContratoID = TC.TipoContratoID)
	LEFT JOIN CuentaSAP CS ON (C.CuentaSAPID = CS.CuentaSAPID)
	INNER JOIN TipoFlete TF ON (C.TipoFleteID = TF.TipoFleteID)
	INNER JOIN Proveedor PR ON (C.ProveedorID = PR.ProveedorID)
	INNER JOIN TipoCambio TCB ON (C.TipoCambioID = TCB.TipoCambioID)
	INNER JOIN Estatus E ON (C.EstatusID = E.EstatusID)
	WHERE @Folio IN (C.Folio,0)
		AND @ContratoID IN (C.ContratoID,0)
		AND @OrganizacionID IN (C.OrganizacionID,0)
		AND @ProductoID IN (C.ProductoID,0)
		AND @TipoContratoID IN (C.TipoContratoID,0)
		AND @TipoFleteID IN (C.TipoFleteID,0)
		AND @ProveedorID IN (C.ProveedorID,0)
		AND @TipoCambioID IN (C.TipoCambioID,0)
		AND @PesoNegociar IN (C.PesoNegociar,'')
		AND @EstatusID IN (C.EstatusID,0)
		AND (PR.CodigoSAP LIKE '%' + @CodigoSAP + '%' OR @CodigoSAP = '')

	CREATE TABLE #CANTIDADESCONTRATO (
		ContratoID INT
		,CantidadSurtida INT
		)
	CREATE TABLE #CANTIDADCONTRATOORIGEN
	(
		ContratoID INT
		,CantidadSurtida INT
	)	
	
	CREATE TABLE #CANTIDADCONTRATOMERMA
	(
		ContratoID INT
		,CantidadMerma INT		
	)	
	CREATE TABLE #CANTIDADCONTRATOSUPERAVIT
	(
		ContratoID INT
		,CantidadSuperavit INT		
	)	

	INSERT INTO #CANTIDADESCONTRATO
	SELECT rv.ContratoID
		,sum(amd.Cantidad) AS TotalEntregado
	FROM RegistroVigilancia rv
	INNER JOIN EntradaProducto ep ON rv.RegistroVigilanciaID = ep.RegistroVigilanciaID
	INNER JOIN AlmacenMovimiento am ON ep.AlmacenMovimientoID = am.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amd ON am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	WHERE am.TipoMovimientoID IN (@TipoMovimientoEntradaCompra,@TipoMovimientoEntradaAlmacen)
		AND rv.ContratoID IS NOT NULL
	GROUP BY rv.ContratoID
	
	INSERT INTO #CANTIDADCONTRATOORIGEN
	SELECT rv.ContratoID
		,sum(ep.PesoBonificacion) AS TotalEntregado
	FROM RegistroVigilancia rv
	INNER JOIN EntradaProducto ep ON rv.RegistroVigilanciaID = ep.RegistroVigilanciaID
	INNER JOIN AlmacenMovimiento am ON ep.AlmacenMovimientoID = am.AlmacenMovimientoID
	INNER JOIN AlmacenMovimientoDetalle amd ON am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	WHERE am.TipoMovimientoID IN (@TipoMovimientoEntradaCompra,@TipoMovimientoEntradaAlmacen)
		AND rv.ContratoID IS NOT NULL
	GROUP BY rv.ContratoID
	
	INSERT INTO #CANTIDADCONTRATOMERMA
	SELECT amd1.ContratoID	
		,sum(amd2.Cantidad) AS CantidadMerma		
	FROM AlmacenMovimiento am1
	INNER JOIN AlmacenMovimientoDetalle amd1 on am1.AlmacenMovimientoID = amd1.AlmacenMovimientoID
	inner JOIN AlmacenMovimientoDetalle amd2 ON amd1.AlmacenInventarioLoteID = amd2.AlmacenInventarioLoteID
	inner JOIN AlmacenMovimiento am2 ON amd2.AlmacenMovimientoID = am2.AlmacenMovimientoID	
	WHERE am1.TipoMovimientoID IN (@TipoMovimientoEntradaCompra,@TipoMovimientoEntradaAlmacen,@TipoMovimientoEntradaBodegaTerceros)
		AND amd1.ContratoID IS NOT NULL and amd1.ContratoID <> 0
		and am2.TipoMovimientoID in (@TipoMovimientoSalidaAjuste)
	GROUP BY amd1.ContratoID
	
	INSERT INTO #CANTIDADCONTRATOSUPERAVIT
	SELECT amd1.ContratoID	
		,sum(amd2.Cantidad) AS CantidadMerma		
	FROM AlmacenMovimiento am1
	INNER JOIN AlmacenMovimientoDetalle amd1 on am1.AlmacenMovimientoID = amd1.AlmacenMovimientoID
	inner JOIN AlmacenMovimientoDetalle amd2 ON amd1.AlmacenInventarioLoteID = amd2.AlmacenInventarioLoteID
	inner JOIN AlmacenMovimiento am2 ON amd2.AlmacenMovimientoID = am2.AlmacenMovimientoID	
	WHERE am1.TipoMovimientoID IN (@TipoMovimientoEntradaCompra,@TipoMovimientoEntradaAlmacen,@TipoMovimientoEntradaBodegaTerceros)
		AND amd1.ContratoID IS NOT NULL and amd1.ContratoID <> 0
		and am2.TipoMovimientoID in (@TipoMovimientoEntradaAjuste)
	GROUP BY amd1.ContratoID

	SELECT dt.ContratoID
		,OrganizacionID
		,OrganizacionDescripcion
		,TipoOrganizacionID
		,Folio
		,ProductoID
		,ProductoDescripcion
		,SubFamiliaID
		,CuentaSAPID
		,CuentaSAP
		,CuentaDescripcion
		,TipoContratoID
		,TipoContratoDescripcion
		,TipoFleteID
		,TipoFleteDescripcion
		,ProveedorID
		,ProveedorDescripcion
		,CodigoSAP
		,TipoProveedorID
		,Precio
		,TipoCambioID
		,TipoCambioDescripcion
		,Cambio
		,EstatusID
		,TipoEstatus
		,EstatusDescripcion
		,Cantidad
		,Tolerancia
		,Parcial
		,Merma
		,PesoNegociar
		,Fecha
		,FechaVigencia
		,CalidadOrigen
		,FolioAserca
		,FolioCobertura
		,CostoSecado
		,AplicaDescuento
		,Activo
		,FechaCreacion
		,UsuarioCreacionID		
		,CASE 
			WHEN dt.PesoNegociar = 'Destino'
				THEN 				
				cc.CantidadSurtida + isnull(ccm.CantidadMerma,0) -  isnull(ccs.CantidadSuperavit,0)
			ELSE cco.CantidadSurtida + isnull(ccm.CantidadMerma,0) -  isnull(ccs.CantidadSuperavit,0)
				END AS CantidadSurtida 
	FROM #Datos dt
	LEFT JOIN #CANTIDADESCONTRATO cc ON dt.ContratoID = cc.ContratoID
	LEFT JOIN #CANTIDADCONTRATOORIGEN cco on dt.ContratoID = cco.ContratoID
	LEFT JOIN #CANTIDADCONTRATOMERMA ccm on dt.ContratoID = ccm.ContratoID
	LEFT JOIN #CANTIDADCONTRATOSUPERAVIT ccs on dt.ContratoID = ccs.ContratoID
	WHERE RowNum BETWEEN @Inicio
			AND @Limite

	SELECT COUNT(ContratoID) AS TotalReg
	FROM #Datos

	DROP TABLE #Datos

	SET NOCOUNT OFF;
END

GO

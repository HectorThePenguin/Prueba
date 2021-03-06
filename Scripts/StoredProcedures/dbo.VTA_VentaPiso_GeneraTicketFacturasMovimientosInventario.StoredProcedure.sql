USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario]
GO
/****** Object:  StoredProcedure [dbo].[VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario]
	@nAlmacen [smallint],
	@nVenta [int],
	@bFactura [bit],
	@bFacturaNoFiscal [bit] = 0
WITH EXECUTE AS CALLER
AS
/******************************************************************
Sistema			:COMERCIAL SUKARNE
Módulo			:COMERCIAL y PUNTO DE VENTA
Procedimiento	:VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario
Fecha			:22 de Marzo del 2007
Desarrolló		:Lic. David Adan Velazquez Sanchez
Que Hace?		:Genera el ticket o facturas segun sea el caso para la venta, asi como sus correspondientes
				 movimientos de inventario
				 
Parametros		:@nAlmacen.- Folio del almacen del cual se hace la salida
				:@nVenta.- Folio de Venta
				:@bFactura.- Determina si se generara ticket o factura
Validaciones	:
					1)Que exista la venta
					2)Que no este suspendida ,cancelada	y aplicada

Acciones		:
					1)Determina el numero de facturas o tickets (solo uno) a generar
					2)Genera el movimiento de inventario en base al ticket o factura
					3)Genera el ticket o factura					

Pendientes		:Que proceso actualizara el saldo de puntos del cliente			-ya quedo a la hora de cerrar la venta
				:Agrupar el resto de detalle que no es combo ni obsequio --- versiones futuras
		
Modificaciones	:13-Abril-07.- Al procesar la venta ,los productos tomados como oferta se actualizara su disponible
				:17-Abril-07.- Grabar la venta la cual esta generando las facturas
							   Considerar los porductos de regalo. (marcarlos) estos no se tomaran en cuenta al determinar las lineas de facturacion							   

				:Al insertar el detalle de la factura tenemos que reindexar a los combos y los obsequios ya que se puede generar mas de una factura
				 28-Abril-2007.- Se modifico para agregar los codigos de barra en un tercer nivel de detalle
				 1-Mayo-2007.- Generaremos un cargo por cada factura generada, estos cargos se aplicaran hasta que caigan a corporativo
				 30-Mayo-2007.- Consideramos los puntos por ticket ya que estos puntos los estabamos perdiendo a la hora  de facturar
								, estos los repartimos equitativamente entre los documentos generados

				:15-Junio-2007.- Los servicios por producto que no se agregan al precio, tambien los dejaremos caer en la tabla de servicios, pero con el bit apagado
				:9-Agosto-2007--Cambio pedido por arturo.- Marcamos el renglon del movimiento de inventario generado para el detalle
*******************************************************************/

/************************VALIDACIONES********************************/

	--Creamos una temporal del encabezado para mejorar el performance
	SELECT * INTO #VTA_VentaPiso FROM VTA_VentaPiso(NOLOCK) WHERE nAlmacen = @nAlmacen AND nVenta = @nVenta

--1)Que exista la venta
IF NOT EXISTS(SELECT 1 FROM #VTA_VentaPiso(NOLOCK))
BEGIN
	ROLLBACK TRAN
	RAISERROR('VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario: La venta proporcionada no existe en el sistema' ,16 ,1)
	RETURN
END

--2)Que no este suspendida ,cancelada	y aplicada
IF EXISTS(SELECT 1 FROM #VTA_VentaPiso(NOLOCK) WHERE (bSuspendido = 1 OR bActivo = 0 OR bAplicado = 1))
BEGIN
	ROLLBACK TRAN
	RAISERROR('VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario: La venta proporcionada se encuentra suspendida, cancelada o aplicada' ,16 ,1)
	RETURN
END


/**********************FIN VALIDACIONES******************************/

/************************DECLARACION DE VARIABLES********************************/

	 DECLARE   @nLineasXDetalle    INT  
    ,@nTipoDocumento    TINYINT  
    ,@nClienteDistinguido   INT      
    ,@cSerie      VARCHAR(4)  
    ,@nFacturaTicket    INT  
    ,@nFolioTipo     INT  
    ,@nMovimientoInventario   INT  
    ,@nCargo      INT  
    ,@nAbono      INT  
    ,@cFolioFacturaTicket   VARCHAR(100)  
    ,@cFolioTipoFacturaTicket  VARCHAR(100)  
    ,@cFolioMovimiento    VARCHAR(100)  
    ,@cFolioCargo     VARCHAR(100)  
    ,@cFolioAbono     VARCHAR(100)  
    ,@nCanalDistribucion   SMALLINT  
    ,@nTipoMovimientoInventario  SMALLINT  
    ,@nTipoManejoPiezasPeso   TINYINT  
    ,@nTipoInventarioSumarizado  TINYINT  
    ,@nVendedorDefault    INT  
    ,@nPuntosXTicketGanados   INT  
    ,@nPuntosAlMomentoDeCompra  INT  
    ,@nLineasProductos    SMALLINT  
    ,@nLineasEdoCuenta    SMALLINT  
    ,@nLineasPagare     SMALLINT  
    ,@bEdoCuenta     BIT  
    ,@bPagare      BIT  
    ,@nCliente      INT  
    ,@nTipoSurtido     TINYINT  
    ,@nRenglonActual    SMALLINT  
    ,@nMaxRenglones     SMALLINT  
    ,@nRenglonesTotales    SMALLINT  
    ,@nNumeroFactura    TINYINT  
    ,@nTotalFacturas    TINYINT  
    ,@nRenglonesUtilizados   TINYINT  
    ,@nRenglonesAUtilizar   TINYINT  
    ,@nRenglonesXFacturaNetos  INT  
    ,@nRenglonesTotalesUtilizados SMALLINT  
    ,@nTipoMovimientoCargo   SMALLINT  
    ,@nTipoMovimientoAbono   SMALLINT  
    ,@cSQLText      VARCHAR(500)  
    ,@nPuntosXTicket    INT   
    ,@nUnidadPiezas     TINYINT  
    ,@Rfc       VARCHAR(13)  
    ,@VtaPubGral     TINYINT  
  
 SET NOCOUNT ON  
  
 --SELECT 'Inicio y Preparacion'  
      
 SELECT @nCanalDistribucion = nCanalDistribucion FROM CTL_Almacenes(NOLOCK) WHERE nAlmacen = @nAlmacen  
 SELECT @cFolioFacturaTicket = 'VTA_FacturasTickets_Canal_' + CONVERT(VARCHAR(10) ,@nCanalDistribucion)  
      
 IF @bFactura = 1  
 BEGIN  
  SELECT  @nTipoDocumento = dbo.ADSUM_ParametroAdministrado_Numerico( 'TIPOFACTURA_FACTURA_CONTADO' )  
    ,@cFolioTipoFacturaTicket = @cFolioFacturaTicket + '_Tipo_FACTURA'  
  SELECT @cSerie = cSerie FROM CTL_ConfiguracionCanalesDistribucion(NOLOCK) WHERE nCanalDistribucion = @nCanalDistribucion  
 END  
 ELSE  
  SELECT  @nTipoDocumento = dbo.ADSUM_ParametroAdministrado_Numerico( 'TIPOFACTURA_TICKETS' )  
    ,@cFolioTipoFacturaTicket = @cFolioFacturaTicket + '_Tipo_TICKETS'  
      
 SELECT  @cFolioMovimiento = 'INV_MovimientoInventario_Almacen_' + CONVERT(VARCHAR(5) ,@nAlmacen)  
   ,@nTipoMovimientoInventario = dbo.ADSUM_ParametroAdministrado_Numerico( 'TM_Salida_Por_Venta' )  
   ,@nTipoManejoPiezasPeso = dbo.ADSUM_ParametroAdministrado_Numerico( 'Administra_TipoManejoInventario_PiezasyPeso' )  
   ,@nTipoInventarioSumarizado = dbo.ADSUM_ParametroAdministrado_Numerico( 'Administra_TipoInventario_Sumarizado' )  
   ,@nLineasProductos = dbo.ADSUM_ParametroAdministrado_Numerico( 'RENGLONESXPRODUCTO' )     
   ,@nLineasEdoCuenta = dbo.ADSUM_ParametroAdministrado_Numerico( 'RENGLONESXEDOCUENTA' )  
   ,@nLineasPagare = dbo.ADSUM_ParametroAdministrado_Numerico( 'RENGLONESXPAGARE' )  
   ,@cFolioCargo = 'Cxc_Cargos_CD_' + CONVERT(VARCHAR(5) ,@nCanalDistribucion)  
   ,@cFolioAbono = 'CXC_Abono_CD' + CONVERT(VARCHAR(5) ,@nCanalDistribucion)  
   ,@nTipoMovimientoCargo = dbo.ADSUM_ParametroAdministrado_Numerico( 'CXC_TM_FACTURA' )  
   ,@nTipoMovimientoAbono = dbo.ADSUM_ParametroAdministrado_Numerico( 'CXC_TM_PAGO' )  
   ,@nUnidadPiezas=dbo.ADSUM_ParametroAdministrado_Numerico('UNIDAD_PIEZAS')  
     
  
 SELECT @nLineasXDetalle = @nLineasProductos   
 SELECT @nPuntosXTicketGanados = nPuntosGanados ,@nCliente = nCliente,@nClienteDistinguido = nClienteDistinguido ,@nTipoSurtido = nTipoSurtido ,@nPuntosAlMomentoDeCompra = nPuntosSaldoAlMomentoDeCompra FROM #VTA_VentaPiso(NOLOCK)  
 --SELECT @nPuntosAlMomentoDeCompra = ISNULL(nPuntos ,0) FROM COM_ClienteDistinguido(NOLOCK) WHERE nClienteDistinguido = @nClienteDistinguido  
 SELECT @bEdoCuenta = bEdoCuenta ,@bPagare = bPagare, @Rfc = cRFC FROM CTL_Clientes(NOLOCK) WHERE nCliente = @nCliente  
 SELECT @nVendedorDefault = nVendedorDefault FROM CTL_ConfiguracionCanalesDistribucion(NOLOCK) WHERE nCanalDistribucion = @nCanalDistribucion  
 SELECT @nPuntosAlMomentoDeCompra = ISNULL(@nPuntosAlMomentoDeCompra ,0)  
  
    --En esta opcion de la Venta Piso las facturas son de contado  
    Set @VtaPubGral = dbo.TipoVentaPublico(@Rfc, 1)  
   
 --Agregamos lineas disponibles al detalle en caso de no imprimir pagare y estado de cuenta  
 IF @bEdoCuenta = 0  
  SELECT @nLineasXDetalle = @nLineasXDetalle + @nLineasEdoCuenta  
  
 IF @bPagare = 0    SELECT @nLineasXDetalle = @nLineasXDetalle + @nLineasPagare  
   
 --Modificacion: Lic. David Adán Velázquez Sánchez 30/Nov/2008  
  --Para los tickets se toamra en cuenta la division de documentos, sin considerar el pagare  
 --Para cuando son tickets la impresion es continua, y con estas lineas por detalle nunca nos pasaremos :)  
 IF @bFactura = 0  
  SELECT @nLineasXDetalle = @nLineasXDetalle + @nLineasEdoCuenta  
  --SELECT @nLineasXDetalle = 10000000  
     
  
/************************DECLARACION DE VARIABLES********************************/  
  
  
/********************    INICIO DE ACCIONES      ********************************/  
  
 --Calculamos los puntos x ticket  
 IF EXISTS(SELECT 1 FROM #VTA_VentaPiso WHERE NOT nClienteDistinguido IS NULL)  
 BEGIN  
  CREATE TABLE #PuntosXticket (nPuntos INT ,nConfiguracionPuntos INT)  
  
  INSERT #PuntosXticket  
  EXEC VTA_ObtenPuntosXTicket @nAlmacen ,@nVenta  
  --EXEC VTA_ObtenPuntosXTicket 6,24  
  
  UPDATE VTA_VentaPiso   
   SET  nPuntosGanados = nPuntosGanados + ISNULL(T.nPuntos ,0)   
    ,nConfiguracionPuntos = T.nConfiguracionPuntos  
  FROM #PuntosXticket AS T  
  
  --Tambien la temporal  
  UPDATE #VTA_VentaPiso   
   SET  nPuntosGanados = nPuntosGanados + ISNULL(T.nPuntos ,0)   
    ,nConfiguracionPuntos = T.nConfiguracionPuntos  
  FROM #PuntosXticket AS T  
  
  DROP TABLE #PuntosXticket  
 END  
    
 --Almacenamos el detalle de la venta piso en una temporal para agilizar este proceso cuando la bd crezca   
 SELECT * INTO #VTA_VentaPisoDetalle FROM VTA_VentaPisoDetalle(NOLOCK) WHERE nAlmacen = @nAlmacen AND nVenta = @nVenta AND bCancelado = 0 AND bCancelacion = 0   
  
 --Calculamos los puntos x ticket  
 SELECT @nPuntosXTicket = nPuntosGanados - (SELECT SUM(nPuntosGanados) FROM #VTA_VentaPisoDetalle)  
 FROM #VTA_VentaPiso   
  
 --En caso de venir de una integracion de surtido, cancelamos el detalle de la integracion  
 --Desmarcamos la integracion  
 UPDATE TLM_PedidosDetalleEmbarque   
  SET bActivo = 0  
 WHERE nAlmacen = @nAlmacen AND nVenta = @nVenta  
  
 --13-Abril-07.- Al procesar la venta ,los productos tomados como oferta se actualizara su disponible  
 SELECT  T.Oferta_nListaPrecios ,T.Oferta_nRenglon   
   ,nCantidadVolumen =  SUM ( CASE   
            WHEN P.nTipoManejoInventario <> @nTipoManejoPiezasPeso THEN T.nCantidadEnUnidadBase  
            ELSE T.nPiezas  
           END  
          )  
 INTO #ActualizacionOfertas  
 FROM #VTA_VentaPisoDetalle AS T  
 INNER JOIN CTL_ProductosMarcasUnidades AS PMU(NOLOCK) ON (T.nProductoMarcaUnidad = PMU.nProductoMarcaUnidad AND NOT T.Oferta_nListaPrecios IS NULL)  
 INNER JOIN CTL_Productos AS P(NOLOCK) ON (PMU.nProducto = P.nProducto)   
 GROUP BY T.Oferta_nListaPrecios ,T.Oferta_nRenglon  
  
 UPDATE COM_PrecioProductos  
  SET nCantidadVendida = ISNULL(nCantidadVendida ,0) + T.nCantidadVolumen  
 FROM #ActualizacionOfertas AS T  
 WHERE nListaPrecios = T.Oferta_nListaPrecios AND nRenglon = Oferta_nRenglon  
   
 ALTER TABLE #VTA_VentaPisoDetalle ADD bAgrupado BIT NOT NULL DEFAULT 0  
  
 SELECT nProductoMarcaUnidad , nCanalDistribucion ,nCodigoBarra ,nRenglon ,CONVERT(BIT ,0) AS bActualizado  
 INTO #RealacionCodigos  
 FROM #VTA_VentaPisoDetalle AS D(NOLOCK)   
 WHERE D.bAgrupado = 0 AND NOT D.nCodigoBarra IS NULL   
   
 --Armaremos un agrupado de detalle de combos para que a la hora de impresion salga un detalle de facturacion mas pequeño  
 SELECT nAlmacen ,nVenta ,MIN(nRenglon) AS nRenglon ,nProductoMarcaUnidad ,nCanalDistribucion ,nCodigoBarra = NULL,MIN(Combo_nRenglon) AS Combo_nRenglon   
  ,Obsequio_nRenglon = NULL ,nServicioGeneral = NULL ,SUM(nCantidadEnUnidadMovimiento) AS nCantidadEnUnidadMovimiento  
  ,SUM(nPiezas) AS nPiezas ,nFactorConversionAUnidadBase ,SUM(nCantidadEnUnidadBase) AS nCantidadEnUnidadBase   
  ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase   
  ,SUM(nImporteVentaConImpuesto) AS nImporteVentaConImpuesto ,SUM(nImpuestoVenta) AS nImpuestoVenta  
  ,SUM(nImporteEnProrrateos) AS nImporteEnProrrateos ,SUM(nImpuestoEnProrrateos) AS nImpuestoEnProrrateos  
  ,SUM(nImporteEnPagoConTarjeta) AS nImporteEnPagoConTarjeta ,SUM(nImpuestoEnPagoConTarjeta) AS nImpuestoEnPagoConTarjeta  
  ,SUM(nPuntosGanados) AS nPuntosGanados,SUM(nPuntosUtilizados) AS nPuntosUtilizados ,SUM(nImportePuntosUtilizados) AS nImportePuntosUtilizados --Estos no los deberia de sumarizar por que se supone que no genran, ni se puden cambiar por puntos pero bueno 
  
  ,SUM(nImpuesto) AS nImpuesto ,SUM(nCostoPartidaSinImpuesto) AS nCostoPartidaSinImpuesto  
  ,SUM(nCostoPartida) AS nCostoPartida ,SUM(nImpuestoEnMonedaBase) AS nImpuestoEnMonedaBase   
  ,SUM(nCostoPartidaSinImpuestoEnMonedaBase) AS nCostoPartidaSinImpuestoEnMonedaBase ,SUM(nCostoPartidaEnMonedaBase) AS nCostoPartidaEnMonedaBase   
  ,0 AS bCancelado ,0 AS bCancelacion ,1 AS bOfertado   
  ,0 AS bPrecioPadre ,0 AS bEsObsequio  
 INTO #CombosAgrupados  
 FROM #VTA_VentaPisoDetalle AS D(NOLOCK)  
 WHERE (NOT D.Combo_nRenglon IS NULL OR EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE D.nRenglon = A.Combo_nRenglon) )  
 GROUP BY nAlmacen ,nVenta ,nProductoMarcaUnidad ,nCanalDistribucion ,nFactorConversionAUnidadBase--,nCodigoBarra ,nFactorConversionAUnidadBase  
   ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase  
  
 UPDATE #RealacionCodigos  
  SET nRenglon = O.nRenglon  
  ,bActualizado = 1  
 FROM #CombosAgrupados AS O  
 INNER JOIN #VTA_VentaPisoDetalle AS D ON (O.nProductoMarcaUnidad = D.nProductoMarcaUnidad)  
 WHERE (NOT D.Combo_nRenglon IS NULL OR EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE D.nRenglon = A.Combo_nRenglon) )  
  AND #RealacionCodigos.nRenglon = D.nRenglon  
  AND #RealacionCodigos.bActualizado = 0  
  
   
 --SELECT * FROM #VTA_VentaPisoDetalle  
 --SELECT * FROM #CombosAgrupados  
  
 --Actualizamos la tabla de trabajo con los valores agrupados  
 UPDATE #VTA_VentaPisoDetalle  
   SET  Combo_nRenglon = T.Combo_nRenglon  
    ,nCantidadEnUnidadMovimiento = T.nCantidadEnUnidadMovimiento  
    ,nPiezas = T.nPiezas   
    ,nFactorConversionAUnidadBase = T.nFactorConversionAUnidadBase   
    ,nCantidadEnUnidadBase = T.nCantidadEnUnidadBase   
    ,nPrecioNormalEnUnidadBase = T.nPrecioNormalEnUnidadBase   
    ,nPrecioOfertadoEnUnidadBase = T.nPrecioOfertadoEnUnidadBase   
    ,nImporteVentaConImpuesto = T.nImporteVentaConImpuesto  
    ,nImpuestoVenta = T.nImpuestoVenta  
    ,nImporteEnProrrateos = T.nImporteEnProrrateos   
    ,nImpuestoEnProrrateos = T.nImpuestoEnProrrateos   
    ,nImporteEnPagoConTarjeta = T.nImporteEnPagoConTarjeta   
    ,nImpuestoEnPagoConTarjeta = T.nImpuestoEnPagoConTarjeta  
    ,nPuntosGanados = T.nPuntosGanados  
    ,nPuntosUtilizados = T.nPuntosUtilizados  
    ,nImportePuntosUtilizados = T.nImportePuntosUtilizados  
    ,nImpuesto = T.nImpuesto  
    ,nCostoPartidaSinImpuesto = T.nCostoPartidaSinImpuesto  
    ,nCostoPartida = T.nCostoPartida   
    ,nImpuestoEnMonedaBase = T.nImpuestoEnMonedaBase   
    ,nCostoPartidaSinImpuestoEnMonedaBase = T.nCostoPartidaSinImpuestoEnMonedaBase   
    ,nCostoPartidaEnMonedaBase = T.nCostoPartidaEnMonedaBase  
    ,bAgrupado = 1  
 FROM #CombosAgrupados AS T(NOLOCK)  
 WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
  
 --Borramos de nuestro universo los datos que quedaron dentro de una agrupacion  
 DELETE #VTA_VentaPisoDetalle   
 WHERE NOT EXISTS(SELECT 1 FROM #CombosAgrupados AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon)  
 AND (NOT #VTA_VentaPisoDetalle.Combo_nRenglon IS NULL OR EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = A.Combo_nRenglon) )  
      
   
 --Actualizamos el folio de promocion para los articulos que conforman el regalo  
 UPDATE #VTA_VentaPisoDetalle  
  SET nPromocion = ( SELECT nPromocion FROM #VTA_VentaPisoDetalle AS D  
       WHERE #VTA_VentaPisoDetalle.Obsequio_nRenglon = D.nRenglon)  
 WHERE NOT Obsequio_nRenglon IS NULL  
   
  
 --Armaremos un agrupado de detalle de obsequios para que a la hora de impresion salga un detalle de facturacion mas pequeño  
 SELECT nAlmacen ,nVenta ,MIN(nRenglon) AS nRenglon ,nProductoMarcaUnidad ,nCanalDistribucion ,nCodigoBarra = NULL ,Combo_nRenglon  = NULL  
  ,MIN(Obsequio_nRenglon) AS Obsequio_nRenglon ,nServicioGeneral = NULL ,SUM(nCantidadEnUnidadMovimiento) AS nCantidadEnUnidadMovimiento  
  ,SUM(nPiezas) AS nPiezas ,nFactorConversionAUnidadBase ,SUM(nCantidadEnUnidadBase) AS nCantidadEnUnidadBase   
  ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase   
  ,SUM(nImporteVentaConImpuesto) AS nImporteVentaConImpuesto ,SUM(nImpuestoVenta) AS nImpuestoVenta  
  ,SUM(nImporteEnProrrateos) AS nImporteEnProrrateos ,SUM(nImpuestoEnProrrateos) AS nImpuestoEnProrrateos  
  ,SUM(nImporteEnPagoConTarjeta) AS nImporteEnPagoConTarjeta ,SUM(nImpuestoEnPagoConTarjeta) AS nImpuestoEnPagoConTarjeta  
  ,SUM(nPuntosGanados) AS nPuntosGanados,SUM(nPuntosUtilizados) AS nPuntosUtilizados ,SUM(nImportePuntosUtilizados) AS nImportePuntosUtilizados --Estos no los deberia de sumarizar por que se supone que no genran, ni se puden cambiar por puntos pero bueno
  
  ,SUM(nImpuesto) AS nImpuesto ,SUM(nCostoPartidaSinImpuesto) AS nCostoPartidaSinImpuesto  
  ,SUM(nCostoPartida) AS nCostoPartida ,SUM(nImpuestoEnMonedaBase) AS nImpuestoEnMonedaBase   
  ,SUM(nCostoPartidaSinImpuestoEnMonedaBase) AS nCostoPartidaSinImpuestoEnMonedaBase ,SUM(nCostoPartidaEnMonedaBase) AS nCostoPartidaEnMonedaBase   
  ,0 AS bCancelado ,0 AS bCancelacion ,1 AS bOfertado   
  ,0 AS bPrecioPadre ,bEsObsequio ,nPromocion--,0 AS bEsObsequio  
 INTO #ObsequiosAgrupados  
 FROM #VTA_VentaPisoDetalle AS D(NOLOCK)  
 WHERE (NOT D.Obsequio_nRenglon IS NULL OR EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE D.nRenglon = A.Obsequio_nRenglon) )  
 GROUP BY nAlmacen ,nVenta ,nProductoMarcaUnidad ,nCanalDistribucion ,nFactorConversionAUnidadBase--,nCodigoBarra ,nFactorConversionAUnidadBase  
   ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase ,bEsObsequio ,nPromocion  
  
 --SELECT * FROM #RealacionCodigos  
 --SELECT * FROM #ObsequiosAgrupados  
 --SELECT * FROM #VTA_VentaPisoDetalle  
  
 UPDATE #RealacionCodigos  
  SET nRenglon = O.nRenglon  
  ,bActualizado = 1  
 FROM #ObsequiosAgrupados AS O  
 INNER JOIN #VTA_VentaPisoDetalle AS D ON (O.nProductoMarcaUnidad = D.nProductoMarcaUnidad AND O.nPromocion = D.nPromocion  
--   AND (  
--     (D.bEsObsequio = 1 AND O.nPromocion = D.nPromocion)   
--     OR (NOT D.Obsequio_nRenglon IS NULL)  
--    )  
   )   
 WHERE (NOT D.Obsequio_nRenglon IS NULL OR EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE D.nRenglon = A.Obsequio_nRenglon))  
  AND #RealacionCodigos.nRenglon = D.nRenglon  
  AND #RealacionCodigos.bActualizado = 0    
    
 --SELECT * FROM #RealacionCodigos  
 --SELECT * FROM #ObsequiosAgrupados  
 --SELECT * FROM #VTA_VentaPisoDetalle  
  
 --Borramos los obsequios que no quedaron en la agrupacion  
 DELETE #VTA_VentaPisoDetalle   
 WHERE bEsObsequio = 1  
  AND NOT EXISTS (SELECT 1 FROM #ObsequiosAgrupados AS T WHERE T.nRenglon = #VTA_VentaPisoDetalle.nRenglon)  
  
 --Actualizamos la tabla de trabajo con los valores agrupados  
 UPDATE #VTA_VentaPisoDetalle  
   SET  Combo_nRenglon = T.Combo_nRenglon  
    ,Obsequio_nRenglon = T.Obsequio_nRenglon  
    ,nCantidadEnUnidadMovimiento = T.nCantidadEnUnidadMovimiento  
    ,nPiezas = T.nPiezas   
    ,nFactorConversionAUnidadBase = T.nFactorConversionAUnidadBase   
    ,nCantidadEnUnidadBase = T.nCantidadEnUnidadBase   
    ,nPrecioNormalEnUnidadBase = T.nPrecioNormalEnUnidadBase   
    ,nPrecioOfertadoEnUnidadBase = T.nPrecioOfertadoEnUnidadBase   
    ,nImporteVentaConImpuesto = T.nImporteVentaConImpuesto  
    ,nImpuestoVenta = T.nImpuestoVenta  
    ,nImporteEnProrrateos = T.nImporteEnProrrateos   
    ,nImpuestoEnProrrateos = T.nImpuestoEnProrrateos   
    ,nImporteEnPagoConTarjeta = T.nImporteEnPagoConTarjeta   
    ,nImpuestoEnPagoConTarjeta = T.nImpuestoEnPagoConTarjeta  
    ,nPuntosGanados = T.nPuntosGanados  
    ,nPuntosUtilizados = T.nPuntosUtilizados  
    ,nImportePuntosUtilizados = T.nImportePuntosUtilizados  
    ,nImpuesto = T.nImpuesto  
    ,nCostoPartidaSinImpuesto = T.nCostoPartidaSinImpuesto  
    ,nCostoPartida = T.nCostoPartida   
    ,nImpuestoEnMonedaBase = T.nImpuestoEnMonedaBase   
    ,nCostoPartidaSinImpuestoEnMonedaBase = T.nCostoPartidaSinImpuestoEnMonedaBase   
    ,nCostoPartidaEnMonedaBase = T.nCostoPartidaEnMonedaBase  
    ,bAgrupado = 1  
 FROM #ObsequiosAgrupados AS T(NOLOCK)  
 WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
  
 --SELECT * FROM #VTA_VentaPisoDetalle  
   
  
 --Borramos de nuestro universo los datos que quedaron dentro de una agrupacion  
 DELETE #VTA_VentaPisoDetalle   
 WHERE NOT EXISTS(SELECT 1 FROM #ObsequiosAgrupados AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon)  
 AND (NOT #VTA_VentaPisoDetalle.Obsequio_nRenglon IS NULL OR EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = A.Obsequio_nRenglon) )  
      
 --SELECT * FROM #VTA_VentaPisoDetalle  
  
 --Armaremos un agrupado de detalle de productos normales  
 SELECT nAlmacen ,nVenta ,MIN(nRenglon) AS nRenglon ,nProductoMarcaUnidad ,nCanalDistribucion = NULL ,nCodigoBarra = NULL,Combo_nRenglon  = NULL  
  ,Obsequio_nRenglon = NULL ,nServicioGeneral ,SUM(nCantidadEnUnidadMovimiento) AS nCantidadEnUnidadMovimiento  
  ,SUM(nPiezas) AS nPiezas ,nFactorConversionAUnidadBase ,SUM(nCantidadEnUnidadBase) AS nCantidadEnUnidadBase   
  ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase   
  ,SUM(nImporteVentaConImpuesto) AS nImporteVentaConImpuesto ,SUM(nImpuestoVenta) AS nImpuestoVenta  
  ,SUM(nImporteEnProrrateos) AS nImporteEnProrrateos ,SUM(nImpuestoEnProrrateos) AS nImpuestoEnProrrateos  
  ,SUM(nImporteEnPagoConTarjeta) AS nImporteEnPagoConTarjeta ,SUM(nImpuestoEnPagoConTarjeta) AS nImpuestoEnPagoConTarjeta  
  ,SUM(nPuntosGanados) AS nPuntosGanados,SUM(nPuntosUtilizados) AS nPuntosUtilizados ,SUM(nImportePuntosUtilizados) AS nImportePuntosUtilizados --Estos no los deberia de sumarizar por que se supone que no genran, ni se puden cambiar por puntos pero bueno 
 
  ,SUM(nImpuesto) AS nImpuesto ,SUM(nCostoPartidaSinImpuesto) AS nCostoPartidaSinImpuesto  
  ,SUM(nCostoPartida) AS nCostoPartida ,SUM(nImpuestoEnMonedaBase) AS nImpuestoEnMonedaBase   
  ,SUM(nCostoPartidaSinImpuestoEnMonedaBase) AS nCostoPartidaSinImpuestoEnMonedaBase ,SUM(nCostoPartidaEnMonedaBase) AS nCostoPartidaEnMonedaBase   
  ,0 AS bCancelado ,0 AS bCancelacion ,bOfertado   
  ,bPrecioPadre ,0 AS bEsObsequio  
 INTO #SumarizadosAgrupados  
 FROM #VTA_VentaPisoDetalle AS D(NOLOCK)  
 WHERE D.bAgrupado = 0 AND D.nCodigoBarra IS NULL  
 GROUP BY nAlmacen ,nVenta ,nProductoMarcaUnidad ,nCanalDistribucion ,nFactorConversionAUnidadBase ,nServicioGeneral  
   ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase ,bOfertado ,bPrecioPadre  
  
 --SELECT * FROM #SumarizadosAgrupados  
 --SELECT * FROM #VTA_VentaPisoDetalle  
  
 UPDATE #VTA_VentaPisoDetalle  
   SET  Combo_nRenglon = T.Combo_nRenglon  
    ,nCantidadEnUnidadMovimiento = T.nCantidadEnUnidadMovimiento  
    ,nPiezas = T.nPiezas   
    ,nFactorConversionAUnidadBase = T.nFactorConversionAUnidadBase   
    ,nCantidadEnUnidadBase = T.nCantidadEnUnidadBase   
    ,nPrecioNormalEnUnidadBase = T.nPrecioNormalEnUnidadBase   
    ,nPrecioOfertadoEnUnidadBase = T.nPrecioOfertadoEnUnidadBase   
    ,nImporteVentaConImpuesto = T.nImporteVentaConImpuesto  
    ,nImpuestoVenta = T.nImpuestoVenta  
    ,nImporteEnProrrateos = T.nImporteEnProrrateos   
    ,nImpuestoEnProrrateos = T.nImpuestoEnProrrateos   
    ,nImporteEnPagoConTarjeta = T.nImporteEnPagoConTarjeta   
    ,nImpuestoEnPagoConTarjeta = T.nImpuestoEnPagoConTarjeta  
    ,nPuntosGanados = T.nPuntosGanados  
    ,nPuntosUtilizados = T.nPuntosUtilizados  
    ,nImportePuntosUtilizados = T.nImportePuntosUtilizados  
    ,nImpuesto = T.nImpuesto  
    ,nCostoPartidaSinImpuesto = T.nCostoPartidaSinImpuesto  
    ,nCostoPartida = T.nCostoPartida   
    ,nImpuestoEnMonedaBase = T.nImpuestoEnMonedaBase   
    ,nCostoPartidaSinImpuestoEnMonedaBase = T.nCostoPartidaSinImpuestoEnMonedaBase   
    ,nCostoPartidaEnMonedaBase = T.nCostoPartidaEnMonedaBase  
    ,bAgrupado = 1  
 FROM #SumarizadosAgrupados AS T(NOLOCK)  
 WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
  
  
 --Borramos de nuestro universo los datos que quedaron dentro de una agrupacion  
 DELETE #VTA_VentaPisoDetalle   
 WHERE NOT EXISTS(SELECT 1 FROM #SumarizadosAgrupados AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon)  
 AND bAgrupado = 0 AND nCodigoBarra IS NULL  
  
  
   
  
 --SELECT * FROM #RealacionCodigos  
  
 --Armaremos el detalle agrupado de codigos de barra   
 SELECT nAlmacen ,nVenta ,MIN(nRenglon) AS nRenglon ,nProductoMarcaUnidad ,nCanalDistribucion = NULL ,nCodigoBarra = NULL,Combo_nRenglon  = NULL  
  ,Obsequio_nRenglon = NULL ,nServicioGeneral ,SUM(nCantidadEnUnidadMovimiento) AS nCantidadEnUnidadMovimiento  
  ,SUM(nPiezas) AS nPiezas ,nFactorConversionAUnidadBase ,SUM(nCantidadEnUnidadBase) AS nCantidadEnUnidadBase   
  ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase   
  ,SUM(nImporteVentaConImpuesto) AS nImporteVentaConImpuesto ,SUM(nImpuestoVenta) AS nImpuestoVenta  
  ,SUM(nImporteEnProrrateos) AS nImporteEnProrrateos ,SUM(nImpuestoEnProrrateos) AS nImpuestoEnProrrateos  
  ,SUM(nImporteEnPagoConTarjeta) AS nImporteEnPagoConTarjeta ,SUM(nImpuestoEnPagoConTarjeta) AS nImpuestoEnPagoConTarjeta  
  ,SUM(nPuntosGanados) AS nPuntosGanados,SUM(nPuntosUtilizados) AS nPuntosUtilizados ,SUM(nImportePuntosUtilizados) AS nImportePuntosUtilizados --Estos no los deberia de sumarizar por que se supone que no genran, ni se puden cambiar por puntos pero bueno 
  
  ,SUM(nImpuesto) AS nImpuesto ,SUM(nCostoPartidaSinImpuesto) AS nCostoPartidaSinImpuesto  
  ,SUM(nCostoPartida) AS nCostoPartida ,SUM(nImpuestoEnMonedaBase) AS nImpuestoEnMonedaBase   
  ,SUM(nCostoPartidaSinImpuestoEnMonedaBase) AS nCostoPartidaSinImpuestoEnMonedaBase ,SUM(nCostoPartidaEnMonedaBase) AS nCostoPartidaEnMonedaBase   
  ,0 AS bCancelado ,0 AS bCancelacion ,bOfertado   
  ,bPrecioPadre ,0 AS bEsObsequio  
 INTO #DetalladosAgrupados  
 FROM #VTA_VentaPisoDetalle AS D(NOLOCK)  
 WHERE D.bAgrupado = 0 AND NOT D.nCodigoBarra IS NULL  
 GROUP BY nAlmacen ,nVenta ,nProductoMarcaUnidad ,nCanalDistribucion ,nFactorConversionAUnidadBase ,nServicioGeneral  
   ,nPrecioNormalEnUnidadBase ,nPrecioOfertadoEnUnidadBase ,bOfertado ,bPrecioPadre  
  
 UPDATE #RealacionCodigos  
  SET nRenglon = O.nRenglon  
  ,bActualizado = 1  
 FROM #DetalladosAgrupados AS O  
 INNER JOIN #VTA_VentaPisoDetalle AS D ON (O.nProductoMarcaUnidad = D.nProductoMarcaUnidad)  
 WHERE #RealacionCodigos.nRenglon = D.nRenglon  
  AND #RealacionCodigos.bActualizado = 0  
  
 --SELECT * FROM #ObsequiosAgrupados  
 --SELECT * FROM #RealacionCodigos  
   
 --SELECT * FROM #VTA_VentaPisoDetalle  
 --SELECT *FROM #DetalladosAgrupados  
  
 UPDATE #VTA_VentaPisoDetalle  
   SET  Combo_nRenglon = T.Combo_nRenglon  
    ,nCantidadEnUnidadMovimiento = T.nCantidadEnUnidadMovimiento  
    ,nPiezas = T.nPiezas   
    ,nFactorConversionAUnidadBase = T.nFactorConversionAUnidadBase   
    ,nCantidadEnUnidadBase = T.nCantidadEnUnidadBase   
    ,nPrecioNormalEnUnidadBase = T.nPrecioNormalEnUnidadBase   
    ,nPrecioOfertadoEnUnidadBase = T.nPrecioOfertadoEnUnidadBase   
    ,nImporteVentaConImpuesto = T.nImporteVentaConImpuesto  
    ,nImpuestoVenta = T.nImpuestoVenta  
    ,nImporteEnProrrateos = T.nImporteEnProrrateos   
    ,nImpuestoEnProrrateos = T.nImpuestoEnProrrateos   
    ,nImporteEnPagoConTarjeta = T.nImporteEnPagoConTarjeta   
    ,nImpuestoEnPagoConTarjeta = T.nImpuestoEnPagoConTarjeta  
    ,nPuntosGanados = T.nPuntosGanados  
    ,nPuntosUtilizados = T.nPuntosUtilizados  
    ,nImportePuntosUtilizados = T.nImportePuntosUtilizados  
    ,nImpuesto = T.nImpuesto  
    ,nCostoPartidaSinImpuesto = T.nCostoPartidaSinImpuesto  
    ,nCostoPartida = T.nCostoPartida   
    ,nImpuestoEnMonedaBase = T.nImpuestoEnMonedaBase   
    ,nCostoPartidaSinImpuestoEnMonedaBase = T.nCostoPartidaSinImpuestoEnMonedaBase   
    ,nCostoPartidaEnMonedaBase = T.nCostoPartidaEnMonedaBase  
    ,bAgrupado = 1  
 FROM #DetalladosAgrupados AS T(NOLOCK)  
 WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
  
  
 --Borramos de nuestro universo los datos que quedaron dentro de una agrupacion  
 DELETE #VTA_VentaPisoDetalle   
 WHERE bAgrupado = 0   
  
 --Grabamos con importe de venta 0 aquellos productos que van de regalo o forman parte de un combo  
 UPDATE #VTA_VentaPisoDetalle  
  SET  nImporteVentaConImpuesto = 0  
   ,nImpuestoVenta = 0  
 WHERE bEsObsequio = 1  
  OR NOT Combo_nRenglon IS NULL  
     
 --SELECT * FROM #VTA_VentaPisoDetalle  
   
 --Agregamos un campo al detalle que nos dira el numero de pedimentos relacionados con el detalle de la venta  
 ALTER TABLE #VTA_VentaPisoDetalle ADD nPedimentosAImprimir TINYINT NOT NULL DEFAULT 0  
 ALTER TABLE #VTA_VentaPisoDetalle ADD bEsParteDeCombo BIT NOT NULL DEFAULT 0  
 ALTER TABLE #VTA_VentaPisoDetalle ADD nNuevoRenglon SMALLINT NULL  
 ALTER TABLE #VTA_VentaPisoDetalle ADD nLineasQueComponen SMALLINT NOT NULL DEFAULT 0  
 ALTER TABLE #VTA_VentaPisoDetalle ADD bEsParteDeObsequio BIT NOT NULL DEFAULT 0  
 ALTER TABLE #VTA_VentaPisoDetalle ADD nPrioridad INT NOT NULL DEFAULT 6   
   
 --Actualizamos los detalles que son o forman parte de un combo  
 UPDATE #VTA_VentaPisoDetalle SET bEsParteDeCombo = 1 ,nPrioridad = 2 WHERE NOT Combo_nRenglon IS NULL  
 UPDATE #VTA_VentaPisoDetalle SET bEsParteDeCombo = 1 ,nPrioridad = 1 WHERE EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = A.Combo_nRenglon)  
 UPDATE #VTA_VentaPisoDetalle SET nLineasQueComponen = (SELECT COUNT(T.Combo_nRenglon) FROM #VTA_VentaPisoDetalle AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.Combo_nRenglon) WHERE bEsParteDeCombo = 1  
 UPDATE #VTA_VentaPisoDetalle SET nLineasQueComponen = (SELECT COUNT(T.Obsequio_nRenglon) FROM #VTA_VentaPisoDetalle AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.Obsequio_nRenglon) WHERE bEsParteDeCombo = 0  
  
 UPDATE #VTA_VentaPisoDetalle SET bEsParteDeObsequio = 1 ,nPrioridad = 4 WHERE NOT Obsequio_nRenglon IS NULL  
 UPDATE #VTA_VentaPisoDetalle SET bEsParteDeObsequio = 1 ,nPrioridad = 3 WHERE EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle AS A(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = A.Obsequio_nRenglon)  
 UPDATE #VTA_VentaPisoDetalle SET nPrioridad = 5 WHERE NOT nServicioGeneral IS NULL  
  
 --Con el ordenamiento que aplicamos dejamos en los primeros registros el articulo combo, en segundo lugar el detalle del combo y en tercero los obsequios y en cuarto el resto  
 --SELECT nRenglon FROM #VTA_VentaPisoDetalle ORDER BY nPrioridad ASC  
 --SELECT nRenglon ,IDENTITY(INT ,1 ,1) AS nNuevoRenglon INTO #NuevoOrden FROM #VTA_VentaPisoDetalle ORDER BY bEsParteDeCombo DESC ,bEsParteDeObsequio DESC, ISNULL(Combo_nRenglon ,nRenglon) ,Combo_nRenglon ,ISNULL(Obsequio_nRenglon ,nRenglon) ,Obsequio_nRenglon   
 SELECT nRenglon ,IDENTITY(INT ,1 ,1) AS nNuevoRenglon INTO #NuevoOrden FROM #VTA_VentaPisoDetalle ORDER BY nPrioridad ASC  
 UPDATE #VTA_VentaPisoDetalle SET nNuevoRenglon = T.nNuevoRenglon FROM #NuevoOrden AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
 DROP TABLE #NuevoOrden  
  
 SELECT @nMaxRenglones = MAX(nNuevoRenglon) FROM #VTA_VentaPisoDetalle  
  
 --Pedimentos desglosados  
 SELECT VD.nRenglon ,ISNULL( SUM(DISTINCT DP.Pedimento_nPedimento) ,0) AS nPedimentos   
 INTO #PedimentosDesglosados  
 FROM #RealacionCodigos AS D(NOLOCK)  
 INNER JOIN #VTA_VentaPisoDetalle AS VD(NOLOCK) ON (D.nProductoMarcaUnidad = VD.nProductoMarcaUnidad)  
 INNER JOIN CTL_DatosProduccion AS DP(NOLOCK) ON (D.nCanalDistribucion = DP.nCanalDistribucion AND D.nCodigoBarra = DP.nCodigoBarra)  
 GROUP BY VD.nRenglon  
  
   
  
 --Actualizamos el detalle de desglosados  
 UPDATE #VTA_VentaPisoDetalle SET nPedimentosAImprimir = CASE WHEN T.nPedimentos > 2 THEN 2 ELSE T.nPedimentos END FROM #PedimentosDesglosados AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
 DROP TABLE #PedimentosDesglosados  
  
 --Obtenemos los pedimentos sumarizados  
 SELECT D.nRenglon ,CASE WHEN  COUNT(P.nPedimento) > 2 THEN 2 ELSE COUNT(P.nPedimento) END AS nPedimentos  
 INTO #PedimentosSumarizados  
 FROM #VTA_VentaPisoDetalle AS D(NOLOCK)  
 INNER JOIN CTL_ProductosMarcasUnidades AS PMU(NOLOCK) ON (D.nProductoMarcaUnidad = PMU.nProductoMarcaUnidad AND D.nCodigoBarra IS NULL)  
 INNER JOIN INV_ProductosSumarizadosPedimentos AS PS(NOLOCK) ON (PMU.nProducto = PS.nProducto AND PMU.nMarca = PS.nMarca)  
 INNER JOIN INV_Pedimentos AS P(NOLOCK) ON (PS.Pedimento_nCanalDistribucion = P.nCanalDistribucion AND PS.Pedimento_nPedimento = P.nPedimento AND P.bActivo = 1)  
 GROUP BY D.nRenglon  
   
  
 UPDATE #VTA_VentaPisoDetalle SET nPedimentosAImprimir = T.nPedimentos FROM #PedimentosSumarizados AS T(NOLOCK) WHERE #VTA_VentaPisoDetalle.nRenglon = T.nRenglon  
 DROP TABLE #PedimentosSumarizados  
  
 --SELECT * FROM #VTA_VentaPisoDetalle  
  
 CREATE TABLE #ConfiguracionFacturas (nNuevoRenglon SMALLINT PRIMARY KEY ,nFactura TINYINT)  
  
 SELECT @nRenglonesTotales = SUM(1 + nLineasQueComponen + CASE WHEN bEsParteDeCombo = 0 THEN nPedimentosAImprimir ELSE 0 END ) FROM #VTA_VentaPisoDetalle WHERE Combo_nRenglon IS NULL AND Obsequio_nRenglon IS NULL  
  
 DECLARE @bAplicacionLineasNormal  BIT  
 SET @bAplicacionLineasNormal = 1  
  
 --SELECT 'FIN Agrupacion y Preparacion de registros'  
  
 --L.I. ALDO MANUEL SANZ CASTRO 04/FEBRERO/2009  
 ALTER TABLE #RealacionCodigos ADD nNuevoRenglon SMALLINT  
   
 --SELECT * FROM #VTA_VentaPisoDetalle ORDER BY nNuevoRenglon  
 --Aqui empezaremos a recorrer el detalle de la venta y decidiremos en que factura quedara cada renglon  
 SELECT @nRenglonActual = 1 ,@nNumeroFactura = 1 ,@nRenglonesUtilizados = 0 ,@nRenglonesAUtilizar = 0 ,@nRenglonesXFacturaNetos = 0 ,@nRenglonesTotalesUtilizados = 0  
 WHILE @nRenglonActual <= @nMaxRenglones  
 BEGIN  
  --Considerar los porductos de regalo. (marcarlos) estos no se tomaran en cuenta al determinar las lineas de facturacion   
  --SELECT Combo_nRenglon ,Obsequio_nRenglon ,@nRenglonActual FROM #VTA_VentaPisoDetalle WHERE Combo_nRenglon IS NULL AND Obsequio_nRenglon IS NULL AND nNuevoRenglon = @nRenglonActual  
  IF EXISTS(SELECT 1 FROM #VTA_VentaPisoDetalle WHERE Combo_nRenglon IS NULL AND Obsequio_nRenglon IS NULL AND nNuevoRenglon = @nRenglonActual)  
  BEGIN  
   SELECT @nRenglonesAUtilizar = 1 + nLineasQueComponen + CASE WHEN bEsParteDeCombo = 0 THEN nPedimentosAImprimir ELSE 0 END FROM #VTA_VentaPisoDetalle WHERE nNuevoRenglon = @nRenglonActual  
   IF @bAplicacionLineasNormal = 1  
    SELECT @nRenglonesXFacturaNetos = @nLineasXDetalle  
   --SELECT Faltantes = @nRenglonesTotales - @nRenglonesUtilizados ,TotalConEdoCuenta = @nLineasXDetalle + @nLineasEdoCuenta  
   IF @bEdoCuenta = 1 AND @bAplicacionLineasNormal = 1  
   BEGIN  
    --Validamos que en caso del detalle faltante a imprimir, se pueda imprimir en el espacio del edo cuenta, se lo asignamos  
    IF (@nRenglonesTotales - @nRenglonesUtilizados) > (@nLineasXDetalle + @nLineasEdoCuenta)      
     SELECT @nRenglonesXFacturaNetos = @nRenglonesXFacturaNetos + @nLineasEdoCuenta ,@bAplicacionLineasNormal = 0       
      
    ELSE  
    BEGIN  
     --Validamos que si tenemos mas lineas que imprimir y podemos utilizar los renglones del edo cuenta, los utilizamos, dejando un renglon de detalle a imprimir en la factura  
     IF (@nRenglonesTotales - @nRenglonesUtilizados) > @nLineasXDetalle  
      SELECT @nRenglonesXFacturaNetos = @nRenglonesXFacturaNetos + ((@nRenglonesTotales - @nRenglonesUtilizados) - @nLineasXDetalle) - 1,@bAplicacionLineasNormal = 0   
    END  
   END  
     
   --SELECT @nLineasXDetalle AS LienasDetalle ,@nRenglonesXFacturaNetos AS LineasNetas ,@nRenglonesAUtilizar AS Utilizar ,@nRenglonesUtilizados AS Utilizados ,@nRenglonesTotales AS Totales     
   IF (@nRenglonesAUtilizar + @nRenglonesUtilizados) > @nRenglonesXFacturaNetos-- AND @nRenglonesTotales > @nRenglonesXFacturaNetos  
   BEGIN  
    SELECT @nNumeroFactura = @nNumeroFactura + 1 ,@bAplicacionLineasNormal = 1  
    SELECT @nRenglonesTotalesUtilizados = @nRenglonesTotalesUtilizados + @nRenglonesUtilizados  
    SELECT @nRenglonesUtilizados = 0  
   END  
  
   SELECT @nRenglonesUtilizados = @nRenglonesUtilizados + @nRenglonesAUtilizar  
  END  
   
  INSERT #ConfiguracionFacturas  
  SELECT @nRenglonActual ,@nNumeroFactura  
     
  SELECT @nRenglonActual = @nRenglonActual + 1  
 END  
  
 --SELECT 'FIN Agrupacion y Preparacion de registros'    
  
 --SELECT * FROM #ConfiguracionFacturas  
   
 CREATE TABLE #Resultado (nCanalDistribucion SMALLINT ,nFacturaTicket INT)  
   
 SELECT @nTotalFacturas = MAX(nFactura) ,@nNumeroFactura = 1 FROM #ConfiguracionFacturas  
  
 WHILE @nNumeroFactura <= @nTotalFacturas  
 BEGIN  
  
  --Obtenemos el universo filtrado del detalle de la venta por factura  
  SELECT D.* INTO #VTA_VentaPisoDetalleUniverso FROM #VTA_VentaPisoDetalle AS D(NOLOCK) INNER JOIN #ConfiguracionFacturas AS CF(NOLOCK) ON (D.nNuevoRenglon = CF.nNuevoRenglon AND CF.nFactura = @nNumeroFactura) ORDER BY D.nNuevoRenglon  
  
  --Obtencion de Folios  
  UPDATE ADSUM_FoliosAdministrados SET nConsecutivo = nConsecutivo + 1 WHERE cFolioAdministrado = @cFolioFacturaTicket  
  UPDATE ADSUM_FoliosAdministrados SET nConsecutivo = nConsecutivo + 1 WHERE cFolioAdministrado = @cFolioTipoFacturaTicket  
  UPDATE ADSUM_FoliosAdministrados SET nConsecutivo = nConsecutivo + 1 WHERE cFolioAdministrado = @cFolioMovimiento  
    
    
  SELECT  @nFacturaTicket = dbo.ADSUM_FolioAdministrado_Actual( @cFolioFacturaTicket )   
    ,@nFolioTipo = dbo.ADSUM_FolioAdministrado_Actual( @cFolioTipoFacturaTicket )  
    ,@nMovimientoInventario = dbo.ADSUM_FolioAdministrado_Actual( @cFolioMovimiento )  
      
  
  INSERT #Resultado  
  SELECT @nCanalDistribucion ,@nFacturaTicket  
    
  --SELECT @nPuntosXTicketGanados = @nPuntosXTicketGanados - SUM(nPuntosGanados) FROM #VTA_VentaPisoDetalle(NOLOCK)  
  
    
  --Encabezado de movimiento de inventario  
  INSERT INV_MovimientoInventario( nAlmacen ,nMovimientoInventario ,nTipoMovimiento ,cFolioMovimiento ,dFecha ,nMoneda ,nTipoCambio   
          ,Recepcion_nCanalDistribucion ,Recepcion_nRecepcion ,bAplicado ,bEnviadoCorporativo ,bActivo   
          ,cUsuario_Registro ,dFecha_Registro ,cMaquina_Registro ,cUsuario_UltimaModificacion ,dFecha_UltimaModificacion   
          ,cMaquina_UltimaModificacion ,cUsuario_Eliminacion ,dFecha_Eliminacion ,cMaquina_Eliminacion)  
  SELECT  nAlmacen ,@nMovimientoInventario ,@nTipoMovimientoInventario ,
  ISNULL(@cSerie ,'0') + '-' + CONVERT(VARCHAR(20) ,@nFolioTipo)   
    ,dFecha ,nMoneda ,nTipoCambio ,NULL ,NULL ,0 ,0 ,1  
    ,cUsuario_Registro ,GETDATE() ,cMaquina_Registro ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL   
  FROM #VTA_VentaPiso(NOLOCK)   
  
  -- Guardar los folios de movimientos de inventario en la tabla #TMP_FoliosMovimientosInventario.
  if OBJECT_ID('tempdb..#TMP_FoliosMovimientosInventario') is not null
	INSERT INTO #TMP_FoliosMovimientosInventario 
		--(cFolioMovimiento)
		(nMovimientoInventario)
	VALUES (
			--ISNULL(@cSerie ,'0') + '-' + CONVERT(VARCHAR(20) ,@nFolioTipo)
			@nMovimientoInventario
		)
  
  
  
  --Creamos nuestro detalle de inventario  
  SELECT  VD.nAlmacen ,@nMovimientoInventario AS nMovimientoInventario ,IDENTITY(SMALLINT ,1 ,1) AS nRenglon ,VD.nProductoMarcaUnidad  
    ,SUM(VD.nPiezas) AS nPiezas   
    --En el caso de ser un producto sumarizado piezas peso no estandard las salidas se hacen a peso promedio de inventario  
    ,SUM(CASE WHEN P.nTipoInventario = @nTipoInventarioSumarizado AND P.nTipoManejoInventario = @nTipoManejoPiezasPeso AND P.bEsPesoEstandard = 0 THEN ( (EA.nExistencia / EA.nPiezas) * VD.nPiezas ) ELSE VD.nCantidadEnUnidadMovimiento END) AS nCantidadEnUnidadMovimiento  
    --el peso promedio que me da lo divido entre el factor por si el producto viniera en una unidad diferente a la de operacion  
    --,SUM(CASE WHEN P.nTipoInventario = @nTipoInventarioSumarizado AND P.nTipoManejoInventario = @nTipoManejoPiezasPeso AND P.bEsPesoEstandard = 1 THEN ( ((EA.nExistencia / EA.nPiezas) * VD.nPiezas) / PMU.nEquivalencia ) ELSE VD.nCantidadEnUnidadMovimiento END) AS nCantidadEnUnidadMovimiento  
    ,SUM(VD.nCantidadEnUnidadMovimiento) AS nCantidadFacturaUnidadMovimiento ,VD.nFactorConversionAUnidadBase  
    ,SUM(CASE WHEN P.nTipoInventario = @nTipoInventarioSumarizado AND P.nTipoManejoInventario = @nTipoManejoPiezasPeso AND P.bEsPesoEstandard = 0 THEN ( (EA.nExistencia / EA.nPiezas) * VD.nPiezas ) ELSE VD.nCantidadEnUnidadMovimiento END * VD.nFactorConversionAUnidadBase) AS nCantidadEnUnidadBase  
    ,SUM(VD.nCantidadEnUnidadMovimiento * VD.nFactorConversionAUnidadBase) AS nCantidadFacturadaEnUnidadBase   
    ,0 AS nPrecioNegociado --los precios negociados de y facturados no los proporciono por que los precios que manejo son en unidad base  
    ,SUM(nCostoPartidaSinImpuesto) / SUM(nCantidadEnUnidadBase) AS nPrecioFactura --Agregare el precio de facturacion para que se pueda calcular el importe del movimiento  
    ,CONVERT(NUMERIC(12 ,2) ,NULL) AS nCostoSinImpuestoDelMovimientoMonedaMovimiento  
    ,CONVERT(NUMERIC(12 ,2) ,NULL) AS nImpuestoMonedaMovimiento  
    ,CONVERT(NUMERIC(12 ,2) ,NULL) AS nCostoDelMovimientoMonedaMovimiento  
    ,CONVERT(NUMERIC(12 ,2) ,NULL) AS nCostoSinImpuestoDelMovimientoMonedaBase  
    ,CONVERT(NUMERIC(12 ,2) ,NULL) AS nImpuestoMonedaBase  
    ,CONVERT(NUMERIC(12 ,2) ,NULL) AS nCostoDelMovimientoMonedaBase  
    ,CONVERT(SMALLINT ,NULL) AS nCausaDevolucion  
    --Calculamos el costo en unidad base, considerando que todas las salidas son a unidad y moneda base  
    ,(EA.nCostoTotal / EA.nExistencia) AS nCostoUnitario  
  INTO #INV_MovimientoInventarioDetalle  
  FROM #VTA_VentaPisoDetalleUniverso AS VD(NOLOCK)  
  INNER JOIN CTL_ProductosMarcasUnidades AS PMU(NOLOCK) ON (VD.nProductoMarcaUnidad = PMU.nProductoMarcaUnidad)  
  INNER JOIN CTL_Productos AS P(NOLOCK) ON (PMU.nProducto = P.nProducto)  
  INNER JOIN INV_ExistenciasAlmacen AS EA(NOLOCK) ON (VD.nAlmacen = EA.nAlmacen AND PMU.nProducto = EA.nProducto AND PMU.nMarca = EA.nMarca)  
  GROUP BY VD.nAlmacen ,VD.nProductoMarcaUnidad ,VD.nFactorConversionAUnidadBase ,EA.nCostoTotal ,EA.nExistencia --,VD.nPrecioFacturado  
  
  UPDATE #INV_MovimientoInventarioDetalle  
   SET nCostoSinImpuestoDelMovimientoMonedaMovimiento = nCantidadEnUnidadBase * nCostoUnitario  
   ,nImpuestoMonedaMovimiento = 0  
   ,nCostoDelMovimientoMonedaMovimiento = nCantidadEnUnidadBase * nCostoUnitario  
   ,nCostoSinImpuestoDelMovimientoMonedaBase = nCantidadEnUnidadBase * nCostoUnitario  
   ,nImpuestoMonedaBase = 0  
   ,nCostoDelMovimientoMonedaBase = nCantidadEnUnidadBase * nCostoUnitario  
  
  ALTER TABLE #INV_MovimientoInventarioDetalle DROP COLUMN nCostoUnitario  
  
  
  --Recalculamos el costo del movimiento de inventario con los datos de produccion  
  SELECT MID.nAlmacen ,MID.nMovimientoInventario ,MID.nRenglon ,SUM(nCosto) AS nCosto  
  INTO #CostosCodigos  
  FROM #RealacionCodigos AS VD(NOLOCK)  
  INNER JOIN #INV_MovimientoInventarioDetalle AS MID(NOLOCK) ON (VD.nProductoMarcaUnidad = MID.nProductoMarcaUnidad)  
  INNER JOIN CTL_DatosProduccion AS DP(NOLOCK) ON (VD.nCanalDistribucion = DP.nCanalDistribucion AND VD.nCodigoBarra = DP.nCodigoBarra)  
  GROUP BY MID.nAlmacen ,MID.nMovimientoInventario ,MID.nRenglon  
  
  UPDATE #INV_MovimientoInventarioDetalle  
   SET  nCostoSinImpuestoDelMovimientoMonedaMovimiento = T.nCosto  
    ,nCostoDelMovimientoMonedaMovimiento = T.nCosto  
    ,nCostoSinImpuestoDelMovimientoMonedaBase= T.nCosto  
    ,nCostoDelMovimientoMonedaBase = T.nCosto  
  FROM #CostosCodigos AS T  
  WHERE #INV_MovimientoInventarioDetalle.nAlmacen = T.nAlmacen  
  AND #INV_MovimientoInventarioDetalle.nMovimientoInventario = T.nMovimientoInventario  
  AND #INV_MovimientoInventarioDetalle.nRenglon = T.nRenglon  
  
  DROP TABLE #CostosCodigos  
  
  --Detalle de Inventario  
  INSERT INV_MovimientoInventarioDetalle(  nAlmacen ,nMovimientoInventario ,nRenglon ,nProductoMarcaUnidad   
            ,nPiezas ,nCantidadEnUnidadMovimiento ,nCantidadFacturaUnidadMovimiento ,nFactorConversionAUnidadBase   
            ,nCantidadEnUnidadBase ,nCantidadFacturadaEnUnidadBase ,nPrecioNegociado ,nPrecioFactura   
            ,nCostoSinImpuestoDelMovimientoMonedaMovimiento ,nImpuestoMonedaMovimiento   
            ,nCostoDelMovimientoMonedaMovimiento ,nCostoSinImpuestoDelMovimientoMonedaBase   
            ,nImpuestoMonedaBase ,nCostoDelMovimientoMonedaBase ,nCausaDevolucion)  
  SELECT * FROM #INV_MovimientoInventarioDetalle(NOLOCK)  
   
  
  SELECT  MID.nAlmacen ,MID.nMovimientoInventario ,MID.nRenglon ,VD.nCanalDistribucion ,VD.nCodigoBarra   
    ,0 AS bCapturadoWorkAbout ,'' AS cReferenciaWorkAbout  
  INTO #CodigosBarraInventario  
  FROM #INV_MovimientoInventarioDetalle AS MID(NOLOCK)  
  INNER JOIN #RealacionCodigos AS VD(NOLOCK) ON (VD.nProductoMarcaUnidad = MID.nProductoMarcaUnidad)  
  
  --SELECT * FROM #RealacionCodigos  
  --SELECT * FROM #CodigosBarraInventario  
      
  
  --Codigos de barra  
  INSERT INV_MovimientoInventarioCodigosBarra(nAlmacen ,nMovimientoInventario ,nRenglon ,Codigo_nCanalDistribucion ,Codigo_nCodigoBarra   
             ,bCapturadoWorkAbout ,cReferenciaWorkAbout)  
  SELECT * FROM #CodigosBarraInventario  
    
  
  
  --Aplicamos el movimiento de inventario  
  SET @cSQLText = 'SELECT ' + CONVERT(VARCHAR(10) ,@nAlmacen) + ' ,' + CONVERT(VARCHAR(10) ,@nMovimientoInventario)  
  EXEC INV_AplicaDesaplica_MovimientoInventario_Masivo @cSQLText ,1  
  IF @@ERROR <> 0  
  BEGIN  
   ROLLBACK TRAN  
   RAISERROR('VTA_VentaPiso_GeneraTicketFacturasMovimientosInventario: Error al aplicar el movimiento de inventario' ,16 ,1)  
   RETURN  
  END  
  
  --Creamos el encabezado de la factura  
  --Grabar la venta la cual esta generando las facturas  
  INSERT VTA_FacturasTickets(  nCanalDistribucion ,nFacturaTicket ,nTipoFactura ,nAperturaCaja ,cSerie ,nFolioTipo ,nCliente ,nSucursalCliente   
         ,nClienteDistinguido ,nCanalDistribucionVenta ,DatosFacturacion_nCanalDistribucion ,DatosFacturacion_nDatoFacturacion ,dFecha   
         ,dFechaVencimiento ,nSubTotal ,nDescuentos ,nImpuesto ,nImporte ,nMoneda ,nTipoCambio ,nImpuesto_Base   
         ,nImporte_Base ,nTipoSurtido ,nVendedor ,Puntos_nGanados ,Puntos_nUtilizados ,Puntos_nSaldoAlMomentoDeCompra   
         ,FacturaFinDia_nCanalDistribucion ,FacturaFinDia_nFacturaTicket ,Inventario_nAlmacen ,Inventario_nMovimientoInventario   
         ,bAplicado ,bEnviadoCorporativo ,bActivo ,cUsuario_Registro ,dFecha_Registro ,cMaquina_Registro   
         ,cUsuario_UltimaModificacion ,dFecha_UltimaModificacion ,cMaquina_UltimaModificacion ,cUsuario_Eliminacion   
         ,dFecha_Eliminacion ,cMaquina_Eliminacion ,VentaPiso_nAlmacen ,VentaPiso_nVenta, cRFC, nVentaPubGral,nprospecto
         ,bFacturaNoFiscal) --Cambio CFD  
  
  SELECT  @nCanalDistribucion ,@nFacturaTicket ,@nTipoDocumento ,V.nAperturaCaja ,@cSerie ,@nFolioTipo ,V.nCliente ,V.nSucursalCliente  
    ,V.nClienteDistinguido ,@nCanalDistribucion ,V.DatosFacturacion_nCanalDistribucion  ,V.DatosFacturacion_nDatoFacturacion ,V.dFecha  
    ,V.dFecha ,SUM(VD.nCostoPartidaSinImpuestoEnMonedaBase) AS nSubTotal  
    --Aqui redondeamos el importe calculado con el precio normal, ya que esto es lo que hacemos en la pantalla, esto con el fin de que cuadre al 100  
    ,SUM(ROUND( VD.nPrecioNormalEnUnidadBase * VD.nCantidadEnUnidadBase ,2 )) - SUM(ROUND( VD.nPrecioOfertadoEnUnidadBase * VD.nCantidadEnUnidadBase ,2 )) AS nDescuentos  
    ,SUM(VD.nImpuesto) AS nImpuesto ,SUM(VD.nCostoPartida) AS nImporte  
    ,V.nMoneda ,V.nTipoCambio ,SUM(VD.nImpuestoEnMonedaBase) ,SUM(VD.nCostoPartidaEnMonedaBase) ,@nTipoSurtido ,@nVendedorDefault  
    ,FLOOR(@nPuntosXTicket / @nTotalFacturas) + SUM(VD.nPuntosGanados) AS nPuntosGanados ,SUM(VD.nPuntosUtilizados) AS nPuntosUtilizados  
    ,@nPuntosAlMomentoDeCompra ,NULL ,NULL ,@nAlmacen ,@nMovimientoInventario  
    ,1 /*las facturas naceran desaplicadas, cuando se les genere el cargo cambiaran de estatus*/ ,0 ,1 ,V.cUsuario_Registro ,GETDATE() ,V.cMaquina_Registro  
    ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL  
    ,@nAlmacen ,@nVenta, @Rfc, @VtaPubGral,V.nprospecto
    ,@bFacturaNoFiscal  
  FROM #VTA_VentaPiso AS V(NOLOCK)  
  INNER JOIN #VTA_VentaPisoDetalleUniverso AS VD(NOLOCK) ON (V.nAlmacen = VD.nAlmacen AND V.nVenta = VD.nVenta)  
  WHERE Combo_nRenglon IS NULL  
  GROUP BY V.nAperturaCaja ,V.nCliente ,V.nSucursalCliente ,V.nClienteDistinguido ,V.DatosFacturacion_nCanalDistribucion    
    ,V.DatosFacturacion_nDatoFacturacion ,V.dFecha ,V.nMoneda ,V.nTipoCambio ,V.cUsuario_Registro ,V.cMaquina_Registro,V.nprospecto  
  
  --El Cargo y abonos los generamos desde net, debido a las reglas de cheques  
  
    
  ALTER TABLE #VTA_VentaPisoDetalleUniverso ADD nCantidadFacturada NUMERIC(14,3)  
  ALTER TABLE #VTA_VentaPisoDetalleUniverso ADD nTipoPrecioFacturado TINYINT  
  
 --Actualiza la cantidad facturada para obtener el precio facturado  
 UPDATE #VTA_VentaPisoDetalleUniverso  
 SET #VTA_VentaPisoDetalleUniverso.nCantidadFacturada=CASE WHEN CTU.bEsPeso=1 THEN VTA.nCantidadEnUnidadBase ELSE CASE WHEN (CTP.nUnidad=@nUnidadPiezas AND ISNULL(VTA.nContenidoProducto, 0)>0) THEN VTA.nPiezas*VTA.nContenidoProducto ELSE ISNULL(NULLIF(VTA
.nPiezas, 0), VTA.nCantidadEnUnidadBase) END END  
 FROM #VTA_VentaPisoDetalleUniverso VTA   
  INNER JOIN CTL_TiposPrecios CTP (NOLOCK)  
   ON(VTA.nTipoPrecio=CTP.nTipoPrecio)  
  INNER JOIN CTL_Unidades CTU (NOLOCK)  
   ON(CTU.nUnidad=CTP.nUnidad)  
  
--SELECT 'AMS', nTipoPrecio, nCantidadEnUnidadBase, nContenidoProducto, nPiezas, * FROM #VTA_VentaPisoDetalleUniverso  
--SELECT 'AMS', nCantidadFacturada FROM #VTA_VentaPisoDetalleUniverso  
  
  SELECT @nPuntosXTicketGanados = 0  
  
  --Creamos en una temporal el detalle de la factura  
  --dejamos pendiente la definicion del renglon del combo y si el producto es combo para el siguiente paso  
  --Filtramos por articulos solo pertenecientes a combos  
  SELECT @nCanalDistribucion AS nCanalDistribucion ,@nFacturaTicket AS nFacturaTicket,IDENTITY(SMALLINT ,1 ,1) AS nRenglon   
    ,D.nProductoMarcaUnidad ,D.nServicioGeneral ,Combo_nRenglon   
    ,CONVERT(BIT ,CASE WHEN bEsParteDeCombo = 1 AND Combo_nRenglon IS NULL THEN 1 ELSE 0 END) AS bEsCombo   
    ,D.nCantidadEnUnidadMovimiento ,D.nPiezas ,D.nFactorConversionAUnidadBase ,D.nCantidadEnUnidadBase   
    , D.nPrecioNormalEnUnidadBase, D.nTipoPrecio AS nTipoPrecioNormal   
    , D.nPrecioOfertadoEnUnidadBase, D.nTipoPrecio AS nTipoPrecioOferta  
    ,D.nImporteVentaConImpuesto ,D.nImpuestoVenta ,D.nImporteEnProrrateos ,D.nImpuestoEnProrrateos  
    ,D.nImporteEnPagoConTarjeta ,nImpuestoEnPagoConTarjeta  ,D.nImportePuntosUtilizados      
      
    --,D.nCostoPartida / D.nCantidadFacturada AS nPrecioFacturado  
    ,D.nCostoPartidaSinImpuesto / D.nCantidadFacturada AS nPrecioFacturado  
      
    , nTipoPrecio AS nTipoPrecioFacturado  
    ,D.nPuntosGanados ,D.nPuntosUtilizados ,NULL AS Pedido_nRenglon  
    ,D.nImpuesto ,D.nCostoPartidaSinImpuesto ,D.nCostoPartida   
    ,D.nImporteVentaConImpuesto AS nImporteVentaConImpuestoEnMonedaBase ,D.nImpuestoVenta AS nImpuestoVentaEnMonedaBase  
    ,D.nImporteEnProrrateos AS nImporteAgregadoEnProrrateosEnMonedaBase ,D.nImpuestoEnProrrateos AS nImpuestoEnProrrateosEnMonedaBase  
    ,D.nImporteEnPagoConTarjeta AS nImporteAgregadoEnPagoConTarjetaEnMonedaBase ,nImpuestoEnPagoConTarjeta AS nImpuestoEnPagoConTarjetaEnMonedaBase   
    ,D.nImportePuntosUtilizados AS nImporteRestadoDePuntosUtilizadosEnMonedaBase  
    ,D.nImpuestoEnMonedaBase ,D.nCostoPartidaSinImpuestoEnMonedaBase  
    ,D.nCostoPartidaEnMonedaBase ,0 AS Devolucion_nCantidadEnUnidadMovimiento,0 AS Devolucion_nPiezas,0 AS Devolucion_nCantidadEnUnidadBase  
    ,D.nAlmacen AS VentaPiso_nAlmacen ,D.nVenta  AS VentaPiso_nVenta ,D.nRenglon AS VentaPiso_nRenglon  
    ,D.Obsequio_nRenglon ,CONVERT(BIT ,CASE WHEN bEsParteDeObsequio = 1 AND Obsequio_nRenglon IS NULL THEN 1 ELSE 0 END) AS bEsObsequio  
    ,D.nNuevoRenglon   
    ,D.Servicio_nRenglon  
    ,D.nRenglon AS nRenglonViejo  
    , D.nContenidoProducto  
  INTO #VTA_FacturasTicketsDetalle  
  FROM #VTA_VentaPisoDetalleUniverso AS D(NOLOCK)    
  
  --SELECT * FROM #VTA_FacturasTicketsDetalle  
  --Al insertar el detalle de la factura tenemos que reindexar a los combos y los obsequios ya que se puede generar mas de una factura  
  UPDATE #VTA_FacturasTicketsDetalle SET Combo_nRenglon = (SELECT nRenglon FROM #VTA_FacturasTicketsDetalle AS A WHERE A.VentaPiso_nRenglon = #VTA_FacturasTicketsDetalle.Combo_nRenglon AND A.bEsCombo = 1) WHERE NOT Combo_nRenglon IS NULL  
  UPDATE #VTA_FacturasTicketsDetalle SET Obsequio_nRenglon = (SELECT nRenglon FROM #VTA_FacturasTicketsDetalle AS A WHERE A.VentaPiso_nRenglon = #VTA_FacturasTicketsDetalle.Obsequio_nRenglon AND A.bEsObsequio = 1) WHERE NOT Obsequio_nRenglon IS NULL  
  
  --L.I. ALDO MANUEL SANZ CASTRO 04/FEBRERO/2009  
--  ALTER TABLE #RealacionCodigos ADD nNuevoRenglon SMALLINT  
  
  UPDATE #RealacionCodigos  
   SET nNuevoRenglon = T.nRenglon  
  FROM #VTA_FacturasTicketsDetalle AS T  
  WHERE #RealacionCodigos.nRenglon = T.nRenglonViejo  
    
  
  INSERT VTA_FacturasTicketsDetalle (   nCanalDistribucion ,nFacturaTicket ,nRenglon ,nProductoMarcaUnidad ,nServicio   
           ,Combo_nRenglon ,bEsCombo ,Obsequio_nRenglon ,bEsObsequio ,nCantidadEnUnidadMovimiento   
           ,nPiezas ,nFactorConversionAUnidadBase ,nCantidadEnUnidadBase ,nPrecioNormalUnidadBase   
           ,nPrecioOfertaUnidadBase ,nImporteVentaConImpuesto ,nImpuestoVenta ,nImporteAgregadoEnProrrateos   
           ,nImpuestoEnProrrateos ,nImporteAgregadoEnPagoConTarjeta ,nImpuestoEnPagoConTarjeta   
           ,nImporteRestadoDePuntosUtilizados ,nPrecioFacturado ,nPuntosGanados ,nPuntosUtilizados   
           ,Pedido_nRenglon ,nImpuesto ,nCostoPartidaSinImpuesto ,nCostoPartida ,nImporteVentaConImpuestoEnMonedaBase   
           ,nImpuestoVentaEnMonedaBase ,nImporteAgregadoEnProrrateosEnMonedaBase ,nImpuestoEnProrrateosEnMonedaBase   
           ,nImporteAgregadoEnPagoConTarjetaEnMonedaBase ,nImpuestoEnPagoConTarjetaEnMonedaBase ,nImporteRestadoDePuntosUtilizadosEnMonedaBase   
           ,nImpuestoEnMonedaBase ,nCostoPartidaSinImpuestoEnMonedaBase ,nCostoPartidaEnMonedaBase ,Devolucion_nCantidadEnUnidadMovimiento   
           ,Devolucion_nPiezas ,Devolucion_nCantidadEnUnidadBase ,Devolucion_nImporteBase ,Devolucion_nPuntosGanados   
           ,Devolucion_nPuntosUtilizados, nTipoPrecioNormal, nTipoPrecioOferta, nTipoPrecioFacturado, nContenidoProducto)  
  SELECT  nCanalDistribucion ,nFacturaTicket ,nRenglon ,nProductoMarcaUnidad            
    ,nServicioGeneral ,Combo_nRenglon ,bEsCombo ,Obsequio_nRenglon ,bEsObsequio   
    ,nCantidadEnUnidadMovimiento ,nPiezas ,nFactorConversionAUnidadBase ,nCantidadEnUnidadBase ,nPrecioNormalEnUnidadBase   
    ,nPrecioOfertadoEnUnidadBase ,nImporteVentaConImpuesto ,nImpuestoVenta ,nImporteEnProrrateos   
    ,nImpuestoEnProrrateos ,nImporteEnPagoConTarjeta ,nImpuestoEnPagoConTarjeta   
    ,nImportePuntosUtilizados ,nPrecioFacturado ,nPuntosGanados ,nPuntosUtilizados   
    ,Pedido_nRenglon ,nImpuesto ,nCostoPartidaSinImpuesto ,nCostoPartida ,nImporteVentaConImpuestoEnMonedaBase   
    ,nImpuestoVentaEnMonedaBase ,nImporteAgregadoEnProrrateosEnMonedaBase ,nImpuestoEnProrrateosEnMonedaBase   
    ,nImporteAgregadoEnPagoConTarjetaEnMonedaBase ,nImpuestoEnPagoConTarjetaEnMonedaBase ,nImporteRestadoDePuntosUtilizadosEnMonedaBase   
    ,nImpuestoEnMonedaBase ,nCostoPartidaSinImpuestoEnMonedaBase ,nCostoPartidaEnMonedaBase ,0   
    ,0 ,0 ,0 ,0 ,0  
    , nTipoPrecioNormal, nTipoPrecioOferta, nTipoPrecioFacturado, nContenidoProducto  
  FROM #VTA_FacturasTicketsDetalle    
  
  --Cambio pedido por arturo 9 Agosto del 2007.- Marcamos el renglon del movimiento de inventario generado para el detalle  
  --Lo pondremos hasta que le de luz verde el arturo  
  UPDATE VTA_FacturasTicketsDetalle  
   SET Inventario_nRenglon = T.nRenglon  
  FROM #INV_MovimientoInventarioDetalle AS T  
  WHERE VTA_FacturasTicketsDetalle.nCanalDistribucion = @nCanalDistribucion  
   AND VTA_FacturasTicketsDetalle.nFacturaTicket = @nFacturaTicket  
   AND VTA_FacturasTicketsDetalle.nProductoMarcaUnidad = T.nProductoMarcaUnidad  
  
    
  DROP TABLE #INV_MovimientoInventarioDetalle  
      
--  --Insertamos el detalle de los codigos de barra  
--  --#DetalladosAgrupados  
--  INSERT VTA_FacturasTicketsDetalleCodigosBarras(nCanalDistribucion ,nFacturaTicket ,nRenglon ,Codigo_nCanalDistribucion ,Codigo_nCodigoBarra)   
--  SELECT @nCanalDistribucion ,@nFacturaTicket ,T.nRenglon ,D.nCanalDistribucion ,D.nCodigoBarra  
--  FROM #RealacionCodigos AS D  
--  INNER JOIN #VTA_FacturasTicketsDetalle AS T ON (T.nProductoMarcaUnidad = D.nProductoMarcaUnidad AND T.Combo_nRenglon IS NULL AND Obsequio_nRenglon IS NULL)  
  
  --Insertamos el detalle de los codigos de barra  
  --#DetalladosAgrupados  
  INSERT VTA_FacturasTicketsDetalleCodigosBarras(nCanalDistribucion ,nFacturaTicket ,nRenglon ,Codigo_nCanalDistribucion ,Codigo_nCodigoBarra)   
  SELECT @nCanalDistribucion ,@nFacturaTicket ,D.nNuevoRenglon ,D.nCanalDistribucion ,D.nCodigoBarra  
  FROM #RealacionCodigos AS D    
   INNER JOIN #VTA_FacturasTicketsDetalle FTD --L.I. ALDO MANUEL SANZ CASTRO 04/FEBRERO/2009  
    ON(FTD.nRenglonViejo=D.nRenglon)  
  
  
    
  --Agrupamos las tablas de servicios para encontrar concidencia con lo agrupado a nivel de detalle  
  SELECT  S.nAlmacen ,S.nVenta ,MIN(S.nRenglon) AS nRenglon ,MIN(S.nConsecutivoServicio) AS nConsecutivoServicio  
    ,S.nProducto ,S.nServicio ,S.nPrecio ,E.nProductoMarcaUnidad  
    ,SUM(S.nCosto) AS nCosto ,SUM(S.nImpuesto) AS nImpuesto  
    ,SUM(S.nCostoPartida) AS nCostoPartida  
  INTO #VTA_VentaPisoDetalleServicios   
  FROM VTA_VentaPisoDetalle AS E(NOLOCK)  
  INNER JOIN VTA_VentaPisoDetalleServicios AS S(NOLOCK) ON (E.nAlmacen = @nAlmacen AND E.nVenta = @nVenta  
   AND E.nAlmacen = S.nAlmacen AND E.nVenta = S.nVenta AND E.nRenglon = S.nRenglon  
   AND E.bCancelado = 0 AND E.bCancelacion = 0)  
  GROUP BY S.nAlmacen ,S.nVenta ,S.nProducto ,S.nServicio ,S.nPrecio ,E.nProductoMarcaUnidad  
    
  
  SELECT  @nCanalDistribucion AS nCanalDistribucion ,@nFacturaTicket AS nFacturaTicket  
    ,IDENTITY(TINYINT ,1 ,1) AS nConsecutivoServicio ,VDS.nServicio ,VDS.nProducto   
    ,SUM(D.nPiezas) AS nPiezas ,SUM(D.nCantidadEnUnidadBase) AS nCantidadEnUnidadBase  
    ,VDS.nPrecio ,SUM(VDS.nCosto) AS nCostoPartidaSinImpuesto ,SUM(VDS.nImpuesto) AS nImpuesto ,SUM(VDS.nCostoPartida) AS nCostoPartida  
    ,SUM(VDS.nCosto * E.nTipoCambio) AS nCostoPartidaSinImpuestoEnMonedaBase ,SUM(VDS.nImpuesto * E.nTipoCambio) AS nImpuestoEnMonedaBase   
    ,SUM(VDS.nCostoPartida * E.nTipoCambio) AS nCostoPartidaEnMonedaBase ,CONVERT(BIT ,1) AS bAnexarPrecioProducto  
  INTO #tmpServicios  
  FROM #VTA_VentaPiso AS E  
  INNER JOIN #VTA_VentaPisoDetalleUniverso AS D ON (D.nAlmacen = E.nAlmacen AND D.nVenta = E.nVenta)      
  INNER JOIN #VTA_VentaPisoDetalleServicios AS VDS(NOLOCK) ON (D.nAlmacen = VDS.nAlmacen AND D.nVenta = VDS.nVenta AND D.nProductoMarcaUnidad = VDS.nProductoMarcaUnidad) --Hacemos join con producto ya que el renglon lo perdemos por el agrupado    
  GROUP BY VDS.nServicio ,VDS.nProducto ,VDS.nPrecio  
  
  
  --15-Junio-2005.- Los servicios por producto que no se agregan al precio, tambien los dejaremos caer en la tabla de servicios, pero con el bit apagado  
  INSERT #tmpServicios (  nCanalDistribucion ,nFacturaTicket ,nServicio ,nProducto ,nPiezas ,nCantidadEnUnidadBase ,nPrecio   
        ,nCostoPartidaSinImpuesto ,nImpuesto ,nCostoPartida ,nCostoPartidaSinImpuestoEnMonedaBase   
        ,nImpuestoEnMonedaBase ,nCostoPartidaEnMonedaBase ,bAnexarPrecioProducto)  
  SELECT  @nCanalDistribucion AS nCanalDistribucion ,@nFacturaTicket AS nFacturaTicket ,V.nServicioGeneral  
    ,PMU.nProducto ,D.nPiezas ,D.nCantidadEnUnidadBase  
    ,V.nPrecioOfertadoEnUnidadBase ,V.nCostoPartidaSinImpuesto ,V.nImpuesto ,V.nCostoPartida  
    ,V.nCostoPartidaSinImpuesto * nTipoCambio ,V.nImpuesto * nTipoCambio ,V.nCostoPartida * nTipoCambio ,0  
  FROM #VTA_VentaPisoDetalleUniverso AS V  
  INNER JOIN #VTA_VentaPisoDetalle AS D ON (NOT V.Servicio_nRenglon IS NULL AND V.Servicio_nRenglon = D.nRenglon)    
  INNER JOIN CTL_ProductosMarcasUnidades AS PMU(NOLOCK) ON (D.nProductoMarcaUnidad = PMU.nProductoMarcaUnidad)  
  INNER JOIN #VTA_VentaPiso AS E ON (V.nAlmacen = E.nAlmacen AND V.nVenta = E.nVenta)  
  WHERE NOT V.Servicio_nRenglon IS NULL  
  
  --Los servicios cobrados de manera global en la factura los agregaremos en el detalle de servicios pero sin anexar precio a productos  
  INSERT #tmpServicios (  nCanalDistribucion ,nFacturaTicket ,nServicio ,nProducto ,nPiezas ,nCantidadEnUnidadBase ,nPrecio   
        ,nCostoPartidaSinImpuesto ,nImpuesto ,nCostoPartida ,nCostoPartidaSinImpuestoEnMonedaBase   
        ,nImpuestoEnMonedaBase ,nCostoPartidaEnMonedaBase ,bAnexarPrecioProducto)  
  SELECT  nCanalDistribucion ,nFacturaTicket ,nServicioGeneral ,NULL ,nPiezas ,nCantidadEnUnidadBase ,nPrecioOfertadoEnUnidadBase  
    ,nCostoPartidaSinImpuesto ,nImpuesto ,nCostoPartida  
    ,nCostoPartidaSinImpuestoEnMonedaBase ,nImpuestoEnMonedaBase ,nCostoPartidaEnMonedaBase ,0  
  FROM #VTA_FacturasTicketsDetalle  
  WHERE nProductoMarcaUnidad IS NULL AND NOT nServicioGeneral IS NULL AND Servicio_nRenglon IS NULL  
  
  INSERT VTA_FacturasTicketsServicios(  nCanalDistribucion ,nFacturaTicket ,nConsecutivoServicio ,nServicio ,nProducto ,nPiezas ,nCantidadEnUnidadBase   
            ,nPrecio ,nCostoPartidaSinImpuesto ,nImpuesto ,nCostoPartida ,nCostoPartidaSinImpuestoEnMonedaBase   
            ,nImpuestoEnMonedaBase ,nCostoPartidaEnMonedaBase ,bAnexarPrecioProducto)  
  SELECT * FROM #tmpServicios  
    
  
  SELECT @nNumeroFactura = @nNumeroFactura + 1  
  DROP TABLE #VTA_FacturasTicketsDetalle  
  DROP TABLE #VTA_VentaPisoDetalleUniverso  
  DROP TABLE #tmpServicios  
  DROP TABLE #CodigosBarraInventario  
  DROP TABLE #VTA_VentaPisoDetalleServicios    
    
 END  
  
  
 SELECT * FROM #Resultado  
  
 SET NOCOUNT OFF  

GO

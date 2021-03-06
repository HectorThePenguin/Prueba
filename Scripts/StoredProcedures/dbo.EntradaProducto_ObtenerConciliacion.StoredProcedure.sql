USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerConciliacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerConciliacion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerConciliacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 08/08/2014
-- Description:  Obtiene los datos para la generacion de la poliza
--				 entrada por compra
-- EntradaProducto_ObtenerConciliacion 1, '20141101', '20141101'
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerConciliacion]
@OrganizacionID INT
, @FechaInicial DATE
, @FechaFinal	DATE
AS
BEGIN
	SET NOCOUNT ON
		CREATE TABLE #tEntradas
		(
			ContratoID					INT
			,  Folio					INT
			,  ProductoID				INT
			,  OrganizacionID			INT
			,  Fecha					DATETIME
			,  Observaciones			VARCHAR(250)
			,  PesoOrigen				INT
			,  PesoBruto				INT
			,  PesoTara					INT
			,  AlmacenMovimientoID		BIGINT
			,  AlmacenID				INT
			,  FolioMovimiento			BIGINT
			,  FechaMovimiento			DATETIME
			,  ProveedorID				INT
			,  EntradaProductoID		INT
			,  TipoContratoID			INT
			,  Lote						INT
			,  Producto					VARCHAR(100)
			,  UnidadID					INT
			,  CodigoSAP				VARCHAR(100)
			,  Proveedor				VARCHAR(250)
			,  ObservacionesAlmacen		VARCHAR(250)
			,  TipoCambioID				INT
			,  PesoNegociar				VARCHAR(50)
			,  PesoDescuento			INT
			,  CuentaSAPID				INT
			,  PesoBonificacion			INT
		)
		INSERT INTO #tEntradas
		SELECT C.ContratoID
			,  EP.Folio
			,  EP.ProductoID
			,  EP.OrganizacionID
			,  EP.Fecha
			,  EP.Observaciones
			,  EP.PesoOrigen
			,  EP.PesoBruto
			,  EP.PesoTara
			,  AM.AlmacenMovimientoID
			,  AI.AlmacenID-- AM.AlmacenID
			,  AM.FolioMovimiento
			,  AM.FechaMovimiento
			,  AM.ProveedorID
			,  EP.EntradaProductoID
			,  C.TipoContratoID
			,  AIL.Lote
			,  P.Descripcion
			,  P.UnidadID
			,  Prov.CodigoSAP
			,  Prov.Descripcion			AS Proveedor
			,  AM.Observaciones			AS ObservacionesAlmacenMovimiento
			,  C.TipoCambioID
			,  C.PesoNegociar
			,  EP.PesoDescuento
			,  C.CuentaSAPID
			,  EP.PesoBonificacion
		FROM EntradaProducto EP		(NOLOCK)
		INNER JOIN AlmacenMovimiento AM (NOLOCK)
			ON (EP.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		INNER JOIN AlmacenInventarioLote AIL (NOLOCK)
			ON (EP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		INNER JOIN AlmacenInventario AI (NOLOCK)
			ON (AIL.AlmacenInventarioID = AI.AlmacenInventarioID)
		INNER JOIN Producto P (NOLOCK)
			ON (EP.ProductoID = P.ProductoID)
		INNER JOIN Proveedor Prov (NOLOCK)
			ON (AM.ProveedorID = Prov.ProveedorID)
		LEFT JOIN Contrato C (NOLOCK)
			ON (C.ContratoID = EP.ContratoID)
		WHERE EP.OrganizacionID = @OrganizacionID
			AND CAST(EP.Fecha AS DATE) BETWEEN @FechaInicial AND @FechaFinal
		CREATE TABLE #tAlmacenMovimiento
		(
			AlmacenMovimientoID	BIGINT
		)
		CREATE TABLE #tEntradaProducto
		(
			EntradaProductoID	INT
			, AlmacenMovimientoID	BIGINT
		)
		INSERT INTO #tAlmacenMovimiento
		SELECT AlmacenMovimientoID
		FROM #tEntradas
		INSERT INTO #tEntradaProducto
		SELECT EntradaProductoID 
			,  AlmacenMovimientoID
		FROM #tEntradas
		SELECT 	ContratoID
			,	Folio
			,	ProductoID
			,	OrganizacionID
			,	Fecha
			,	Observaciones
			,	PesoOrigen
			,	PesoBruto
			,	PesoTara
			,	AlmacenMovimientoID
			,	AlmacenID
			,	FolioMovimiento
			,	FechaMovimiento
			,	ProveedorID
			,   TipoContratoID
			,   Lote
			,   Producto
			,   UnidadID
			,   CodigoSAP
			,   Proveedor
			,   ObservacionesAlmacen
			,   TipoCambioID
			,   PesoNegociar
			,   PesoDescuento
			,   CuentaSAPID
			,   PesoBonificacion
		FROM #tEntradas
		SELECT AMD.ProductoID
			,  AMD.Precio
			,	AMD.Cantidad
			,	AMD.Importe	
			,	AMD.ContratoID
			,	AMD.Piezas
			,   AMD.AlmacenMovimientoID
		FROM AlmacenMovimientoDetalle AMD(NOLOCK)
		INNER JOIN #tAlmacenMovimiento AM
			ON (AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		SELECT AMC.ProveedorID
			,  AMC.CostoID
			,  AMC.Cantidad
			,  AMC.Importe
			,  AM.AlmacenMovimientoID
		FROM AlmacenMovimientoCosto AMC(NOLOCK)
		INNER JOIN #tAlmacenMovimiento AM
			ON (AMC.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		SELECT EPC.CostoID
			,  EPC.TieneCuenta
			,  EPC.ProveedorID
			,  EPC.Iva
			,  EPC.Retencion
			,  P.Descripcion
			,  P.CodigoSAP
			,  EPC.Observaciones
			,  EPC.CuentaProvision
			,  EP.AlmacenMovimientoID
		FROM EntradaProductoCosto EPC (NOLOCK)
		LEFT OUTER JOIN #tEntradaProducto EP
			ON (EPC.EntradaProductoID = EP.EntradaProductoID)
		LEFT OUTER JOIN Proveedor P (NOLOCK)
			ON (EPC.ProveedorID = P.ProveedorID)
		DROP TABLE #tEntradas
		DROP TABLE #tAlmacenMovimiento
		DROP TABLE #tEntradaProducto
	SET NOCOUNT OFF
END

GO

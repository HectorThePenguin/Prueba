USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerFolioEntradaContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerFolioEntradaContrato]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerFolioEntradaContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 08/08/2014
-- Description:  Obtiene los datos para la generacion de la poliza
--				 entrada por compra
-- EntradaProducto_ObtenerFolioEntradaContrato 7, 0, 2
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerFolioEntradaContrato]
@Folio INT
, @ContratoID INT
, @OrganizacionID INT
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
			ON (EP.AlmacenMovimientoID = AM.AlmacenMovimientoID
					AND EP.Folio = @Folio
					AND EP.OrganizacionID = @OrganizacionID)
		INNER JOIN AlmacenInventarioLote AIL (NOLOCK)
			ON (EP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		INNER JOIN AlmacenInventario AI (NOLOCK)
			ON (AIL.AlmacenInventarioID = AI.AlmacenInventarioID)
		INNER JOIN Producto P (NOLOCK)
			ON (EP.ProductoID = P.ProductoID)
		INNER JOIN Proveedor Prov (NOLOCK)
			ON (AM.ProveedorID = Prov.ProveedorID)
		LEFT JOIN Contrato C (NOLOCK)
			ON (C.ContratoID = EP.ContratoID
					AND C.ContratoID = @ContratoID)
		DECLARE @AlmacenMovimientoID		BIGINT
			,	@EntradaProductoID			INT
		SET @AlmacenMovimientoID = (SELECT TOP 1 AlmacenMovimientoID FROM #tEntradas)
		SET @EntradaProductoID = (SELECT TOP 1 EntradaProductoID FROM #tEntradas)
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
		SELECT ProductoID
			,  Precio
			,	Cantidad
			,	Importe	
			,	ContratoID
			,	Piezas
		FROM AlmacenMovimientoDetalle (NOLOCK)
		WHERE AlmacenMovimientoID = @AlmacenMovimientoID
		SELECT ProveedorID
			,  CostoID
			,  Cantidad
			,  Importe
		FROM AlmacenMovimientoCosto (NOLOCK)
		WHERE AlmacenMovimientoID = @AlmacenMovimientoID
		SELECT EPC.CostoID
			,  EPC.TieneCuenta
			,  EPC.ProveedorID
			,  EPC.Iva
			,  EPC.Retencion
			,  P.Descripcion
			,  P.CodigoSAP
			,  EPC.Observaciones
			,  EPC.CuentaProvision
		FROM EntradaProductoCosto EPC (NOLOCK)
		LEFT OUTER JOIN Proveedor P (NOLOCK)
			ON (EPC.ProveedorID = P.ProveedorID)
		WHERE EntradaProductoID = @EntradaProductoID
		DROP TABLE #tEntradas
	SET NOCOUNT OFF
END

GO

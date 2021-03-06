USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Conciliacion_ObtenerPasesProceso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Conciliacion_ObtenerPasesProceso]
GO
/****** Object:  StoredProcedure [dbo].[Conciliacion_ObtenerPasesProceso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 04/08/2014
-- Description: Obtiene todos los contratos de un proveedor
-- SpName     : Conciliacion_ObtenerPasesProceso 1, '20151106', '20151107'
-- 001 Jorge Luis Velazquez Araujo 09/11/2015 **Se cambia el Sp' para obtener los valores guardados de los movimientos
--======================================================
CREATE PROCEDURE [dbo].[Conciliacion_ObtenerPasesProceso]
@OrganizacionID INT
, @FechaInicial DATE
, @FechaFinal DATE
AS
BEGIN
	SET NOCOUNT ON
		DECLARE @TipoMovimientoPaseProceso INT
		SET @TipoMovimientoPaseProceso = 25
		SELECT Prod.ProductoID
			,  Prod.Descripcion					AS Producto
			,  Prod.UnidadID
			,  ProgMatPri.Observaciones
			,  ProgMatPri.ProgramacionMateriaPrimaID
			,  CAST(ISNULL(PMP.PesoBruto - PMP.PesoTara, 0) AS DECIMAL(18,2)) AS CantidadEntregada
			,  0								AS CostoID
			,  0								AS Tarifa	
			,  AIL.Lote
			,  AMD.Precio						AS PrecioAlmacenMovimientoDetalle
			,  AMD.Importe						AS ImporteAlmacenMovimientoDetalle
			,  ''								AS CodigoSAP
			,  ''								AS Proveedor
			,  0								AS ProveedorID
			,  O.OrganizacionID
			,  O.Descripcion					AS Organizacion
			,  ProgMatPri.AlmacenID
			,  A.Descripcion					AS Almacen
			,  P.FolioPedido					AS FolioPedido
			,  P.FechaPedido					AS FechaPedido
			,  PMP.Ticket						AS Ticket
			,  ISNULL(FI.AlmacenIDOrigen, AM.AlmacenID)	AS AlmacenIDOrigen
			,  A2.Descripcion					AS AlmacenOrigen
			,  AMD.AlmacenMovimientoID
			,  AI.AlmacenInventarioID
			,  PD.InventarioLoteIDDestino
			,  I.TasaIva
			,  PMP.ProveedorChoferID
			,  FI.FleteInternoID
			,  AMD.AlmacenMovimientoDetalleID
			,  AM.TipoMovimientoID
			,  pmp.PesajeMateriaPrimaID --001
		INTO #tPaseProceso
		FROM Pedido P(NOLOCK)
		INNER JOIN PedidoDetalle PD(NOLOCK)
			ON (P.PedidoID = PD.PedidoID)
		INNER JOIN ProgramacionMateriaPrima ProgMatPri(NOLOCK)
			ON (PD.PedidoDetalleID = ProgMatPri.PedidoDetalleID)
		INNER JOIN Organizacion O(NOLOCK)
			ON (P.OrganizacionID = O.OrganizacionID)
		INNER JOIN Almacen A(NOLOCK)
			ON (P.AlmacenID = A.AlmacenID
				AND O.OrganizacionID = A.OrganizacionID)
		INNER JOIN Producto Prod(NOLOCK)
			ON (PD.ProductoID = Prod.ProductoID)
		LEFT OUTER JOIN FleteInterno FI(NOLOCK)
			ON (Prod.ProductoID = FI.ProductoID
				AND P.OrganizacionID = FI.OrganizacionID)
		INNER JOIN PesajeMateriaPrima PMP(NOLOCK)
			ON (ProgMatPri.ProgramacionMateriaPrimaID = PMP.ProgramacionMateriaPrimaID)
		INNER JOIN AlmacenMovimientoDetalle AMD(NOLOCK)
			ON (Prod.ProductoID = AMD.ProductoID
				AND PD.ProductoID = AMD.ProductoID
				AND pmp.AlmacenMovimientoDestinoID = AMD.AlmacenMovimientoID) --001
		INNER JOIN AlmacenInventario AI(NOLOCK)
			ON (AI.ProductoID = PROD.ProductoID
				AND AI.AlmacenID = ProgMatPri.AlmacenID)
		INNER JOIN AlmacenInventarioLote AIL(NOLOCK)
			ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID
				AND PD.InventarioLoteIDDestino = AIL.AlmacenInventarioLoteID)
		LEFT OUTER JOIN Almacen A2(NOLOCK)
			ON (FI.AlmacenIDOrigen = A2.AlmacenID)
		INNER JOIN 
		(
			SELECT AM.AlmacenID
				,  AM.AlmacenMovimientoID
				,  AM.TipoMovimientoID
			FROM AlmacenMovimiento AM(NOLOCK)
			WHERE TipoMovimientoID = @TipoMovimientoPaseProceso
		) AM
			ON (PMP.AlmacenMovimientoOrigenID = AM.AlmacenMovimientoID)
		INNER JOIN Iva I(NOLOCK)
			ON (O.IvaID = I.IvaID)
		WHERE P.OrganizacionID = @OrganizacionID
			AND CAST(P.FechaPedido AS DATE) BETWEEN @FechaInicial AND @FechaFinal
		GROUP BY Prod.ProductoID
			,  Prod.Descripcion
			,  Prod.UnidadID
			,  ProgMatPri.ProgramacionMateriaPrimaID
			,  ProgMatPri.Observaciones
			,  PMP.PesoBruto
			,  PMP.PesoTara
			,  AIL.Lote
			,  AMD.Precio
			,  AMD.Importe
			,  O.OrganizacionID
			,  O.Descripcion
			,  ProgMatPri.AlmacenID
			,  A.Descripcion
			,  P.FolioPedido
			,  P.FechaPedido
			,  PMP.Ticket
			,  FI.AlmacenIDOrigen
			,  A2.Descripcion
			,  AMD.AlmacenMovimientoID
			,  AI.AlmacenInventarioID
			,  PD.InventarioLoteIDDestino
			,  I.TasaIva
			,  PMP.ProveedorChoferID
			,  FI.FleteInternoID
			,  AM.AlmacenID
			,  AMD.AlmacenMovimientoDetalleID
			,  AM.TipoMovimientoID
			,  pmp.PesajeMateriaPrimaID --001
		SELECT ProductoID
			,  Producto
			,  UnidadID
			,  Observaciones
			,  ProgramacionMateriaPrimaID
			,  CantidadEntregada
			,  CostoID
			,  Tarifa	
			,  Lote
			,  PrecioAlmacenMovimientoDetalle
			,  ImporteAlmacenMovimientoDetalle
			,  CodigoSAP
			,  Proveedor
			,  ProveedorID
			,  OrganizacionID
			,  Organizacion
			,  AlmacenID
			,  Almacen
			,  FolioPedido
			,  FechaPedido
			,  Ticket
			,  AlmacenIDOrigen
			,  AlmacenOrigen
			,  AlmacenMovimientoID
			,  AlmacenInventarioID
			,  InventarioLoteIDDestino
			,  TasaIva
			,  ISNULL(ProveedorChoferID, 0) AS ProveedorChoferID
			,  ISNULL(FleteInternoID, 0)	AS FleteInternoID
			,  AlmacenMovimientoDetalleID
			,  TipoMovimientoID
			,  PesajeMateriaPrimaID --001
		FROM #tPaseProceso
		ORDER BY AlmacenMovimientoDetalleID
		SELECT ISNULL(P.ProveedorID, 0) AS ProveedorID
			,  P.CodigoSAP
			,  P.Descripcion
			,  ISNULL(PC.ProveedorChoferID, 0)	AS ProveedorChoferID
		INTO #tProveedor
		FROM #tPaseProceso PP
		LEFT JOIN ProveedorChofer PC(NOLOCK)
			ON (PP.ProveedorChoferID = PC.ProveedorChoferID)
		LEFT JOIN Proveedor P(NOLOCK)
			ON (PC.ProveedorID = P.ProveedorID)
		GROUP BY P.ProveedorID
			,  P.CodigoSAP
			,  P.Descripcion
			,  PC.ProveedorChoferID
		SELECT ProveedorID
			,  CodigoSAP
			,  Descripcion
			,  ProveedorChoferID
		FROM #tProveedor

			SELECT ProveedorID
				,  CostoID
				,  Tarifa
				,  FleteInternoDetalleID
				,  FleteInternoID
				,  TipoTarifaID
		FROM 
		(
			SELECT FID.ProveedorID
				,  FIC.CostoID
				,  FIC.Tarifa
				,  FID.FleteInternoDetalleID
				,  FI.FleteInternoID
				,  fid.TipoTarifaID
			FROM #tPaseProceso PP
			INNER JOIN FleteInterno FI(NOLOCK)
				ON (PP.FleteInternoID = FI.FleteInternoID)
			INNER JOIN FleteInternoDetalle FID(NOLOCK)
				ON (FI.FleteInternoID = FID.FleteInternoID
					AND FID.Activo = 1)
			INNER JOIN ProveedorChofer PC(NOLOCK)
				ON (PP.ProveedorChoferID = PC.ProveedorChoferID)
			INNER JOIN #tProveedor P
				ON (FID.ProveedorID = P.ProveedorID
					AND PC.ProveedorID = P.ProveedorID)
			INNER JOIN FleteInternoCosto FIC(NOLOCK)
				ON (FID.FleteInternoDetalleID = FIC.FleteInternoDetalleID
					AND FIC.Activo = 1)
			where pp.ProductoID = FI.ProductoID 		
			GROUP BY FID.ProveedorID
				,  FIC.CostoID
				,  FIC.Tarifa
				,  FID.FleteInternoDetalleID
				,  FI.FleteInternoID
				,  FID.TipoTarifaID
			UNION
			SELECT 0, 0, 0, 0, 0, 0
		) A


		SELECT pmp.PesajeMateriaPrimaID
			,  PMP.ProgramacionMateriaPrimaID
			,  PMP.ProveedorChoferID
		INTO #tPesajes
		FROM #tPaseProceso PP
		INNER JOIN PesajeMateriaPrima PMP(NOLOCK)
			ON (PP.ProgramacionMateriaPrimaID = PMP.ProgramacionMateriaPrimaID)
		GROUP BY PMP.ProgramacionMateriaPrimaID
			,  PMP.ProveedorChoferID
			, pmp.PesajeMateriaPrimaID

		SELECT ISNULL(P.ProveedorChoferID, 0)	AS ProveedorChoferID
			,  P.ProgramacionMateriaPrimaID
			,  Pmp.AlmacenMovimientoDestinoID
			,  pmp.PesajeMateriaPrimaID
			,  PesoBruto
			,  PesoTara
		FROM #tPesajes P
		INNER JOIN PesajeMateriaPrima PMP(NOLOCK)
			ON (P.PesajeMateriaPrimaID = PMP.PesajeMateriaPrimaID)

				SELECT 
			amc.AlmacenMovimientoCostoID
			,amc.AlmacenMovimientoID
			,amc.ProveedorID
			,amc.CuentaSAPID
			,co.CostoID
			,co.Descripcion AS Costo
			,amc.Cantidad
			,amc.Importe		
		FROM #tPesajes pe
		inner join PesajeMateriaPrima pmp (NOLOCK) on pe.PesajeMateriaPrimaID = pmp.PesajeMateriaPrimaID
		inner join AlmacenMovimientoCosto amc (NOLOCK) on pmp.AlmacenMovimientoDestinoID = amc.AlmacenMovimientoID
		inner join Costo co (NOLOCK) on amc.CostoID = co.CostoID	

		DROP TABLE #tPaseProceso
		DROP TABLE #tProveedor
		DROP TABLE #tPesajes
	SET NOCOUNT OFF
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerSurtidoPorPedido]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerSurtidoPorPedido]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerSurtidoPorPedido]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 11/06/2014
-- Description: Obtiene el surtido del pedido
-- SpName     : EXEC EntradaMateriaPrima_ObtenerSurtidoPorPedido 311,2,'<ROOT><TiposPesajes><TipoPesajeID>1</TipoPesajeID></TiposPesajes><TiposPesajes><TipoPesajeID>2</TipoPesajeID></TiposPesajes><TiposPesajes>	<TipoPesajeID>3</TipoPesajeID>	</TiposPesajes></ROOT>',1
-- 001 Jorge Luis Velazquez Araujo 10/12/2015 **Se agrega la Organizacion del Folio
--======================================================
CREATE PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerSurtidoPorPedido]
@FolioPedido INT,
@TipoProveedor INT,
@XmlTiposPesajes XML,
@Activo BIT,
@OrganizacionID INT --001
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TmpTiposPesajes TABLE(TipoPesajeID INT)
	INSERT INTO @TmpTiposPesajes
	SELECT TipoPesajeID  = T.item.value('./TipoPesajeID[1]', 'INT')
	  FROM @XmlTiposPesajes.nodes('ROOT/TiposPesajes') AS T(item)
SELECT
	PEAP.PesajeMateriaPrimaID,
	PEAP.FechaSurtido,
	PEAP.Ticket,
	PEAP.TipoPesajeID,
	PEAP.ProveedorChoferID,
	TP.Descripcion AS DescripcionTipoPesaje,
	PRO.ProductoID,
	PRO.Descripcion AS DescripcionProducto,
	CHO.ChoferID,
	CHO.Nombre,
	CHO.ApellidoPaterno,
	CHO.ApellidoMaterno,
	PROV.ProveedorID,
	PROV.Descripcion AS DescripcionProveedor,
	AIL.AlmacenInventarioLoteID,
	AIL.Lote,
	AIL.AlmacenInventarioID,
	AIL.Cantidad,
	AIL.PrecioPromedio,
	AIL.Piezas,
	AIL.Importe,
	PD.PedidoDetalleID,
	PD.CantidadSolicitada,
	PD.InventarioLoteIDDestino,
	PRMP.ProgramacionMateriaPrimaID,
	CAST( ISNULL(PEAP.PesoBruto, 0) - ISNULL(PEAP.PesoTara, 0) AS Decimal) AS CantidadEntregada,
	ISNULL(PRMP.CantidadProgramada, 0) AS CantidadProgramada,
	PRMP.InventarioLoteIDOrigen,
	AI.AlmacenID,
	PEAP.PesoBruto,
	PEAP.PesoTara,
	PEAP.Piezas AS PiezasPesaje,
	PEAP.Activo AS ActivoPesaje,
	PEAP.AlmacenMovimientoOrigenID,
	PEAP.AlmacenMovimientoDestinoID
FROM
	Pedido P
INNER JOIN PedidoDetalle PD ON P.PedidoID = PD.PedidoID AND PD.Activo = @Activo
INNER JOIN Producto PRO ON PD.ProductoID = PRO.ProductoID AND PRO.Activo = @Activo
INNER JOIN ProgramacionMateriaPrima PRMP ON PRMP.PedidoDetalleID = PD.PedidoDetalleID AND PRMP.Activo = @Activo
INNER JOIN PesajeMateriaPrima PEAP ON PEAP.ProgramacionMateriaPrimaID = PRMP.ProgramacionMateriaPrimaID
LEFT JOIN ProveedorChofer PROCHO ON PROCHO.ProveedorChoferID = PEAP.ProveedorChoferID AND PROCHO.Activo = @Activo
LEFT JOIN Proveedor PROV ON PROV.ProveedorID = PROCHO.ProveedorID AND PROV.Activo = @Activo
LEFT JOIN Chofer CHO ON CHO.ChoferID = PROCHO.ChoferID AND CHO.Activo = @Activo
INNER JOIN TipoPesaje TP ON TP.TipoPesajeID = PEAP.TipoPesajeID AND TP.Activo = @Activo
INNER JOIN AlmacenInventarioLote AIL ON AIL.AlmacenInventarioLoteID = PD.InventarioLoteIDDestino AND AIL.Activo = @Activo
INNER JOIN AlmacenInventario AI ON AI.AlmacenInventarioID = AIL.AlmacenInventarioID 
WHERE
	p.OrganizacionID = @OrganizacionID --001
AND	PEAP.TipoPesajeID IN (SELECT TipoPesajeID FROM @TmpTiposPesajes)
AND(PROV.TipoProveedorID = @TipoProveedor OR PROV.TipoProveedorID IS NULL)
AND P.FolioPedido = @FolioPedido
AND P.Activo = @Activo
AND CAST(P.FechaPedido AS DATE) >= CAST(GETDATE()-3 AS DATE)
AND (PEAP.PesoBruto > 0 OR PEAP.ProveedorChoferID is null)    
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerCantidadEntregadaLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerCantidadEntregadaLote]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerCantidadEntregadaLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/10/2014
-- Description: Obtiene si el Lote tiene cantidad entregada
-- SpName     : Pedido_ObtenerCantidadEntregadaLote 30
--======================================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerCantidadEntregadaLote]
@AlmacenInventarioLoteID INT
AS
BEGIN
	SET NOCOUNT ON;
SELECT
	P.PedidoID,
	P.FolioPedido,
	PD.PedidoID,
	PD.PedidoDetalleID,
	PD.ProductoID,
	PD.InventarioLoteIDDestino,	
	prmp.PedidoDetalleID,
	prmp.ProgramacionMateriaPrimaID,
	prmp.InventarioLoteIDOrigen,	
	prmp.CantidadProgramada,
	peap.ProgramacionMateriaPrimaID,
	PEAP.PesajeMateriaPrimaID,
	peap.EstatusID,
	peap.PesoBruto - peap.PesoTara AS PesoNeto,
	peap.AlmacenMovimientoOrigenID,
	peap.AlmacenMovimientoDestinoID
FROM
	Pedido P
INNER JOIN PedidoDetalle PD ON P.PedidoID = PD.PedidoID AND PD.Activo = 1
INNER JOIN Producto PRO ON PD.ProductoID = PRO.ProductoID AND PRO.Activo = 1
INNER JOIN ProgramacionMateriaPrima PRMP ON PRMP.PedidoDetalleID = PD.PedidoDetalleID AND PRMP.Activo = 1
INNER JOIN PesajeMateriaPrima PEAP ON PEAP.ProgramacionMateriaPrimaID = PRMP.ProgramacionMateriaPrimaID
WHERE
PRMP.InventarioLoteIDOrigen = @AlmacenInventarioLoteID
AND CAST(P.FechaPedido AS DATE) >= CAST(GETDATE()-3 AS DATE)
and p.Activo = 1
AND PEAP.AlmacenMovimientoOrigenID IS NULL 
AND PEAP.AlmacenMovimientoDestinoID IS NULL
	SET NOCOUNT OFF;
END

GO

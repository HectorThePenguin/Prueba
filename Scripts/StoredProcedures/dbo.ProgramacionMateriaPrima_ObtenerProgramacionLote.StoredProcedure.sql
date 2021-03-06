USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerProgramacionLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerProgramacionLote]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerProgramacionLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 25/09/2014
-- Description: Obtiene si el Lote tiene programaciones pendientes
-- SpName     : ProgramacionMateriaPrima_ObtenerProgramacionLote 182
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerProgramacionLote]
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
	peap.AlmacenMovimientoOrigenID,
	peap.EstatusID
FROM
	Pedido P
INNER JOIN PedidoDetalle PD ON P.PedidoID = PD.PedidoID AND PD.Activo = 1
INNER JOIN Producto PRO ON PD.ProductoID = PRO.ProductoID AND PRO.Activo = 1
INNER JOIN ProgramacionMateriaPrima PRMP ON PRMP.PedidoDetalleID = PD.PedidoDetalleID AND PRMP.Activo = 1
LEFT JOIN PesajeMateriaPrima PEAP ON PEAP.ProgramacionMateriaPrimaID = PRMP.ProgramacionMateriaPrimaID
WHERE
prmp.InventarioLoteIDOrigen = @AlmacenInventarioLoteID
AND CAST(P.FechaPedido AS DATE) >= CAST(GETDATE()-3 AS DATE)
AND p.Activo = 1
	SET NOCOUNT OFF;
END

GO

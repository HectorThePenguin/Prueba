USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoDetalle_ObtenerPorDetallePedidoId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PedidoDetalle_ObtenerPorDetallePedidoId]
GO
/****** Object:  StoredProcedure [dbo].[PedidoDetalle_ObtenerPorDetallePedidoId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 01/07/2014
-- Description: Obtiene el detalle del pedido ingresado
-- SpName     : EXEC PedidoDetalle_ObtenerPorDetallePedidoId 1
--======================================================
CREATE PROCEDURE [dbo].[PedidoDetalle_ObtenerPorDetallePedidoId]
@PedidoDetalleId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT PedidoDetalleID, PedidoID, ProductoID, InventarioLoteIDDestino, CantidadSolicitada, Activo
	FROM PedidoDetalle WHERE PedidoDetalleID = @PedidoDetalleId AND Activo = 1
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoDetalle_ObtenerPorPedido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PedidoDetalle_ObtenerPorPedido]
GO
/****** Object:  StoredProcedure [dbo].[PedidoDetalle_ObtenerPorPedido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 10/06/2014
-- Description: Obtiene el detalle del pedido ingresado
-- SpName     : EXEC PedidoDetalle_ObtenerPorPedido 1
--======================================================
CREATE PROCEDURE [dbo].[PedidoDetalle_ObtenerPorPedido]
@PedidoId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT PedidoDetalleID, PedidoID, ProductoID, InventarioLoteIDDestino, CantidadSolicitada, Activo
	FROM PedidoDetalle WHERE PedidoID = @PedidoId AND Activo = 1
	SET NOCOUNT OFF;
END

GO

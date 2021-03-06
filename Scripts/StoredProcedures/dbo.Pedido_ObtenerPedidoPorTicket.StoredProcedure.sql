USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPedidoPorTicket]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ObtenerPedidoPorTicket]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ObtenerPedidoPorTicket]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 21/06/2014
-- Description: 
-- SpName     : Pedido_ObtenerPedidoPorTicket 1, 1
--======================================================
CREATE PROCEDURE [dbo].[Pedido_ObtenerPedidoPorTicket]
@Ticket INT,
@Activo INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT P.FolioPedido
	FROM Pedido P (NOLOCK)
	INNER JOIN PedidoDetalle PD (NOLOCK) ON PD.PedidoID = P.PedidoID
	INNER JOIN ProgramacionMateriaPrima PMP (NOLOCK) ON PMP.PedidoDetalleID = PD.PedidoDetalleID
	INNER JOIN PesajeMateriaPrima PMPR (NOLOCK) ON PMPR.ProgramacionMateriaPrimaID = PMP.ProgramacionMateriaPrimaID
	WHERE PMPR.Ticket = @Ticket
	AND PMPR.Activo = @Activo
	AND P.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorTicketPedido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorTicketPedido]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorTicketPedido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Alejandro Quiroz
-- Create date: 31/07/2014
-- Description: 
-- SpName     : PesajeMateriaPrima_ObtenerPorTicketPedido 1, 1,1
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorTicketPedido]
@Ticket INT,
@PedidoID INT,
@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    PMP.PesajeMateriaPrimaID,
		PMP.ProgramacionMateriaPrimaID,
		PMP.ProveedorChoferID,
		PMP.Ticket,
		PMP.CamionID,
		PMP.PesoBruto,
		PMP.PesoTara,
		PMP.Piezas,
		PMP.TipoPesajeID,
		PMP.UsuarioIDSurtido,
		PMP.FechaSurtido,
		PMP.UsuarioIDRecibe,
		PMP.FechaRecibe,
		PMP.EstatusID,
		PMP.Activo,
		PMP.FechaCreacion,
		PMP.UsuarioCreacionID
	FROM PesajeMateriaPrima (NOLOCK) PMP 
	INNER JOIN ProgramacionMateriaPrima (NOLOCK) PROMP ON PMP.ProgramacionMateriaPrimaID = PROMP.ProgramacionMateriaPrimaID
	INNER JOIN PedidoDetalle (NOLOCK) PD ON PROMP.PedidoDetalleID = PD.PedidoDetalleID
	INNER JOIN Pedido (NOLOCK) P ON PD.PedidoID = P.PedidoID
	WHERE P.PedidoID = @PedidoID
	AND PMP.Ticket = @Ticket
	AND PMP.Activo = @Activo
	SET NOCOUNT OFF;
END

GO

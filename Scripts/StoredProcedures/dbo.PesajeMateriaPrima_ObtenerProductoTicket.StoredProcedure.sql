USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerProductoTicket]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerProductoTicket]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerProductoTicket]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 18/06/2014
-- Description: Obtiene los Tickets para la Produccion Diaria de Molino
-- SpName     : PesajeMateriaPrima_ObtenerProductoTicket 4,'',80
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerProductoTicket] @OrganizacionID INT
	,@Ticket VARCHAR(50)	
AS
SELECT pmp.PesajeMateriaPrimaID
	,pm.OrganizacionID
	,cast(pe.FolioPedido AS VARCHAR(25)) + '-' + cast(pmp.Ticket AS VARCHAR(25)) AS Ticket
	,pro.ProductoID
	,pro.Descripcion
FROM PesajeMateriaPrima pmp
INNER JOIN ProgramacionMateriaPrima pm ON pmp.ProgramacionMateriaPrimaID = pm.ProgramacionMateriaPrimaID
INNER JOIN PedidoDetalle pd ON pm.PedidoDetalleID = pd.PedidoDetalleID
INNER JOIN Pedido pe ON pd.PedidoID = pe.PedidoID
INNER JOIN Producto pro ON pd.ProductoID = pro.ProductoID
WHERE pmp.EstatusID = 33 -- Estatus Pesaje Completado
	AND pm.OrganizacionID = @OrganizacionID
	AND (
		@Ticket = ''
		OR @Ticket = cast(pe.FolioPedido AS VARCHAR(25)) + '-' + cast(pmp.Ticket AS VARCHAR(25))
		)	
	AND NOT EXISTS (
		SELECT ProduccionDiariaDetalleID
		FROM ProduccionDiariaDetalle pdd
		WHERE pdd.PesajeMateriaPrimaID = pmp.PesajeMateriaPrimaID
		)

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Ticket_ObtenerValoresProduccionMolino]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Ticket_ObtenerValoresProduccionMolino]
GO
/****** Object:  StoredProcedure [dbo].[Ticket_ObtenerValoresProduccionMolino]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 18/06/2014
-- Description: Obtiene los Productos filtrados por la Familia
-- SpName     : Ticket_ObtenerValoresProduccionMolino 1,'22-3',4
--======================================================
CREATE PROCEDURE [dbo].[Ticket_ObtenerValoresProduccionMolino] @OrganizacionID INT
	,@Ticket VARCHAR(50)
	,@IndicadorID INT
AS
DECLARE @EstatusCompleto INT = 33 --Estatus Pesaje Completado
SELECT pmp.PesajeMateriaPrimaID
	,pmp.PesoBruto - pmp.PesoTara AS KilosNetos
	,pmp.Piezas AS ConteoPacas
	,ail.Lote
	,cmp.Resultado AS HumedadForraje
	,pro.ProductoID
	,pro.Descripcion
FROM PesajeMateriaPrima pmp
INNER JOIN ProgramacionMateriaPrima pm ON pmp.ProgramacionMateriaPrimaID = pm.ProgramacionMateriaPrimaID
INNER JOIN PedidoDetalle pd ON pm.PedidoDetalleID = pd.PedidoDetalleID
INNER JOIN Pedido pe ON pd.PedidoID = pe.PedidoID
INNER JOIN Producto pro ON pd.ProductoID = pro.ProductoID
INNER JOIN AlmacenInventario ai ON pro.ProductoID = ai.ProductoID
INNER JOIN AlmacenInventarioLote ail ON (
		ai.AlmacenInventarioID = ail.AlmacenInventarioID
		AND ail.AlmacenInventarioLoteID = pd.InventarioLoteIDDestino
		)
INNER JOIN CalidadMateriaPrima cmp ON pd.PedidoDetalleID = cmp.PedidoDetalleID
INNER JOIN IndicadorObjetivo iob ON cmp.IndicadorObjetivoID = iob.IndicadorObjetivoID
INNER JOIN IndicadorProductoCalidad ipc ON iob.IndicadorProductoCalidadID = ipc.IndicadorProductoCalidadID
WHERE pmp.EstatusID = @EstatusCompleto
	--AND pm.Activo = 1
	AND pm.OrganizacionID = @OrganizacionID
	AND ipc.IndicadorID = @IndicadorID
	AND @Ticket = cast(pe.FolioPedido AS VARCHAR(25)) + '-' + cast(pmp.Ticket AS VARCHAR(25))

GO

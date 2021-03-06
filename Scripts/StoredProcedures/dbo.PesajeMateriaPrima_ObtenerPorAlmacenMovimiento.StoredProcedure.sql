USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorAlmacenMovimiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorAlmacenMovimiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 29/10/2014
-- Description: Obtiene los folios de los Pases a Proceso
-- SpName     : EXEC PesajeMateriaPrima_ObtenerPorAlmacenMovimiento '<ROOT><Movimientos><AlmacenMovimientoID>106804</AlmacenMovimientoID></Movimientos></ROOT>'
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorAlmacenMovimiento]
@XmlMovimientos XML
AS
BEGIN
create table #Movimientos
(
	AlmacenMovimientoID INT
)
	insert into #Movimientos
	SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @XmlMovimientos.nodes('/ROOT/Movimientos') as T(N)
	SELECT 
	CAST(pe.FolioPedido AS VARCHAR(10)) + '-' + CAST(pp.Ticket AS VARCHAR(10)) AS FolioPaseProceso
	,am.TipoMovimientoID AS TipoMovimientoOrigenID
	,am1.TipoMovimientoID AS TipoMovimientoDestinoID
	,pp.AlmacenMovimientoOrigenID
	,pp.AlmacenMovimientoDestinoID
	 FROM PesajeMateriaPrima pp
	INNER JOIN ProgramacionMateriaPrima pmp on pp.ProgramacionMateriaPrimaID = pmp.ProgramacionMateriaPrimaID
	INNER JOIN PedidoDetalle pd on pmp.PedidoDetalleID = pd.PedidoDetalleID
	INNER JOIN Pedido pe on pd.PedidoID = pe.PedidoID
	INNER JOIN AlmacenMovimiento am on pp.AlmacenMovimientoOrigenID = am.AlmacenMovimientoID
	INNER JOIN AlmacenMovimiento am1 on pp.AlmacenMovimientoDestinoID = am1.AlmacenMovimientoID
	inner join #Movimientos mo on mo.AlmacenMovimientoID = am.AlmacenMovimientoID or mo.AlmacenMovimientoID = am1.AlmacenMovimientoID
END

GO

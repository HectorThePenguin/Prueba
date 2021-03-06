USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesCantidadProgramada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesCantidadProgramada]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesCantidadProgramada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 28/11/2014 12:00:00 a.m.
-- Description: Obtiene el listado de lotes para la verificacion de lote en uso
-- SpName     : AlmacenInventarioLote_ObtenerLotesCantidadProgramada 25,1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesCantidadProgramada]
@Lote INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COALESCE(SUM(PRMP.CantidadProgramada),0) 
			FROM
				Pedido P
			INNER JOIN PedidoDetalle PD ON P.PedidoID = PD.PedidoID AND PD.Activo = @Activo
			INNER JOIN Producto PRO ON PD.ProductoID = PRO.ProductoID AND PRO.Activo = @Activo
			INNER JOIN ProgramacionMateriaPrima PRMP ON PRMP.PedidoDetalleID = PD.PedidoDetalleID AND PRMP.Activo = @Activo
			INNER JOIN AlmacenInventarioLote AIL ON  AIL.AlmacenInventarioLoteID = PRMP.InventarioLoteIDOrigen
			WHERE CAST(P.FechaPedido AS DATE) >= CAST(GETDATE()-3 AS DATE)
					AND (CantidadEntregada IS NULL OR CantidadEntregada = 0.00)
					AND PRMP.Activo = @Activo
					AND AIL.Lote = @Lote
	SET NOCOUNT OFF;
END

GO

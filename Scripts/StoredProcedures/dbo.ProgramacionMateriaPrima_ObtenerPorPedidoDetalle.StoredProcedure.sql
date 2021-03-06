USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerPorPedidoDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerPorPedidoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ObtenerPorPedidoDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 12/06/2014
-- Description: Obtiene el detalle del pedido ingresado
-- SpName     : EXEC ProgramacionMateriaPrima_ObtenerPorPedidoDetalle 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ObtenerPorPedidoDetalle]
@PedidoDetalleId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ProgramacionMateriaPrimaID, PedidoDetalleID, OrganizacionID, AlmacenID, InventarioLoteIDOrigen,
	CantidadProgramada, CantidadEntregada, Observaciones, Justificacion, FechaProgramacion, Activo
	FROM ProgramacionMateriaPrima WHERE PedidoDetalleID = @PedidoDetalleId AND Activo = 1
	SET NOCOUNT OFF;
END

GO

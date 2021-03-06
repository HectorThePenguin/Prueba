USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelacionMovimiento_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CancelacionMovimiento_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CancelacionMovimiento_ObtenerPorID]
@CancelacionMovimientoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CancelacionMovimientoID,
		TipoCancelacionID,
		PedidoID,
		Ticket,
		AlmacenMovimientoIDOrigen,
		AlmacenMovimientoIDCancelado,
		FechaCancelacion,
		Justificacion,
		Activo
	FROM CancelacionMovimiento
	WHERE CancelacionMovimientoID = @CancelacionMovimientoID
	SET NOCOUNT OFF;
END

GO

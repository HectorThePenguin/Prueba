USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelacionMovimiento_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CancelacionMovimiento_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[CancelacionMovimiento_ObtenerTodos]
@Activo BIT = NULL
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
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

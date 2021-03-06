USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelacionMovimiento_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CancelacionMovimiento_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CancelacionMovimiento_Actualizar]
@CancelacionMovimientoID int,
@TipoCancelacionID int,
@PedidoID int,
@Ticket int,
@AlmacenMovimientoIDOrigen bigint,
@AlmacenMovimientoIDCancelado bigint,
@FechaCancelacion smalldatetime,
@Justificacion varchar(255),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	if @PedidoID = 0 begin set @PedidoID = null end
	if @Ticket = 0 begin set @Ticket = null end
	SET NOCOUNT ON;
	UPDATE CancelacionMovimiento SET
		TipoCancelacionID = @TipoCancelacionID,
		PedidoID = @PedidoID,
		Ticket = @Ticket,
		AlmacenMovimientoIDOrigen = @AlmacenMovimientoIDOrigen,
		AlmacenMovimientoIDCancelado = @AlmacenMovimientoIDCancelado,
		FechaCancelacion = @FechaCancelacion,
		Justificacion = @Justificacion,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE CancelacionMovimientoID = @CancelacionMovimientoID
	SET NOCOUNT OFF;
END

GO

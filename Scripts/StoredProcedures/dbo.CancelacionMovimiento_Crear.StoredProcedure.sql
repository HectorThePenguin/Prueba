USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelacionMovimiento_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CancelacionMovimiento_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CancelacionMovimiento_Crear
--======================================================
CREATE PROCEDURE [dbo].[CancelacionMovimiento_Crear] @TipoCancelacionID INT
	,@PedidoID INT
	,@Ticket INT
	,@AlmacenMovimientoIDOrigen BIGINT
	,@AlmacenMovimientoIDCancelado BIGINT
	,@Justificacion VARCHAR(255)
	,@Activo BIT
	,@UsuarioCreacionID INT
AS
BEGIN
	IF @PedidoID = 0
	BEGIN
		SET @PedidoID = NULL
	END
	IF @Ticket = 0
	BEGIN
		SET @Ticket = NULL
	END
	SET NOCOUNT ON;
	INSERT CancelacionMovimiento (
		TipoCancelacionID
		,PedidoID
		,Ticket
		,AlmacenMovimientoIDOrigen
		,AlmacenMovimientoIDCancelado
		,FechaCancelacion
		,Justificacion
		,Activo
		,UsuarioCreacionID
		,FechaCreacion
		)
	VALUES (
		@TipoCancelacionID
		,@PedidoID
		,@Ticket
		,@AlmacenMovimientoIDOrigen
		,@AlmacenMovimientoIDCancelado
		,GETDATE()
		,@Justificacion
		,@Activo
		,@UsuarioCreacionID
		,GETDATE()
		)
	SELECT CancelacionMovimientoID
		,TipoCancelacionID
		,PedidoID
		,Ticket
		,AlmacenMovimientoIDOrigen
		,AlmacenMovimientoIDCancelado
		,FechaCancelacion
		,Justificacion
		,Activo
		,UsuarioCreacionID
		,FechaCreacion
	FROM CancelacionMovimiento
	WHERE CancelacionMovimientoID = SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

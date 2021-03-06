USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ActualizarEstatusPedido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_ActualizarEstatusPedido]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_ActualizarEstatusPedido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jesus Alvarez
-- Fecha: 25/06/2014
-- Descripci�n:	Actualiza el estatus del pedido
-- EXEC Pedido_ActualizarEstatusPedido
-- =============================================
CREATE PROCEDURE [dbo].[Pedido_ActualizarEstatusPedido]
@PedidoID INT,
@EstatusID INT,
@UsuarioModificacionID INT
AS
BEGIN
SET NOCOUNT ON;
	UPDATE Pedido
	SET EstatusID = @EstatusID,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	FROM Pedido 
	WHERE PedidoID = @PedidoID
SET NOCOUNT OFF;
END

GO

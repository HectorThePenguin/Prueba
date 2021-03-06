USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionMateriaPrima_ActualizarEstatusPedido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionMateriaPrima_ActualizarEstatusPedido]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionMateriaPrima_ActualizarEstatusPedido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Roque Solis
-- Fecha: 2014-06-15
-- Descripci�n:	Actualiza el estatus del pedido
-- EXEC RecepcionMateriaPrima_ActualizarEstatusPedido 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionMateriaPrima_ActualizarEstatusPedido]
@PedidoID INT,
@EstatusID INT,
@UsuarioModificacion INT
AS
BEGIN
SET NOCOUNT ON;
	UPDATE Pedido
	SET EstatusID = @EstatusID,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacion
	FROM Pedido 
	WHERE PedidoID = @PedidoID
SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_Actualizar]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_Actualizar]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Actualizar pedido ganado.
-- Origen: APInterfaces
-- PedidoGanado_Actualizar 1,1,1,1,1,1,1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanado_Actualizar]
	@PedidoGanadoID INT,
	@CabezasPromedio INT,
	@Lunes INT,
	@Martes INT,
	@Miercoles INT,
	@Jueves INT,
	@Viernes INT,
	@Sabado INT,
	@Domingo INT,
	@UsuarioModificacionID INT
AS
  BEGIN
      SET NOCOUNT ON
	UPDATE PedidoGanado SET 
		CabezasPromedio = @CabezasPromedio,
		Lunes = @Lunes,
		Martes = @Martes,
		Miercoles = @Miercoles,
		Jueves = @Jueves,
		Viernes = @Viernes,
		Sabado = @Sabado,
		Domingo = @Domingo,
		UsuarioModificacionID = @UsuarioModificacionID, 
		FechaModificacion = GETDATE()
	WHERE PedidoGanadoID = @PedidoGanadoID
	SET NOCOUNT OFF
  END
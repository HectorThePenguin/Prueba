USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanadoEspejo_ActualizarEstatus]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanadoEspejo_ActualizarEstatus]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanadoEspejo_ActualizarEstatus]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Metodo para actualizar Estatus de una solicitud de captura de pedido
-- Origen: APInterfaces
-- PedidoGanadoEspejo_ActualizarEstatus 1,1,1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanadoEspejo_ActualizarEstatus]
	@PedidoGanadoEspejoID INT,
	@UsuarioModificacionID INT,
	@UsuarioAproboID INT,
	@Estatus BIT,
	@Activo BIT
AS
  BEGIN
      SET NOCOUNT ON
	UPDATE PedidoGanadoEspejo SET 
		UsuarioModificacionID = @UsuarioModificacionID, 
		FechaModificacion = GETDATE(),
		UsuarioAproboID = @UsuarioAproboID,
		Estatus = @Estatus,
		Activo =@Activo
	WHERE PedidoGanadoEspejoID = @PedidoGanadoEspejoID
	SET NOCOUNT OFF
  END
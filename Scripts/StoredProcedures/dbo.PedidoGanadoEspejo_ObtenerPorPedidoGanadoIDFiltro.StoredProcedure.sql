USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanadoEspejo_ObtenerPorPedidoGanadoIDFiltro]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanadoEspejo_ObtenerPorPedidoGanadoIDFiltro]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanadoEspejo_ObtenerPorPedidoGanadoIDFiltro]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Obtiene los pedidos ganados espejos de un pedido ganado por su ID y Activo
-- Origen: APInterfaces
-- PedidoGanadoEspejo_ObtenerPorPedidoGanadoIDFiltro 1,1
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanadoEspejo_ObtenerPorPedidoGanadoIDFiltro]
	@PedidoGanadoID INT, 
	@Activo BIT
AS
  BEGIN
      SET NOCOUNT ON
			
	SELECT PedidoGanadoEspejoID,
			PedidoGanadoID,
			OrganizacionID, 
			CabezasPromedio,
			FechaInicio,
			Lunes,
			Martes,
			Miercoles,
			Jueves,
			Viernes,
			Sabado,
			Domingo,
			FechaCreacion, 
			UsuarioCreacionID,
			UsuarioSolicitanteID,
			UsuarioAproboID,
			Justificacion,
			Activo,
			Estatus
	FROM PedidoGanadoEspejo 
	WHERE PedidoGanadoID = @PedidoGanadoID AND Activo = @Activo
	SET NOCOUNT OFF
  END
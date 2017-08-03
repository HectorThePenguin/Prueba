USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_ObtenerPorSemana]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanado_ObtenerPorSemana]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_ObtenerPorSemana]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Obtiene el pedido de ganado dada una organización y una fecha inicial.
-- Origen: APInterfaces
-- PedidoGanado_ObtenerPorSemana 1,'2017-01-01'
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanado_ObtenerPorSemana]
	@OrganizacionID INT, 
	@FechaInicio DATE
AS
  BEGIN
      SET NOCOUNT ON
			
	SELECT OrganizacionID, 
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
			PedidoGanadoID
	FROM PedidoGanado 
	WHERE OrganizacionID = @OrganizacionID AND CAST(FechaInicio AS DATE) = @FechaInicio 
	SET NOCOUNT OFF
  END
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_ObtenerNumeroSemana]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[PedidoGanado_ObtenerNumeroSemana]
GO
/****** Object:  StoredProcedure [dbo].[PedidoGanado_ObtenerNumeroSemana]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description:  Función para obtener el numero de la semana data una fecha en especifica
-- Origen: APInterfaces
-- PedidoGanado_ObtenerNumeroSemana '2017-01-01'
-- =============================================
CREATE PROCEDURE [dbo].[PedidoGanado_ObtenerNumeroSemana]
	@FechaInicial DATE
AS
  BEGIN
      SET NOCOUNT ON
			
	SELECT (DATEPART(WW, @FechaInicial)) AS Semana
	SET NOCOUNT OFF
  END
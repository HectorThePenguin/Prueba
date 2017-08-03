USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroSemana_ObtenerPorDescripcion]    Script Date: 23/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ParametroSemana_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[ParametroSemana_ObtenerPorDescripcion]    Script Date: 23/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Luis Manuel Garcia Lopez
-- Create date: 29/05/2017
-- Description: Obtener el parametro semana configurado por descripcion
-- Origen: APInterfaces
-- ParametroSemana_ObtenerPorDescripcion 'Pedido'
-- =============================================
CREATE PROCEDURE [dbo].[ParametroSemana_ObtenerPorDescripcion]
	@Descripcion VARCHAR(50)
AS
  BEGIN
      SET NOCOUNT ON
			
	SELECT ParametroSemanaID,
			Descripcion,
			Lunes,
			Martes,
			Miercoles,
			Jueves,
			Viernes,
			Sabado,
			Domingo,
			FechaCreacion, 
			UsuarioCreacionID
	FROM ParametroSemana 
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF
  END
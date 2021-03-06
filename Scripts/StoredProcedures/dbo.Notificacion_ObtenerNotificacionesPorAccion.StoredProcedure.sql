USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Notificacion_ObtenerNotificacionesPorAccion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Notificacion_ObtenerNotificacionesPorAccion]
GO
/****** Object:  StoredProcedure [dbo].[Notificacion_ObtenerNotificacionesPorAccion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque Solis
-- Origen:      Apinterfaces
-- Create date: 07/04/2014
-- Description:	Obtiene las notificaciones de una accion.
-- EXEC [Notificacion_ObtenerNotificacionesPorAccion] 1, 1
--=============================================
CREATE PROCEDURE [dbo].[Notificacion_ObtenerNotificacionesPorAccion]
@AccionesSiapID INT,
@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	    
		NotificacionID,
		AccionesSiapID,
		UsuarioDestino
	FROM Notificaciones
	WHERE  AccionesSiapID = @AccionesSiapID
	AND Activo = @Activo
	SET NOCOUNT OFF;
END

GO

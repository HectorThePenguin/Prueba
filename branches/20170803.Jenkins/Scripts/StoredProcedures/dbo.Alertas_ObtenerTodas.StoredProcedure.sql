USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Alertas_ObtenerTodas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Alertas_ObtenerTodas]
GO
/****** Object:  StoredProcedure [dbo].[Alertas_ObtenerTodas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 14/03/2016
-- Description: Obtiene todas las alertas activas
-- Alertas_ObtenerTodas 
--=============================================
CREATE PROCEDURE [dbo].[Alertas_ObtenerTodas]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	A.AlertaID, 
			A.Descripcion, 
			A.HorasRespuesta, 
			A.TerminadoAutomatico, 
			A.Activo

		FROM Alerta (NOLOCK)  A 
			WHERE A.Activo = 1
	SET NOCOUNT OFF;
END


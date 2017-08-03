USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlertasConfiguracion_ObtenerTodasActivas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlertasConfiguracion_ObtenerTodasActivas]
GO
/****** Object:  StoredProcedure [dbo].[AlertasConfiguracion_ObtenerTodasActivas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 14/03/2016
-- Description: Obtiene todas las alertas activas
-- AlertasConfiguracion_ObtenerTodasActivas 
--=============================================
CREATE PROCEDURE [dbo].[AlertasConfiguracion_ObtenerTodasActivas] 
@Activo bit 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	A.AlertaID, 
			A.Descripcion, 
			A.HorasRespuesta, 
			A.TerminadoAutomatico, 
			A.Activo, 
			AC.AlertaConfiguracionID,
			AC.Datos,
			AC.Fuentes,
			AC.Condiciones,
			AC.Agrupador,
			AC.NivelAlertaID

		FROM Alerta (NOLOCK)  A 
		INNER JOIN AlertaConfiguracion (NOLOCK)  AC ON AC.AlertaID = A.AlertaID
			WHERE A.Activo = @Activo AND AC.Activo = @Activo
	SET NOCOUNT OFF;
END
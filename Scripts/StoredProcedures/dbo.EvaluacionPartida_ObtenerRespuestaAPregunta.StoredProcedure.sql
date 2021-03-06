USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionPartida_ObtenerRespuestaAPregunta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EvaluacionPartida_ObtenerRespuestaAPregunta]
GO
/****** Object:  StoredProcedure [dbo].[EvaluacionPartida_ObtenerRespuestaAPregunta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez
-- Create date: 19/05/2014
-- Description:  Obtener respuesta a pregunta de evaluacion de partida
-- EvaluacionPartida_ObtenerRespuestaAPregunta 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[EvaluacionPartida_ObtenerRespuestaAPregunta]
	@EvaluacionID INT,
	@PreguntaID INT
AS
  BEGIN
    SET NOCOUNT ON;
	SELECT Respuesta
	  FROM EvaluacionCorralDetalle 
	 WHERE EvaluacionCorralID = @EvaluacionID 
	   AND PreguntaID = @PreguntaID
	   AND Activo = 1
    SET NOCOUNT OFF;
END

GO

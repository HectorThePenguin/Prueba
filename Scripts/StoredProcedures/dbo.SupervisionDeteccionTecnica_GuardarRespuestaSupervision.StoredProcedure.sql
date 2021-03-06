USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_GuardarRespuestaSupervision]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDeteccionTecnica_GuardarRespuestaSupervision]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_GuardarRespuestaSupervision]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 19/02/2013
-- Description: Registra en detalle de la supervision de tecnica de deteccion
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SupervisionDeteccionTecnica_GuardarRespuestaSupervision]
@SupervisionDetectoresId INT,
@PreguntaId INT,
@Respuesta	INT,
@UsuarioCreacionId INT
AS
BEGIN
INSERT INTO SupervisionDetectoresDetalle (SupervisionDetectoresId, PreguntaId,Respuesta, Activo, FechaCreacion, UsuarioCreacion)
VALUES (@SupervisionDetectoresId, @PreguntaId, @Respuesta, 1, GETDATE(), @UsuarioCreacionId)
END

GO

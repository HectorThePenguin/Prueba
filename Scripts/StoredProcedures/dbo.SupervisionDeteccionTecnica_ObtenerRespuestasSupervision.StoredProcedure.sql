USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_ObtenerRespuestasSupervision]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDeteccionTecnica_ObtenerRespuestasSupervision]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_ObtenerRespuestasSupervision]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 19/02/2013
-- Description: Registra en el encabezado de la supervision de tecnica de deteccion
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SupervisionDeteccionTecnica_ObtenerRespuestasSupervision]
@SupervisionDeteccionId INT
AS
BEGIN
SELECT SD.SupervisionDetectoresDetalleId, SD.SupervisionDetectoresID, SD.PreguntaId, SD.Respuesta, SD.ACtivo, 
	SD.FechaCreacion, SD.UsuarioCreacion, PR.Descripcion, PR.Valor, PR.TipoPreguntaId
FROM SupervisionDetectoresDetalle SD
INNER JOIN Pregunta PR ON PR.PreguntaID = SD.PreguntaID
where SD.SupervisionDetectoresID = @SupervisionDeteccionId  AND SD.Activo = 1 
ORDER BY SD.PreguntaId
END

GO

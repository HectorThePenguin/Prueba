USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_ObtenerActivas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_ObtenerActivas]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_ObtenerActivas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 14/03/2016
-- Description: Obtiene todas las Incidencia activas
-- Incidencia_ObtenerActivas  1,59
--=============================================
CREATE PROCEDURE [dbo].[Incidencia_ObtenerActivas] 
@Activo bit ,
@EstatusID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	I.IncidenciaID, 
			COALESCE(I.OrganizacionID,0) AS OrganizacionID,
			A.AlertaID,
			A.HorasRespuesta,
			A.TerminadoAutomatico,
			COALESCE(I.Folio,0) AS Folio, 
			I.XmlConsulta, 
			I.Fecha, 
			I.EstatusID, 
			I.FechaVencimiento, 
			ISNULL(I.Comentarios, '') AS Comentarios ,
			I.NivelAlertaID,
			ISNULL(I.AccionID, 0) AS AccionID ,
			I.Activo, 
			I.FechaCreacion, 
			ISNULL(I.UsuarioResponsableID, 0) AS UsuarioResponsableID 
		FROM Incidencia I
		INNER JOIN Alerta AS A ON A.AlertaID = I.AlertaID 
			--WHERE I.EstatusID <> @EstatusID AND I.Activo = @Activo AND I.OrganizacionID IS NOT NULL
	SET NOCOUNT OFF;
END
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerPermisoTrampa]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerPermisoTrampa]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerPermisoTrampa]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Edgar.Villarreal
-- Create date: 2014/01/12
-- Description: SP para consultar permiso
-- Origen     : APInterfaces
-- EXEC CorteTransferenciaGanado_ObtenerPermisoTrampa 1,'M','HostName'
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerPermisoTrampa]
	@OrganizacionID INT,
	@TipoTrampa CHAR(1),
	@HostName VARCHAR(50)
AS
BEGIN
	SELECT 		
		TR.TrampaID,
		TR.Descripcion,
		TR.OrganizacionID,
		TR.TipoTrampa,
		TR.HostName,
		TR.Activo,
		TR.FechaCreacion,
		TR.UsuarioCreacionID,
		TR.FechaModificacion,
		TR.UsuarioModificacionID
	FROM Trampa TR
	INNER JOIN ParametroTrampa AS PR ON PR.TrampaID=TR.TrampaID
	INNER JOIN Parametro AS P ON P.ParametroID=PR.ParametroID
	INNER JOIN TipoParametro AS TP ON TP.TipoParametroID=P.TipoParametroID
	WHERE OrganizacionID = @OrganizacionID
	  AND TR.TipoTrampa = @TipoTrampa 
	  AND TR.HostName = @HostName
END

GO

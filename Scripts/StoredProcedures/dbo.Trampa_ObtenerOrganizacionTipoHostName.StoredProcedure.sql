USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerOrganizacionTipoHostName]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_ObtenerOrganizacionTipoHostName]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerOrganizacionTipoHostName]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2013/12/19
-- Description: SP para obtener una trampa
-- Origen     : APInterfaces
-- EXEC Trampa_ObtenerOrganizacionTipoHostName 1,'M','HostName'
-- =============================================
CREATE PROCEDURE [dbo].[Trampa_ObtenerOrganizacionTipoHostName]
	@OrganizacionID INT,
	@TipoTrampa CHAR(1),
	@HostName VARCHAR(50)
AS
BEGIN
	SELECT 
		TrampaID,
		Descripcion,
		OrganizacionID,
		TipoTrampa,
		HostName,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM Trampa 
	WHERE OrganizacionID = @OrganizacionID
	  AND TipoTrampa = @TipoTrampa 
	  AND HostName = @HostName
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoGanado_ObtenerCorralPorCodigoGrupo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TraspasoGanado_ObtenerCorralPorCodigoGrupo]
GO
/****** Object:  StoredProcedure [dbo].[TraspasoGanado_ObtenerCorralPorCodigoGrupo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jesus.Alvarez
-- Create date: 2014-02-14
-- Origen: APInterfaces
-- Description:	Obtiene un Corral de un grupo determinado.
-- EXEC Corral_ObtenerCorralPorCodigoGrupo '001', 4, 1
--=============================================
CREATE PROCEDURE [dbo].[TraspasoGanado_ObtenerCorralPorCodigoGrupo]
	@Codigo CHAR(10),
	@OrganizacionID INT,
    @GrupoCorral INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
		C.CorralID,
		C.OrganizacionID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		C.MetrosLargo,
		C.MetrosAncho,
		C.Seccion,
		C.Orden,
		C.Activo,
		C.FechaCreacion,
		TC.GrupoCorralID,
		C.UsuarioCreacionID
	FROM Corral C (NOLOCK) 
	INNER JOIN TipoCorral TC ON C.TipoCorralID= TC.TipoCorralID
	WHERE Codigo = @Codigo
	AND C.OrganizacionID = @OrganizacionID
    AND TC.GrupoCorralID = @GrupoCorral
	SET NOCOUNT OFF;
END

GO

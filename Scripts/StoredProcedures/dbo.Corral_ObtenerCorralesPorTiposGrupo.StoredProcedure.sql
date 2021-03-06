USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorTiposGrupo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralesPorTiposGrupo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorTiposGrupo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-02-14
-- Origen: 		APInterfaces
-- Description:	Obtiene un lista de corrales por grupo y tipos proporcionado
-- EXEC Corral_ObtenerCorralesPorTiposGrupo 3,1,
-- '<ROOT>
-- 		<TiposCorral><TipoCorralID>7</TipoCorralID></TiposCorral>
-- 		<TiposCorral><TipoCorralID>8</TipoCorralID></TiposCorral>
-- </ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralesPorTiposGrupo]
    @GrupoCorral INT,
	@OrganizacionID INT,
	@XmlTiposCorral XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TiposCorral AS TABLE ([TipoCorralID] INT)
	INSERT @TiposCorral ([TipoCorralID])
	SELECT [TipoCorralID] = t.item.value('./TipoCorralID[1]', 'INT')
	FROM @XmlTiposCorral.nodes('ROOT/TiposCorral') AS t(item)
	SELECT 
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
	WHERE C.OrganizacionID = @OrganizacionID
    AND TC.GrupoCorralID = @GrupoCorral
    AND C.TipoCorralID IN (SELECT TipoCorralID FROM @TiposCorral);
	SET NOCOUNT OFF;
END

GO

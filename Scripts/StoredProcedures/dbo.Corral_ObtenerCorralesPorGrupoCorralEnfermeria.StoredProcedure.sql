USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorGrupoCorralEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralesPorGrupoCorralEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorGrupoCorralEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Ramses Santos
-- Create date: 2014-04-03
-- Origen: APInterfaces
-- Description:	Obtiene un Corral de un grupo Enfermeria.
-- EXEC Corral_ObtenerCorralesPorGrupoCorralEnfermeria 1, 4
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralesPorGrupoCorralEnfermeria]
	@GrupoCorralID INT,
	@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	Select C.CorralID, C.OrganizacionID, C.Codigo, GC.GrupoCorralID, C.TipoCorralID, C.Capacidad, C.MetrosLargo,
	C.MetrosAncho, C.Seccion, C.Orden, C.Activo
	from Corral (NOLOCK) AS C 
	INNER JOIN Lote (NOLOCK) AS L ON (L.CorralID = C.CorralID)
	INNER JOIN TipoCorral (NOLOCK) AS TC ON (C.TipoCorralID = TC.TipoCorralID)
	INNER JOIN GrupoCorral (NOLOCK) AS GC ON (TC.GrupoCorralID = GC.GrupoCorralID)
	/*INNER JOIN ServicioAlimento (NOLOCK) AS SA ON (SA.CorralID = C.CorralID)*/
	WHERE GC.GrupoCorralID = @GrupoCorralID AND C.OrganizacionID = @OrganizacionID
	AND TC.Activo = 1 AND GC.Activo = 1 AND L.Activo = 1 AND C.Activo = 1 /*AND SA.Activo = 1*/
	SET NOCOUNT OFF;
END

GO

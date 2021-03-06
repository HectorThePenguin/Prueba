USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerProblemasDeteccionUltimaDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerProblemasDeteccionUltimaDeteccion]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerProblemasDeteccionUltimaDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Cesar Valdez
-- Create date: 18/02/2014
-- Origen: APInterfaces
-- Description:	Obtiene los problemas de una deteccion.
-- Enfermeria_ObtenerProblemasDeteccionUltimaDeteccion 53340;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerProblemasDeteccionUltimaDeteccion] @DetecionID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT COUNT(pr.ProblemaID) [contador]
		,pr.ProblemaID
		,pr.Descripcion
		,pr.TipoProblemaID
	FROM DeteccionAnimal dt(NOLOCK)
	INNER JOIN DeteccionSintomaAnimal ds(NOLOCK) ON dt.DeteccionAnimalID = ds.DeteccionAnimalID
		AND ds.Activo = 1
	INNER JOIN Sintoma si ON ds.SintomaID = si.SintomaID
		AND si.Activo = 1
	INNER JOIN ProblemaSintoma ps ON ds.SintomaID = ps.SintomaID
		AND ps.Activo = 1
	LEFT OUTER JOIN DiagnosticoAnalista DA
		ON (DT.DeteccionAnimalID = DA.DeteccionAnimalID)
	LEFT OUTER JOIN DiagnosticoAnalistaDetalle DAT
		ON (DA.DiagnosticoAnalistaID = DAT.DiagnosticoAnalistaID)
	INNER JOIN Problema pr ON ISNULL(DAT.ProblemaID, ps.ProblemaID) = pr.ProblemaID
		AND pr.Activo = 1
	WHERE ds.DeteccionAnimalID = @DetecionID
		AND dt.Activo = 1
	GROUP BY pr.ProblemaID
		,pr.Descripcion
		,pr.TipoProblemaID

	SET NOCOUNT OFF;
END

GO

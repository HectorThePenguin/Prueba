USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerProblemasDeteccionSinActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerProblemasDeteccionSinActivo]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerProblemasDeteccionSinActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Roque Solis
-- Create date: 18/02/2014
-- Origen: APInterfaces
-- Description:	Obtiene los problemas de una deteccion.
-- execute Enfermeria_ObtenerProblemasDeteccionSinActivo 1;
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerProblemasDeteccionSinActivo]
	@DetecionID INT,
	@EstatusDeteccion INT
AS

BEGIN	

SET NOCOUNT ON;

    IF @EstatusDeteccion = 1
	BEGIN
		SELECT 
			COUNT(pr.ProblemaID)[contador],
			pr.ProblemaID,      
			pr.Descripcion,
			pr.TipoProblemaID
		FROM DeteccionAnimal dt(NOLOCK)
		INNER JOIN DeteccionSintomaAnimal ds(NOLOCK) ON dt.DeteccionAnimalID = ds.DeteccionAnimalID AND ds.Activo = 1
		INNER JOIN Sintoma si ON ds.SintomaID = si.SintomaID AND si.Activo = 1
		INNER JOIN ProblemaSintoma ps ON ds.SintomaID = ps.SintomaID AND ps.Activo = 1
		INNER JOIN Problema pr ON ps.ProblemaID = pr.ProblemaID AND pr.Activo = 1
		WHERE ds.DeteccionAnimalID = @DetecionID
		AND dt.Activo = 1
		GROUP BY pr.ProblemaID,pr.Descripcion,pr.TipoProblemaID
	END
	ELSE 
	BEGIN
		SELECT 
			COUNT(pr.ProblemaID)[contador],
			pr.ProblemaID,      
			pr.Descripcion,
			pr.TipoProblemaID
		FROM Deteccion dt
		INNER JOIN DeteccionSintoma ds ON dt.DeteccionID = ds.DeteccionID AND ds.Activo = 1
		INNER JOIN Sintoma si ON ds.SintomaID = si.SintomaID AND si.Activo = 1
		INNER JOIN ProblemaSintoma ps ON ds.SintomaID = ps.SintomaID AND ps.Activo = 1
		INNER JOIN Problema pr ON ps.ProblemaID = pr.ProblemaID AND pr.Activo = 1
		WHERE ds.DeteccionID = @DetecionID
		/*AND dt.Activo = 1*/
		GROUP BY pr.ProblemaID,pr.Descripcion,pr.TipoProblemaID
	
	END
	
	
	
	SET NOCOUNT OFF;	

END
GO

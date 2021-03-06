USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerSintomas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerSintomas]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerSintomas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 17/02/2014
-- Description:	Obtiene los sintomas de un problema.
-- [DeteccionGanado_ObtenerSintomas] 1,1
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerSintomas]
@ProblemaID INT
AS
BEGIN
	SELECT S.SintomaID,S.Descripcion,S.Activo
	FROM Problema (NOLOCK) P
	INNER JOIN ProblemaSintoma (NOLOCK) PS
	ON (P.ProblemaID = PS.ProblemaID)
	INNER JOIN Sintoma (NOLOCK) S
	ON (PS.SintomaID = S.SintomaID)
	WHERE PS.ProblemaID = @ProblemaID
END

GO

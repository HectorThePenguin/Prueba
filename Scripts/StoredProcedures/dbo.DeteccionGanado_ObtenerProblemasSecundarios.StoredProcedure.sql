USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerProblemasSecundarios]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerProblemasSecundarios]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerProblemasSecundarios]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 17/02/2014
-- Description:	Obtiene los problemas secundarios que se 
-- muestran en el grid de la opcion Ver.
-- [DeteccionGanado_ObtenerProblemasSecundarios]
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerProblemasSecundarios]
AS
BEGIN
	SELECT ProblemaId,Descripcion 
	FROM Problema (NOLOCK)
	WHERE TipoProblemaID = 2 AND Activo = 1
END

GO

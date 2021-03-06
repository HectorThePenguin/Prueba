USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerCorral]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 01/03/2014
-- Description:	Obtiene el tipo de corral y la cantidad de lotes.
-- [DeteccionGanado_ObtenerCorral] 1
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerCorral]
@OrganizacionID INT,
@Codigo VARCHAR(10)
AS
BEGIN
	SELECT 	    
		C.CorralID,
		C.Codigo,
		C.TipoCorralID,
		C.Activo,
		TC.GrupoCorralID
	FROM Corral (NOLOCK) C 
	INNER JOIN TipoCorral (NOLOCK) TC
	ON (C.TipoCorralID = TC.TipoCorralID AND C.Activo = 1)
	WHERE Codigo = @Codigo AND C.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerDetectorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerDetectorCorral]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerDetectorCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 17/02/2014
-- Description:	Obtiene el numero de partida disponible.
-- [DeteccionGanado_ObtenerDetectorCorral] 1,1
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerDetectorCorral]
@OrganizacionID INT,
@CorralID INT,
@OperadorID INT
AS 
BEGIN
	SELECT O.OperadorID,O.Nombre,O.ApellidoPaterno,O.ApellidoMaterno,O.CodigoSAP,
				 O.RolID,O.OrganizacionID,O.Activo
	FROM Corral C
	INNER JOIN CorralDetector CD ON (C.CorralID = CD.CorralID)
	INNER JOIN Operador O ON (CD.OperadorID = O.OperadorID)
	WHERE C.CorralID = @CorralID 
			AND C.OrganizacionID = @OrganizacionID
			AND O.OperadorID = @OperadorID
			AND C.Activo = 1 
			AND CD.Activo = 1 
			AND O.Activo = 1
END

GO

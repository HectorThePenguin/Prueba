USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorTipoCorralDetector]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralesPorTipoCorralDetector]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorTipoCorralDetector]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		C�sar Valdez
-- Create date: 2014-08-16
-- Origen: 		APInterfaces
-- Description:	Obtiene un lista de corrales por tipo proporcionado y que no estan asignados a un detector
-- EXEC Corral_ObtenerCorralesPorTipoCorralDetector 3,1,
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralesPorTipoCorralDetector]
	@OrganizacionID INT,
	@TipoCorralID INT,
	@OperadorID INT
AS
BEGIN
	SET NOCOUNT ON;
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
    AND C.TipoCorralID = @TipoCorralID 
	/*
	AND (
			EXISTS( SELECT CorralID
					  FROM CorralDetector CD
				     WHERE CD.Activo = 1
				  	   AND CD.CorralID = C.CorralID
					   AND CD.OperadorID = @OperadorID
			) 
		OR
			NOT EXISTS( SELECT CorralID
						  FROM CorralDetector CD
					     WHERE CD.Activo = 1
						   AND CD.CorralID = C.CorralID
						   AND CD.OperadorID != @OperadorID
			)
		)
	*/
	SET NOCOUNT OFF;
END

GO

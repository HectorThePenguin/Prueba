USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorTipo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralesPorTipo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorTipo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		César Valdez
-- Create date: 2014-08-15
-- Origen: 		APInterfaces
-- Description:	Obtiene un lista de corrales por tipo proporcionado
-- EXEC Corral_ObtenerCorralesPorTipo 3,1,
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralesPorTipo]
	@OrganizacionID INT,
	@TipoCorralID INT
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
    and c.Activo = 1
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerCorralesReparto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerCorralesReparto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerCorralesReparto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/21
-- Description: SP para obtener los corrales validos para reparto
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerCorralesReparto 1
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerCorralesReparto]
@OrganizacionID INT
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
		C.FechaCreacion,
		C.Activo,
		TC.GrupoCorralID
	FROM Corral C (NOLOCK)
	INNER JOIN TipoCorral TC (NOLOCK) ON TC.TipoCorralID=C.TipoCorralID 
	LEFT JOIN Lote L (NOLOCK) ON C.CorralID=L.CorralID AND L.Activo = 1	
	WHERE C.OrganizacionID = @OrganizacionID
    AND C.Activo = 1	
	ORDER BY C.Seccion
SET NOCOUNT OFF;
END

GO

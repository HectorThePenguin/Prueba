USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralRango_ObtenerPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CorralRango_ObtenerPorOrganizacionID]
	@OrganizacionID INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT DISTINCT c.CorralID, c.Codigo
	FROM  Corral c
	INNER JOIN ServicioAlimento sa ON c.CorralID = sa.CorralID
	LEFT  JOIN Lote l ON c.CorralID = l.CorralID AND l.Activo = 1
	WHERE c.OrganizacionID = @OrganizacionID AND sa.Activo = 1 
		  /*AND NOT EXISTS(SELECT CorralID 
					     FROM CorralRango 
				         WHERE CorralID = c.CorralID 
						 AND OrganizacionID = @OrganizacionID 
						 AND Activo = 1)	*/
	AND l.LoteID IS NULL
SET NOCOUNT OFF;	
END

GO

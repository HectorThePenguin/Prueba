USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jose Gilberto Quintero Lopez  
-- Create date: 15/10/2013  
-- Description:  Obtener listado de Organizaciones  
-- Organizacion_ObtenerPorID 1  
-- =============================================  
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorID]  
@OrganizacionID int  
AS  
BEGIN  
	SET NOCOUNT ON;  
		SELECT 
			O.OrganizacionID,  
			O.TipoOrganizacionID,  
			O.Descripcion,  
			O.Activo,  
			O.Direccion,  
			TOO.Descripcion AS TipoOrganizacion,
			ISNULL(O.Division,'') AS Division,
			ISNULL(Mon.Abreviatura,'') AS Moneda,
			ISNULL(SOC.Codigo,0) AS Sociedad
		FROM Organizacion O  
		INNER JOIN TipoOrganizacion TOO  
		ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID)  
		LEFT JOIN Sociedad SOC ON (O.Sociedad = SOC.Codigo)
		LEFT JOIN Moneda Mon ON (Mon.MonedaId = SOC.MonedaId)
		WHERE O.OrganizacionID = @OrganizacionID  
	SET NOCOUNT OFF;  
END

GO


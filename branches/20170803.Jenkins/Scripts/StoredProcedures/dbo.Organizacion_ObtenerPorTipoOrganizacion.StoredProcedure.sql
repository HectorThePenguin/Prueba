USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorTipoOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorTipoOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorTipoOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 19/03/2016
-- Description:  Obtener listado de Organizaciones
-- Organizacion_ObtenerPorTipoOrganizacion 1,2
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorTipoOrganizacion]
@Activo BIT,
@TipoOrganizacionID INT
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT 
		OrganizacionID,  
		TipoOrganizacionID,  
		Descripcion,  
		Activo,  
		ISNULL(Direccion, '') AS Direccion,
		Division
	FROM Organizacion  
	WHERE Activo = @Activo AND TipoOrganizacionID = @TipoOrganizacionID 
	SET NOCOUNT OFF;  
END

GO

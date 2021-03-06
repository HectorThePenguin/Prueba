USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Organizaciones
-- Organizacion_ObtenerTodos 1
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerTodos]
@Activo BIT = NULL  
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
	WHERE Activo = @Activo OR @Activo IS NULL  
	SET NOCOUNT OFF;  
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerSeccionesCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerSeccionesCorral]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerSeccionesCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Luis.Velazquez	
-- Create date: 13/10/2014
-- Description: SP para consultar las secciones que hay en los corrales
-- EXEC Corral_ObtenerSeccionesCorral 1
-- =============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerSeccionesCorral] @OrganizacionID INT	
AS
BEGIN
	select distinct 
	Seccion
	from Corral 
	where OrganizacionID = @OrganizacionID
	and Activo = 1
	order by Seccion
END

GO

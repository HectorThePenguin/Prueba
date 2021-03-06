USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerAlmacenPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerAlmacenPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerAlmacenPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 30/06/2014
-- Description:  Obtiene los Almacenes de la Organizaci�n
-- Almacen_ObtenerAlmacenPorOrganizacion 4,1
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerAlmacenPorOrganizacion]
	@OrganizacionID INT
	,@Activo BIT
AS
BEGIN
	SELECT 
	AlmacenID
	,TipoAlmacenID
	,Descripcion AS Almacen
	FROM Almacen
	WHERE OrganizacionID = @OrganizacionID
	AND Activo = @Activo
END	

GO

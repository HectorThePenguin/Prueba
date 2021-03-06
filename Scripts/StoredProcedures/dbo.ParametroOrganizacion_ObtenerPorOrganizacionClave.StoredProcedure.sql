USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorOrganizacionClave]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorOrganizacionClave]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorOrganizacionClave]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 27/03/2014
-- Description: Obtiene un Parametro Organizacion
-- ParametroOrganizacion_ObtenerPorOrganizacionClave 4, 'MesesAtrasReporteInventario'
-- =============================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorOrganizacionClave]
	@OrganizacionID INT,
	@Clave VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT PO.ParametroOrganizacionID
		   , PO.ParametroID
		   , PO.OrganizacionID
		   , PO.Valor
		   , PO.Activo
		   , P.Descripcion AS Parametro
		   , O.Descripcion AS Organizacion
	 FROM ParametroOrganizacion PO (NOLOCK) 
	 INNER JOIN Parametro P(NOLOCK)  
		ON (P.ParametroID = PO.ParametroID
			AND P.Clave = @Clave
			AND PO.OrganizacionID = @OrganizacionID
			AND PO.Activo = 1)
	 INNER JOIN Organizacion O
		ON (PO.OrganizacionID = O.OrganizacionID)
	SET NOCOUNT OFF
END

GO

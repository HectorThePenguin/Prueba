USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerDatosCompra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerDatosCompra]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerDatosCompra]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/12
-- Description: SP para obtener datos de la compra por folio
-- Origen     : APInterfaces
-- EXEC Enfermeria_ObtenerDatosCompra 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerDatosCompra]
	@FolioEntrada INT,
	@OrganizacionID INT
AS
BEGIN
	DECLARE @IdentityID BIGINT;
	SELECT enga.FechaEntrada, 
			too.TipoOrganizacionID, 
			too.Descripcion [TipoOrganizacion],
			org.OrganizacionID,
			org.Descripcion [Proveedor]
		FROM EntradaGanado enga
		INNER JOIN Organizacion org (NOLOCK) ON org.OrganizacionID = enga.OrganizacionID
		INNER JOIN TipoOrganizacion too(NOLOCK) ON org.TipoOrganizacionID = too.TipoOrganizacionID
		WHERE enga.FolioEntrada = @FolioEntrada
		AND org.OrganizacionID = @OrganizacionID
		AND enga.Activo = 1 AND org.Activo = 1
		AND too.Activo = 1 
END

GO

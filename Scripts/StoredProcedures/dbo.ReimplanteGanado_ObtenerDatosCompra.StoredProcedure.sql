USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerDatosCompra]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerDatosCompra]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerDatosCompra]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2013/12/26
-- Description: SP para obtener datos de la compra
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ObtenerDatosCompra 1,1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerDatosCompra] @AnimalID INT
	,@OrganizacionID INT
AS
BEGIN
	DECLARE @IdentityID BIGINT;

	SELECT enga.FechaEntrada
		,too.TipoOrganizacionID
		,too.Descripcion [TipoOrganizacion]
		,org.OrganizacionID
		,org.Descripcion [Proveedor]
		,tpga.Descripcion [TipoAnimal]
	FROM Animal anm(NOLOCK)
	INNER JOIN EntradaGanado enga(NOLOCK) ON anm.FolioEntrada = enga.FolioEntrada
	INNER JOIN Organizacion org(NOLOCK) ON org.OrganizacionID = enga.OrganizacionID
	INNER JOIN TipoOrganizacion too(NOLOCK) ON org.TipoOrganizacionID = too.TipoOrganizacionID
	INNER JOIN TipoGanado tpga(NOLOCK) ON tpga.TipoGanadoID = anm.TipoGanadoID
	WHERE anm.AnimalID = @AnimalID
		AND org.OrganizacionID = @OrganizacionID
		AND anm.Activo = 1
		AND enga.Activo = 1
		AND org.Activo = 1
		AND too.Activo = 1
		AND tpga.Activo = 1
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorIDConIva]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorIDConIva]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorIDConIva]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 04/12/2013
-- Description:  Obtener listado de Organizaciones
-- Organizacion_ObtenerPorIDConIva 4
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorIDConIva]
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT O.OrganizacionID,
		O.TipoOrganizacionID,
		O.Descripcion,
		O.Direccion,
		O.Activo
		, O.Division
		, O.Sociedad
		, I.IvaID
		, I.TasaIva
		, I.IndicadorIvaPagar
		, I.IndicadorIvaRecuperar
		, I.CuentaPagar
		, I.CuentaRecuperar
		, I.Descripcion AS DescripcionIva
		, TP.TipoProcesoID
		, TP.Descripcion AS DescripcionTipoProceso
		, TOO.Descripcion AS TipoOrganizacion
	FROM Organizacion O
	INNER JOIN Iva I
		ON (O.IvaID = I.IvaID)
	INNER JOIN TipoOrganizacion TOO
		ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID)
	INNER JOIN TipoProceso TP
		ON (TOO.TipoProcesoID = TP.TipoProcesoID)
	WHERE OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

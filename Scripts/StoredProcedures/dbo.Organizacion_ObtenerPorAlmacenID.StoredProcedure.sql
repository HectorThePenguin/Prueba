USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 20/07/2013
-- Description:  Obtiene la Organizacion de un Almacen
-- Organizacion_ObtenerPorAlmacenID 1
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorAlmacenID] @AlmacenID INT
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
	INNER JOIN Almacen a on O.OrganizacionID = a.OrganizacionID
	INNER JOIN Iva I
		ON (O.IvaID = I.IvaID)
	INNER JOIN TipoOrganizacion TOO
		ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID)
	INNER JOIN TipoProceso TP
		ON (TOO.TipoProcesoID = TP.TipoProcesoID)
	WHERE a.AlmacenID = @AlmacenID
	SET NOCOUNT OFF;
END

GO

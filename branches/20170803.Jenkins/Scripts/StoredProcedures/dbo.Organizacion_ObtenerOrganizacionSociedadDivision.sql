USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerOrganizacionSociedadDivision]    Script Date: 04/12/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerOrganizacionSociedadDivision]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerOrganizacionSociedadDivision]    Script Date: 04/12/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Hugo Castillo
-- Create date: 04/12/2015
-- Description:  Obtiene la descripcion division y sociedad
-- Organizacion_ObtenerOrganizacionSociedadDivision 1, 2
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerOrganizacionSociedadDivision]
@OrganizacionID INT,
@SociedadID INT
AS
BEGIN
	SET NOCOUNT ON;	
	DECLARE @DivisionIntensivo VARCHAR(4)
	DECLARE @DescripcionIntensivo VARCHAR(50)
	DECLARE @CodigoSociedad INT
	
	SELECT @DivisionIntensivo = Division FROM Organizacion WHERE OrganizacionID = @OrganizacionID 

	SELECT @DescripcionIntensivo = upper(Descripcion), @CodigoSociedad = Codigo  FROM Sociedad WHERE SociedadID = @SociedadID
	
	SELECT O.OrganizacionID,
		O.TipoOrganizacionID,
		RTRIM(@DescripcionIntensivo) + ' (' + RTRIM(@DivisionIntensivo) + ')'  AS TituloPoliza,
		o.Descripcion,
		O.Direccion,
		O.Activo
		, O.Division
		, @CodigoSociedad Sociedad
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

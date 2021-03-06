USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FolioFactura_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FolioFactura_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[FolioFactura_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Ramses Santos
-- Create date: 13/08/2014
-- Description:  Obtiene el folio siguiente para la factura.
-- Origen: APInterfaces
/*
	DECLARE @Factura VARCHAR(15)
	DECLARE @Serie VARCHAR(15)
	DECLARE @Folio VARCHAR(15)
	EXEC FolioFactura_Obtener 1, @Factura OUTPUT, @Serie OUTPUT, @Folio OUTPUT
	SELECT @Factura AS Factura, @Serie AS Serie, @Folio AS Folio
*/
-- =============================================
CREATE PROCEDURE [dbo].[FolioFactura_Obtener]
	@OrganizacionID INT,
	@Factura VARCHAR(15) OUTPUT,
	@SerieFactura VARCHAR(5) OUTPUT,
	@Folio VARCHAR(10) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @parametroOrganizacionID BIGINT
	-- Obtenemos el Valor del ParametroOrganizacion de la factura
	SELECT @Folio = CAST(PO.Valor AS BIGINT) + 1, @parametroOrganizacionID = PO.ParametroOrganizacionID
	 FROM ParametroOrganizacion PO (NOLOCK) 
	 INNER JOIN Parametro P(NOLOCK)  
		ON (P.ParametroID = PO.ParametroID
			AND P.Clave = 'FolioFactura'
			AND PO.OrganizacionID = @OrganizacionID
			AND PO.Activo = 1)
	 INNER JOIN Organizacion O
		ON (PO.OrganizacionID = O.OrganizacionID)
	UPDATE ParametroOrganizacion SET Valor = CAST(@Folio AS VARCHAR) WHERE ParametroOrganizacionID = @parametroOrganizacionID
	SELECT @SerieFactura = PO.Valor
	 FROM ParametroOrganizacion PO (NOLOCK) 
	 INNER JOIN Parametro P(NOLOCK)  
		ON (P.ParametroID = PO.ParametroID
			AND P.Clave = 'SerieFactura'
			AND PO.OrganizacionID = @OrganizacionID
			AND PO.Activo = 1)
	 INNER JOIN Organizacion O
		ON (PO.OrganizacionID = O.OrganizacionID)
	SELECT @Factura = CONCAT(@SerieFactura, CAST(@Folio AS VARCHAR))
	SET NOCOUNT OFF
END	

GO

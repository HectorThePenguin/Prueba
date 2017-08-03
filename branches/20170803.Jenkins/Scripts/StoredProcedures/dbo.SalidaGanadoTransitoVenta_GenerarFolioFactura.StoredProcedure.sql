USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransitoVenta_GenerarFolioFactura]    Script Date: 18/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransitoVenta_GenerarFolioFactura]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransitoVenta_GenerarFolioFactura]    Script Date: 18/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 18/04/2016 4:55:00 a.m.
-- Description: SP para asignar el folio de factura para una salida de ganado en transito por venta
-- SpName     : SalidaGanadoTransitoVenta_GenerarFolioFactura
--======================================================
CREATE PROCEDURE [dbo].SalidaGanadoTransitoVenta_GenerarFolioFactura
@FolioSalida INT,--folio de la salida que se facturara
@OrganizacionID INT
AS
DECLARE @FolioFactura INT--folio de factura generado
DECLARE @ParametroIDFacturaOrganizacion INT
BEGIN

	/* Se obtiene el folio factura consecutivo*/
	SELECT @FolioFactura = CAST(PO.Valor AS BIGINT) + 1, @ParametroIDFacturaOrganizacion = PO.ParametroOrganizacionID
	 FROM ParametroOrganizacion PO (NOLOCK) 
	 INNER JOIN Parametro P(NOLOCK)  
		ON (P.ParametroID = PO.ParametroID
			AND P.Clave = 'FolioFactura'
			AND PO.OrganizacionID = @OrganizacionID
			AND PO.Activo = 1)
	 INNER JOIN Organizacion O
		ON (PO.OrganizacionID = O.OrganizacionID)
	
	/* Se actualiza el folio Consecutivo*/
	UPDATE ParametroOrganizacion SET Valor = CAST(@FolioFactura AS VARCHAR) WHERE ParametroOrganizacionID = @ParametroIDFacturaOrganizacion
	
	/* Se actualiza el folio de factura en la tabla de salida */
	UPDATE SalidaGanadoTransito SET FolioFactura=@FolioFactura WHERE OrganizacionID = @OrganizacionID AND Folio=@FolioSalida AND Venta=1;

END
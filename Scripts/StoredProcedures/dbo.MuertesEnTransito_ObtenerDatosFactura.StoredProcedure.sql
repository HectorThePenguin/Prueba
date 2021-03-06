USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_ObtenerDatosFactura]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuertesEnTransito_ObtenerDatosFactura]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_ObtenerDatosFactura]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Ramses Santos
-- Create date: 12/08/2014
-- Description:  Obtiene los datos para generar el xml de la factura.
-- MuertesEnTransito_ObtenerDatosFactura 0, 1
-- =============================================
CREATE PROCEDURE [dbo].[MuertesEnTransito_ObtenerDatosFactura]
	@FolioMuerte BIGINT
  , @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @CostoPorMuerte DECIMAL(12,2)
		
		SELECT @CostoPorMuerte = CAST (PO.Valor AS DECIMAL(12,2))
		FROM ParametroOrganizacion PO (NOLOCK) 
		INNER JOIN Parametro P(NOLOCK) ON (P.ParametroID = PO.ParametroID
			AND P.Clave = 'DESCUENTOGANADOMUERTO'
			AND PO.OrganizacionID = @OrganizacionID
			AND PO.Activo = 1 )
		INNER JOIN Organizacion O ON (PO.OrganizacionID = O.OrganizacionID)

		SELECT
				  EGM.FolioMuerte 
				, EGM.Fecha
				, EGM.FolioFactura
				, Cte.CodigoSAP
				, Cte.Descripcion AS NombreCliente
				, Cte.RFC
				, Cte.Calle
				, Cte.CodigoPostal
				, Cte.Poblacion
				, Cte.Estado
				, Cte.Pais
				, Cte.CondicionPago
				, Cte.DiasPago
				, MP.MetodoPagoID  AS MetodoPago
				, EGM.AnimalID
				, @CostoPorMuerte AS CostoMuerte
		FROM EntradaGanadoMuerte (NOLOCK) EGM
		INNER JOIN EntradaGanado (NOLOCK) EG ON (EG.EntradaGanadoID = EGM.EntradaGanadoID)
		INNER JOIN EmbarqueDetalle (NOLOCK) ED ON (ED.EmbarqueID = EG.EmbarqueID AND ED.OrganizacionDestinoID = EG.OrganizacionID AND ED.Activo = 1)
		INNER JOIN Proveedor (NOLOCK) P ON (ED.ProveedorID = P.ProveedorID AND P.Activo = 1)
		LEFT JOIN Cliente (NOLOCK) AS Cte ON (Cte.CodigoSAP = P.CodigoSAP)
		LEFT JOIN MetodoPago (NOLOCK) AS MP ON (Cte.MetodoPagoID = MP.MetodoPagoID)
		WHERE EGM.FolioMuerte = @FolioMuerte
		  AND EG.OrganizacionID = @OrganizacionID

	SET NOCOUNT OFF

END

GO

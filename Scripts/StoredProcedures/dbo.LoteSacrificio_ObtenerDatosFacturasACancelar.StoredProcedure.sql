USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelar]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene los datos de la factura del dia o del dia anterior para cancelarla.
-- SpName     : LoteSacrificio_ObtenerDatosFacturasACancelar 2
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelar]
@OrganizacionID	INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT OS.OrdenSacrificioID
		,LS.Fecha
		,LS.ClienteID
		,C.CodigoSAP
		,C.Descripcion AS NombreCliente
	FROM OrdenSacrificio(NOLOCK) AS OS
	INNER JOIN LoteSacrificio(NOLOCK) AS LS ON (OS.OrdenSacrificioID = LS.OrdenSacrificioID)
	INNER JOIN Cliente(NOLOCK) AS C ON (C.ClienteID = LS.ClienteID)
	WHERE CAST(LS.Fecha AS DATE) BETWEEN CAST(GETDATE() - 1 AS DATE)
			AND CAST(GETDATE() AS DATE)
		AND ISNULL(LS.Folio, 0) <> 0
		AND ISNULL(Serie, '') <> ''
		AND OS.OrganizacionID = @OrganizacionID
	GROUP BY OS.OrdenSacrificioID
		,LS.Fecha
		,LS.ClienteID
		,C.CodigoSAP
		,C.Descripcion
	ORDER BY LS.Fecha DESC

	SET NOCOUNT OFF
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelarLucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelarLucero]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelarLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene los datos de la factura del dia o del dia anterior para cancelarla.
-- SpName     : LoteSacrificio_ObtenerDatosFacturasACancelarLucero 5
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosFacturasACancelarLucero]
@OrganizacionID	INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 0 AS OrdenSacrificioID
		,LS.Fecha
		,LS.ClienteID
		,C.CodigoSAP
		,C.Descripcion AS NombreCliente
	FROM LoteSacrificioLucero(NOLOCK) AS LS
	INNER JOIN Cliente(NOLOCK) AS C 
		ON (C.ClienteID = LS.ClienteID)
	INNER JOIN Lote L
		ON (LS.LoteID = L.LoteID
			AND L.OrganizacionID = @OrganizacionID)
	WHERE CAST(LS.Fecha AS DATE) BETWEEN CAST(GETDATE() - 5 AS DATE)
			AND CAST(GETDATE() AS DATE)
		AND LEN(LS.Folio) > 0
		AND LEN(Serie) > 0
	GROUP BY LS.Fecha
		,LS.ClienteID
		,C.CodigoSAP
		,C.Descripcion
	ORDER BY LS.Fecha DESC

	SET NOCOUNT OFF
END

GO

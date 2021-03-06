USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelarLucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelarLucero]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelarLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene las facturas generadas para la orden de sacrificio seleccionada
-- SpName     : LoteSacrificio_ObtenerDetalleFacturasACancelarLucero 5
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelarLucero] @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT CAST(LS.Serie AS VARCHAR(15)) + CAST(LS.Folio AS VARCHAR(15)) AS FolioFactura
	FROM LoteSacrificioLucero(NOLOCK) AS LS
	INNER JOIN Cliente(NOLOCK) AS C ON (C.ClienteID = LS.ClienteID)
	INNER JOIN Lote L
		ON (LS.LoteID = L.LoteID
			AND L.OrganizacionID = @OrganizacionID)
	WHERE CAST(LS.Fecha AS DATE) BETWEEN CAST(GETDATE() - 3 AS DATE)
			AND CAST(GETDATE() AS DATE)
		AND LEN(LS.Folio) > 0
		AND LEN(Serie) > 0
	ORDER BY LS.Fecha DESC

	SET NOCOUNT OFF
END

GO

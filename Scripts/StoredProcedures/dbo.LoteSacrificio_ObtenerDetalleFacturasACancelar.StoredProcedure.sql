USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelar]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene las facturas generadas para la orden de sacrificio seleccionada
-- SpName     : LoteSacrificio_ObtenerDetalleFacturasACancelar 142
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDetalleFacturasACancelar] @OrdenSacrificioId INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT CAST(LS.Serie AS VARCHAR(15)) + CAST(LS.Folio AS VARCHAR(15)) AS FolioFactura
	FROM LoteSacrificio(NOLOCK) AS LS
	INNER JOIN Cliente(NOLOCK) AS C ON (C.ClienteID = LS.ClienteID)
	WHERE CAST(LS.Fecha AS DATE) BETWEEN CAST(GETDATE() - 1 AS DATE)
			AND CAST(GETDATE() AS DATE)
		AND ISNULL(LS.Folio, 0) <> 0
		AND ISNULL(Serie, '') <> ''
		AND LS.OrdenSacrificioID = @OrdenSacrificioId
	ORDER BY LS.Fecha DESC
	SET NOCOUNT OFF
END

GO

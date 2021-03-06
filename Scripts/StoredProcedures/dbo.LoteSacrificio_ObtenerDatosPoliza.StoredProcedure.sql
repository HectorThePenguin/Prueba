USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosPoliza]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosPoliza]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosPoliza]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 12/09/2014
-- Description: Obtiene los datos para la poliza de lote sacrificio
-- SpName     : LoteSacrificio_ObtenerDatosPoliza 209
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosPoliza]
@OrdenSacrificioID INT
AS
BEGIN

	SET NOCOUNT ON

		SELECT LS.LoteID
			,  LS.Fecha
			,  LS.ImporteCanal		AS ImporteCanal
			,  LS.ImportePiel		AS ImportePiel
			,  LS.ImporteVisera		AS ImporteViscera
			,  LS.Serie
			,  LS.Folio
			,  L.Lote
			,  COUNT(AM.AnimalID)	AS Canales
			,  0					AS Peso
		FROM LoteSacrificio LS
		INNER JOIN Lote L
			ON (LS.LoteID = L.LoteID)
		INNER JOIN LoteSacrificioDetalle LSD
			ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
		INNER JOIN LoteSacrificioDetalle AM(NOLOCK)
			ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
		WHERE LS.OrdenSacrificioID = @OrdenSacrificioID
		GROUP BY LS.LoteID
			,  LS.Fecha
			,  LS.ImporteCanal
			,  LS.ImportePiel
			,  LS.ImporteVisera
			,  LS.Serie
			,  LS.Folio
			,  L.Lote
		ORDER BY LS.Folio

	SET NOCOUNT OFF

END

GO

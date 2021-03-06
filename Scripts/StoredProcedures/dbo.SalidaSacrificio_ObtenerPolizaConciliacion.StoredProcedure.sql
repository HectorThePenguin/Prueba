USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaSacrificio_ObtenerPolizaConciliacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaSacrificio_ObtenerPolizaConciliacion]
GO
/****** Object:  StoredProcedure [dbo].[SalidaSacrificio_ObtenerPolizaConciliacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 09-07-2014
-- Description:	Obtiene los datos para generar la poliza de salida
-- SalidaSacrificio_ObtenerPolizaConciliacion 4, '20150428', '20150428'
-- =============================================
CREATE PROCEDURE [dbo].[SalidaSacrificio_ObtenerPolizaConciliacion]
@OrganizacionID INT
, @FechaInicial DATE
, @FechaFinal DATE
AS
BEGIN

	SET NOCOUNT ON

		SELECT *
		FROM 
		(
			SELECT LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal		AS ImporteCanal
				,  LS.ImportePiel		AS ImportePiel
				,  LS.ImporteVisera		AS ImporteViscera
				,  LS.Serie
				,  LS.Folio
				,  L.Lote
				,  COUNT(LSD.AnimalID)	AS Canales
				,  LS.PesoCanal			AS Peso
				,  @OrganizacionID		AS OrganizacionID
				,  LS.LoteSacrificioID
				,  LS.OrdenSacrificioID
			FROM LoteSacrificio LS
			INNER JOIN Lote L
				ON (LS.LoteID = L.LoteID
					AND L.OrganizacionID = @OrganizacionID)
			INNER JOIN LoteSacrificioDetalle LSD
				ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
			WHERE CAST(LS.Fecha AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			GROUP BY LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal
				,  LS.ImportePiel
				,  LS.ImporteVisera
				,  LS.Serie
				,  LS.Folio
				,  L.Lote
				,  LS.PesoCanal
				,  LS.LoteSacrificioID
				,  LS.OrdenSacrificioID
			UNION
			SELECT LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal		AS ImporteCanal
				,  LS.ImportePiel		AS ImportePiel
				,  LS.ImporteVisera		AS ImporteViscera
				,  LS.Serie
				,  LS.Folio
				,  ''					AS Lote
				,  COUNT(LSD.AnimalID)	AS Canales
				,  LS.PesoCanal			AS Peso
				,  @OrganizacionID		AS OrganizacionID
				,  LS.LoteSacrificioID
				,  1					AS OrdenSacrificioID
			FROM LoteSacrificioLucero LS
			INNER JOIN LoteSacrificioLuceroDetalle LSLD
				ON (LS.LoteSacrificioID = LSLD.LoteSacrificioID)
			INNER JOIN Lote L
				ON (LS.LoteID = L.LoteID
					AND L.OrganizacionID = @OrganizacionID)
			INNER JOIN LoteSacrificioLuceroDetalle LSD
				ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
			WHERE CAST(LS.Fecha AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			GROUP BY LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal
				,  LS.ImportePiel
				,  LS.ImporteVisera
				,  LS.Serie
				,  LS.PesoCanal
				,  LS.Folio
				,  LS.LoteSacrificioID
		) A
		ORDER BY A.Folio

	SET NOCOUNT OFF

END

GO

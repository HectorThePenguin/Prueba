USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerDatosPoliza]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerDatosPoliza]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerDatosPoliza]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 12/09/2014
-- Description: Obtiene los datos para la poliza de lote sacrificio
-- SpName     : LoteSacrificioLucero_ObtenerDatosPoliza '20150323', 5
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerDatosPoliza]
@Fecha				DATE
, @OrganizacionID	INT
, @XmlInterface		XML
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @SacrificioGanado INT
		SET @SacrificioGanado = 16

		SELECT LS.LoteID
			,  LS.Fecha
			,  LS.ImporteCanal		AS ImporteCanal
			,  LS.ImportePiel		AS ImportePiel
			,  LS.ImporteVisera		AS ImporteViscera
			,  LS.Serie
			,  LS.Folio
			,  L.Lote
			,  COUNT(AM.AnimalID)	AS Canales
			,  SUM(AM.Peso)			AS Peso		
			,  SUM(AH.PesoPiel)		AS PesoPiel
			,  LS.InterfaceSalidaTraspasoDetalleID
		FROM LoteSacrificioLucero LS
		INNER JOIN Lote L
			ON (LS.LoteID = L.LoteID
				AND L.OrganizacionID = @OrganizacionID)
		INNER JOIN LoteSacrificioLuceroDetalle LSD
			ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
		INNER JOIN AnimalMovimientoHistorico AM(NOLOCK)
			ON (LSD.AnimalID = AM.AnimalID
				AND AM.Activo = 1
				AND AM.TipoMovimientoID = @SacrificioGanado)
		INNER JOIN 
		(
			SELECT
				t.item.value('./Id[1]', 'BIGINT') AS Id
			FROM @XmlInterface.nodes('ROOT/InterfaceDetalleID') AS T (item)
		) X ON (LS.InterfaceSalidaTraspasoDetalleID = x.Id)
		INNER JOIN AnimalHistorico AH(NOLOCK)
			ON (AM.AnimalID = AH.AnimalID)
		WHERE CAST(LS.Fecha AS DATE) = @Fecha
			AND LS.ImporteCanal > 0
		GROUP BY LS.LoteID
			,  LS.Fecha
			,  LS.ImporteCanal
			,  LS.ImportePiel
			,  LS.ImporteVisera
			,  LS.Serie
			,  LS.Folio
			,  L.Lote
			,  LS.InterfaceSalidaTraspasoDetalleID
		ORDER BY LS.Folio

	SET NOCOUNT OFF

END

GO

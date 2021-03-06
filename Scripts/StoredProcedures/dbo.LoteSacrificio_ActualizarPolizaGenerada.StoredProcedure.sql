USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ActualizarPolizaGenerada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ActualizarPolizaGenerada]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ActualizarPolizaGenerada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 28/04/2015
-- Description:	Actualiza bandera para indicar si se ha generado poliza
-- LoteSacrificio_ActualizarPolizaGenerada
--=============================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ActualizarPolizaGenerada]
@XmlSacrificio XML
AS
BEGIN
	SET NOCOUNT ON;

		CREATE TABLE #tDatosSacrificio
		(
			LoteID								INT
			, Fecha								DATE
			, OrganizacionID					INT
			, InterfaceSalidaTraspasoDetalleID	INT
			, PolizaGenerada					BIT
		)

		INSERT INTO #tDatosSacrificio
		SELECT
			t.item.value('./LoteID[1]', 'INT') AS LoteID
			, t.item.value('./Fecha[1]', 'DATETIME') AS Fecha
			, t.item.value('./OrganizacionID[1]', 'INT') AS OrganizacionID
			, t.item.value('./InterfaceSalidaTraspasoDetalleID[1]', 'INT') AS InterfaceSalidaTraspasoDetalleID
			, t.item.value('./PolizaGenerada[1]', 'BIT') AS PolizaGenerada
		FROM @XmlSacrificio.nodes('ROOT/DatosSacrificio') AS T (item)

		UPDATE LS
		SET LS.PolizaGenerada = tDS.PolizaGenerada
		FROM LoteSacrificio LS
		INNER JOIN #tDatosSacrificio tDS
			ON (LS.LoteID = tDS.LoteID
				AND CAST(LS.Fecha AS DATE) = CAST(tDS.Fecha AS DATE))
		INNER JOIN Lote L
			ON (LS.LoteID = L.LoteID
				AND L.OrganizacionID = tDS.OrganizacionID)

		UPDATE LS
		SET LS.PolizaGenerada = tDS.PolizaGenerada
		FROM LoteSacrificioLucero LS
		INNER JOIN #tDatosSacrificio tDS
			ON (LS.LoteID = tDS.LoteID
				AND CAST(LS.Fecha AS DATE) = CAST(tDS.Fecha AS DATE)
				AND LS.InterfaceSalidaTraspasoDetalleID = tDS.InterfaceSalidaTraspasoDetalleID)
		INNER JOIN Lote L
			ON (LS.LoteID = L.LoteID
				AND L.OrganizacionID = tDS.OrganizacionID)

	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorLotesSacrificadosXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerPorLotesSacrificadosXML]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerPorLotesSacrificadosXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 13/04/2015
-- Description:  Obtiene animales por su disponibilidad
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerPorLotesSacrificadosXML]
@XmlLote XML
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tLote
	(
		LoteID INT
	)

	INSERT INTO #tLote
	SELECT 
		t.item.value('./LoteID[1]', 'INT') AS LoteID
	FROM @XmlLote.nodes('ROOT/Lotes') AS T(item)

	DECLARE @Animal AS TABLE 
	(
		AnimalID BIGINT
		,LoteID INT
	)

	DECLARE @SacrificioGanado INT
	SET @SacrificioGanado = 16

	INSERT INTO @Animal (
		AnimalID
		,LoteID
		)
	SELECT AM.AnimalID
		,  tL.LoteID
	FROM LoteSacrificioLucero LSL
	INNER JOIN #tLote tL
		ON (LSL.LoteID = tL.LoteID)
	INNER JOIN LoteSacrificioLuceroDetalle LSLD
		ON (LSL.LoteSacrificioID = LSLD.LoteSacrificioID)
	INNER JOIN AnimalMovimiento AM(NOLOCK)
		ON (LSLD.AnimalID = AM.AnimalID
			AND AM.Activo = 1
			AND AM.TipoMovimientoID = @SacrificioGanado)
	WHERE LEN(LSL.Serie) > 0
		AND LEN(LSL.Folio) > 0

	INSERT INTO @Animal (
		AnimalID
		,LoteID
		)
	SELECT AM.AnimalID
		,  tL.LoteID
	FROM LoteSacrificioLucero LSL
	INNER JOIN #tLote tL
		ON (LSL.LoteID = tL.LoteID)
	INNER JOIN LoteSacrificioLuceroDetalle LSLD
		ON (LSL.LoteSacrificioID = LSLD.LoteSacrificioID)
	INNER JOIN AnimalMovimientoHistorico AM(NOLOCK)
		ON (LSLD.AnimalID = AM.AnimalID
			AND AM.Activo = 1
			AND AM.TipoMovimientoID = @SacrificioGanado)
	WHERE LEN(LSL.Serie) > 0
		AND LEN(LSL.Folio) > 0

	SELECT AnimalID
		,LoteID
	FROM @Animal	

	SET NOCOUNT OFF;
END

GO

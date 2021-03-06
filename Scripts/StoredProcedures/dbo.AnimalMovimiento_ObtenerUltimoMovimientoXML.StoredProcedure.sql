USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene el ultimo movimiento de los animales
-- AnimalMovimiento_ObtenerUltimoMovimientoXML '<ROOT><Animales><AnimalID>85813</AnimalID></Animales></ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerUltimoMovimientoXML]
@XmlAnimales XML
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #tMovimientos
		(
			AnimalMovimientoID BIGINT
			, AnimalID		   BIGINT
		)

		CREATE TABLE #tAnimales
		(
			AnimalID BIGINT
		)

		INSERT INTO #tAnimales
		SELECT AnimalID = T.item.value('./AnimalID[1]', 'BIGINT')
		FROM  @XmlAnimales.nodes('ROOT/Animales') AS T(item)

		INSERT INTO #tMovimientos
		SELECT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  AM.AnimalID
		FROM AnimalMovimiento AM(NOLOCK)
		INNER JOIN #tAnimales B	
			ON (AM.AnimalID = B.AnimalID)		
		GROUP BY AM.AnimalID
		UNION 
		SELECT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  AM.AnimalID
		FROM AnimalMovimientoHistorico AM(NOLOCK)
		INNER JOIN #tAnimales B	
			ON (AM.AnimalID = B.AnimalID)
		GROUP BY AM.AnimalID

		SELECT AM.AnimalID
			,	AM.AnimalMovimientoID
			,	AM.OrganizacionID
			,	AM.CorralID
			,	AM.LoteID
			,	AM.FechaMovimiento
			,	AM.Peso
			,	AM.Temperatura
			,	AM.TipoMovimientoID
			,	AM.TrampaID
			,	AM.OperadorID
			,	AM.Observaciones
			,	ISNULL(AM.LoteIDOrigen, 0)	AS LoteIDOrigen
			,	ISNULL(AM.AnimalMovimientoIDAnterior, 0)	AS AnimalMovimientoIDAnterior
		FROM #tMovimientos M
		INNER JOIN AnimalMovimiento AM(NOLOCK)
			ON (M.AnimalMovimientoID = AM.AnimalMovimientoID)
		UNION 
		SELECT AM.AnimalID
			,	AM.AnimalMovimientoID
			,	AM.OrganizacionID
			,	AM.CorralID
			,	AM.LoteID
			,	AM.FechaMovimiento
			,	AM.Peso
			,	AM.Temperatura
			,	AM.TipoMovimientoID
			,	AM.TrampaID
			,	AM.OperadorID
			,	AM.Observaciones
			,	ISNULL(AM.LoteIDOrigen, 0)	AS LoteIDOrigen
			,	ISNULL(AM.AnimalMovimientoIDAnterior, 0)	AS AnimalMovimientoIDAnterior		
		FROM #tMovimientos M
		INNER JOIN AnimalMovimientoHistorico AM(NOLOCK)
			ON (M.AnimalMovimientoID = AM.AnimalMovimientoID)

		DROP TABLE #tAnimales
		DROP TABLE #tMovimientos

	SET NOCOUNT OFF

END

GO

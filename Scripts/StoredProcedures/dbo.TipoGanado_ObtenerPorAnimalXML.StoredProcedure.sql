USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorAnimalXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_ObtenerPorAnimalXML]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorAnimalXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/07/10
-- Description: 
-- TipoGanado_ObtenerPorAnimalXML
--=============================================
CREATE PROCEDURE [dbo].[TipoGanado_ObtenerPorAnimalXML]
@XmlAnimales XML
, @TipoMovimiento INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @tAnimales TABLE
		(
			AnimalID BIGINT
		)
		INSERT INTO @tAnimales
		SELECT AnimalID = T.item.value('./AnimalID[1]', 'BIGINT')
		FROM  @XmlAnimales.nodes('ROOT/Animales') AS T(item)
		CREATE TABLE #tTipos
		(
			AnimalID			BIGINT
			, OrganizacionID	INT
			, TipoGanado		VARCHAR(100)
			, TipoGanadoID		INT
			, PesoCompra		INT
			, PesoLlegada		INT
		)
		INSERT INTO #tTipos
		(
			AnimalID
			, OrganizacionID
			, TipoGanado
			, TipoGanadoID
			, PesoCompra
			, PesoLlegada
		)
		SELECT DISTINCT A.AnimalID
			,  A.OrganizacionIDEntrada
			,  TG.Descripcion
			,  TG.TipoGanadoID
			,  A.PesoCompra
			,  A.PesoLlegada
		FROM @tAnimales tA
		INNER JOIN Animal A
			ON (tA.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)
		INSERT INTO #tTipos
		(
			AnimalID
			, OrganizacionID
			, TipoGanado
			, TipoGanadoID
			, PesoCompra
			, PesoLlegada
		)
		SELECT DISTINCT A.AnimalID
			,  A.OrganizacionIDEntrada
			,  TG.Descripcion
			,  TG.TipoGanadoID
			,  A.PesoCompra
			,  A.PesoLlegada
		FROM @tAnimales tA
		INNER JOIN AnimalHistorico A
			ON (tA.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)
		CREATE TABLE #tCorral
		(
			AnimalID				INT
			, Corral				VARCHAR(100)
			, Lote					VARCHAR(100)
			, AnimalMovimientoID	INT
			, CorralID				INT
			, LoteID				INT
			, FechaMovimiento		DATETIME
		)
		INSERT INTO #tCorral
		(
			AnimalMovimientoID
			, CorralID
			, Corral
			, AnimalID
			, Lote
			, LoteID
			, FechaMovimiento
		)
		SELECT DISTINCT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  AM.CorralID	
			,  C.Codigo
			,  AM.AnimalID
			,  ISNULL(L.Lote, '')	AS Lote
			,  ISNULL(L.LoteID, 0)	AS LoteID
			,  MAX(AM.FechaMovimiento) AS FechaMovimiento
		FROM @tAnimales tA
		INNER JOIN AnimalMovimiento AM
			ON (tA.AnimalID = AM.AnimalID
				AND AM.TipoMovimientoID = @TipoMovimiento)
		INNER JOIN Corral C
			ON (AM.CorralID = C.CorralID)
		INNER JOIN Lote L
			ON (C.CorralID = L.CorralID
				AND AM.LoteID = L.LoteID)
		GROUP BY AM.CorralID
			,	 C.Codigo
			,	 AM.AnimalID
			,	 L.Lote
			,	 L.LoteID
		INSERT INTO #tCorral
		(
			AnimalMovimientoID
			, CorralID
			, Corral
			, AnimalID
			, Lote
			, LoteID
			, FechaMovimiento
		)
		SELECT DISTINCT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  AM.CorralID	
			,  C.Codigo
			,  AM.AnimalID
			,  ISNULL(L.Lote, '')	AS Lote
			,  ISNULL(L.LoteID, 0)	AS LoteID
			,  MAX(AM.FechaMovimiento) AS FechaMovimiento
		FROM @tAnimales tA
		INNER JOIN AnimalMovimientoHistorico AM
			ON (tA.AnimalID = AM.AnimalID
				AND AM.TipoMovimientoID = @TipoMovimiento)
		LEFT OUTER JOIN Corral C
			ON (AM.CorralID = C.CorralID)
		LEFT OUTER JOIN Lote L
			ON (C.CorralID = L.CorralID
				AND AM.LoteID = L.LoteID)
		GROUP BY AM.CorralID
			,	 C.Codigo
			,	 AM.AnimalID
			,	 L.Lote
			,	 L.LoteID
		SELECT DISTINCT tT.AnimalID			
			, OrganizacionID	
			, TipoGanado
			, TipoGanadoID
			, PesoCompra		
			, PesoLlegada		
			, Corral			
			, Lote				
			, FechaMovimiento
		FROM #tTipos tT
		INNER JOIN #tCorral tC
			ON (tT.AnimalID = tC.AnimalID)
	DROP TABLE #tTipos
	DROP TABLE #tCorral
	SET NOCOUNT OFF;
END

GO

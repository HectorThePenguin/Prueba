USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecuperacionDeMerma_ObtenerPorFechas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecuperacionDeMerma_ObtenerPorFechas]
GO
/****** Object:  StoredProcedure [dbo].[RecuperacionDeMerma_ObtenerPorFechas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 21/02/2014
-- Description: Obtiene los datos para el reporte dia a dia
-- SpName     : RecuperacionDeMerma_ObtenerPorFechas 1, '20150627' , '20150627'
--======================================================
CREATE PROCEDURE [dbo].[RecuperacionDeMerma_ObtenerPorFechas]
@OrganizacionID INT
, @FechaInicio  DATETIME
, @FechaFin		DATETIME
AS
BEGIN
	SET NOCOUNT ON
		SET @FechaFin = DATEADD(HH,23, @FechaFin)
		DECLARE @GrupoProduccion INT
	  , @GrupoEnfermeria INT
	  , @GrupoCorraleta  INT
	  , @Venta			 INT
	  , @Muerte			 INT
		SET @GrupoProduccion = 2
		SET @GrupoEnfermeria = 3
		SET @GrupoCorraleta = 4
		SET @Venta = 11
		SET @Muerte = 8
		CREATE TABLE #tMerma
		(
			FechaEntrada		SMALLDATETIME
			, FolioEntrada		INT
			, CabezasOrigen		INT
			, OrganizacionID	INT
			, CabezasProduccion	INT
			, CabezasEnfermeria	INT
			, CabezasMuertas	INT
			, CabezasVenta		INT
			, KilosOrigen		NUMERIC(18,2)
			, KilosLlegada		NUMERIC(18,2)
			, KilosCorte		NUMERIC(18,2)
			, MermaTransito		NUMERIC(18,2)
			, RecuperacionMerma NUMERIC(18,2)
		)
		INSERT INTO #tMerma
		SELECT EG.FechaEntrada
			,  EG.FolioEntrada
			,  EG.CabezasOrigen
			,  EG.OrganizacionID
			,  0
			,  0
			,  0
			,  0
			,  0.0
			,  EG.PesoBruto - EG.PesoTara
			,  0.0
			,  0.0
			,  0.0
		FROM EntradaGanado EG
		WHERE EG.FechaEntrada BETWEEN @FechaInicio AND @FechaFin
			AND EG.OrganizacionID = @OrganizacionID
			AND EG.Manejado = 1
		----------------------------------------Cabezas Produccion/Enfermeria-----------------------------
		UPDATE M
		SET CabezasProduccion = Cabezas
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT COUNT(A.AnimalID) AS Cabezas
				,  GC.GrupoCorralID
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN Animal A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada)
			INNER JOIN AnimalMovimiento AM
				ON (A.AnimalID = AM.AnimalID
					AND AM.Activo = 1)
			INNER JOIN Lote L 
				ON (AM.LoteID = L.LoteID)
			INNER JOIN TipoCorral TC
				ON (L.TipoCorralID = TC.TipoCorralID)
			INNER JOIN GrupoCorral GC
				ON (TC.GrupoCorralID = GC.GrupoCorralID
					AND GC.GrupoCorralID = @GrupoProduccion)
			GROUP BY GC.GrupoCorralID
				,	 E.FolioEntrada
				,	 E.OrganizacionID
		) C	ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		UPDATE M
		SET CabezasEnfermeria = Cabezas
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT COUNT(A.AnimalID) AS Cabezas
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN Animal A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada)
			INNER JOIN AnimalMovimiento AM
				ON (A.AnimalID = AM.AnimalID
					AND AM.Activo = 1)
			INNER JOIN Lote L 
				ON (AM.LoteID = L.LoteID)
			INNER JOIN TipoCorral TC
				ON (L.TipoCorralID = TC.TipoCorralID)
			INNER JOIN GrupoCorral GC
				ON (TC.GrupoCorralID = GC.GrupoCorralID
					AND GC.GrupoCorralID IN (@GrupoEnfermeria, @GrupoCorraleta))
			GROUP BY E.FolioEntrada
				,	 E.OrganizacionID
		) C	ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		----------------------------------------Cabezas Produccion/Enfermeria-----------------------------
		----------------------------------------Cabezas Muertas/Venta-----------------------------
		UPDATE M
		SET M.CabezasMuertas = Cabezas
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT COUNT(A.AnimalID) AS Cabezas	
				,  AM.TipoMovimientoID
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN Animal A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada
					AND A.Activo = 0)
			INNER JOIN AnimalMovimiento AM
				ON (A.AnimalID = AM.AnimalID
					AND AM.TipoMovimientoID = @Muerte)
			GROUP BY AM.TipoMovimientoID
				,	 E.FolioEntrada
				,	 E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		UPDATE M
		SET M.CabezasMuertas = M.CabezasMuertas + Cabezas
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT COUNT(A.AnimalID) AS Cabezas	
				,  AM.TipoMovimientoID
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN AnimalHistorico A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada
					AND A.Activo = 0)
			INNER JOIN AnimalMovimientoHistorico AM
				ON (A.AnimalID = AM.AnimalID
					AND AM.TipoMovimientoID = @Muerte)
			GROUP BY AM.TipoMovimientoID
				,	 E.FolioEntrada
				,	 E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		UPDATE M
		SET M.CabezasVenta = Cabezas
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT COUNT(A.AnimalID) AS Cabezas	
				,  AM.TipoMovimientoID
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN Animal A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada
					AND A.Activo = 0)
			INNER JOIN AnimalMovimiento AM
				ON (A.AnimalID = AM.AnimalID
					AND AM.TipoMovimientoID = @Venta)
			GROUP BY AM.TipoMovimientoID
				,	 E.FolioEntrada
				,	 E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		UPDATE M
		SET M.CabezasVenta = M.CabezasVenta + Cabezas
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT COUNT(A.AnimalID) AS Cabezas	
				,  AM.TipoMovimientoID
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN AnimalHistorico A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada
					AND A.Activo = 0)
			INNER JOIN AnimalMovimientoHistorico AM
				ON (A.AnimalID = AM.AnimalID
					AND AM.TipoMovimientoID = @Venta)
			GROUP BY AM.TipoMovimientoID
				,	 E.FolioEntrada
				,	 E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		----------------------------------------Cabezas Muertas/Venta-----------------------------
		----------------------------------------Kilos Origen-----------------------------
		UPDATE M
		SET KilosOrigen = C.KilosOrigen
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT SUM(A.PesoCompra) AS KilosOrigen
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN Animal A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada)
			GROUP BY E.FolioEntrada
				,    E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		UPDATE M
		SET KilosOrigen = M.KilosOrigen + C.KilosOrigen
		FROM #tMerma M
		INNER JOIN 
		(
			SELECT SUM(A.PesoCompra) AS KilosOrigen
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMerma E
			INNER JOIN AnimalHistorico A
				ON (E.OrganizacionID = A.OrganizacionIDEntrada
					AND E.FolioEntrada = A.FolioEntrada)
			GROUP BY E.FolioEntrada
				,    E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		----------------------------------------Kilos Origen-----------------------------
		----------------------------------------Kilos Corte-----------------------------
		CREATE TABLE #tMermaMov
		(
			FolioEntrada					INT
			, OrganizacionID			INT
			, AnimalID						INT
			, AnimalMovimientoID	INT
		)
		INSERT INTO #tMermaMov
		SELECT A.FolioEntrada
			,  A.OrganizacionIDEntrada
			,	 A.AnimalID
			,  MIN(AM.AnimalMovimientoID)
		FROM #tMerma E
		INNER JOIN Animal A
			ON (E.OrganizacionID = A.OrganizacionIDEntrada
				AND E.FolioEntrada = A.FolioEntrada)
		INNER JOIN AnimalMovimiento AM
			ON A.AnimalID = AM.AnimalID
		GROUP BY A.FolioEntrada
			,  A.OrganizacionIDEntrada
			,	 A.AnimalID
		UPDATE M
		SET KilosCorte = C.KilosCorte
		FROM #tMerma M
		INNER JOIN
		(
			SELECT SUM(AM.Peso) AS KilosCorte
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMermaMov E
			INNER JOIN AnimalMovimiento AM
				ON E.AnimalMovimientoID = AM.AnimalMovimientoID
			GROUP BY E.FolioEntrada
				,  E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		DELETE FROM #tMermaMov
		INSERT INTO #tMermaMov
		SELECT A.FolioEntrada
			,  A.OrganizacionIDEntrada
			,	 A.AnimalID
			,  min(AMH.AnimalMovimientoID)
		FROM #tMerma E
		INNER JOIN AnimalHistorico A
			ON (E.OrganizacionID = A.OrganizacionIDEntrada
				AND E.FolioEntrada = A.FolioEntrada)
		INNER JOIN AnimalMovimientoHistorico AMH
			ON A.AnimalID = AMH.AnimalID
		GROUP BY A.FolioEntrada
			,	 A.OrganizacionIDEntrada
			,	 A.AnimalID
		UPDATE M
		SET KilosCorte = M.KilosCorte + C.KilosCorte
		FROM #tMerma M
		INNER JOIN
		(
			SELECT SUM(AMH.Peso) AS KilosCorte
				,  E.FolioEntrada
				,  E.OrganizacionID
			FROM #tMermaMov E
			INNER JOIN AnimalMovimientoHistorico AMH
				ON E.AnimalMovimientoID = AMH.AnimalMovimientoID
			GROUP BY E.FolioEntrada
				,  E.OrganizacionID
		) C ON (M.FolioEntrada = C.FolioEntrada
				AND M.OrganizacionID = C.OrganizacionID)
		SELECT CONVERT(CHAR(10), FechaEntrada, 103)	AS FechaEntrada
			,  FolioEntrada
			,  CabezasOrigen
			,  OrganizacionID
			,  CabezasProduccion
			,  CabezasEnfermeria
			,  CabezasMuertas
			,  CabezasVenta
			,  KilosOrigen
			,  KilosLlegada
			,  KilosCorte
		FROM #tMerma
		----------------------------------------Kilos Corte-----------------------------
		DROP TABLE #tMerma
		DROP TABLE #tMermaMov
	SET NOCOUNT OFF
END

GO

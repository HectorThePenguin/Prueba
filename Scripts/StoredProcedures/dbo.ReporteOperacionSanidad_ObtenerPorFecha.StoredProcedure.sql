USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteOperacionSanidad_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteOperacionSanidad_ObtenerPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[ReporteOperacionSanidad_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velázquez
-- Create date: 21/04/2014
-- Description: Obtiene los datos para el reporte Operacion de Sanidad
-- SpName     : ReporteOperacionSanidad_ObtenerPorFecha 4, '20140401' , '20140515'
--======================================================
CREATE PROCEDURE [dbo].[ReporteOperacionSanidad_ObtenerPorFecha] @OrganizacionID INT
	,@FechaInicio DATETIME
	,@FechaFin DATETIME
AS
BEGIN
	SET NOCOUNT ON
	SET @FechaFin = DATEADD(HH, 23, @FechaFin)
	DECLARE @TipoMovimientoEntradaEnfermeria INT
		,@TipoMovimientoMuerte INT
		,@TipoCorralCronico INT
		,@TipoCorralCronicoVentaMuerte INT
	SET @TipoMovimientoEntradaEnfermeria = 7 --Entrada a Enfermeria
	SET @TipoMovimientoMuerte = 8 --Muerte
	SET @TipoCorralCronico = 8 --Tipo Corral Cronicos en Recuperacion
	SET @TipoCorralCronicoVentaMuerte = 7 -- Tipo Corral Cronicos, Venta o Muerte
	create table #TABLAANIMALESMOVIMIENTO 
	(
		AnimalID INT		
		,FechaMovimiento SMALLDATETIME
	)
	INSERT INTO #TABLAANIMALESMOVIMIENTO
	SELECT 
	A.AnimalID	
	,MIN(AM.FechaMovimiento) AS FechaMovimiento
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID	
		AND (AM.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria OR AM.TipoMovimientoID = @TipoMovimientoMuerte)	
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY A.AnimalID
	create table #TABLAANIMALESMOVIMIENTOHISTORICO 
	(
		AnimalID INT		
		,FechaMovimiento SMALLDATETIME
	)
	INSERT INTO #TABLAANIMALESMOVIMIENTOHISTORICO
	SELECT 
	A.AnimalID	
	,MIN(AM.FechaMovimiento) AS FechaMovimiento
	FROM EntradaGanado EG
	INNER JOIN AnimalHistorico A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN AnimalMovimientoHistorico AM ON A.AnimalID = AM.AnimalID
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID	
		AND AM.TipoMovimientoID = @TipoMovimientoMuerte		
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY A.AnimalID
	CREATE TABLE #TABLASANIDAD (
		Concepto VARCHAR(50)
		,DiferenciaDias INT
		,TotalEntradas INT
		,AnimalID INT
		,TotalMedicados INT
		)
	--INSERT DEFAULT DE CADA UNO DE LOS CONCEPTOS
	INSERT INTO #TABLASANIDAD (
		Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
		)
	VALUES (
		'Morbilidad'
		,0
		,0
		,0
		,0
		)
	INSERT INTO #TABLASANIDAD (
		Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
		)
	VALUES (
		'Recaidos'
		,0
		,0
		,0
		,0
		)
	INSERT INTO #TABLASANIDAD (
		Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
		)
	VALUES (
		'Mortalidad'
		,0
		,0
		,0
		,0
		)
	INSERT INTO #TABLASANIDAD (
		Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
		)
	VALUES (
		'Crónicos en Recuperación'
		,0
		,0
		,0
		,0
		)
	INSERT INTO #TABLASANIDAD (
		Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
		)
	VALUES (
		'Crónico, Venta o Muerte'
		,0
		,0
		,0
		,0
		)
	INSERT INTO #TABLASANIDAD (
		Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
		)
	VALUES (
		'Crónicos > 30 Días'
		,0
		,0
		,0
		,0
		)
	--Insertar Morbilidad
	INSERT INTO #TABLASANIDAD
	SELECT 'Morbilidad' AS Concepto
		,DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
			INNER JOIN #TABLAANIMALESMOVIMIENTO TM ON A.AnimalID = TM.AnimalID
	INNER JOIN AnimalMovimiento AM ON (A.AnimalID = AM.AnimalID AND AM.FechaMovimiento = TM.FechaMovimiento)
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND AM.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY EG.FechaEntrada
		,AM.FechaMovimiento
		,A.AnimalID
	--Insertar Recaidos
	INSERT INTO #TABLASANIDAD
	SELECT 'Recaidos' AS Concepto
		,DATEDIFF(DD, AM1.FechaMovimiento, AM2.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
			INNER JOIN #TABLAANIMALESMOVIMIENTO TM ON A.AnimalID = TM.AnimalID	
	INNER JOIN AnimalMovimiento AM1 ON (A.AnimalID = AM1.AnimalID AND AM1.FechaMovimiento = TM.FechaMovimiento)
	INNER JOIN AnimalMovimiento AM2 ON (
			A.AnimalID = AM2.AnimalID
			AND AM1.TipoMovimientoID = AM2.TipoMovimientoID
			AND AM2.FechaMovimiento > AM1.FechaMovimiento
			)
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND AM1.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
		AND DATEDIFF(DD, AM1.FechaMovimiento, AM2.FechaMovimiento) >= 4
		AND DATEDIFF(DD, AM1.FechaMovimiento, AM2.FechaMovimiento) <= 21
	GROUP BY EG.FechaEntrada
		,AM1.FechaMovimiento
		,AM2.FechaMovimiento
		,A.AnimalID
	--Insertar Mortalidad 
	INSERT INTO #TABLASANIDAD
	SELECT 'Mortalidad' AS Concepto
		,DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN #TABLAANIMALESMOVIMIENTO TM ON A.AnimalID = TM.AnimalID
	INNER JOIN AnimalMovimiento AM ON (A.AnimalID = AM.AnimalID AND AM.FechaMovimiento = TM.FechaMovimiento)
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND AM.TipoMovimientoID = @TipoMovimientoMuerte
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY EG.FechaEntrada
		,AM.FechaMovimiento
		,A.AnimalID
	--Insertar Mortalidad Historico
	INSERT INTO #TABLASANIDAD
	SELECT 'Mortalidad' AS Concepto
		,DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN AnimalHistorico A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN #TABLAANIMALESMOVIMIENTOHISTORICO TM ON A.AnimalID = TM.AnimalID
	INNER JOIN AnimalMovimientoHistorico AM ON (A.AnimalID = AM.AnimalID AND AM.FechaMovimiento = TM.FechaMovimiento)
	INNER JOIN SalidaAnimal sa ON A.AnimalID = sa.AnimalID
	INNER JOIN SalidaGanado sg ON sa.SalidaGanadoID = sg.SalidaGanadoID
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND sg.TipoMovimientoID = @TipoMovimientoMuerte
		AND AM.TipoMovimientoID = @TipoMovimientoMuerte
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY EG.FechaEntrada
		,AM.FechaMovimiento
		,A.AnimalID
	--Insertar Cronicos en Recuperacion
	INSERT INTO #TABLASANIDAD
	SELECT 'Crónicos en Recuperación' AS Concepto
		,DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN #TABLAANIMALESMOVIMIENTO TM ON A.AnimalID = TM.AnimalID
	INNER JOIN AnimalMovimiento AM ON (A.AnimalID = AM.AnimalID AND AM.FechaMovimiento = TM.FechaMovimiento)
	INNER JOIN Corral co ON AM.CorralID = co.CorralID
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND co.TipoCorralID = @TipoCorralCronico
		AND AM.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY EG.FechaEntrada
		,AM.FechaMovimiento
		,A.AnimalID
	--Insertar Cronicos en Recuperacion
	INSERT INTO #TABLASANIDAD	
	SELECT 'Crónico, Venta o Muerte' AS Concepto
		,DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN #TABLAANIMALESMOVIMIENTO TM ON A.AnimalID = TM.AnimalID
	INNER JOIN AnimalMovimiento AM ON (A.AnimalID = AM.AnimalID AND AM.FechaMovimiento = TM.FechaMovimiento)
	INNER JOIN Corral co ON AM.CorralID = co.CorralID
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND co.TipoCorralID = @TipoCorralCronicoVentaMuerte
		AND AM.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
	GROUP BY EG.FechaEntrada
		,AM.FechaMovimiento
		,A.AnimalID	
	--Insertar Cronicos en Recuperacion
	INSERT INTO #TABLASANIDAD
	SELECT 'Crónicos > 30 Días' AS Concepto
		,DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) AS DiasDiferencia
		,0
		,A.AnimalID
		,0
	FROM EntradaGanado EG
	INNER JOIN Animal A ON (
			EG.FolioEntrada = A.FolioEntrada
			AND A.OrganizacionIDEntrada = @OrganizacionID
			)
	INNER JOIN #TABLAANIMALESMOVIMIENTO TM ON A.AnimalID = TM.AnimalID
	INNER JOIN AnimalMovimiento AM ON (A.AnimalID = AM.AnimalID AND AM.FechaMovimiento = TM.FechaMovimiento)
	INNER JOIN Corral co ON AM.CorralID = co.CorralID
	WHERE EG.Activo = 1
		AND EG.OrganizacionID = @OrganizacionID
		AND (
			co.TipoCorralID = @TipoCorralCronicoVentaMuerte
			OR co.TipoCorralID = @TipoCorralCronico
			)
		AND AM.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria
		AND (
			EG.FechaEntrada >= @FechaInicio
			AND EG.FechaEntrada <= @FechaFin
			)
		AND DATEDIFF(DD, EG.FechaEntrada, AM.FechaMovimiento) > 30
	GROUP BY EG.FechaEntrada
		,AM.FechaMovimiento
		,A.AnimalID
	UPDATE #TABLASANIDAD
	SET TotalEntradas = ISNULL((
			SELECT SUM(eg1.CabezasRecibidas)
			FROM EntradaGanado eg1
			WHERE eg1.OrganizacionID = @OrganizacionID
				AND eg1.FechaEntrada >= @FechaInicio
				AND eg1.FechaEntrada <= @FechaFin
			GROUP BY eg1.OrganizacionID
			),1), 
			TotalMedicados = ISNULL((
			SELECT COUNT(a.AnimalID)
			FROM EntradaGanado eg1
			INNER JOIN Animal A ON (eg1.FolioEntrada = A.FolioEntrada AND A.OrganizacionIDEntrada = @OrganizacionID)
			INNER JOIN AnimalMovimiento am ON A.AnimalID = am.AnimalID
			WHERE eg1.OrganizacionID = @OrganizacionID
				AND am.TipoMovimientoID = @TipoMovimientoEntradaEnfermeria
				AND eg1.FechaEntrada >= @FechaInicio
				AND eg1.FechaEntrada <= @FechaFin
			GROUP BY eg1.OrganizacionID
			),1)
	SELECT Concepto
		,DiferenciaDias
		,TotalEntradas
		,AnimalID
		,TotalMedicados
	FROM #TABLASANIDAD
	SET NOCOUNT OFF
END

GO

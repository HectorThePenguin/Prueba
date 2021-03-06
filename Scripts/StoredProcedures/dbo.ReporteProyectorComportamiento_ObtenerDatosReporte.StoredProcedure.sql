USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteProyectorComportamiento_ObtenerDatosReporte]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteProyectorComportamiento_ObtenerDatosReporte]
GO
/****** Object:  StoredProcedure [dbo].[ReporteProyectorComportamiento_ObtenerDatosReporte]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Abdiel Santos Beltran
-- Create date: 30/09/2014
-- Description: 
-- SpName     : ReporteProyectorComportamiento_ObtenerDatosReporte 5
--======================================================
CREATE PROCEDURE [dbo].[ReporteProyectorComportamiento_ObtenerDatosReporte] @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #TABLAREPORTEPROYECTOR (
		CorralID INT
		,CodigoCorral VARCHAR(10)
		,LoteID INT
		,CodigoLote VARCHAR(10)
		,Cabezas INT
		,TipoGanado VARCHAR(50)
		,FechaInicio SMALLDATETIME
		,FechaDisponibilidad SMALLDATETIME
		,DisponibilidadManual BIT
		,DiasF4 INT
		,DiasZilmax INT
		,DiasSacrificio INT
		,Clasificacion VARCHAR(50)
		,PesoOrigen INT
		,Merma DECIMAL(18, 2)
		,PesoProyectado INT
		,DiasEngorda INT
		,GananciaDiaria DECIMAL(18, 2)
		,FechaReimplante1 DATE
		,PesoReimplante1 INT
		,GananciaReimplante1 DECIMAL(18, 2)
		,FechaReimplante2 DATE
		,PesoReimplante2 INT
		,GananciaReimplante2 DECIMAL(18, 2)
		,FechaReimplante3 DATE
		,PesoReimplante3 INT
		,GananciaReimplante3 DECIMAL(18, 2)
		,FechaSacrificio DATE
		)
		CREATE TABLE #TIPOSGANADO (
		TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,LoteID INT
		,Corral varchar(10)		
		,Sexo CHAR(1)
		)
	INSERT INTO #TABLAREPORTEPROYECTOR (
		CorralID
		,CodigoCorral
		,LoteID
		,CodigoLote
		,Cabezas
		,TipoGanado
		,FechaInicio
		,FechaDisponibilidad
		,DisponibilidadManual
		,DiasF4
		,DiasZilmax
		,DiasSacrificio
		,Clasificacion
		,PesoOrigen
		,Merma
		,PesoProyectado
		,DiasEngorda
		,GananciaDiaria
		,FechaReimplante1
		,PesoReimplante1
		,GananciaReimplante1
		,FechaReimplante2
		,PesoReimplante2
		,GananciaReimplante2
		,FechaReimplante3
		,PesoReimplante3
		,GananciaReimplante3
		,FechaSacrificio
		)
	SELECT co.CorralID
		,co.Codigo
		,lo.LoteID
		,lo.Lote
		,lo.Cabezas
		,''
		,lo.FechaInicio
		,lo.FechaDisponibilidad
		,lo.DisponibilidadManual
		,0 AS DiasF4
		,0 AS DiasZilmax
		,0 AS DiasSacrificio
		,'' AS Clasificacion
		,0 AS PesoOrigen
		,0 AS Merma
		,0 AS PesoProyectado
		,DATEDIFF(DD, dbo.ObtenerFechaEntradaPromedioParaLote(lo.LoteID), GETDATE()) AS DiasEngorda
		,lp.GananciaDiaria
		,NULL AS FechaReimplante1
		,0 AS PesoReimplante1
		,0 AS GananciaReimplante1
		,NULL AS FechaReimplante2
		,0 AS PesoReimplante2
		,0 AS GananciaReimplante2
		,NULL AS FechaReimplante3
		,0 AS PesoReimplante3
		,0 AS GananciaReimplante3
		,CASE 
			WHEN lo.FechaDisponibilidad IS NULL
				THEN DATEADD(day, lp.DiasEngorda, lo.FechaInicio)
			ELSE lo.FechaDisponibilidad
			END
	FROM Corral co
	INNER JOIN Lote lo ON co.CorralID = lo.CorralID
	LEFT JOIN LoteProyeccion lp ON lo.LoteID = lp.LoteID
	INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
	INNER JOIN GrupoCorral gc ON tc.GrupoCorralID = gc.GrupoCorralID
	WHERE co.OrganizacionID = @OrganizacionID
		AND co.Activo = 1
		AND co.Codigo <> 'ZZZ'
		AND lo.Activo = 1
		and lo.Cabezas > 0
		AND gc.GrupoCorralID = 2 --Corrales del Grupo de Produccion  
	-- SE ACTUALIZA EL PESO ORIGEN DE CADA LOTE
	SELECT SUM(A.PesoCompra) AS PesoOrigen
		,TRP.LoteID
	INTO #TMPPESOORIGEN
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN Lote AS L ON L.LoteID = TRP.LoteID
	INNER JOIN AnimalMovimiento AS AM ON AM.LoteID = L.LoteID
	INNER JOIN Animal AS A ON A.AnimalID = AM.AnimalID
	WHERE L.LoteID = TRP.LoteID
		AND AM.Activo = 1
	GROUP BY TRP.LoteID
	UPDATE TRP
	SET TRP.PesoOrigen = TPO.PesoOrigen / TRP.Cabezas
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPPESOORIGEN AS TPO ON TRP.LoteID = TPO.LoteID
	-- SE OBTIENE LA MERMA DE CADA LOTE
	SELECT AVG(CAST(A.PesoCompra AS DECIMAL(18, 2))) AS PesoCompra
		,AVG(CAST(A.PesoLlegada AS DECIMAL(18, 2))) AS PesoLlegada
		,TRP.LoteID
	INTO #TMPMERMAS
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN Lote AS L ON L.LoteID = TRP.LoteID
	INNER JOIN AnimalMovimiento AS AM ON AM.LoteID = L.LoteID
	INNER JOIN Animal AS A ON A.AnimalID = AM.AnimalID
	WHERE L.LoteID = TRP.LoteID
		AND AM.Activo = 1
		AND TRP.Cabezas > 0
		AND A.PesoCompra > 0
	GROUP BY TRP.LoteID
	--	,A.PesoCompra
	--	,A.PesoLlegada
	UPDATE TRP
	SET TRP.Merma = ((CAST(TEMPM.PesoCompra AS DECIMAL(18, 2)) - CAST(TEMPM.PesoLlegada AS DECIMAL(18, 2))) / CAST(TEMPM.PesoCompra AS DECIMAL(18, 2))) * 100
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPMERMAS AS TEMPM ON TRP.LoteID = TEMPM.LoteID
	-- SE OBTIENE EL PESO ORIGEN Y LA FECHA DEL REIMPLANTE 1
	SELECT FechaReal
		,PesoReal
		,dbo.ObtenerDiasEngordaLoteALaFechaIndicada(L.LoteID, LR.FechaReal) AS DiasTranscurridos
		,TRP.LoteID
	INTO #TMPREIMPLANTE1
	FROM LoteReimplante AS LR
	INNER JOIN LoteProyeccion AS LP ON LP.LoteProyeccionID = LR.LoteProyeccionID
	INNER JOIN Lote AS L ON L.LoteID = LP.LoteID
	INNER JOIN #TABLAREPORTEPROYECTOR AS TRP ON TRP.LoteID = LP.LoteID
	WHERE NumeroReimplante = 1
	UPDATE TRP
	SET TRP.FechaReimplante1 = TMPREI.FechaReal
		,TRP.PesoReimplante1 = COALESCE(TMPREI.PesoReal, 0)
		,TRP.GananciaReimplante1 = CASE 
			WHEN TMPREI.DiasTranscurridos = 0
				OR COALESCE(TMPREI.PesoReal, 0) = 0
				THEN 0
			ELSE (CAST(COALESCE(TMPREI.PesoReal, 0) AS DECIMAL(18, 2)) - CAST(COALESCE(TRP.PesoOrigen, 0) AS DECIMAL(18, 2))) / CAST(TMPREI.DiasTranscurridos AS DECIMAL(18, 2))
			END
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPREIMPLANTE1 AS TMPREI ON TRP.LoteID = TMPREI.LoteID
	-- SE OBTIENE EL PESO ORIGEN Y LA FECHA DEL REIMPLANTE 2
	SELECT FechaReal
		,PesoReal
		,dbo.ObtenerDiasEngordaLoteALaFechaIndicada(L.LoteID, LR.FechaReal) AS DiasTranscurridos
		,TRP.LoteID
	INTO #TMPREIMPLANTE2
	FROM LoteReimplante AS LR
	INNER JOIN LoteProyeccion AS LP ON LP.LoteProyeccionID = LR.LoteProyeccionID
	INNER JOIN Lote AS L ON L.LoteID = LP.LoteID
	INNER JOIN #TABLAREPORTEPROYECTOR AS TRP ON TRP.LoteID = LP.LoteID
	WHERE NumeroReimplante = 2
	UPDATE TRP
	SET TRP.FechaReimplante2 = TMPREI.FechaReal
		,TRP.PesoReimplante2 = COALESCE(TMPREI.PesoReal, 0)
		,TRP.GananciaReimplante2 = CASE 
			WHEN TMPREI.DiasTranscurridos = 0
				OR COALESCE(TMPREI.PesoReal, 0) = 0
				THEN 0
			ELSE (CAST(COALESCE(TMPREI.PesoReal, 0) AS DECIMAL(18, 2)) - CAST(COALESCE(TRP.PesoOrigen, 0) AS DECIMAL(18, 2))) / CAST(TMPREI.DiasTranscurridos AS DECIMAL(18, 2))
			END
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPREIMPLANTE2 AS TMPREI ON TRP.LoteID = TMPREI.LoteID
	-- SE OBTIENE EL PESO ORIGEN Y LA FECHA DEL REIMPLANTE 3
	SELECT FechaReal
		,PesoReal
		,L.FechaInicio
		,dbo.ObtenerDiasEngordaLoteALaFechaIndicada(L.LoteID, LR.FechaReal) AS DiasTranscurridos
		,TRP.LoteID
	INTO #TMPREIMPLANTE3
	FROM LoteReimplante AS LR
	INNER JOIN LoteProyeccion AS LP ON LP.LoteProyeccionID = LR.LoteProyeccionID
	INNER JOIN Lote AS L ON L.LoteID = LP.LoteID
	INNER JOIN #TABLAREPORTEPROYECTOR AS TRP ON TRP.LoteID = LP.LoteID
	WHERE NumeroReimplante = 3
	UPDATE TRP
	SET TRP.FechaReimplante3 = TMPREI.FechaReal
		,TRP.PesoReimplante3 = COALESCE(TMPREI.PesoReal, 0)
		,TRP.GananciaReimplante3 = CASE 
			WHEN TMPREI.DiasTranscurridos = 0
				OR COALESCE(TMPREI.PesoReal, 0) = 0
				THEN 0
			ELSE (CAST(COALESCE(TMPREI.PesoReal, 0) AS DECIMAL(18, 2)) - CAST(COALESCE(TRP.PesoOrigen, 0) AS DECIMAL(18, 2))) / CAST(TMPREI.DiasTranscurridos AS DECIMAL(18, 2))
			END
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPREIMPLANTE3 AS TMPREI ON TRP.LoteID = TMPREI.LoteID
	-- CONTEO DE LOS DIAS QUE SE HA SERVIDO LA FORMULA F4R
	SELECT COUNT(DISTINCT (R.FECHA)) AS Dias
		,LoteID
	INTO #DIASFORMULA
	FROM Reparto AS R
	INNER JOIN RepartoDetalle AS RD ON R.RepartoID = RD.RepartoID
	inner join Formula fo on rd.FormulaIDServida = fo.FormulaID
	WHERE fo.TipoFormulaID = 8 -- Formula F4R
	GROUP BY LoteID
	UPDATE TRP
	SET TRP.DiasF4 = TMPDIAS.Dias
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #DIASFORMULA AS TMPDIAS ON TRP.LoteID = TMPDIAS.LoteID
	-- CONTEO DE LOS DIAS QUE SE HA SERVIDO LA FORMULA F2 ZILMAX
	SELECT COUNT(DISTINCT (R.FECHA)) AS Dias
		,LoteID
	INTO #DIASFORMULA2
	FROM Reparto AS R
	INNER JOIN RepartoDetalle AS RD ON R.RepartoID = RD.RepartoID
	inner join Formula fo on rd.FormulaIDServida = fo.FormulaID
	WHERE fo.TipoFormulaID = 4 -- Formula ZILMAX
	GROUP BY LoteID
	UPDATE TRP
	SET TRP.DiasZilmax = TMPDIAS.Dias
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #DIASFORMULA2 AS TMPDIAS ON TRP.LoteID = TMPDIAS.LoteID
	-- SE RECALCULA EL PESO PROYECTADO
	SELECT L.LoteID
		,DATEDIFF(DAY, CAST(ROUND(AVG(ROUND(CAST(EG.FechaEntrada AS FLOAT), 0, 1)), 0) AS SMALLDATETIME), GETDATE()) AS DiasEngordaActual
	INTO #TMPDIASTRANSCURRIDOS
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN Lote AS L ON L.LoteID = TRP.LoteID
	INNER JOIN AnimalMovimiento AS AM ON AM.LoteID = L.LoteID
	INNER JOIN Animal AS A ON A.AnimalID = AM.AnimalID
	INNER JOIN EntradaGanado EG ON EG.OrganizacionID = A.OrganizacionIDEntrada
		AND EG.FolioEntrada = A.FolioEntrada
	WHERE L.LoteID = TRP.LoteID
		AND AM.Activo = 1
		AND A.Activo = 1
	GROUP BY L.LoteID
	UPDATE #TABLAREPORTEPROYECTOR
	SET PesoProyectado = (TDT.DiasEngordaActual * GananciaDiaria) + PesoOrigen
	FROM #TABLAREPORTEPROYECTOR TRP
	INNER JOIN #TMPDIASTRANSCURRIDOS TDT ON TDT.LoteID = TRP.LoteID
	--UPDATE #TABLAREPORTEPROYECTOR SET PesoProyectado = (DiasEngorda * GananciaDiaria) + PesoOrigen
	 --SE OBTIENE EL TIPO DE GANADO DE CADA LOTE
	SELECT COUNT(A.TipoGanadoID) AS ContadorTipoGanado
		,A.TipoGanadoID
		,L.LoteID
	INTO #TMPTIPOGANADO
	FROM Animal AS A
	INNER JOIN AnimalMovimiento AS AM ON A.AnimalID = AM.AnimalID
	INNER JOIN Lote AS L ON L.LoteID = AM.LoteID
	WHERE AM.Activo = 1
	GROUP BY A.TipoGanadoID
		,L.LoteID
	ORDER BY L.LoteID
	SELECT MAX(ContadorTipoGanado) AS ContadorTipoGanado
		,TMPTG.LoteID
		,TG.Descripcion
	INTO #TMPTIPOGANADO2
	FROM #TMPTIPOGANADO AS TMPTG
	INNER JOIN #TABLAREPORTEPROYECTOR AS TRP ON TRP.LoteID = TMPTG.LoteID
	INNER JOIN TipoGanado AS TG ON TG.TipoGanadoID = TMPTG.TipoGanadoID
	GROUP BY TMPTG.LoteID
		,TG.Descripcion
	--INSERT INTO #TIPOSGANADO
	--exec dbo.ObtenerTipoGanadoMayor @OrganizacionID
	UPDATE TRP
	SET TipoGanado = TMPTG2.Descripcion
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPTIPOGANADO2 AS TMPTG2 ON TMPTG2.LoteID = TRP.LoteID
	where TMPTG2.ContadorTipoGanado = (select MAX(tmp.ContadorTipoGanado) from #TMPTIPOGANADO2 tmp where tmp.LoteID = trp.LoteID)
	--UPDATE TRP
	--SET TipoGanado = tg.TipoGanado
	--FROM #TABLAREPORTEPROYECTOR AS TRP
	--INNER JOIN #TIPOSGANADO AS tg ON tg.LoteID = TRP.LoteID
	-- SE OBTIENE LA CLASIFICACION DEL GANADO
	SELECT COUNT(A.ClasificacionGanadoID) AS ContadorClasificacionGanado
		,A.ClasificacionGanadoID
		,L.LoteID
	INTO #TMPCLASIFGANADO
	FROM Animal AS A
	INNER JOIN AnimalMovimiento AS AM ON A.AnimalID = AM.AnimalID
	INNER JOIN Lote AS L ON L.LoteID = AM.LoteID
	WHERE AM.Activo = 1
	GROUP BY A.ClasificacionGanadoID
		,L.LoteID
	ORDER BY L.LoteID
	SELECT MAX(ContadorClasificacionGanado) AS ContadorClasificacionGanado
		,TMPCG.LoteID
		,CG.Descripcion
	INTO #TMPCLASIFGANADO2
	FROM #TMPCLASIFGANADO AS TMPCG
	INNER JOIN #TABLAREPORTEPROYECTOR AS TRP ON TRP.LoteID = TMPCG.LoteID
	INNER JOIN ClasificacionGanado AS CG ON CG.ClasificacionGanadoID = TMPCG.ClasificacionGanadoID
	GROUP BY TMPCG.LoteID
		,CG.Descripcion
	UPDATE TRP
	SET Clasificacion = TMPCG2.Descripcion
	FROM #TABLAREPORTEPROYECTOR AS TRP
	INNER JOIN #TMPCLASIFGANADO2 AS TMPCG2 ON TMPCG2.LoteID = TRP.LoteID
	where TMPCG2.ContadorClasificacionGanado = (select MAX(tmp.ContadorClasificacionGanado) from #TMPCLASIFGANADO2 tmp where tmp.LoteID = trp.LoteID)
	SELECT CodigoCorral
		,ISNULL(LoteID, 0) AS LoteID
		,CodigoLote
		,ISNULL(Cabezas, 0) AS Cabezas
		,TipoGanado
		,Clasificacion
		,PesoOrigen
		,Merma
		,ISNULL(PesoProyectado, 0) AS PesoProyectado
		,ISNULL(GananciaDiaria, 0.0) AS GananciaDiaria
		,ISNULL(DiasEngorda, 0) AS DiasEngorda
		,FechaReimplante1
		,PesoReimplante1
		,GananciaReimplante1
		,FechaReimplante2
		,PesoReimplante2
		,GananciaReimplante2
		,FechaReimplante3
		,PesoReimplante3
		,GananciaReimplante3
		,ISNULL(DiasF4, 0) AS DiasF4
		,ISNULL(DiasZilmax, 0) AS DiasZilmax
		,FechaSacrificio
	FROM #TABLAREPORTEPROYECTOR
	order by RIGHT('00000' + LTRIM(RTRIM(CodigoCorral)), 5) asc
	DROP TABLE #TIPOSGANADO
	DROP TABLE #TMPREIMPLANTE1
	DROP TABLE #TMPREIMPLANTE2
	DROP TABLE #TMPREIMPLANTE3
	DROP TABLE #TMPMERMAS
	DROP TABLE #TMPPESOORIGEN
	DROP TABLE #DIASFORMULA
	DROP TABLE #DIASFORMULA2
	DROP TABLE #TABLAREPORTEPROYECTOR
	DROP TABLE #TMPDIASTRANSCURRIDOS
	DROP TABLE #TMPCLASIFGANADO
	DROP TABLE #TMPCLASIFGANADO2
	SET NOCOUNT OFF;
END

GO

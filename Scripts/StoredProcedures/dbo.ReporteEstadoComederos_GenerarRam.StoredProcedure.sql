USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_GenerarRam]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteEstadoComederos_GenerarRam]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_GenerarRam]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: 25/04/2013
-- Description: Consulta para obtener la informacion base para el reporte estado comederos
-- Empresa: SuKarne
-- Uso: ReporteEstadoComederos_GenerarRam 1 , '2014-09-24'
-- =============================================
create procedure [dbo].[ReporteEstadoComederos_GenerarRam]
	@OrganizacionID int,
	@Fecha DATETIME
as
begin
	SET NOCOUNT ON
	--************************************************************************
	--* D E C L A R A C I O N   D E   V A R I A B L E S 
	--************************************************************************

	DECLARE @hoy bigint;
	DECLARE @maniana bigint;
	DECLARE @TipoMovimiento_Corte tinyint;
	DECLARE @TipoMovimiento_CorteTransferencia tinyint;
	DECLARE @TipoServicio_Vesp tinyint;

	SET @hoy = cast(@Fecha as bigint);
	SET @maniana = cast(dateadd(d, 1, @Fecha) as bigint);
	SET @TipoMovimiento_Corte = 5;
	SET @TipoMovimiento_CorteTransferencia = 13;
	SET @TipoServicio_Vesp = 2;

	CREATE TABLE #Lotes
	(
		LoteID int
		, CorralID int
		, OrganizacionID int
		, Codigo varchar(50)
		, Cabezas int
		, Sexo varchar(1)
		, PesoLote float
		, TipoGanadoID int
		, DiasEngordaEntrada int default 0
		, DiasEngordaInicio int default 0
		, GananciaCorral int default 0
		, UltimaFormulaID int default 0 
		, DiasUltimaFormula int default 0
	)

	--************************************************************************
	--* OBTENER LOS LOTES ACTIVOS Y QUE TENGAN REPARTOS
	--************************************************************************
	INSERT INTO #Lotes (LoteID)
	SELECT
		l.LoteID
	FROM
		Lote l
		INNER JOIN AnimalMovimiento am on l.LoteID = am.LoteID
		INNER JOIN Reparto r on l.CorralID = r.CorralID --l.LoteID = r.LoteID
	WHERE
		l.Activo = 1
		AND l.OrganizacionID = @OrganizacionID
		AND CAST(r.Fecha AS DATE) = CAST(GETDATE() AS DATE)
	GROUP BY l.LoteID
	
	UPDATE ls
	SET
		CorralID = c.CorralID
		, Codigo = c.Codigo
		, Cabezas = l.Cabezas
		, DiasEngordaInicio = DATEDIFF(d, l.FechaInicio, @Fecha)
		, OrganizacionID = @OrganizacionID
	FROM	
		#Lotes ls 
		INNER JOIN Lote l on ls.LoteID = l.LoteID
		INNER JOIN Corral c on l.CorralID = c.CorralID

	--************************************************************************
	--* OBTENER EL SEXO, EL PESO DEL LOTE Y LOS DIAS DE ENGORDA
	--************************************************************************
	CREATE TABLE #t1 
	(
		LoteID int
		, Sexo varchar(1)
		, PesoLote float
	)

	CREATE TABLE #tt1 
	(
		LoteID int
		, Animal int
		, Fecha datetime
		, PesoLote float
	)

	CREATE TABLE #tt2 
	(
		LoteID int
		, Animal int
		, Fecha datetime
		, PesoLote float
	)

	INSERT INTO #tt1
	SELECT 
		ls.LoteID
		, am.AnimalID
		, am.FechaMovimiento
		, am.Peso
	FROM
		#Lotes ls 
		INNER JOIN AnimalMovimiento am on ls.LoteID = am.LoteID
/*	WHERE
		am.TipoMovimientoID = @TipoMovimiento_Corte

	INSERT INTO #tt1
	SELECT 
		ls.LoteID
		, am.AnimalID
		, am.FechaMovimiento
		, am.Peso
	FROM
		#Lotes ls 
		INNER JOIN AnimalMovimiento am on ls.LoteID = am.LoteID
	WHERE
		am.TipoMovimientoID = @TipoMovimiento_CorteTransferencia
		AND AM.AnimalID NOT IN (SELECT T.Animal FROM #tt1 T WHERE T.LoteID = LS.LoteID)

	INSERT INTO #tt2
	SELECT 
		ls.LoteID
		, am.AnimalID
		, am.FechaMovimiento
		, am.Peso
	FROM
		#Lotes ls 
		INNER JOIN AnimalMovimiento am on ls.LoteID = am.LoteID
	WHERE
		am.TipoMovimientoID = @TipoMovimiento_CorteTransferencia
		AND AM.AnimalID IN (SELECT T.Animal FROM #tt1 T WHERE T.LoteID = LS.LoteID)

	UPDATE T
	SET
		PesoLote = T2.PesoLote
	FROM
		#tt1 T
		INNER JOIN #tt2 T2 ON T.LoteID = T2.LoteID AND T.Animal = T2.Animal
	WHERE
		DATEDIFF(D, T.Fecha, T2.Fecha) > 0
*/
	INSERT INTO #t1
	SELECT
		T.LoteID
		, TG.Sexo
		, SUM(T.PesoLote)
	FROM
		#tt1 T
		INNER JOIN Animal A ON T.Animal = A.AnimalID
		INNER JOIN TipoGanado TG ON A.TipoGanadoID = TG.TipoGanadoID
	GROUP BY
		T.LoteID
		, TG.Sexo

	UPDATE lt
	SET
		Sexo = t.Sexo
		, PesoLote = t.PesoLote
	FROM
		#Lotes lt
		INNER JOIN #t1 t on lt.LoteID = t.LoteID

	--************************************************************************
	--* OBTENER EL TIPO DE GANADO DEL LOTE
	--************************************************************************
	SELECT *
	INTO #TIPOSGANADOCORTE
	FROM dbo.ObtenerTipoGanadoRam(@OrganizacionID, 0, @TipoMovimiento_Corte, '')
	
	SELECT *
	INTO #TIPOSGANADOCORTETRANS
	FROM dbo.ObtenerTipoGanadoRam(@OrganizacionID, 0, @TipoMovimiento_CorteTransferencia, '')

	UPDATE l
	SET
 		TipoGanadoID = T.TipoGanadoID 
	FROM
		#Lotes l 
		INNER JOIN #TIPOSGANADOCORTE T ON l.LoteId = T.LoteId AND l.Sexo = T.Sexo
/*
	UPDATE l
	SET
 		TipoGanadoID = (select TipoGanadoID from #TIPOSGANADOCORTETRANS T WHERE l.LoteId = T.LoteId AND l.Sexo = T.Sexo)
	FROM
		#Lotes l 
	WHERE
		l.TipoGanadoID is null
*/
	UPDATE l
	SET
 		TipoGanadoID = 0
	FROM
		#Lotes l 
	WHERE
		l.TipoGanadoID is null

	--************************************************************************
	--* OBTENER LOS DIAS DE ENGORDA SEGUN LOS FOLIOS DE ENTRADA DE LOS ANIMALES
	--************************************************************************
	CREATE TABLE #t2 
	(
		LoteID int
		, DiasEntrada float
	)

	INSERT INTO #t2
	SELECT
		l.LoteID
		, sum(DATEDIFF(d, eg.FechaEntrada, @Fecha)) 
	FROM
		#tt1 l
		INNER JOIN Animal a on l.Animal = a.AnimalID
		INNER JOIN EntradaGanado eg on a.FolioEntrada = eg.FolioEntrada
		WHERE eg.OrganizacionId = @OrganizacionID
	GROUP BY
		l.LoteID

	UPDATE l
	SET
		DiasEngordaEntrada = t.DiasEntrada
	FROM
		#Lotes l
		INNER JOIN #t2 t on l.LoteID = t.LoteID

	--************************************************************************
	--* OBTENER LA GANANCIA DEL CORRAL
	--************************************************************************
	UPDATE l
	SET
		GananciaCorral = (CASE l.Cabezas WHEN 0 THEN 0 ELSE lp.GananciaDiaria * (l.DiasEngordaEntrada / l.Cabezas) END)
	FROM
		#Lotes l
		INNER JOIN LoteProyeccion lp on l.LoteID = lp.LoteID

	--************************************************************************
	--* OBTENER LA ULTIMA FORMULA APLICADA AL LOTE ASI COMO SUS DIAS DE 
	--* DE REPARTO CONSECUTIVOS
	--************************************************************************
	CREATE TABLE #t3
	(
		LoteID int
		, Fecha datetime
		, FormulaID int
	)

	CREATE TABLE #txf
	(
		LoteID int
		, Fecha datetime
		, FormulaID int
	)

	INSERT INTO #t3 (LoteID, Fecha)
	SELECT 
		r.LoteID
		, MAX(r.Fecha)
	FROM
		Reparto r
		INNER JOIN RepartoDetalle rd on r.RepartoID = rd.RepartoID
		INNER JOIN #Lotes l on r.LoteID = l.LoteID
	WHERE
		rd.TipoServicioID = @TipoServicio_Vesp
	GROUP BY
		r.LoteID

	UPDATE t
	SET
		FormulaID = rd.FormulaIDServida
	FROM
		#t3 t
		INNER JOIN Reparto r on t.LoteID = r.LoteID AND t.Fecha = r.Fecha
		INNER JOIN RepartoDetalle rd on r.RepartoID = rd.RepartoID

select * from #t3

	INSERT INTO #txf (LoteID, Fecha)
	SELECT 
		r.LoteID
		, MAX(r.Fecha)
	FROM
		Reparto r
		INNER JOIN RepartoDetalle rd on r.RepartoID = rd.RepartoID
		INNER JOIN #Lotes l on r.LoteID = l.LoteID
		INNER JOIN #t3 T3 ON T3.LoteID = l.LoteID
	WHERE
		rd.TipoServicioID = @TipoServicio_Vesp AND rd.FormulaIDServida <> T3.FormulaID
	GROUP BY
		r.LoteID

select * from #txf

	CREATE TABLE #t4 
	(
		LoteID int
		, FormulaID int
		, Dias int
	)

	INSERT INTO #t4
	SELECT
		r.LoteID
		, rd.FormulaIDServida
		, COUNT(distinct r.Fecha)
	FROM
		Reparto r
		INNER JOIN RepartoDetalle rd on r.RepartoID = rd.RepartoID
		INNER JOIN #t3 t on r.LoteID = t.LoteID AND rd.FormulaIDServida = t.FormulaID
		INNER JOIN #txf TX ON r.LoteID = TX.LoteID
	WHERE
		rd.TipoServicioID = @TipoServicio_Vesp AND r.Fecha between TX.Fecha AND t.Fecha
	GROUP BY
		r.LoteID
		, rd.FormulaIDServida

	UPDATE l
	SET
		UltimaFormulaID = t.FormulaID
		, DiasUltimaFormula = t.Dias
	FROM
		#Lotes l
		INNER JOIN #t4 t on l.LoteID = t.LoteID

	--************************************************************************
	--* INFORMACION CONCENTRADA
	--************************************************************************
	SELECT
		LoteID
		, CorralID
		, OrganizacionID
		, Codigo
		, Cabezas
		, Sexo
		, coalesce(PesoLote, 0) as PesoLote
		, TipoGanadoID
		, DiasEngordaEntrada
		, DiasEngordaInicio
		, GananciaCorral
		, UltimaFormulaID
		, DiasUltimaFormula
	FROM
		#Lotes l
	WHERE l.Codigo <> 'ZZZ'
	ORDER BY CASE IsNumeric(Codigo) 
          WHEN 1 THEN Replicate('0', 100 - Len(Codigo)) + Codigo 
          ELSE Codigo
         END
	--************************************************************************
	--* OBTENER EL DETALLADO DEL REPORTE
	--************************************************************************
	CREATE TABLE #Lector
	(
		LoteID int
		, Fecha varchar(10)
		, EstadoComederoID int
	)

	INSERT INTO #Lector
	SELECT
		l.LoteID
		, CONVERT(varchar(10), lr.Fecha, 112) as Fecha
		, lr.EstadoComederoID
	FROM
		LectorRegistro lr
		INNER JOIN #Lotes l on lr.LoteID = l.LoteID
	WHERE
		CONVERT(varchar(10), lr.Fecha, 112) >= CONVERT(varchar(10), @Fecha - 7, 112)
	
	SELECT
		l.LoteID
		, r.Fecha
		, rd.TipoServicioID
		, rd.CantidadProgramada
		, rd.FormulaIDProgramada
		, rd.CantidadServida
		, Coalesce(rd.FormulaIDServida, 0) as FormulaIDServida
		, COALESCE(lr.EstadoComederoID, 0) AS EstadoComederoID
		, DATEDIFF(d, r.Fecha, @Fecha) as Dias
	FROM
		#Lotes l
		INNER JOIN Reparto r on l.LoteID = r.LoteID
		INNER JOIN RepartoDetalle rd on r.RepartoID = rd.RepartoID
		LEFT JOIN #Lector lr on r.LoteID = lr.LoteID AND CONVERT(varchar(10), r.Fecha, 112) = lr.Fecha
	WHERE
		CONVERT(varchar(10), r.Fecha, 112) >= CONVERT(varchar(10), @Fecha - 7, 112)
		AND l.Codigo <> 'ZZZ'
	ORDER BY
		l.LoteID
		, r.Fecha DESC
		, rd.TipoServicioID DESC
/*
SELECT * FROM #Lotes ORDER BY LOTEID
SELECT * FROM #t1 ORDER BY LOTEID
SELECT * FROM #tt1 ORDER BY LOTEID
SELECT * FROM #tt2 ORDER BY LOTEID
SELECT * FROM #TIPOSGANADOCORTE ORDER BY LOTEID
SELECT * FROM  #TIPOSGANADOCORTETRANS ORDER BY LOTEID
*/
	DROP TABLE #t1
	DROP TABLE #tt1
	DROP TABLE #tt2
	DROP TABLE #t3
	DROP TABLE #t4
	DROP TABLE #txf
	DROP TABLE #Lector
	DROP TABLE #TIPOSGANADOCORTE
	DROP TABLE #TIPOSGANADOCORTETRANS
	DROP TABLE #Lotes
	SET NOCOUNT OFF
end

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteProyectorComportamiento_Obtener_E]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteProyectorComportamiento_Obtener_E]
GO
/****** Object:  StoredProcedure [dbo].[ReporteProyectorComportamiento_Obtener_E]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--======================================================
-- Author     : Jorge Luis Velázquez Araujo
-- Create date: 20/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ReporteProyectorComportamiento_Obtener_E 1
--======================================================
CREATE PROCEDURE [dbo].[ReporteProyectorComportamiento_Obtener_E] @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TipoMovimientoCorte INT = 5
	DECLARE @TipoMovimientoCorteTransferencia INT = 13

	CREATE TABLE #TABLACORRALES (
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
		)

	CREATE TABLE #TABLAANIMALES (
		CorralID INT
		,AnimalID BIGINT
		,Arete VARCHAR(15)
		,AreteMetalico VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,Sexo CHAR(1)
		,CalidadGanadoID INT
		,CalidadGanado VARCHAR(50)
		,ClasificacionGanadoID INT
		,ClasificacionGanado VARCHAR(50)
		,PesoCompra INT
		,OrganizacionIDEntrada INT
		,FolioEntrada BIGINT
		,PesoLlegada INT
		,Paletas INT
		,CausaRechadoID INT
		,Venta BIT
		,Cronico BIT
		,DiasEngorda INT
		)

	CREATE TABLE #TABLALOTEPROYECCION (
		LoteProyeccionID INT
		,LoteID INT
		,OrganizacionID INT
		,Frame DECIMAL(18, 2)
		,GananciaDiaria DECIMAL(18, 2)
		,ConsumoBaseHumeda DECIMAL(18, 2)
		,Conversion DECIMAL(18, 2)
		,PesoMaduro INT
		,PesoSacrificio INT
		,DiasEngorda INT
		,FechaEntradaZilmax SMALLDATETIME
		)

	CREATE TABLE #TABLALOTEREIMPLANTE (
		LoteReimplanteID INT
		,LoteProyeccionID INT
		,NumeroReimplante INT
		,FechaProyectada SMALLDATETIME
		,PesoProyectado INT
		,FechaReal SMALLDATETIME
		,PesoReal INT
		)

	CREATE TABLE #TIPOSGANADO (
		TipoGanadoID INT
		,Descripcion VARCHAR(50)
		,LoteID INT
		,Cabezas INT
		,PesoLote INT
		,PesoPromedio INT
		,PesoMinimo INT
		,PesoMaximo INT
		,Sexo CHAR(1)
		)

	CREATE TABLE #DIASF4 (
		LoteID INT
		,DiasF4 INT
		)

	CREATE TABLE #DIASZILMAX (
		LoteID INT
		,DiasZilmax INT
		)

	CREATE TABLE #SEXOSLOTE (
		LoteID INT
		,Sexo CHAR(1)
		)

	DECLARE @DiasSacrificio INT

	SET @DiasSacrificio = dbo.ObtenerDiasSacrificio(@OrganizacionID)

	INSERT INTO #TABLACORRALES
	SELECT co.CorralID
		,co.Codigo
		,lo.LoteID
		,lo.Lote
		,lo.Cabezas
		,'' --(select Descripcion from dbo.ObtenerTipoGanado(@OrganizacionID,lo.LoteID,5/*Tipo Movimiento Corte*/, dbo.ObtenerSexoGanadoCorral(@OrganizacionId,lo.LoteID)))  
		--,(dbo.ObtenerTipoGanadoCorral(@OrganizacionID, co.CorralID))  
		,lo.FechaInicio
		,lo.FechaDisponibilidad
		,lo.DisponibilidadManual
		,0 --(dbo.ObtenerDiasF4(@OrganizacionID, lo.LoteID))
		,0 --(dbo.ObtenerDiasZilmax(@OrganizacionID, lo.LoteID, 4)) --Tipo Formula, Finalizacion  
		,0 --(dbo.ObtenerDiasSacrificio(@OrganizacionID))
	FROM Corral co
	INNER JOIN Lote lo ON co.CorralID = lo.CorralID
	INNER JOIN LoteProyeccion lp ON lo.LoteID = lp.LoteID
	INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
	INNER JOIN GrupoCorral gc ON tc.GrupoCorralID = gc.GrupoCorralID
	WHERE co.OrganizacionID = @OrganizacionID
		AND co.Activo = 1
		AND co.Codigo <> 'ZZZ'
		AND lo.Activo = 1
		AND gc.GrupoCorralID = 2 --Corrales del Grupo de Produccion  
		--AND tc.TipoCorralID <> 5
		--AND lo.FechaInicio BETWEEN '20140320' AND '20140330'
	INSERT INTO #TABLAANIMALES
	SELECT am.CorralID
		,a.AnimalID
		,Arete
		,AreteMetalico
		,FechaCompra
		,a.TipoGanadoID
		,''--tg.Descripcion AS [TipoGanado]
		,''--tg.Sexo
		,a.CalidadGanadoID
		,''--cg.Descripcion AS [CalidadGanado]
		,a.ClasificacionGanadoID
		,''--clg.Descripcion AS [ClasificacionGanado]
		,PesoCompra
		,OrganizacionIDEntrada
		,a.FolioEntrada
		,a.PesoLlegada
		,a.Paletas
		,a.CausaRechadoID
		,a.Venta
		,a.Cronico
		,DATEDIFF(DAY, EG.FechaEntrada, getdate()) AS dias
	FROM #TABLACORRALES tc
	INNER JOIN AnimalMovimiento am on tc.LoteID = am.LoteID
	INNER JOIN Animal a ON a.AnimalID = am.AnimalID
	INNER JOIN EntradaGanado EG ON a.FolioEntrada = EG.FolioEntrada
	where am.TipoMovimientoID in (@TipoMovimientoCorteTransferencia, @TipoMovimientoCorte) --Tipomovimiento de Corte
	--INNER JOIN TipoGanado tg ON a.TipoGanadoID = tg.TipoGanadoID
	--INNER JOIN CalidadGanado cg ON a.CalidadGanadoID = cg.CalidadGanadoID
	--INNER JOIN ClasificacionGanado clg ON a.ClasificacionGanadoID = clg.ClasificacionGanadoID			
	 
	

	INSERT INTO #TABLALOTEPROYECCION
	SELECT LoteProyeccionID
		,lp.LoteID
		,OrganizacionID
		,Frame
		,GananciaDiaria
		,ConsumoBaseHumeda
		,Conversion
		,PesoMaduro
		,PesoSacrificio
		,DiasEngorda
		,FechaEntradaZilmax
	FROM LoteProyeccion lp
	INNER JOIN #TABLACORRALES tc ON lp.LoteID = tc.LoteID
	
	

	INSERT INTO #TABLALOTEREIMPLANTE
	SELECT LoteReimplanteID
		,lr.LoteProyeccionID
		,NumeroReimplante
		,FechaProyectada
		,PesoProyectado
		,FechaReal
		,PesoReal
	FROM LoteReimplante lr
	INNER JOIN #TABLALOTEPROYECCION lp ON lr.LoteProyeccionID = lp.LoteProyeccionID
	
	

	INSERT INTO #TIPOSGANADO
	SELECT *
	FROM dbo.ObtenerTipoGanado(@OrganizacionID, 0, @TipoMovimientoCorte, '')
	
	

	INSERT INTO #DIASF4
	SELECT r.LoteID
		,COUNT(r.RepartoID) DiasF4
	FROM #TABLACORRALES tc
	inner join Reparto r on tc.LoteID = r.LoteID
	WHERE r.OrganizacionID = @OrganizacionID
		AND r.Activo = 1
		AND (
			SELECT COUNT(rd.RepartoDetalleID)
			FROM RepartoDetalle rd
			INNER JOIN Formula fo ON rd.FormulaIDServida = fo.FormulaID
			WHERE r.RepartoID = rd.RepartoID
				AND fo.TipoFormulaID = 3 --Tipo Formula Produccion
				AND rd.Activo = 1
			) = 2
			group by r.LoteID
			
			

	INSERT INTO #DIASZILMAX
	SELECT r.LoteID
		,COUNT(r.RepartoID) DiasZilmax
	FROM #TABLACORRALES tc
	inner join Reparto r on tc.LoteID = r.LoteID
	WHERE r.OrganizacionID = @OrganizacionID		
		AND r.Activo = 1
		AND (
			SELECT COUNT(rd.RepartoDetalleID)
			FROM RepartoDetalle rd
			INNER JOIN Formula f ON rd.FormulaIDServida = F.FormulaID
			WHERE r.RepartoID = rd.RepartoID
				AND rd.Activo = 1
				AND F.TipoFormulaID = 4 --Tipo Formula, Finalizacion   
			) = 2
			group by r.LoteID
			
			

	INSERT INTO #SEXOSLOTE
	SELECT a.LoteID
		,tg.Sexo
	FROM (
		SELECT a.LoteID
			,a.TipoGanadoID
			,ROW_NUMBER() OVER (
				PARTITION BY LoteId ORDER BY LoteId
				) AS [Orden]
		FROM (
			SELECT am.LoteId
				,a.TipoGanadoID
			FROM AnimalMovimiento am
			INNER JOIN Animal a ON a.AnimalID = am.AnimalID
			WHERE am.OrganizacionId = @OrganizacionId
			GROUP BY am.LoteId
				,a.TipoGanadoID
			) a
		) a
	INNER JOIN TipoGanado tg ON tg.TipoGanadoID = a.TipoGanadoID
	WHERE Orden = 1	
	

	SELECT tc.CorralID
		,tc.CodigoCorral
		,tc.LoteID
		,tc.CodigoLote
		,tc.Cabezas
		,isnull(tg.Descripcion,'') AS TipoGanado
		,tc.FechaInicio
		,tc.FechaDisponibilidad
		,tc.DisponibilidadManual
		,ISNULL(f4.DiasF4,0) as DiasF4
		,ISNULL(dz.DiasZilmax,0) AS DiasZilmax
		,ISNULL(@DiasSacrificio,0) AS DiasSacrificio
	FROM #TABLACORRALES tc
	LEFT JOIN #DIASF4 f4 ON tc.LoteID = f4.LoteID
	LEFT JOIN #DIASZILMAX dz ON tc.LoteID = dz.LoteID
	LEFT JOIN #SEXOSLOTE sl ON tc.LoteID = sl.LoteID
	LEFT JOIN #TIPOSGANADO tg ON (
			tc.LoteID = tg.LoteID
			AND tg.Sexo = sl.Sexo
			)
	ORDER BY CASE IsNumeric(CodigoCorral) 
          WHEN 1 THEN Replicate('0', 100 - Len(CodigoCorral)) + CodigoCorral 
          ELSE CodigoCorral
         END

	SELECT ta.CorralID
		,ta.AnimalID
		,ta.Arete
		,ta.AreteMetalico
		,ta.FechaCompra
		,tg.TipoGanadoID
		,tg.Descripcion AS TipoGanado
		,tg.Sexo
		,cg.CalidadGanadoID
		,cg.Descripcion AS CalidadGanado
		,clg.ClasificacionGanadoID
		,clg.Descripcion AS ClasificacionGanado
		,ta.PesoCompra
		,ta.OrganizacionIDEntrada
		,ta.FolioEntrada
		,ta.PesoLlegada
		,ta.Paletas
		,ta.CausaRechadoID
		,ta.Venta
		,ta.Cronico
		,ta.DiasEngorda
	FROM #TABLAANIMALES ta
	left JOIN TipoGanado tg ON ta.TipoGanadoID = tg.TipoGanadoID
	left JOIN CalidadGanado cg ON ta.CalidadGanadoID = cg.CalidadGanadoID
	left JOIN ClasificacionGanado clg ON ta.ClasificacionGanadoID = clg.ClasificacionGanadoID			

	SELECT LoteProyeccionID
		,LoteID
		,OrganizacionID
		,Frame
		,GananciaDiaria
		,ConsumoBaseHumeda
		,Conversion
		,PesoMaduro
		,PesoSacrificio
		,DiasEngorda
		,FechaEntradaZilmax
	FROM #TABLALOTEPROYECCION

	SELECT LoteReimplanteID
		,LoteProyeccionID
		,NumeroReimplante
		,FechaProyectada
		,PesoProyectado
		,FechaReal
		,PesoReal
	FROM #TABLALOTEREIMPLANTE

	drop table #TABLALOTEREIMPLANTE
	drop table #TABLALOTEPROYECCION
	drop table #TABLAANIMALES
	drop table #DIASF4
	drop table #DIASZILMAX
	drop table #SEXOSLOTE
	drop table #TIPOSGANADO

	SET NOCOUNT OFF;
END

GO

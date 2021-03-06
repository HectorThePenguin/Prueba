USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerLectorComederoPorOrganizacionId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerLectorComederoPorOrganizacionId]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerLectorComederoPorOrganizacionId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:    Jesus Alberto Garcia Reyes  
-- Create date: 02-04-2014    
-- Description:  Obtiene la información del lector corral.  
-- Corral_ObtenerLectorComederoPorOrganizacionId 1  
-- =============================================    
CREATE PROCEDURE [dbo].[Corral_ObtenerLectorComederoPorOrganizacionId] @OrganizacionID INT
AS
    BEGIN    

		--DECLARE @OrganizacionID INT
		--SET @OrganizacionID = 1

		  IF OBJECT_ID('TEMPDB..#tblAliLectorCorrales') IS NOT NULL
	       DROP TABLE #tblAliLectorCorrales

		  IF OBJECT_ID('TEMPDB..#LoteSexo') IS NOT NULL
			 DROP TABLE #LoteSexo

		  IF OBJECT_ID('TEMPDB..#LoteEntrada') IS NOT NULL
			 DROP TABLE #LoteEntrada

		  IF OBJECT_ID('TEMPDB..#LoteUltimoReparto') IS NOT NULL
			 DROP TABLE #LoteUltimoReparto

		  IF OBJECT_ID('TEMPDB..#LoteUltimoAjuste') IS NOT NULL
			 DROP TABLE #LoteUltimoAjuste

		  CREATE TABLE #tblAliLectorCorrales
		  (
			 IdGanadera int
			 ,IdSeccion int
			 ,IdCorralEngorda varchar(10)
			 ,IdLoteEngorda varchar(10)
			 ,Secuencia int
			 ,IdTipoGanado int
			 ,TipoGanado varchar(50)
			 ,IdEtapaProduccion int
			 ,Cabezas int
			 ,IdUltimaFormula int
			 ,UltimaFormula varchar(10)
			 ,IdUltimaFormula2 int
			 ,UltimaFormula2 varchar(10)
			 ,IdUltimoForraje int
			 ,UltimoForraje varchar(10)
			 ,FechaInicio smalldatetime
			 ,FechaDisponible smalldatetime
			 ,PesoInicio int
			 ,PesoProyectado int
			 ,DiasFormula int
			 ,DiasTransicion int
			 ,FechaReg smalldatetime
			 ,UsuarioReg varchar(100)
			 ,Activo bit
			 ,OrganizacionID int
			 ,Seccion int
			 ,Orden int
			 ,LoteID int
		  )

		  CREATE TABLE #LoteSexo 
		  (
			 LoteID int
			 ,PesoOrigen int
			 ,Sexo char(1)
		  )

		  CREATE TABLE #LoteEntrada
		  (
			 LoteID int
			 ,FechaEntrada smalldatetime
		  )

		  CREATE TABLE #LoteUltimoReparto
		  (
			 LoteID int
			 ,UltimoRepartoID int
		  )

		  CREATE TABLE #LoteUltimoAjuste
		  (
			 LoteID int
			 ,UltimoAjusteID int
		  )

		  --Se obtiene base de lotes
		  INSERT INTO #tblAliLectorCorrales 
		  (
			 IdGanadera
			 ,IdSeccion
			 ,IdCorralEngorda
			 ,IdLoteEngorda
			 ,Secuencia
			 ,IdTipoGanado
			 ,Tipoganado
			 ,IdEtapaProduccion
			 ,Cabezas
			 ,IdUltimaFormula
			 ,UltimaFormula
			 ,IdUltimaformula2
			 ,UltimaFormula2
			 ,IdUltimoForraje
			 ,UltimoForraje
			 ,FechaInicio
			 ,FechaDisponible
			 ,PesoInicio
			 ,PesoProyectado
			 ,DiasFormula
			 ,DiasTransicion
			 ,FechaReg
			 ,UsuarioReg
			 ,Activo
			 ,OrganizacionID
			 ,Seccion
			 ,Orden
			 ,LoteID
			 )
		  SELECT l.OrganizacionID AS IdGanadera
			 ,c.Seccion AS IdSeccion
			 ,c.Codigo AS IdCorralEngorda
			 ,l.Lote AS IdLoteEngorda
			 ,c.Orden AS Secuencia
			 , 0
			 , ''
			 ,l.TipoCorralID AS IdEtapaProduccion
			 ,l.Cabezas
			 , 0
			 , ''
			 , 0
			 , ''
			 , 0
			 , ''
			 ,l.FechaInicio
			 ,COALESCE(l.FechaDisponibilidad, DATEADD(DAY, lp.DiasEngorda, l.FechaInicio)) AS FechaDisponibilidad
			 , 0
			 , 0
			 , 0
			 , 0
			 ,l.FechaCreacion
			 ,u.Nombre
			 ,l.Activo
			 ,l.OrganizacionID
			 ,c.Seccion
			 ,c.Orden
			 ,l.LoteID
		  FROM Lote l (NOLOCK)
		  INNER JOIN Corral c (NOLOCK) ON c.CorralID = l.CorralID
		  INNER JOIN Usuario u (NOLOCK) ON u.UsuarioID = c.UsuarioCreacionID
		  LEFT JOIN LoteProyeccion lp ON lp.LoteID = l.LoteID
		  WHERE l.OrganizacionID = @OrganizacionID
		  AND l.Activo = 1
		  AND c.TipoCorralID NOT IN (1, 4, 6, 10)

		  --Obtenemos el peso promedio y el sexo
		  INSERT INTO #LoteSexo
		  SELECT lc.LoteID
				, SUM(a.PesoCompra)/NULLIF(COUNT(am.AnimalID),0) AS PesoOrigen
				, CASE WHEN SUM(CASE tg.Sexo WHEN 'M' THEN 1 ELSE 0 END) >= SUM(CASE tg.Sexo WHEN 'H' THEN 1 ELSE 0 END) THEN 'M' ELSE 'H' END AS Sexo
		  FROM #tblAliLectorCorrales lc
		  INNER JOIN AnimalMovimiento am(NOLOCK) ON am.LoteID = lc.LoteID AND am.Activo = 1
		  INNER JOIN Animal a(NOLOCK) ON a.AnimalID = am.AnimalID
		  INNER JOIN TipoGanado tg ON tg.TipoGanadoID = a.TipoGanadoID
		  GROUP BY lc.LoteID

		  --Se actualiza el tipo de ganado
		  UPDATE #tblAliLectorCorrales SET IdTipoGanado = tg.TipoGanadoID, TipoGanado = tg.Descripcion, PesoInicio = ls.PesoOrigen
		  FROM #tblAliLectorCorrales ta
		  INNER JOIN #LoteSexo ls ON ls.LoteID = ta.LoteID
		  INNER JOIN TipoGanado tg ON tg.Sexo = ls.Sexo AND ls.PesoOrigen BETWEEN tg.PesoMinimo AND tg.PesoMaximo

		  --Obtenemos el ultimo reparto servido
		  INSERT INTO #LoteUltimoReparto
		  SELECT ta.LoteID, MAX(r.RepartoID) AS UltimoRepartoID
		  FROM #tblAliLectorCorrales ta
		  INNER JOIN Reparto r ON r.LoteID = ta.LoteID
		  INNER JOIN RepartoDetalle rd ON rd.RepartoID = r.RepartoID
		  WHERE rd.Servido = 1 AND rd.TipoServicioID != 3
		  GROUP BY ta.LoteID

		  --Obtenemos el ultimo ajuste
		  INSERT INTO #LoteUltimoAjuste
		  SELECT ta.LoteID, MAX(r.RepartoID) AS UltimoAjusteID
		  FROM #tblAliLectorCorrales ta
		  INNER JOIN Reparto r ON r.LoteID = ta.LoteID
		  INNER JOIN RepartoDetalle rd ON rd.RepartoID = r.RepartoID
		  WHERE rd.Servido = 1
		  GROUP BY ta.LoteID

		  --Actulizamos el ultimo reparto
		  UPDATE #LoteUltimoReparto SET UltimoRepartoID = lua.UltimoAjusteID
		  FROM #LoteUltimoReparto lur
		  INNER JOIN #LoteUltimoAjuste lua ON lua.LoteID = lur.LoteID
		  WHERE lua.UltimoAjusteID > lur.UltimoRepartoID

		  --Se actualiza la ultima formula de la mañana
		  UPDATE #tblAliLectorCorrales SET IdUltimaFormula = COALESCE(rd.FormulaIDServida, rd.FormulaIDProgramada), UltimaFormula = f.Descripcion
		  FROM #LoteUltimoReparto lur
		  INNER JOIN RepartoDetalle rd ON rd.RepartoID = lur.UltimoRepartoID
		  INNER JOIN Formula f ON f.FormulaID = COALESCE(rd.FormulaIDServida, rd.FormulaIDProgramada)
		  INNER JOIN #tblAliLectorCorrales ta ON ta.LoteID = lur.LoteID
		  WHERE rd.TipoServicioID = 1

		  --Se actualiza la ultima formula de la tarde
		  UPDATE #tblAliLectorCorrales SET IdUltimaFormula2 = COALESCE(rd.FormulaIDServida, rd.FormulaIDProgramada), UltimaFormula2 = f.Descripcion
		  FROM #LoteUltimoReparto lur
		  INNER JOIN RepartoDetalle rd ON rd.RepartoID = lur.UltimoRepartoID
		  INNER JOIN Formula f ON f.FormulaID = COALESCE(rd.FormulaIDServida, rd.FormulaIDProgramada)
		  INNER JOIN #tblAliLectorCorrales ta ON ta.LoteID = lur.LoteID
		  WHERE rd.TipoServicioID = 2

		  --Actualizamos los dias en formula
		  UPDATE #tblAliLectorCorrales SET DiasFormula = df.DiasFormula
		  FROM #tblAliLectorCorrales ta
		  INNER JOIN (
			 SELECT ta.LoteID, rd.FormulaIDServida, COUNT(DISTINCT r.Fecha) AS DiasFormula
			 FROM #tblAliLectorCorrales ta
			 INNER JOIN Reparto r ON r.LoteID = ta.LoteID
			 INNER JOIN RepartoDetalle rd ON rd.RepartoID = r.RepartoID
			 WHERE rd.Servido = 1
			 GROUP BY ta.LoteID, rd.FormulaIDServida
				    ) df ON df.LoteID = ta.LoteID AND df.FormulaIDServida = ta.IdUltimaFormula2

		  --Obtenemos la fecha de entrada del ganado
		  INSERT INTO #LoteEntrada
		  SELECT lc.LoteID, CAST(AVG(CAST(FechaEntrada AS float)) AS smalldatetime) AS FechaEntrada
		  FROM #tblAliLectorCorrales lc
		  INNER JOIN AnimalMovimiento am(NOLOCK) ON am.LoteID = lc.LoteID AND am.Activo = 1
		  INNER JOIN Animal a(NOLOCK) ON a.AnimalID = am.AnimalID
		  INNER JOIN EntradaGanado eg ON eg.OrganizacionID = lc.IdGanadera AND eg.FolioEntrada = a.FolioEntrada
		  GROUP BY lc.LoteID

		  --Se actualiza el peso proyectado
		  UPDATE #tblAliLectorCorrales SET PesoProyectado = ISNULL(CAST(PesoInicio + DATEDIFF(day,FechaEntrada, GETDATE()) * GananciaDiaria AS int), 0)
		  FROM #tblAliLectorCorrales ta
		  INNER JOIN #LoteEntrada le ON le.LoteID = ta.LoteID
		  LEFT JOIN LoteProyeccion lp ON lp.LoteProyeccionID = ta.LoteID

		  SELECT *
		  FROM #tblAliLectorCorrales
		  ORDER BY OrganizacionID, Seccion, Orden  
		    
    END    

GO

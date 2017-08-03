IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerCorralesLectorComedero]')
		)
	DROP FUNCTION [dbo].[ObtenerCorralesLectorComedero]
GO

-- =============================================  
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 02-04-2014  
-- Description:  Obtiene la lista de corrales para el lector de comedero
-- Select * From ObtenerCorralesLectorComedero(1,0)
-- =============================================  
CREATE FUNCTION dbo.ObtenerCorralesLectorComedero (
	@OrganizacionID INT
	,@LoteID INT
	)
RETURNS @Corrales TABLE (
	IdGanadera INT
	,IdSeccion VARCHAR(8)
	,IdCorralEngorda VARCHAR(10)
	,IdLoteEngorda VARCHAR(8)
	,Secuencia INT
	,IdTipoGanado INT
	,TipoGanado VARCHAR(50)
	,IdEtapaProduccion INT
	,Cabezas INT
	,IdUltimaFormula INT
	,UltimaFormula VARCHAR(50)
	,IdUltimaFormula2 INT
	,UltimaFormula2 VARCHAR(50)
	,IdUltimoForraje INT
	,UltimoForraje VARCHAR(50)
	,FechaInicio SMALLDATETIME
	,FechaDisponible SMALLDATETIME
	,PesoInicio INT
	,PesoProyectado INT
	,DiasCorral INT
	,DiasEngorda INT
	,DiasFormula INT
	,DiasTransicion INT
	,FechaReg SMALLDATETIME
	,UsuarioReg VARCHAR(100)
	,Activo BIT
	)
AS
BEGIN
	DECLARE @TipoMovimientoCorte INT
	DECLARE @TurnoMatutino INT
	DECLARE @TurnoVespertino INT

	SET @TipoMovimientoCorte = 5
	SET @TurnoMatutino = 1
	SET @TurnoVespertino = 2

	DECLARE @PesosPromedio AS TABLE (
		CorralID INT
		,PesoPromedio INT
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		)
	DECLARE @tTipoGanado TABLE (
		TipoGanadoID INT
		,Descripcion VARCHAR(50)
		,LoteID INT
		)
	DECLARE @Formulas AS TABLE (
		LoteID INT
		,TipoServicioID INT
		,FormulaID INT
		,Formula VARCHAR(50)
		)
	DECLARE @SexoLote AS TABLE (
		LoteId INT
		,Sexo CHAR(1)
		)
	DECLARE @UltimoReparto AS TABLE (
		RepartoID BIGINT
		,LoteId INT
		,Fecha DATETIME
		)
				
	INSERT @UltimoReparto (RepartoID,LoteId,Fecha)
		SELECT RepartoID,LoteId,Fecha
		FROM (
			SELECT 
				RepartoID
				,LoteID
				,Fecha
				,ROW_NUMBER() OVER (PARTITION by LoteID ORDER BY LoteID,Fecha desc) as [Orden]
			FROM Reparto
			WHERE OrganizacionID = @OrganizacionId
		)r
		WHERE Orden = 1		


	INSERT INTO @SexoLote (
		LoteId
		,Sexo
		)
	SELECT a.LoteID
		,tg.Sexo
	FROM (
		SELECT a.LoteID
			,a.TipoGanadoID
			,ROW_NUMBER() OVER (
				PARTITION BY LoteId ORDER BY LoteId) AS [Orden]
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
	

	INSERT INTO @tTipoGanado
	SELECT tg.TipoGanadoID
		,tg.Descripcion
		,tg.LoteID
	FROM dbo.ObtenerTipoGanado(@OrganizacionID, @LoteID, @TipoMovimientoCorte, '') tg
	INNER JOIN @SexoLote sl ON sl.LoteId = tg.LoteID AND tg.Sexo = sl.Sexo

	INSERT INTO @PesosPromedio
	SELECT co.CorralID
		,SUM(am.Peso)
		,0
		,''
	FROM Corral co
	INNER JOIN Organizacion o ON co.OrganizacionID = o.OrganizacionID
	INNER JOIN Lote lo ON co.CorralID = lo.CorralID AND lo.Activo = 1
	INNER JOIN Usuario us ON lo.UsuarioCreacionID = us.UsuarioID
	INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
	INNER JOIN AnimalMovimiento am ON lo.LoteID = am.LoteID
	WHERE co.OrganizacionID = @OrganizacionID
		AND am.TipoMovimientoID = @TipoMovimientoCorte
	GROUP BY co.CorralID

	UPDATE pp
	SET TipoGanadoID = tg.TipoGanadoID
		,TipoGanado = tg.Descripcion
	FROM @PesosPromedio AS pp
	INNER JOIN TipoGanado tg ON (
			pp.PesoPromedio BETWEEN tg.PesoMinimo
				AND tg.PesoMaximo
			)
		AND (dbo.ObtenerSexoGanadoCorral(@OrganizacionID, pp.CorralID) = tg.Sexo)

---[
	INSERT INTO @Formulas
	SELECT lo.LoteID
		,rd.TipoServicioID
		,rd.FormulaIDServida
		,fo.Descripcion AS Formula
	FROM  Lote lo 
	Left join @UltimoReparto re on re.LoteID = lo.LoteID
	left JOIN RepartoDetalle rd ON re.RepartoID = rd.RepartoID
	left JOIN Formula fo ON rd.FormulaIDServida = fo.FormulaID
	--INNER JOIN Lote lo ON re.LoteID = lo.LoteID
	INNER JOIN @PesosPromedio pp ON lo.CorralID = pp.CorralID
	WHERE lo.OrganizacionID = @OrganizacionID
	And TipoCorralID not in (1,4) --Tipo corral recepción y corraleta
--]

	INSERT INTO @Corrales
	SELECT co.OrganizacionID AS [IdGanadera]
		,co.Seccion AS [IdSeccion]
		,co.Codigo AS [IdCorralEngorda]
		,lo.Lote AS [IdLoteEngorda]
		,co.Orden AS [Secuencia]
		,coalesce(tg.TipoGanadoID,0) AS [IdTipoGanado]
		,coalesce(tg.Descripcion,'') AS [TipoGanado]
		,tc.TipoCorralID AS [EtapaProduccion]
		,lo.Cabezas AS [Cabezas]
		,coalesce(fo1.FormulaID, 0) as [FormulaID1]
		,coalesce(fo1.Formula,'') as [Formula1]
		,coalesce(fo2.FormulaID,0) as [FormulaID2]
		,coalesce(fo2.Formula,'') as [Formula2]
		,0 AS [IdUltimoForraje]
		,'' AS [UltimoForraje]
		,lo.FechaInicio
		,lo.FechaDisponibilidad
		,dbo.ObtenerPesoInicio(@OrganizacionID, lo.LoteID, lo.Cabezas) AS PesoInicio
		,dbo.ObtenerPesoProyectado(@OrganizacionID, lo.LoteID, lo.Cabezas) AS PesoProyectado
		,dbo.ObtenerDiasCorral(@OrganizacionID, lo.LoteID)
		,dbo.ObtenerDiasEngorda(@OrganizacionID, lo.LoteID)
		,dbo.ObtenerDiasFormula(@OrganizacionID, lo.LoteID) 
		,dbo.ObtenerDiasTransicion(@OrganizacionID, lo.LoteID)		
		,lo.FechaCreacion AS FechaReg
		,us.UsuarioActiveDirectory AS UsuarioReg
		,lo.Activo
	FROM Corral co
	INNER JOIN Organizacion o ON co.OrganizacionID = o.OrganizacionID
	INNER JOIN Lote lo ON co.CorralID = lo.CorralID
	INNER JOIN TipoCorral tc ON co.TipoCorralID = tc.TipoCorralID
	INNER JOIN Usuario us ON lo.UsuarioCreacionID = us.UsuarioID
	LEFT JOIN @tTipoGanado tg ON lo.LoteID = tg.LoteID
	LEFT JOIN @Formulas fo1 ON lo.LoteID = fo1.LoteID
		AND fo1.TipoServicioID = @TurnoMatutino
	LEFT JOIN @Formulas fo2 ON lo.LoteID = fo2.LoteID
		AND fo2.TipoServicioID = @TurnoVespertino
	WHERE co.OrganizacionID = @OrganizacionID AND tc.GrupoCorralID NOT IN (1,4)
	ORDER BY co.OrganizacionID
		,co.Seccion
		,co.Orden

	--INSERT INTO Corrales
	--SELECT *
	--FROM @Corrales
	RETURN
END
GO

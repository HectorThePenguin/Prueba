USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteMuertesGanado_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteMuertesGanado_ObtenerPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[ReporteMuertesGanado_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    José Gilberto Quintero López 
-- Create date: 02-04-2014  
-- Description:  Obtiene el historial de reparto por organizacionID
-- ReporteMuertesGanado_ObtenerPorFecha 2, '20150924','20150924'
-- Modificacion: Se corrige version para que se muestre el nombre del operador que detecto la muerte
-- =============================================  
CREATE PROCEDURE [dbo].[ReporteMuertesGanado_ObtenerPorFecha] @OrganizacionID INT
	,@FechaIni DATE = NULL
	,@FechaFin DATE = NULL
AS
BEGIN
	DECLARE @TipoMovimientoMuerte INT
	DECLARE @GrupoCorralenfermeria INT
	DECLARE @TipoProcesoID INT
	DECLARE @TipoOrganizacionID INT
	DECLARE @TipoTratamientoID INT
	DECLARE @TipoTratamientoCorteEnfermeria INT

	SET @TipoMovimientoMuerte = 8	-- Muerte
	SET @GrupoCorralenfermeria = 3	-- Enfermería
	SET @TipoProcesoID = 1			-- Engorda Propio
	SET @TipoOrganizacionID = 1		-- Compra Directa
	SET @TipoTratamientoID = 4		-- Enfermeria
	SET @TipoTratamientoCorteEnfermeria = 5 --corte a enfermeria
			

	--[TABLAS]
	DECLARE @AnimalMovimiento AS TABLE (
		OrganizacionID INT
		,CorralID INT
		,LoteID INT
		,FolioEntrada INT
		,AnimalID BIGINT
		,Arete VARCHAR(25)
		,AreteMetalico VARCHAR(15)
		,TipoGanadoID INT
		,Peso INT
		,AnimalMovimientoID BIGINT
		,FechaMovimiento DATETIME
		,TipoMovimientoID INT
		,Tabla VARCHAR(10) --Historico
		,Temperatura DECIMAL(18,2)
		)
	DECLARE @Corral AS TABLE (
		CorralID INT
		,Codigo CHAR(10)
		,TipoCorralID INT
		,TipoCorral VARCHAR(50)
		,GrupoCorralID INT
		,GrupoCorral VARCHAR(50)
		,EnfermeriaID INT
		,Enfermeria VARCHAR(50)
		)
	DECLARE @EntradaGanado AS TABLE (
		EntradaGanadoID INT
		,OrganizacionID INT
		,FolioEntrada INT
		,FechaEntrada DATETIME
		,OrigenID INT
		,Origen VARCHAR(50)
		,TipoOrganizacionID INT
		,TipoOrganizacion varchar(50)
		,TipoProcesoID INT
		,TipoProceso VARCHAR(50)
		,EntradaGanadoCosteoID INT
		,ProveedorID INT
		)
	DECLARE @Muertes AS TABLE (
		MuerteID INT
		,LoteID INT
		,Arete VARCHAR(15)
		,AreteMetalico VARCHAR(15)
		,ProblemaID INT
		,Problema VARCHAR(50)
		,DetectorID INT
		,Detector VARCHAR(1000)
		,AnimalID	BIGINT
		)
	DECLARE @AnimalTratamientoDetalle AS TABLE (
		AnimalMovimientoID BIGINT
		,AnimalId INT
		,FechaMovimiento DATETIME
		,TipoMovimientoID INT
		,Orden INT
		)
	DECLARE @FechaTratamiento AS TABLE (
		AnimalID BIGINT
		,Fecha1 DATETIME
		,Fecha2 DATETIME
		,Fecha3 DATETIME
		)
	DECLARE @Medicamento AS TABLE (
		AnimalID BIGINT
		,Medicamento1 varchar(50)
		,Medicamento2 varchar(50)
		,Medicamento3 varchar(50)
		)
	--DECLARE @Deteccion AS TABLE (
	--	Arete VARCHAR(15)
	--	,TipoDeteccionID INT
	--	,TipoDeteccion VARCHAR(50)
	--	)

	--[INSERT]
	INSERT @AnimalMovimiento (
		OrganizacionID
		,CorralID
		,LoteID
		,FolioEntrada
		,AnimalID
		,Arete
		,AreteMetalico
		,TipoGanadoID
		,Peso
		,AnimalMovimientoID
		,FechaMovimiento
		,TipoMovimientoID
		,Tabla
		,Temperatura
		)
	SELECT OrganizacionID
		,CorralID
		,LoteID
		,FolioEntrada
		,AnimalID
		,Arete
		,AreteMetalico
		,TipoGanadoID
		,Peso
		,AnimalMovimientoID
		,FechaMovimiento
		,TipoMovimientoID
		,Tabla
		,Temperatura
	FROM
	(
		SELECT am.OrganizacionID
			,am.CorralID
			,am.LoteID
			,a.FolioEntrada
			,a.AnimalID
			,a.Arete
			,a.AreteMetalico
			,a.TipoGanadoID
			,am.Peso
			,am.AnimalMovimientoID
			,am.FechaMovimiento
			,am.TipoMovimientoID
			,'A' AS Tabla
			,am.Temperatura
		FROM AnimalMovimiento am
		INNER JOIN Animal a ON a.AnimalID = am.AnimalID
		WHERE OrganizacionID = @OrganizacionID
			AND CAST(FechaMovimiento AS DATE) BETWEEN @FechaIni
				AND @FechaFin
			AND TipoMovimientoID = @TipoMovimientoMuerte
			AND AM.Activo = 1
		UNION 
		SELECT am.OrganizacionID
			,am.CorralID
			,am.LoteID
			,a.FolioEntrada
			,a.AnimalID
			,a.Arete
			,a.AreteMetalico
			,a.TipoGanadoID
			,am.Peso
			,am.AnimalMovimientoID
			,am.FechaMovimiento
			,am.TipoMovimientoID
			,'H' AS Tabla
			,am.Temperatura
		FROM AnimalMovimientoHistorico am
		INNER JOIN AnimalHistorico a ON a.AnimalID = am.AnimalID
		WHERE OrganizacionID = @OrganizacionID
			AND CAST(FechaMovimiento AS DATE) BETWEEN @FechaIni
				AND @FechaFin
			AND TipoMovimientoID = @TipoMovimientoMuerte
			AND AM.Activo = 1
	) A

	INSERT INTO @Corral
	SELECT DISTINCT c.CorralID
		,c.Codigo
		,c.TipoCorralID
		,tc.Descripcion AS [TipoCorral]
		,tc.GrupoCorralID
		,gc.Descripcion AS [GrupoCorral]
		,en.EnfermeriaID
		,en.Descripcion AS [Enfermeria]
	FROM @AnimalMovimiento AM
	INNER JOIN Corral c
		ON (AM.CorralID = C.CorralID)
	INNER JOIN TipoCorral tc ON tc.TipoCorralID = c.TipoCorralID
	INNER JOIN GrupoCorral gc ON gc.GrupoCorralID = tc.GrupoCorralID
	LEFT JOIN EnfermeriaCorral e ON e.CorralID = c.CorralID
									AND E.Activo = 1
	LEFT JOIN Enfermeria en ON en.EnfermeriaID = e.EnfermeriaID

	INSERT INTO @EntradaGanado (
		EntradaGanadoID
		,OrganizacionID
		,FolioEntrada
		,FechaEntrada
		,OrigenID
		,Origen
		,TipoOrganizacionID
		,TipoOrganizacion
		,TipoProcesoID
		,TipoProceso
		,EntradaGanadoCosteoID
		,ProveedorID
		)
	SELECT eg.EntradaGanadoID
		,eg.OrganizacionID
		,eg.FolioEntrada
		,eg.FechaEntrada
		,eg.OrganizacionOrigenID
		,o.Descripcion
		,ot.TipoOrganizacionID
		,ot.Descripcion
		,ot.TipoProcesoID
		,tp.Descripcion
		,egc.EntradaGanadoCosteoID
		,egco.ProveedorID
	FROM EntradaGanado eg
	INNER JOIN Organizacion o ON o.OrganizacionID = eg.OrganizacionOrigenID
	INNER JOIN TipoOrganizacion ot ON ot.TipoOrganizacionID = o.TipoOrganizacionID
	INNER JOIN TipoProceso tp ON tp.TipoProcesoID = ot.TipoProcesoID
	LEFT JOIN EntradaGanadoCosteo egc ON egc.EntradaGanadoID = eg.EntradaGanadoId
	LEFT JOIN EntradaGanadoCosto egco ON egco.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID 
	And egco.CostoID  in (1)
	And egco.TieneCuenta = 0
	WHERE EXISTS (
			SELECT DISTINCT FolioEntrada
			FROM @AnimalMovimiento
			WHERE OrganizacionID = eg.OrganizacionID
				AND FolioEntrada = eg.FolioEntrada
			)

	DECLARE @MuertesPorSiniestro INT
	SET @MuertesPorSiniestro = 26

	INSERT INTO @Muertes
	SELECT m.MuerteID
		,m.LoteID
		,m.Arete
		,m.AreteMetalico
		,p.ProblemaID
		,p.Descripcion AS [Problema]
		,m.OperadorDeteccion
		,dbo.ObtenerDetectoresArete(m.Arete,m.LoteID,L.OrganizacionID)--rtrim(Nombre) + ' ' + rtrim(ApellidoPaterno) + ' ' + rtrim(ApellidoMaterno)
		,M.AnimalID
	FROM Muertes m
	INNER JOIN Lote L ON (L.LoteID = m.LoteID)
	INNER JOIN Problema p ON p.ProblemaID = m.ProblemaID
	INNER JOIN Operador o ON o.OperadorID = m.OperadorDeteccion
	INNER JOIN @AnimalMovimiento am
		ON (M.AnimalID = AM.AnimalID)
	WHERE M.ProblemaID <> @MuertesPorSiniestro
	--WHERE EXISTS (
	--		SELECT DISTINCT Arete
	--		FROM @AnimalMovimiento
	--		WHERE Arete = m.Arete
	--			--AND LoteID = m.LoteID
	--		)

	INSERT INTO @AnimalTratamientoDetalle (
		AnimalMovimientoID
		,AnimalId
		,FechaMovimiento
		,TipoMovimientoID
		,Orden
		)
	SELECT am.AnimalMovimientoID
		,am.AnimalId
		,am.FechaMovimiento
		,am.TipoMovimientoID
		,ROW_NUMBER() OVER (
			PARTITION BY AnimalId ORDER BY AnimalID
				,FechaMovimiento
			) AS [Orden]
	FROM AnimalMovimiento am
	WHERE TipoMovimientoID IN (
			5, 7, 9, 13
			)
		AND EXISTS (
			SELECT DISTINCT AnimalID
			FROM @AnimalMovimiento
			WHERE  AnimalID = am.AnimalID 
			--And CorralId = am.CorralID
			)
		
	--***********************************************************
	-- Historico
	--***********************************************************	
	
	INSERT INTO @AnimalTratamientoDetalle (
		AnimalMovimientoID
		,AnimalId
		,FechaMovimiento
		,TipoMovimientoID
		,Orden
		)
	SELECT am.AnimalMovimientoID
		,am.AnimalId
		,am.FechaMovimiento
		,am.TipoMovimientoID
		,ROW_NUMBER() OVER (
			PARTITION BY AnimalId ORDER BY AnimalID
				,FechaMovimiento
			) AS [Orden]
	FROM AnimalMovimientoHistorico am
	WHERE TipoMovimientoID IN (
			5, 7, 9, 13
			)
		AND EXISTS (
			SELECT DISTINCT AnimalID
			FROM @AnimalMovimiento
			WHERE AnimalID = am.AnimalID
			)

	INSERT INTO @FechaTratamiento (
		AnimalID
		,Fecha1
		,Fecha2
		,Fecha3
		)
	SELECT pv.AnimalId
		,convert(VARCHAR, [1], 111) AS [FechaTratamiento1]
		,convert(VARCHAR, [2], 111) AS [FechaTratamiento2]
		,convert(VARCHAR, [3], 111) AS [FechaTratamiento3]
	FROM (
		SELECT AnimalId
			,FechaMovimiento
			,Orden
		FROM @AnimalTratamientoDetalle
		) AS a --Where Orden > 3
	pivot(min(FechaMovimiento) FOR Orden IN (
				[1]
				,[2]
				,[3]
				)) pv
	
	DECLARE @Tratamientos AS TABLE
		(
			AnimalID bigint
			,AnimalMovimientoID BIGINT
			,TratamientoID int
			,ProductoID int
			,Producto varchar(50)
			,FechaMovimiento datetime	
			,Orden int
		)				
	
	INSERT INTO @Tratamientos
		SELECT *
			,DENSE_RANK() OVER (Partition by a.AnimalId ORDER BY AnimalMovimientoID) [Orden] 
		
		FROM (
			SELECT am.AnimalID
				,am.AnimalMovimientoID
				,amd.TratamientoID
				,p.ProductoID
				,p.Descripcion as [Producto]
				,am.FechaMovimiento
			FROM AlmacenMovimientoDetalle amd
			INNER JOIN AlmacenMovimiento alm ON alm.AlmacenMovimientoID = amd.AlmacenMovimientoID
			INNER JOIN @AnimalTratamientoDetalle am on am.AnimalMovimientoID = alm.AnimalMovimientoID
			INNER JOIN Producto p ON p.ProductoID = amd.ProductoID
			INNER JOIN Tratamiento T ON amd.TratamientoID = T.TratamientoID
			WHERE T.TipoTratamientoID in(@TipoTratamientoID, @TipoTratamientoCorteEnfermeria)
			GROUP BY am.AnimalID
				,am.AnimalMovimientoID
				,amd.TratamientoID
				,p.ProductoID
				,p.Descripcion
				,am.FechaMovimiento
			) a
	
	--****************************************************************
	-- Generación del Reporte
	--****************************************************************
	
	declare @Reporte as table
	(
		AnimalID bigint
		,Enfermeria varchar(50)
		,CorralID int
		,Codigo varchar(10)
		,TipoGanado varchar(50)
		,FechaLlegada datetime
		,Arete varchar(15)
		,Origen varchar(100) --case When eg.TipoOrganizacionID = @TipoOrganizacionID then p.Descripcion else  eg.Origen end AS [Origen]
		,Partida int 
		,Sexo char(1)
		,Peso int
		,DiasEngorda int   --case When eg.FechaEntrada is not null then DATEDIFF(DD, isnull(eg.FechaEntrada, '19000101'), am.FechaMovimiento) else 0 end AS [DiasEngorda]
		,Causa varchar(50) -- upper(m.Problema) AS [Causa]
		,Detector varchar(1000)
		,[FechaTratamiento1] datetime
		,[MedicamentoAplicado1] varchar(100)
		,[FechaTratamiento2] datetime
		,[MedicamentoAplicado2] varchar(100)
		,[FechaTratamiento3] datetime
		,[MedicamentoAplicado3] varchar(100)
		,[TipoDeteccion] varchar(50)
		,[Tabla] varchar(50)		
		,[AnimalMovimientoID] BIGINT
		,Temperatura	DECIMAL(18,2)
		,GradoID		INT
	)
	
	insert @Reporte
	SELECT am.AnimalID
		,CASE 
			WHEN c.GrupoCorralID = @GrupoCorralenfermeria
				THEN c.Enfermeria
			ELSE NULL
			END AS [Enfermeria]
		,am.CorralID
		,c.Codigo
		,tg.Descripcion AS [TipoGanado]
		,am.FechaMovimiento AS [FechaLlegada]
		,M.Arete
		,case When eg.TipoOrganizacionID = @TipoOrganizacionID then p.Descripcion else  eg.Origen end AS [Origen]
		,am.FolioEntrada AS [Partida]
		,tg.Sexo
		,am.Peso
		,case When eg.FechaEntrada is not null then DATEDIFF(DD, isnull(eg.FechaEntrada, '19000101'), am.FechaMovimiento) else 0 end AS [DiasEngorda]
		,upper(m.Problema) AS [Causa]
		,m.Detector
		,ft.Fecha1 AS [FechaTratamiento1]
		,md.Medicamento1 AS [MedicamentoAplicado1]
		,ft.Fecha2 AS [FechaTratamiento2]
		,md.Medicamento2 AS [MedicamentoAplicado2]
		,ft.Fecha3 AS [FechaTratamiento3]
		,md.Medicamento3 AS [MedicamentoAplicado3]
		,c.GrupoCorral as [TipoDeteccion]
		,am.Tabla as [Tabla]		
		,am.AnimalMovimientoID
		,dbo.ObtenerTempEnfermoArete(M.Arete, eg.OrganizacionID)
		,dbo.ObtenerGradoEnfermedadArete(M.Arete, eg.OrganizacionID)
	FROM @AnimalMovimiento am
	INNER JOIN @Corral c ON c.CorralID = am.CorralID
	INNER JOIN @Muertes m 
		ON (AM.AnimalID = M.AnimalID)
		--ON m.Arete = am.Arete
		--	AND m.LoteID = am.LoteID
	INNER JOIN TipoGanado tg ON tg.TipoGanadoID = am.TipoGanadoID
	LEFT JOIN @FechaTratamiento ft ON ft.AnimalID = am.AnimalID
	LEFT JOIN @Medicamento md ON md.AnimalID = am.AnimalID
	LEFT JOIN @EntradaGanado eg ON eg.OrganizacionID = am.OrganizacionID
		AND eg.FolioEntrada = am.FolioEntrada
	LEFT JOIN Proveedor p on p.ProveedorID = eg.ProveedorID
	
	select distinct * from @Reporte
	WHERE Codigo <> 'ZZZ'
	ORDER BY FechaLlegada

	select * from @Tratamientos
END

GO

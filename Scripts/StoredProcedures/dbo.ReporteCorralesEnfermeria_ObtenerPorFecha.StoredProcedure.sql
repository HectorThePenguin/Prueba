USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteCorralesEnfermeria_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteCorralesEnfermeria_ObtenerPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[ReporteCorralesEnfermeria_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jos� Gilberto Quintero L�pez 
-- Create date: 02-04-2014  
-- Description:  Obtiene el historial de reparto por organizacionID
-- ReporteCorralesEnfermeria_ObtenerPorFecha  2, 11
-- =============================================  
CREATE PROCEDURE [dbo].[ReporteCorralesEnfermeria_ObtenerPorFecha] 
	@OrganizacionID INT
	,@EnfermeriaID INT
	AS
BEGIN 
	--DECLARE @OrganizacionID INT, @EnfermeriaID INT
	--SELECT @OrganizacionID = 1, @EnfermeriaID = 1
	DECLARE @Filtro AS TABLE (
		OrganizacionId INT
		,EnfermeriaID INT
		)
	DECLARE @TipoMovimientoEntradaEnfermeriaID INT
	DECLARE @TipoMovimientoEntradaSalidaEnfermeriaID INT
	DECLARE @TipoMovimientoTraspasoID INT
	DECLARE @TipoMovimientoCorteID INT
	DECLARE @GrupoCorralenfermeriaID INT
	DECLARE @TipoProcesoID INT
	--DECLARE @TipoMovimientoMuerteID INT
	SET @TipoMovimientoEntradaEnfermeriaID = 7        --Entrada Enfermer�a	
	SET @TipoMovimientoEntradaSalidaEnfermeriaID = 9  --Entrada Salida Enfermer�a
	SET @TipoMovimientoTraspasoID = 17                --Traspaso
	SET @TipoMovimientoCorteID = 13
	--SET @TipoMovimientoMuerteID = 8
	SET @GrupoCorralenfermeriaID = 3			      --Enfermer�a
	SET @TipoProcesoID = 1						      --Engorda Propio
	--[Filtro]			
	INSERT INTO @Filtro (
		OrganizacionId
		,EnfermeriaID
		)
	VALUES (
		@OrganizacionID
		,@EnfermeriaID
		)
	--[Obtiene los corrales que pertenecen a la enfermer�a]	
	DECLARE @Corrales AS TABLE (
		OrganizacionID INT
		,CorralID INT
		,Codigo VARCHAR(10)
		,LoteID INT
		,Lote VARCHAR(20)
		,Cabezas INT
		,TipoCorralID INT
		,TipoProcesoID INT
		,GrupoCorralID INT
		,EnfermeriaID INT
		,PRIMARY KEY (
			CorralID
			,LoteID
			)
		)
	INSERT INTO @Corrales (
		OrganizacionID
		,CorralID
		,Codigo
		,LoteID
		,Lote
		,Cabezas
		,TipoCorralID
		,TipoProcesoID
		,GrupoCorralID
		,EnfermeriaID
		)
	SELECT e.OrganizacionID
		,c.CorralID
		,c.Codigo
		,l.LoteID
		,l.Lote
		,l.Cabezas
		,c.TipoCorralID
		,l.TipoProcesoID
		,gc.GrupoCorralID
		,ec.EnfermeriaID
	FROM EnfermeriaCorral (NOLOCK) ec 
	INNER JOIN Corral (NOLOCK) c ON c.CorralID = ec.CorralID
	INNER JOIN Lote (NOLOCK) l ON (l.CorralID = c.CorralID 
								AND l.Activo = 1)
	INNER JOIN TipoCorral (NOLOCK) tc ON tc.TipoCorralID = c.TipoCorralID
	INNER JOIN GrupoCorral (NOLOCK) gc ON gc.GrupoCorralID = tc.GrupoCorralID
	INNER JOIN Enfermeria (NOLOCK) e ON ( e.EnfermeriaID = ec.EnfermeriaID
										AND e.OrganizacionID = c.OrganizacionID)
	WHERE  @EnfermeriaID in (e.EnfermeriaID, 0)
		AND gc.GrupoCorralID = @GrupoCorralenfermeriaID
		AND c.OrganizacionID = @OrganizacionID
		AND c.Activo = 1
		AND EC.Activo = 1
	--SELECT * FROM @Corrales
	--[Obtiene los movimientos de Entrada a Enfermer�a]
	DECLARE @AnimalMovimiento AS TABLE (
		 OrganizacionID INT
		,AnimalMovimientoID BIGINT
		,AnimalID BIGINT
		,CorralID INT
		,LoteID INT
		,FechaMovimiento DATETIME
		,TipoMovimientoID INT
		,DiasEnfermeria INT
		,UltimoCorralID INT
	)
	INSERT @AnimalMovimiento
	SELECT am.OrganizacionID 
		,MIN(am.AnimalMovimientoID)
		,am.AnimalID
		,am.CorralID
		,am.LoteID
		,am.FechaMovimiento
		,am.TipoMovimientoID
		,DATEDIFF(dd, am.FechaMovimiento, GETDATE()) AS [DiasEnfermeria]
		,null as [UltimoCorralID]
	FROM AnimalMovimiento (NOLOCK) am
	INNER JOIN @Corrales c ON (c.LoteID = am.LoteID
							AND c.CorralID = am.CorralID)
	WHERE Activo = 1 AND
		am.TipoMovimientoID IN (
			 @TipoMovimientoEntradaEnfermeriaID
			,@TipoMovimientoTraspasoID 
			--,@TipoMovimientoMuerteID
		)
	GROUP BY am.OrganizacionID
		,am.AnimalID
		,am.CorralID
		,am.LoteID
		,am.FechaMovimiento
		,am.TipoMovimientoID
	--[Se obtiene el ultimo corral antes de entrar a enfermeria]
	DECLARE @Kardex AS TABLE (
		AnimalMovimientoID BIGINT
		,AnimalID BIGINT
		,FechaMovimiento DATETIME
		,TipoMovimientoID INT
		,CorralID INT
		,LoteID INT
		,Orden INT
		,PRIMARY KEY (AnimalMovimientoID)
		)
	INSERT @Kardex
	SELECT am.AnimalMovimientoID
		,am.AnimalID
		,am.FechaMovimiento
		,am.TipoMovimientoID
		,am.CorralID
		,am.LoteID
		,Row_number() OVER (
			PARTITION BY am.AnimalID
			,am.AnimalId ORDER BY am.FechaMovimiento
			) AS [Orden]
	FROM AnimalMovimiento (NOLOCK) am
	INNER JOIN @AnimalMovimiento tAm
		ON (AM.AnimalID = tAm.AnimalID)
	UPDATE am
	SET UltimoCorralID = a.CorralID
	FROM @AnimalMovimiento am
	LEFT JOIN @Kardex k ON k.AnimalMovimientoID = am.AnimalMovimientoID
	INNER JOIN (
		SELECT AnimalID
			,CorralID
			,Orden
		FROM @Kardex
		) a ON a.AnimalID = k.AnimalID
		AND a.Orden = k.Orden - 1
	--SELECT * FROM @AnimalMovimiento
	--[Se obtienen las entradas de ganado]
	DECLARE @EntradaGanado AS TABLE (
		EntradaGanadoID INT
		,OrganizacionID INT
		,FolioEntrada INT
		,FolioOrigen INT
		,FechaEntrada DATETIME
		,OrigenID INT
		,Origen VARCHAR(50)
		,TipoOrganizacionID INT
		,TipoOrganizacion VARCHAR(50)
		,TipoProcesoID INT
		,TipoProceso VARCHAR(50)
		,EntradaGanadoCosteoID INT
		,ProveedorID INT
		,Proveedor VARCHAR(100)
		,CodigoSAP VARCHAR(10)
		,OrganizacionOrigenID INT
		,PesoBruto INT
		,PesoTara INT
		)
	INSERT INTO @EntradaGanado (
		EntradaGanadoID
		,OrganizacionID
		,FolioEntrada
		,FolioOrigen
		,FechaEntrada
		,OrigenID
		,Origen
		,TipoOrganizacionID
		,TipoOrganizacion
		,TipoProcesoID
		,TipoProceso
		,EntradaGanadoCosteoID
		,ProveedorID
		,Proveedor
		,CodigoSAP
		,OrganizacionOrigenID
		,PesoBruto
		,PesoTara
		)
	SELECT eg.EntradaGanadoID
		,eg.OrganizacionID
		,eg.FolioEntrada
		,eg.FolioOrigen
		,eg.FechaEntrada
		,eg.OrganizacionOrigenID
		,o.Descripcion
		,ot.TipoOrganizacionID
		,ot.Descripcion
		,ot.TipoProcesoID
		,tp.Descripcion
		,egc.EntradaGanadoCosteoID
		,egco.ProveedorID
		,p.Descripcion
		,p.CodigoSAP
		,eg.OrganizacionOrigenID
		,eg.PesoBruto
		,eg.PesoTara
	FROM EntradaGanado (NOLOCK) eg
	INNER JOIN @Filtro f on f.OrganizacionId = eg.OrganizacionID
	INNER JOIN Organizacion (NOLOCK) o ON o.OrganizacionID = eg.OrganizacionOrigenID
	INNER JOIN TipoOrganizacion (NOLOCK) ot ON ot.TipoOrganizacionID = o.TipoOrganizacionID
	INNER JOIN TipoProceso (NOLOCK) tp ON tp.TipoProcesoID = ot.TipoProcesoID
	INNER JOIN EntradaGanadoCosteo (NOLOCK) egc ON egc.EntradaGanadoID = eg.EntradaGanadoId
	INNER JOIN EntradaGanadoCosto (NOLOCK) egco ON egco.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID
		AND TieneCuenta = 0 
		AND egco.CostoID = 1
	LEFT JOIN Proveedor (NOLOCK) p ON p.ProveedorID = egco.ProveedorID
	WHERE FolioEntrada IN (SELECT distinct FolioEntrada 
		                   FROM Animal A INNER JOIN @AnimalMovimiento AM ON A.AnimalID = AM.AnimalID)
	--[Obtener detecci�n animal]	
	DECLARE @DeteccionAnimal AS TABLE (
		AnimalMovimientoID BIGINT
		,DeteccionAnimalID INT
		,DiagnosticoAnalistaID INT
		,DiagnosticoAnalistaDetalleID INT
		,FechaCreacion DATETIME
		,Observaciones VARCHAR(255)
		,ProblemaID INT
		,Problema VARCHAR(255)
		,Orden INT
		)
	INSERT INTO @DeteccionAnimal
	SELECT *
	FROM (
		SELECT da.AnimalMovimientoID
			,da.DeteccionAnimalID
			,dga.DiagnosticoAnalistaID
			,dgad.DiagnosticoAnalistaDetalleID
			,dgad.FechaCreacion
			,da.Observaciones
			,p.ProblemaID
			, LEFT((SELECT  STUFF((SELECT ', '+ P.Descripcion  from Problema (NOLOCK) P 
				LEFT JOIN DiagnosticoAnalistaDetalle (NOLOCK) DA ON DA.ProblemaId = P.ProblemaId
				WHERE DA.DiagnosticoAnalistaId = dga.DiagnosticoAnalistaId
				FOR XML PATH('')), 1, 1, '')), 255) as descripcion
			,Row_number() OVER (
				PARTITION BY da.AnimalMovimientoID ORDER BY dgad.DiagnosticoAnalistaDetalleID DESC
				) AS [Orden]
		FROM AnimalMovimiento (NOLOCK) am
		LEFT JOIN DeteccionAnimal (NOLOCK) da ON da.AnimalMovimientoID = am.AnimalMovimientoID
		LEFT JOIN DiagnosticoAnalista (NOLOCK) dga ON dga.DeteccionAnimalID = da.DeteccionAnimalID
		LEFT JOIN DiagnosticoAnalistaDetalle (NOLOCK) dgad ON dgad.DiagnosticoAnalistaID = dga.DiagnosticoAnalistaID
		LEFT JOIN Problema (NOLOCK) p ON p.ProblemaId = dgad.ProblemaId
		WHERE EXISTS (
				SELECT DISTINCT AnimalMovimientoID
				FROM @AnimalMovimiento
				WHERE AnimalMovimientoID = da.AnimalMovimientoID
				)
		) da
	WHERE da.Orden = 1
	--[Reporte]	
	SELECT DISTINCT cl.Codigo AS [CorralEnfermeria]
		,CASE 
				WHEN (
						a.Arete IS NULL
						OR a.Arete IN (
							''
							,' '
							)
						)
					THEN a.AreteMetalico
				ELSE a.Arete
				END AS [Arete]
		,am.FechaMovimiento AS [FechaEnfermeria]
		, a.PesoCompra as PesoOrigen
		,tg.Descripcion AS [TipoGanado]
		,ISNULL(
			CASE 
			WHEN da.ProblemaID IS NULL
				THEN da.Observaciones
			ELSE 
				da.problema
			END,'') AS [Problema]
		,COALESCE(ca.Codigo, '') AS [CorralOrigen]
		,ISNULL(DATEDIFF(DD, eg.FechaEntrada, GETDATE()),0) AS [DiasEngorda]
		,ISNULL(am.DiasEnfermeria,0) AS DiasEnfermeria
		,ISNULL(eg.FolioEntrada,0) AS [Partida] -- EntradaGanado
		,eg.FechaEntrada AS [FechaLlegada] -- EntradaGanado
		,CASE 
			WHEN eg.ProveedorID IS NULL
				THEN eg.Origen
			ELSE eg.Proveedor
			END AS [Origen]
		,am.CorralID	
		,am.AnimalMovimientoID
		,am.AnimalID
		,eg.ProveedorID
	INTO #tAnimalesEnfermeria
	FROM @AnimalMovimiento am
	INNER JOIN Animal (NOLOCK) a on a.AnimalID = am.AnimalID
	INNER JOIN TipoGanado (NOLOCK) tg ON tg.TipoGanadoID = a.TipoGanadoID
	INNER JOIN @Corrales cl ON cl.CorralID = am.CorralID
		AND cl.LoteID = am.LoteID
	LEFT JOIN Corral (NOLOCK) ca ON ca.CorralID = am.UltimoCorralID
	LEFT JOIN @EntradaGanado eg ON eg.OrganizacionID = cl.OrganizacionID
		AND eg.FolioEntrada = a.FolioEntrada
	LEFT JOIN InterfaceSalidaAnimal (NOLOCK) isa ON eg.FolioOrigen = isa.SalidaID
		AND isa.Arete = a.Arete
	LEFT JOIN @DeteccionAnimal da ON da.AnimalMovimientoID = am.AnimalMovimientoID
	Order by cl.Codigo 
	UPDATE #tAnimalesEnfermeria
	SET CorralOrigen = ''
	WHERE CorralOrigen = 'ZZZ'
	SELECT * FROM #tAnimalesEnfermeria
	DROP TABLE #tAnimalesEnfermeria
END

GO

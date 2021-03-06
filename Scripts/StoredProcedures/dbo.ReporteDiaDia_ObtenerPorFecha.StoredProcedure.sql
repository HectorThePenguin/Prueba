USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiaDia_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteDiaDia_ObtenerPorFecha]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiaDia_ObtenerPorFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--======================================================
-- Author     : Gilberto Carranza
-- Create date: 21/02/2014
-- Description: Obtiene los datos para el reporte dia a dia
-- SpName     : ReporteDiaDia_ObtenerPorFecha 4, '20150727' , '20150802'
--======================================================
CREATE PROCEDURE [dbo].[ReporteDiaDia_ObtenerPorFecha] @OrganizacionID INT
	,@FechaInicio DATE
	,@FechaFin DATE
AS
BEGIN
	SET NOCOUNT ON

		DECLARE @FechaInicial DATE, @FechaFinal DATE

		SELECT @FechaInicial = @FechaInicio
			 , @FechaFinal = @FechaFin

		CREATE TABLE #tReporteDiaDia (
			FechaEmbarque DATETIME
			,FechaLlegada DATETIME
			,TipoOrigen VARCHAR(150)
			,Origen VARCHAR(150)
			,Proveedor VARCHAR(150)
			,Region VARCHAR(50)
			,EntradaGanado INT
			,CodigoCorral VARCHAR(10)
			,NumeroSalidaFactura INT
			,MuertosDeducibles INT
			,CabezasTotales INT
			,HorasTransito DECIMAL(18, 2)
			,HorasEsperadas DECIMAL(18, 2)
			,DiferenciaHoras DECIMAL(18, 2)
			,JaulasSalida DECIMAL(18, 2)
			,KilosLlegada DECIMAL(18, 2)
			,Chofer VARCHAR(100)
			,Placas VARCHAR(100)
			,HoraLlegada VARCHAR(8)
			,Kilometros DECIMAL(18, 2)
			,TipoOrganizacionID INT
			,MermaEsperada DECIMAL(10,4)
			)

		CREATE TABLE #tCostos (
			Importe DECIMAL(18, 2)
			,FolioEntrada INT
			,CostoID INT
			,Descripcion VARCHAR(100)
			)

		CREATE TABLE #tCostosPivote (
			FolioEntrada INT
			,[Comision] DECIMAL(18, 2)
			,[Fletes] DECIMAL(18, 2)
			,[ImpuestoPredial] DECIMAL(18, 2)
			,[GuiasDeTransito] DECIMAL(18, 2)
			,[PruebasBTTR] DECIMAL(18, 2)
			,[GastosIndirectos] DECIMAL(18, 2)
			,[AlimentoEnCentro] DECIMAL(18, 2)
			,[MedicamentoEnCentro] DECIMAL(18, 2)
			,[SeguroDeTransportes] DECIMAL(18, 2)
			,[SeguroDeGanadoEnCentro] DECIMAL(18, 2)
			,[Renta] DECIMAL(18, 2)
			,[AlimentoEnDescanso] DECIMAL(18, 2)
			,[ManejoDeGanado] DECIMAL(18, 2)
			,[MedicamentoEnCadis] DECIMAL(18, 2)
			,[CostoDePradera] DECIMAL(18, 2)
			,[MedicamentoEnPradera] DECIMAL(18, 2)
			,[Banios] DECIMAL(18, 2)
			,[Demoras] DECIMAL(18, 2)
			,[SeguroCorral] DECIMAL(18, 2)
			)

		CREATE TABLE #tCalidades (
			FolioEntrada INT
			,[1] INT
			,[1.5] INT
			,[2] INT
			,[3] INT
			,[3.5] INT
			,[3.5L] INT
			,[3.5P] INT
			,[3.5VT2] INT
			,[3.5PR] INT
			,[4] INT
			,[5] INT
			,[6] INT
			,[7] INT
			)

		CREATE TABLE #tComprador (
			FolioEntrada INT
			,Nombre VARCHAR(100)
			)

		CREATE TABLE #tFletero (
			FolioEntrada INT
			,Nombre VARCHAR(100)
			)

		CREATE TABLE #tCabezasSexo (
			FolioEntrada INT
			,CabezasMachos INT
			,CabezasHembras INT
			)

		CREATE TABLE #tPesosOrigen (
			FolioEntrda INT
			,PesoOrigen DECIMAL(18, 2)
			)

		CREATE TABLE #tDiasCentro (
			FolioEntrada INT
			,Dias INT
			)

		CREATE TABLE #tCostosAlimentoCentro (
			FolioEntrada INT
			,CostoAlimento DECIMAL(18, 2)
			)

		CREATE TABLE #tPrecioKilo (
			FolioEntrada INT
			,TiposGanado INT
			,SexoAnimal INT
			,PrecioKiloMacho DECIMAL(18, 2)
			,PrecioKiloHembra DECIMAL(18, 2)
			)

		CREATE TABLE #tPreciosGenero (
			FolioEntrada INT
			,PrecioMachos DECIMAL(18, 2)
			,PrecioHembras DECIMAL(18, 2)
			)

		CREATE TABLE #tCostosPorKilo (
			FolioEntrada INT
			,ComisionPorKilo NUMERIC(18, 2)
			,FletePorKilo NUMERIC(18, 2)
			,GuiaPorKilo NUMERIC(18, 2)
			,OtrosPorKilo NUMERIC(18, 2)
			,SeguroTransportePorKilo NUMERIC(18, 2)
			,SeguroCorralPorKilo NUMERIC(18, 2)
			)

		CREATE TABLE #tKilosTotales (
			FolioEntrada INT
			,KilosMachos DECIMAL(18, 2)
			,KilosHembras DECIMAL(18, 2)
			)

		CREATE TABLE #tMortalidad (
			FolioEntrada INT
			,MorbilidadAltoRiesgo INT
			)

		CREATE TABLE #tDistancias (
			EntradaGanado INT
			,Kilometros NUMERIC(18, 2)
			,HorasTransito NUMERIC(18, 2)
			,FechaEmbarque DATETIME
			,TipoOrigen VARCHAR(50)
			,Proveedor VARCHAR(50)
			,TipoOrganizacionID INT
			,JaulasSalida NUMERIC(18, 2)
			,OrganizacionOrigenID INT
			)

		INSERT INTO #tDistancias
		SELECT EG.FolioEntrada AS FolioEntrada
			,(
				SELECT SUM(ed1.Kilometros)
				FROM EmbarqueDetalle ed1
				WHERE ed1.EmbarqueID = EG.EmbarqueID
					AND ed1.FechaSalida >= (
						SELECT TOP 1 ED.FechaSalida
						FROM Embarque e
						INNER JOIN EmbarqueDetalle ED ON e.EmbarqueID = ED.EmbarqueID
							AND ED.OrganizacionOrigenID = EG.OrganizacionOrigenID
							AND ED.EmbarqueID = EG.EmbarqueID
							AND ED.Activo = 1
						)
				GROUP BY ed1.EmbarqueID
				) AS Kilometros
			,ROUND(CAST(DATEDIFF(MINUTE, (
							SELECT TOP 1 ED.FechaSalida
							FROM Embarque e
							INNER JOIN EmbarqueDetalle ED ON e.EmbarqueID = ED.EmbarqueID
								AND ED.OrganizacionOrigenID = EG.OrganizacionOrigenID
								AND ED.EmbarqueID = EG.EmbarqueID
								AND ED.Activo = 1
							), EG.FechaEntrada) AS NUMERIC(18, 2)) / 60, 2) AS HorasTransito
			,(
					SELECT TOP 1 ED.FechaSalida
					FROM Embarque e
					INNER JOIN EmbarqueDetalle ED ON e.EmbarqueID = ED.EmbarqueID
						AND ED.OrganizacionOrigenID = EG.OrganizacionOrigenID
						AND ED.EmbarqueID = EG.EmbarqueID
						AND ED.Activo = 1
				) AS FechaEmbarque
			,TOrg.Descripcion AS TipoOrigen
			,O.Descripcion AS Proveedor
			,TOrg.TipoOrganizacionID AS TipoOrganizacionID
			,(
				SELECT CAST(1 AS NUMERIC(18, 2)) / COUNT(ed2.EmbarqueDetalleID)
				FROM EmbarqueDetalle ed2
				WHERE ed2.EmbarqueID = EG.EmbarqueID
				GROUP BY ed2.EmbarqueID
				) AS JaulasSalida
			,EG.OrganizacionOrigenID
		FROM EntradaGanado EG
		INNER JOIN EntradaGanadoCosteo EGC ON (
				EG.EntradaGanadoID = EGC.EntradaGanadoID
				AND EGC.Activo = 1
				AND EG.OrganizacionID = @OrganizacionID
				)
		INNER JOIN Organizacion O ON (EG.OrganizacionOrigenID = O.OrganizacionID)
		INNER JOIN TipoOrganizacion TOrg ON (O.TipoOrganizacionID = TOrg.TipoOrganizacionID)
		WHERE CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
			AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal

		INSERT INTO #tKilosTotales
		SELECT FolioEntrada
			,[2] AS 'Macho'
			,[1] AS 'Hembra'
		FROM (
			SELECT EG.FolioEntrada
				,CASE 
					WHEN tg.Sexo = 'H'
						THEN 1
					ELSE 2
					END Sexo
				,EDet.PesoOrigen
			FROM EntradaGanado EG
			INNER JOIN EntradaGanadoCosteo EGCosteo ON (
					EG.EntradaGanadoID = EGCosteo.EntradaGanadoID
					AND EGCosteo.Activo = 1
					)
			INNER JOIN EntradaDetalle EDet ON (EGCosteo.EntradaGanadoCosteoID = EDet.EntradaGanadoCosteoID)
			INNER JOIN TipoGanado tg ON EDet.TipoGanadoID = tg.TipoGanadoID
			INNER JOIN Embarque E ON (EG.EmbarqueID = E.EmbarqueID)
			INNER JOIN EmbarqueDetalle ED ON (
					E.EmbarqueID = ED.EmbarqueID
					AND ED.OrganizacionDestinoID = EG.OrganizacionID
					AND (
						CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
						AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
						)
					AND EG.OrganizacionID = @OrganizacionID
					)
			) AS SourceTable
		PIVOT(SUM(PesoOrigen) FOR Sexo IN (
					[1]
					,[2]
					)) AS PivotTable

		INSERT INTO #tPrecioKilo
		SELECT FolioEntrada
			,TiposGanado
			,SexoAnimal
			,[2] AS 'Macho'
			,[1] AS 'Hembra'
		FROM (
			SELECT EG.FolioEntrada
				,CASE 
					WHEN tg.Sexo = 'H'
						THEN 1
					ELSE 2
					END SexoAnimal
				,COUNT(*) AS TiposGanado
				,CASE 
					WHEN tg.Sexo = 'H'
						THEN 1
					ELSE 2
					END Sexo
				,SUM(ED.PrecioKilo) PrecioKilo
			FROM EntradaDetalle ED
			INNER JOIN TipoGanado tg ON ED.TipoGanadoID = tg.TipoGanadoID
			INNER JOIN EntradaGanadoCosteo EGCosteo ON (
					ED.EntradaGanadoCosteoID = EGCosteo.EntradaGanadoCosteoID
					AND EGCosteo.Activo = 1
					)
			INNER JOIN EntradaGanado EG ON (
					EGCosteo.EntradaGanadoID = EG.EntradaGanadoID
					AND EG.OrganizacionID = @OrganizacionID
					)
			INNER JOIN Embarque E ON (EG.EmbarqueID = E.EmbarqueID)
			INNER JOIN EmbarqueDetalle EMD ON (
					E.EmbarqueID = EMD.EmbarqueID
					AND EMD.OrganizacionDestinoID = EG.OrganizacionID
					AND (
						CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
						AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
						)
					AND EG.OrganizacionID = @OrganizacionID
					)
			GROUP BY EG.FolioEntrada
				,tg.Sexo
			) AS SourceTable
		PIVOT(SUM(PrecioKilo) FOR Sexo IN (
					[1]
					,[2]
					)) AS PivotTable

		INSERT INTO #tCostosAlimentoCentro
		SELECT ISNULL(EG.FolioEntrada, 0)
			,ROUND(SUM(ISNULL(EGCosto.Importe, 0)), 2) AS CostoAlimento
		FROM Costo C
		LEFT JOIN EntradaGanadoCosto EGCosto ON (
				C.CostoID = EGCosto.CostoID
				AND C.CostoID = 14
				AND EGCosto.Activo = 1
				)
		LEFT JOIN EntradaGanadoCosteo EGC ON (
				EGCosto.EntradaGanadoCosteoID = EGC.EntradaGanadoCosteoID
				AND EGC.Activo = 1
				)
		LEFT JOIN EntradaGanado EG ON (EGC.EntradaGanadoID = EG.EntradaGanadoID)
		LEFT JOIN Embarque E ON (EG.EmbarqueID = E.EmbarqueID)
		LEFT JOIN EmbarqueDetalle ED ON (
				E.EmbarqueID = ED.EmbarqueID
				AND (
					CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
					AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
					)
				AND ED.OrganizacionDestinoID = @OrganizacionID
				)
		LEFT JOIN Organizacion Org ON (ED.OrganizacionOrigenID = Org.OrganizacionID)
		LEFT JOIN TipoOrganizacion TOrg ON (
				Org.TipoOrganizacionID = TOrg.TipoOrganizacionID
				AND TOrg.TipoOrganizacionID = 4
				)
		GROUP BY EG.FolioEntrada

		INSERT INTO #tDiasCentro
		SELECT eg.FolioEntrada
			,CAST(SUM(DATEDIFF(dd, isaa.FechaCompra, isa.FechaSalida)) AS NUMERIC(18, 2)) / Count(isaa.Arete) AS Dias
		FROM InterfaceSalida isa
		INNER JOIN InterfaceSalidaAnimal isaa ON isa.SalidaID = isaa.SalidaID
			AND isa.OrganizacionID = isaa.OrganizacionID
		INNER JOIN EntradaGanado eg ON (
				eg.OrganizacionOrigenID = isaa.OrganizacionID
				AND eg.FolioOrigen = isaa.SalidaID
				)
		INNER JOIN Embarque E ON eg.EmbarqueID = E.EmbarqueID
		INNER JOIN EmbarqueDetalle ED ON (
				E.EmbarqueID = ED.EmbarqueID
				AND (
					CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
					AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
					)
				AND ED.OrganizacionDestinoID = @OrganizacionID
				)
		INNER JOIN Organizacion Org ON (ED.OrganizacionOrigenID = Org.OrganizacionID)
		INNER JOIN TipoOrganizacion TOrg ON (
				Org.TipoOrganizacionID = TOrg.TipoOrganizacionID
				AND TOrg.TipoOrganizacionID = 4
				)
		GROUP BY isa.SalidaID
			,eg.FolioEntrada

		INSERT INTO #tPesosOrigen
		SELECT EG.FolioEntrada
			,SUM(EDet.PesoOrigen) AS PesoOrigen
		FROM Embarque E
		INNER JOIN EmbarqueDetalle ED ON (
				E.EmbarqueID = ED.EmbarqueID
				AND ED.OrganizacionDestinoID = @OrganizacionID
				)
		INNER JOIN EntradaGanado EG ON (
				ED.EmbarqueID = EG.EmbarqueID
				AND E.EmbarqueID = EG.EmbarqueID
				AND (
					CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
					AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
					)
				AND EG.OrganizacionID = @OrganizacionID
				)
		INNER JOIN EntradaGanadoCosteo EGC ON (
				EG.EntradaGanadoID = EGC.EntradaGanadoID
				AND EGC.Activo = 1
				)
		INNER JOIN EntradaDetalle EDet ON (EGC.EntradaGanadoCosteoID = EDet.EntradaGanadoCosteoID)
		GROUP BY EG.FolioEntrada

		INSERT INTO #tCabezasSexo
		SELECT FolioEntrada
			,ISNULL([2], 0) AS 'Macho'
			,ISNULL([1], 0) AS 'Hembra'
		FROM (
			SELECT EG.FolioEntrada
				,CASE 
					WHEN tg.Sexo = 'H'
						THEN 1
					ELSE 2
					END Sexo
				,ed.Cabezas Cabezas
			FROM EntradaDetalle ED
			INNER JOIN TipoGanado tg ON ED.TipoGanadoID = tg.TipoGanadoID
			INNER JOIN EntradaGanadoCosteo EGCosteo ON (
					ED.EntradaGanadoCosteoID = EGCosteo.EntradaGanadoCosteoID
					AND EGCosteo.Activo = 1
					)
			INNER JOIN EntradaGanado EG ON (
					EGCosteo.EntradaGanadoID = EG.EntradaGanadoID
					AND EG.OrganizacionID = @OrganizacionID
					AND (
						CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
						AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
						)
					AND EG.OrganizacionID = @OrganizacionID
					)
			INNER JOIN Embarque E ON (EG.EmbarqueID = E.EmbarqueID)
			INNER JOIN EmbarqueDetalle EMD ON (
					E.EmbarqueID = EMD.EmbarqueID
					AND EMD.OrganizacionDestinoID = EG.OrganizacionID
					)
			) AS SourceTable
		PIVOT(SUM(Cabezas) FOR Sexo IN (
					[1]
					,[2]
					)) AS PivotTable

		INSERT INTO #tCostos
		SELECT ROUND(ISNULL(EGCosto.Importe, 0), 2)
			,EG.FolioEntrada
			,C.CostoID
			,C.Descripcion
		FROM Costo C
		LEFT JOIN EntradaGanadoCosto EGCosto ON (
				C.CostoID = EGCosto.CostoID
				AND EGCosto.Activo = 1
				)
		LEFT JOIN EntradaGanadoCosteo EGC ON (
				EGCosto.EntradaGanadoCosteoID = EGC.EntradaGanadoCosteoID
				AND EGC.Activo = 1
				)
		LEFT JOIN EntradaGanado EG ON (
				EGC.EntradaGanadoID = EG.EntradaGanadoID
				AND EG.OrganizacionID = @OrganizacionID
				AND (
					CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
					AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
					)
				AND EG.OrganizacionID = @OrganizacionID
				)
		LEFT JOIN EmbarqueDetalle ED ON (
				EG.EmbarqueID = ED.EmbarqueID
				AND ED.OrganizacionDestinoID = EG.OrganizacionID
				)
		WHERE C.CostoID IN (
				3
				,4
				,5
				,6
				,7
				,8
				,14
				,15
				,19
				,20
				,21
				,24
				,25
				,27
				,29
				,30
				,31
				,32
				,33
				)

		INSERT INTO #tCostosPivote
		SELECT FolioEntrada
			,ISNULL([3], 0) AS 'Comision'
			,ISNULL([4], 0) AS 'Fletes'
			,ISNULL([5], 0) AS 'Impuesto Predial'
			,ISNULL([6], 0) AS 'Guias De Transito'
			,ISNULL([7], 0) AS 'Pruebas TB Y BR'
			,ISNULL([8], 0) AS 'Gastos Indirectos'
			,ISNULL([14], 0) AS 'Alimento En Centro'
			,ISNULL([15], 0) AS 'Medicamento En Centro'
			,ISNULL([20], 0) AS 'Seguro De Transportes'
			,ISNULL([21], 0) AS 'Seguro De Ganado En Centro'
			,ISNULL([24], 0) AS 'Renta'
			,ISNULL([25], 0) AS 'Alimento En Descanso'
			,ISNULL([27], 0) AS 'Manejo De Ganado'
			,ISNULL([29], 0) AS 'Medicamento En Cadis'
			,ISNULL([30], 0) AS 'Costo De Pradera'
			,ISNULL([31], 0) AS 'Medicamento En Pradera'
			,ISNULL([32], 0) AS 'Banios'
			,ISNULL([33], 0) AS 'Demoras'
			,ISNULL([19], 0) AS 'Seguro Corral'
		FROM (
			SELECT c.Importe
				,c.FolioEntrada
				,c.CostoID
			FROM #tCostos c
			) AS SourceTable
		PIVOT(SUM(Importe) FOR CostoID IN (
					[1]
					,[2]
					,[3]
					,[4]
					,[5]
					,[6]
					,[7]
					,[8]
					,[9]
					,[10]
					,[11]
					,[12]
					,[13]
					,[14]
					,[15]
					,[16]
					,[17]
					,[18]
					,[19]
					,[20]
					,[21]
					,[22]
					,[23]
					,[24]
					,[25]
					,[26]
					,[27]
					,[28]
					,[29]
					,[30]
					,[31]
					,[32]
					,[33]
					)) AS PivotTable
		WHERE FolioEntrada > 0

		INSERT INTO #tCalidades
		SELECT FolioEntrada
			,ISNULL([1], 0) + ISNULL([14], 0) AS [1]
			,ISNULL([2], 0) + ISNULL([15], 0) AS [1.5]
			,ISNULL([3], 0) + ISNULL([16], 0) AS [2]
			,ISNULL([4], 0) + ISNULL([17], 0) AS [3]
			,ISNULL([5], 0) + ISNULL([18], 0) AS [3.5]
			,ISNULL([6], 0) + ISNULL([19], 0) AS [3.5L]
			,ISNULL([7], 0) + ISNULL([20], 0) AS [3.5P]
			,ISNULL([8], 0) + ISNULL([21], 0) AS [3.5VT2]
			,ISNULL([9], 0) + ISNULL([22], 0) AS [3.5PR]
			,ISNULL([10], 0) + ISNULL([23], 0) AS [4]
			,ISNULL([11], 0) + ISNULL([24], 0) AS [5]
			,ISNULL([12], 0) + ISNULL([25], 0) AS [6]
			,ISNULL([13], 0) + ISNULL([26], 0) AS [7]
		FROM (
			SELECT EGC.Valor
				,EG.FolioEntrada
				,EGC.CalidadGanadoID
			FROM CalidadGanado CG
			INNER JOIN EntradaGanadoCalidad EGC ON (
					CG.CalidadGanadoID = EGC.CalidadGanadoID
					AND EGC.Activo = 1
					)
			INNER JOIN EntradaGanado EG ON (
					EGC.EntradaGanadoID = EG.EntradaGanadoID
					AND EG.OrganizacionID = @OrganizacionID
					AND (
						CAST(EG.FechaEntrada AS DATE) >= @FechaInicial
						AND CAST(EG.FechaEntrada AS DATE) <= @FechaFinal
						)
					AND EG.OrganizacionID = @OrganizacionID
					)
			INNER JOIN EntradaGanadoCosteo EGCosteo ON (EG.EntradaGanadoID = EGCosteo.EntradaGanadoID)
			INNER JOIN Embarque E ON (EG.EmbarqueID = E.EmbarqueID)
			INNER JOIN EmbarqueDetalle ED ON (
					E.EmbarqueID = ED.EmbarqueID
					AND ED.OrganizacionDestinoID = EG.OrganizacionID
					)
			) AS SourceTable
		PIVOT(SUM(Valor) FOR CalidadGanadoID IN (
					[1]
					,[2]
					,[3]
					,[4]
					,[5]
					,[6]
					,[7]
					,[8]
					,[9]
					,[10]
					,[11]
					,[12]
					,[13]
					,[14]
					,[15]
					,[16]
					,[17]
					,[18]
					,[19]
					,[20]
					,[21]
					,[22]
					,[23]
					,[24]
					,[25]
					,[26]
					)) AS PivotTable

		INSERT INTO #tReporteDiaDia (
			FechaEmbarque
			,FechaLlegada
			,TipoOrigen
			,Origen
			,Proveedor
			,EntradaGanado
			,CodigoCorral
			,NumeroSalidaFactura
			,MuertosDeducibles
			,CabezasTotales
			,HorasTransito
			,HorasEsperadas
			,DiferenciaHoras
			,JaulasSalida
			,KilosLlegada
			,Chofer
			,Placas
			,HoraLlegada
			,Kilometros
			,TipoOrganizacionID
			,MermaEsperada
			)
		SELECT dis.FechaEmbarque AS FechaEmbarque
			,EG.FechaEntrada AS FechaLlegada
			,dis.TipoOrigen
			,o.Descripcion AS Origen
			,dis.Proveedor AS Proveedor
			,EG.FolioEntrada AS EntradaGanado
			,C.Codigo AS CodigoCorral
			,CASE 
				WHEN dis.TipoOrganizacionID = 3
					THEN EG.Factura
				ELSE EG.FolioOrigen
				END AS NumeroSalidaFactura
			,ISNULL((
					SELECT SUM(EC.Cabezas)
					FROM EntradaCondicion ec
					WHERE EC.EntradaGanadoID = EG.EntradaGanadoID
						AND EC.CondicionID = 3
					), 0) AS MuertosDeducibles
			,EG.CabezasRecibidas AS CabezasTotales
			,dis.HorasTransito AS HorasTransito
			,CF.Horas AS HorasEsperadas
			,dis.HorasTransito - CF.Horas AS DiferenciaHoras
			,dis.JaulasSalida AS JaulasSalida
			,EG.PesoBruto - EG.PesoTara AS KilosLlegada
			,cho.Nombre + ' ' + cho.ApellidoPaterno + ' ' + ISNULL(cho.ApellidoMaterno, '') AS Chofer
			,cam.PlacaCamion AS Placas
			,CONVERT(CHAR(8), EG.FechaEntrada, 108) AS HoraLlegada
			,dis.Kilometros AS Kilometros
			,o.TipoOrganizacionID
			,m.Merma
		FROM EntradaGanado EG
		INNER JOIN EntradaGanadoCosteo EGC ON (EG.EntradaGanadoID = EGC.EntradaGanadoID
												AND EGC.Activo = 1)
		INNER JOIN Organizacion o on eg.OrganizacionOrigenID = o.OrganizacionID
		INNER JOIN Lote L ON (L.LoteID = EG.LoteID)
		INNER JOIN Corral C ON (C.CorralID = L.CorralID)
		LEFT JOIN ConfiguracionEmbarque CF ON (
				EG.OrganizacionID = CF.OrganizacionDestinoID
				AND EG.OrganizacionOrigenID = CF.OrganizacionOrigenID
				)
		INNER JOIN Chofer cho ON (EG.ChoferID = cho.ChoferID)
		LEFT JOIN Camion cam ON EG.CamionID = cam.CamionID
		INNER JOIN Jaula Jau ON (EG.JaulaID = Jau.JaulaID)
		INNER JOIN #tDistancias dis ON EG.FolioEntrada = dis.EntradaGanado
		LEFT JOIN MermaEsperada m ON (EG.OrganizacionID = m.OrganizacionDestinoID AND dis.OrganizacionOrigenID = m.OrganizacionOrigenID)
		WHERE EG.OrganizacionID = @OrganizacionID
			AND CAST(EG.FechaEntrada AS DATE) BETWEEN @FechaInicial AND @FechaFinal

		INSERT INTO #tComprador
		SELECT EG.FolioEntrada
			,ISNULL(PComprador.Descripcion, EGComprador.CuentaProvision) AS Comprador
		FROM Costo C
		INNER JOIN EntradaGanadoCosto EGComprador ON (
				C.CostoID = EGComprador.CostoID
				AND C.CostoID = 3
				AND EGComprador.Activo = 1
				)
		INNER JOIN EntradaGanadoCosteo EGC ON (
				EGComprador.EntradaGanadoCosteoID = EGC.EntradaGanadoCosteoID
				AND EGC.Activo = 1
				)
		INNER JOIN EntradaGanado EG ON (
				EGC.EntradaGanadoID = EG.EntradaGanadoID
				AND EG.OrganizacionID = @OrganizacionID
				)
		INNER JOIN #tReporteDiaDia R ON (EG.FolioEntrada = R.EntradaGanado)
		INNER JOIN Proveedor PComprador ON (
				EGComprador.ProveedorID = PComprador.ProveedorID
				AND PComprador.ProveedorID <> 4685
				)

		INSERT INTO #tFletero
		SELECT EG.FolioEntrada
			,FIRST_VALUE(PFletero.Descripcion) OVER (PARTITION BY EG.FolioEntrada 
                                   ORDER BY EG.FolioEntrada ASC) AS Fletero
		FROM Costo C
		INNER JOIN EntradaGanadoCosto EGFletero ON (
				C.CostoID = EGFletero.CostoID
				AND C.CostoID = 4
				AND EGFletero.Activo = 1
				)
		INNER JOIN EntradaGanadoCosteo EGC ON (
				EGFletero.EntradaGanadoCosteoID = EGC.EntradaGanadoCosteoID
				AND EGC.Activo = 1
				)
		INNER JOIN EntradaGanado EG ON (
				EGC.EntradaGanadoID = EG.EntradaGanadoID
				AND EG.OrganizacionID = @OrganizacionID
				)
		INNER JOIN #tReporteDiaDia R ON (EG.FolioEntrada = R.EntradaGanado)
		INNER JOIN Proveedor PFletero ON (EGFletero.ProveedorID = PFletero.ProveedorID)

		INSERT INTO #tPreciosGenero
		SELECT CS.FolioEntrada
			,CASE 
				WHEN CS.CabezasMachos = 0
					THEN 0
				ELSE ROUND(ISNULL(CAST(PKMachos.PrecioKiloMacho / PKMachos.TiposGanado AS DECIMAL(18, 2)), 0), 2)
				END AS PrecioMachos
			,CASE 
				WHEN CS.CabezasHembras = 0
					THEN 0
				ELSE ROUND(ISNULL(CAST(PKHembras.PrecioKiloHembra / PKHembras.TiposGanado AS DECIMAL(18, 2)), 0), 2)
				END AS PrecioHembras
		FROM #tCabezasSexo CS
		LEFT JOIN #tPrecioKilo PKMachos ON (
				PKMachos.FolioEntrada = CS.FolioEntrada
				AND PKMachos.SexoAnimal = 2
				)
		LEFT JOIN #tPrecioKilo PKHembras ON (
				PKHembras.FolioEntrada = CS.FolioEntrada
				AND PKHembras.SexoAnimal = 1
				)

		INSERT INTO #tCostosPorKilo
		SELECT CP.FolioEntrada
			,ROUND(ISNULL(CP.Comision, 0) / R.KilosLlegada, 2) AS ComisionPorKilo
			,ROUND(ISNULL(CP.Fletes, 0) / R.KilosLlegada, 2) AS FletePorKilo
			,ROUND(ISNULL(CP.GuiasDeTransito, 0) / R.KilosLlegada, 2) AS GuiaPorKilo
			,ROUND((ISNULL(CP.Banios, 0) + ISNULL(CP.ManejoDeGanado, 0) + ISNULL(CP.Renta, 0) + ISNULL(CP.MedicamentoEnCentro, 0) + ISNULL(CP.MedicamentoEnCadis, 0) + ISNULL(CP.MedicamentoEnPradera, 0)) / R.KilosLlegada, 2) AS OtrosPorKilo
			,ROUND(ISNULL(CP.SeguroDeTransportes, 0) / R.KilosLlegada, 2) AS SeguroTransportePorKilo
			,ROUND(ISNULL(CP.SeguroCorral, 0) / R.KilosLlegada, 2) AS CorralPorKilo
		FROM #tReporteDiaDia R
		INNER JOIN #tCostosPivote CP ON (R.EntradaGanado = CP.FolioEntrada)

		INSERT INTO #tMortalidad
		SELECT ega.FolioEntrada
			,SUM(CAST(ecd.Respuesta AS INT))
		FROM EvaluacionCorral ec
		INNER JOIN EvaluacionCorralDetalle ecd ON ec.EvaluacionID = ecd.EvaluacionCorralID
		INNER JOIN Lote lo ON ec.LoteID = lo.LoteID
		INNER JOIN EntradaGanado ega ON lo.LoteID = ega.LoteID
		INNER JOIN #tReporteDiaDia R ON ega.FolioEntrada = R.EntradaGanado
		WHERE PreguntaID IN (
				7
				,8
				,9
				) -- Preguntas Numero de Enfermos Grado 2, Numero de Enfermos Grado 3, Numero de Enfermos Grado 4
		GROUP BY ega.FolioEntrada

		SELECT DISTINCT FORMAT(FechaEmbarque,'dd/MM/yyyy hh:mm:ss tt') AS FechaEmbarque
			,FORMAT(FechaLlegada,'dd/MM/yyyy hh:mm:ss tt') AS FechaLlegada
			,TipoOrigen
			,Origen
			,CASE WHEN R.TipoOrganizacionID = 3
				THEN ISNULL((select top 1 pr.Descripcion from EntradaGanado eg1
				inner join EntradaGanadoCosteo egc1 on eg1.EntradaGanadoID = egc1.EntradaGanadoID
				inner join EntradaGanadoCosto egco1 on egc1.EntradaGanadoCosteoID = egco1.EntradaGanadoCosteoID
				inner join Proveedor pr on egco1.ProveedorID = pr.ProveedorID
				where eg1.FolioEntrada = R.EntradaGanado
				and eg1.OrganizacionID = @OrganizacionID
				and egco1.CostoID = 1),
				(select top 1 cs.Descripcion from EntradaGanado eg1
				inner join EntradaGanadoCosteo egc1 on eg1.EntradaGanadoID = egc1.EntradaGanadoID
				inner join EntradaGanadoCosto egco1 on egc1.EntradaGanadoCosteoID = egco1.EntradaGanadoCosteoID
				inner join CuentaSAP cs on egco1.CuentaProvision = cs.CuentaSAP
				where eg1.FolioEntrada = R.EntradaGanado
				and eg1.OrganizacionID = @OrganizacionID
				and egco1.CostoID = 1)
				) 	
			 ELSE	
				Proveedor
			 END as Proveedor
			,EntradaGanado
			,CodigoCorral
			,NumeroSalidaFactura
			,MuertosDeducibles
			,CS.CabezasHembras + CS.CabezasMachos AS CabezasTotales--CabezasTotales
			,HorasTransito
			,ISNULL(HorasEsperadas,0) AS HorasEsperadas
			,ISNULL(DiferenciaHoras,0) AS DiferenciaHoras
			,JaulasSalida
			,CAST(KilosLlegada AS INT) AS KilosLlegada
			,Chofer
			,Placas
			,HoraLlegada
			,Kilometros
			,ISNULL(C.Nombre,'') AS Comprador
			,ISNULL(F.Nombre,'') AS LineaFletera
			,ISNULL(ROUND([Comision], 2),0) AS [Comision]
			,ISNULL(ROUND([Fletes], 2),0) AS [Fletes]
			,ISNULL(ROUND([ImpuestoPredial], 2),0) [ImpuestoPredial]
			,ISNULL(ROUND([GuiasDeTransito], 2),0) [GuiasDeTransito]
			,ISNULL(ROUND([PruebasBTTR], 2),0) [PruebasBTTR]
			,ISNULL(ROUND([GastosIndirectos], 2),0) [GastosIndirectos]
			,ISNULL(ROUND([AlimentoEnCentro], 2),0) [AlimentoEnCentro]
			,ISNULL(ROUND([MedicamentoEnCentro], 2),0) [MedicamentoEnCentro]
			,ISNULL(ROUND([SeguroDeTransportes], 2),0) [SeguroDeTransportes]
			,ISNULL(ROUND([SeguroDeGanadoEnCentro], 2),0) [SeguroDeGanadoEnCentro]
			,ISNULL(ROUND([Renta], 2),0) [Renta]
			,ISNULL(ROUND([AlimentoEnDescanso], 2),0) [AlimentoEnDescanso]
			,ISNULL(ROUND([ManejoDeGanado], 2),0) [ManejoDeGanado]
			,ISNULL(ROUND([MedicamentoEnCadis], 2),0) [MedicamentoEnCadis]
			,ISNULL(ROUND([CostoDePradera], 2),0) [CostoDePradera]
			,ISNULL(ROUND([MedicamentoEnPradera], 2),0) [MedicamentoEnPradera]
			,ISNULL(ROUND([Banios], 2),0) [Banios]
			,ISNULL(ROUND([Demoras], 2),0) [Demoras]
			,[1] AS Calidad1
			,[1.5] AS Calidad15
			,[2] AS Calidad2
			,[3] AS Calidad3
			,[3.5] AS Calidad35
			,[3.5L] AS Calidad35L
			,[3.5P] AS Calidad35P
			,[3.5VT2] AS Calidad35VT2
			,[3.5PR] AS Calidad35PR
			,[4] AS Calidad4
			,[5] AS Calidad5
			,[6] AS Calidad6
			,[7] AS Calidad7
			,CS.CabezasHembras
			,CS.CabezasMachos
			,cast(((cast(PO.PesoOrigen AS DECIMAL(18, 2)) - cast(R.KilosLlegada AS DECIMAL(18, 2))) / cast(PO.PesoOrigen AS DECIMAL(18, 2))) * 100 AS DECIMAL(18, 2)) AS MermaReal
			,ISNULL(R.MermaEsperada,0.0) AS MermaEsperada
			,ISNULL(dc.Dias, 0) AS DiasCentro
			,([3.5VT2] + [4] + [5] + [6] + [7] + [3.5L] + [3.5P]) AS TotalRechazo
			,CAST((CAST([3.5VT2] + [4] + [5] + [6] + [7] + [3.5L] + [3.5P] AS NUMERIC(18, 2)) / R.CabezasTotales) * 100 AS NUMERIC(18, 4)) AS PorcentajeRechazo
			,ISNULL(M.MorbilidadAltoRiesgo, 0) AS MorbilidadAltoRiesgo
			,ISNULL(CAST((CAST(M.MorbilidadAltoRiesgo AS NUMERIC(18, 2)) / R.CabezasTotales) * 100 AS NUMERIC(18, 4)), 0) AS PorcentajeMorbilidadAltoRiesgo
			,ISNULL(ROUND(ISNULL(CAC.CostoAlimento, 0), 2),0) AS CostoKGAlimento
			,ISNULL(ROUND(ISNULL(CAST(CAC.CostoAlimento AS NUMERIC(18, 2)) / R.CabezasTotales, 0), 2),0) AS CostoAlimentoCabeza
			,ISNULL(ROUND(CP.Comision / R.KilosLlegada, 2),0) AS ComisionPorKilo
			,ISNULL(ROUND(CP.Fletes / R.KilosLlegada, 2),0) AS FletePorKilo
			,ISNULL(ROUND(ISNULL(CAC.CostoAlimento, 0) / R.KilosLlegada, 2),0) AS AlimentoPorKilo
			,ISNULL(ROUND(CP.GuiasDeTransito / R.KilosLlegada, 2),0) AS GuiaPorKilo
			--,ROUND(ROUND(CP.Banios,2) + ROUND(CP.ManejoDeGanado,2) + ROUND(CP.Renta,2) + ROUND(CP.MedicamentoEnCentro,2) + ROUND(CP.MedicamentoEnCadis,2) + ROUND((CP.MedicamentoEnPradera / R.KilosLlegada),2),2) AS OtrosPorKilo
			,ISNULL(ROUND(CP.SeguroDeTransportes / R.KilosLlegada, 2),0) AS SeguroTransportePorKilo
			,ISNULL(ROUND(PG.PrecioHembras, 2),0) AS PrecioHembras
			,ISNULL(ROUND(PG.PrecioMachos, 2),0) AS PrecioMachos
			,ISNULL(ROUND(CK.ComisionPorKilo, 2),0) AS ComisionPorKilo
			,ISNULL(ROUND(CK.FletePorKilo, 2),0) AS FletePorKilo
			,ISNULL(ROUND(CK.GuiaPorKilo, 2),0) AS GuiaPorKilo
			,ISNULL(ROUND(CK.OtrosPorKilo, 2),0) AS OtrosPorKilo
			,ISNULL(ROUND(CK.SeguroTransportePorKilo, 2),0) AS SeguroTransportePorKilo
			,ISNULL(ROUND(CK.SeguroCorralPorKilo, 2),0) AS SeguroCorralPorKilo
			,CASE 
				WHEN PG.PrecioMachos = 0
					THEN 0
				ELSE ISNULL(ROUND(ROUND(PG.PrecioMachos, 2) + (ROUND(CK.ComisionPorKilo, 2) + ROUND(CK.FletePorKilo, 2) + ROUND(CK.GuiaPorKilo, 2) + ROUND(CK.OtrosPorKilo, 2) + ROUND(CK.SeguroTransportePorKilo, 2) + ROUND(CK.SeguroCorralPorKilo, 2) + ROUND((ISNULL(CAC.CostoAlimento, 0) / R.KilosLlegada), 2)), 2),0)
				END AS CostoIntegradoMachos
			,CASE 
				WHEN PG.PrecioHembras = 0
					THEN 0
				ELSE ISNULL(ROUND(ROUND(PG.PrecioHembras, 2) + (ROUND(CK.ComisionPorKilo, 2) + ROUND(CK.FletePorKilo, 2) + ROUND(CK.GuiaPorKilo, 2) + ROUND(CK.OtrosPorKilo, 2) + ROUND(CK.SeguroTransportePorKilo, 2) + ROUND(CK.SeguroCorralPorKilo, 2) + ROUND((ISNULL(CAC.CostoAlimento, 0) / R.KilosLlegada), 2)), 2),0)
				END AS CostoIntegradoHembras
			,ISNULL(CAST(KT.KilosMachos AS INT), 0) AS TotalKilosMachos
			,ISNULL(CAST(KT.KilosHembras AS INT), 0) AS TotalKilosHembras
			,ISNULL(CAST(KT.KilosHembras AS INT), 0) + ISNULL(CAST(KT.KilosMachos AS INT), 0) AS TotalKilos
			,ROUND(ISNULL(PG.PrecioMachos, 1) * ISNULL(KT.KilosMachos, 1), 2) AS CostoCompraMachos
			,ROUND(ISNULL(PG.PrecioHembras, 1) * ISNULL(KT.KilosHembras, 1), 2) AS CostoCompraHembras
			,CASE 
				WHEN PG.PrecioMachos = 0
					THEN 0
				ELSE ISNULL(ROUND(ISNULL(KT.KilosMachos, 1) * (ROUND(PG.PrecioMachos, 2) + (ROUND(CK.ComisionPorKilo, 2) + ROUND(CK.FletePorKilo, 2) + ROUND(CK.GuiaPorKilo, 2) + ROUND(CK.OtrosPorKilo, 2) + ROUND(CK.SeguroTransportePorKilo, 2) + ROUND(CK.SeguroCorralPorKilo, 2) + ROUND((ISNULL(CAC.CostoAlimento, 0) / R.KilosLlegada), 2))), 2),0)
				END AS CostoTotalMachos
			,CASE 
				WHEN PG.PrecioHembras = 0
					THEN 0
				ELSE ISNULL(ROUND(ISNULL(KT.KilosHembras, 1) * (ROUND(PG.PrecioHembras, 2) + (ROUND(CK.ComisionPorKilo, 2) + ROUND(CK.FletePorKilo, 2) + ROUND(CK.GuiaPorKilo, 2) + ROUND(CK.OtrosPorKilo, 2) + ROUND(CK.SeguroTransportePorKilo, 2) + ROUND(CK.SeguroCorralPorKilo, 2) + ROUND((ISNULL(CAC.CostoAlimento, 0) / R.KilosLlegada), 2))), 2),0)
				END AS CostoTotalHembras
			,CASE 
				WHEN CS.CabezasMachos = 0
					THEN 0
				ELSE CAST(KT.KilosMachos / CS.CabezasMachos AS INT)
				END AS PesoPromedioMachos
			,CASE 
				WHEN CS.CabezasHembras = 0
					THEN 0
				ELSE CAST(KT.KilosHembras / CS.CabezasHembras AS INT)
				END AS PesoPromedioHembras
			,ISNULL(ROUND(MedicamentoEnCadis + MedicamentoEnCentro + MedicamentoEnPradera, 2),0) AS Medicamento
		FROM #tReporteDiaDia R
		LEFT JOIN #tCostosPivote CP ON (R.EntradaGanado = CP.FolioEntrada)
		INNER JOIN #tCalidades Cal ON (R.EntradaGanado = Cal.FolioEntrada)
		RIGHT JOIN #tCabezasSexo CS ON (R.EntradaGanado = CS.FolioEntrada)
		INNER JOIN #tPesosOrigen PO ON (R.EntradaGanado = PO.FolioEntrda)
		LEFT JOIN #tDiasCentro DC ON (R.EntradaGanado = DC.FolioEntrada)
		INNER JOIN #tPreciosGenero PG ON (R.EntradaGanado = PG.FolioEntrada)
		LEFT JOIN #tCostosPorKilo CK ON (R.EntradaGanado = CK.FolioEntrada)
		LEFT JOIN #tKilosTotales KT ON (R.EntradaGanado = KT.FolioEntrada)
		LEFT JOIN #tCostosAlimentoCentro CAC ON (R.EntradaGanado = CAC.FolioEntrada)
		LEFT JOIN #tComprador C ON (R.EntradaGanado = C.FolioEntrada)
		LEFT JOIN #tFletero F ON (R.EntradaGanado = F.FolioEntrada)
		LEFT JOIN #tMortalidad M ON (R.EntradaGanado = M.FolioEntrada)
		ORDER BY R.EntradaGanado

		DROP TABLE #tReporteDiaDia

		DROP TABLE #tComprador

		DROP TABLE #tFletero

		DROP TABLE #tCostos

		DROP TABLE #tCostosPivote

		DROP TABLE #tCalidades

		DROP TABLE #tCabezasSexo

		DROP TABLE #tPesosOrigen

		DROP TABLE #tDiasCentro

		DROP TABLE #tCostosAlimentoCentro

		DROP TABLE #tPrecioKilo

		DROP TABLE #tPreciosGenero

		DROP TABLE #tCostosPorKilo

		DROP TABLE #tKilosTotales

		DROP TABLE #tMortalidad

		DROP TABLE #tDistancias


	SET NOCOUNT OFF
END

GO

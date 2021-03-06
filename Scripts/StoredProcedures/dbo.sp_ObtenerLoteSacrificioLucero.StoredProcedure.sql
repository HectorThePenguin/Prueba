USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerLoteSacrificioLucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_ObtenerLoteSacrificioLucero]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerLoteSacrificioLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerLoteSacrificioLucero] @fechas VARCHAR(400)
	,@organizacionId INT
AS
BEGIN

	/*declare @organizacionId int
	declare @fechas varchar(400)
	set @organizacionId = 4
	set @fechas = '2015-03-23'*/

	DECLARE @xml XML
		,@str VARCHAR(100)
		,@delimiter VARCHAR(10)

	SET @delimiter = ','
	SET @xml = cast(('<X>' + replace(@fechas, @delimiter, '</X><X>') + '</X>') AS XML)

	SELECT C.value('.', 'varchar(10)') AS fecha
	INTO #Fechas
	FROM @xml.nodes('X') AS X(C)

	CREATE TABLE #CostosSacrificio (
		FechaOperacion SMALLDATETIME
		,tipoGanado VARCHAR(1)
		,Descripcion VARCHAR(50)
		,FolioSalida INT
		,lote VARCHAR(10)
		,Cabezas DECIMAL(18, 2)
		,Kilos DECIMAL(18, 2)
		,Costos DECIMAL(18, 2)
		)

	CREATE TABLE #Costos (
		FechaOperacion SMALLDATETIME
		,lote VARCHAR(10)
		,Cabezas INT
		,PesoNoqueo DECIMAL(18, 2)
		,PesoCanal DECIMAL(18, 2)
		,PesoPiel DECIMAL(18, 2)
		,PesoViscera DECIMAL(18, 2)
		,CostoCanal DECIMAL(18, 2)
		,CostoPiel DECIMAL(18, 2)
		,CostoViscera DECIMAL(18, 2)
		)

	CREATE TABLE #Sacrificio (
		NUM_SALI INT
		,NUM_CORR VARCHAR(3)
		,NUM_PRO VARCHAR(4)
		,FEC_SACR VARCHAR(10)
		,NUM_CAB INT
		)

	IF @organizacionId = 1
	BEGIN
		INSERT INTO #CostosSacrificio
		SELECT a.FechaOperacion
			,tipoGanado
			,CASE 
				WHEN indicador = 2
					THEN 'Canal'
				WHEN indicador = 6
					THEN 'Piel'
				WHEN indicador = 7
					THEN 'Visceras'
				END AS 'Descripcion'
			,FolioSalida
			,lote
			,sum(piezas) Cabezas
			,sum(kilos) Kilos
			,SUM(costo) Costos
		FROM [srvppisocln].basculas_sl.dbo.CierreSacrificio a
		INNER JOIN [srvppisocln].basculas_sl.dbo.CosteoSacrificio b ON a.CierreSacrificioId = b.CierreSacrificioId
		INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		GROUP BY a.FechaOperacion
			,tipoGanado
			,indicador
			,FolioSalida
			,lote
		ORDER BY a.FechaOperacion
			,foliosalida
			,b.lote

		INSERT INTO #Costos
		SELECT canal.FechaOperacion
			,canal.Lote
			,canal.Cabezas
			,noqueo.Peso AS PesoNoqueo
			,canal.Kilos AS PesoCanal
			,piel.Kilos AS PesoPiel
			,viscera.Kilos AS PesoViscera
			,canal.Costos AS CostoCanal
			,piel.Costos AS CostoPiel
			,viscera.Costos AS CostoViscera
		FROM (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Canal'
			) canal
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Piel'
			) piel ON canal.FolioSalida = piel.FolioSalida
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Visceras'
			) viscera ON canal.FolioSalida = viscera.FolioSalida
		INNER JOIN (
			SELECT Lote_Padre
				,sum(Cantidad_Primaria) AS Peso
			FROM [srvppisocln].basculas_sl.dbo.Layout_Subida
			WHERE Fecha_de_Produccion IN (
					SELECT Fecha
					FROM #Fechas
					)
				AND Indicador = 1
			GROUP BY Lote_Padre
			) noqueo ON canal.Lote = noqueo.Lote_Padre

		INSERT INTO #Sacrificio
		SELECT NUM_SALI
			,ss.NUM_CORR
			,NUM_PRO
			,FEC_SACR
			, Count(1) 
		FROM [srvppisocln].basculas_sl.dbo.salida_sacrificio ss
		INNER JOIN [srvppisocln].basculas_sl.dbo.layout_subida ls on
			ss.fec_sacr = ls.fecha_de_produccion
			and ss.num_corr + ss.num_pro = ls.lote_padre
			and ls.indicador = 1
		INNER JOIN #Fechas f ON ss.FEC_SACR = f.fecha
		WHERE origen = 'I' AND Origen_Ganado = 'E'
		GROUP BY NUM_SALI,ss.NUM_CORR,NUM_PRO,FEC_SACR
	END

	IF @organizacionId = 3
	BEGIN
		INSERT INTO #CostosSacrificio
		SELECT a.FechaOperacion
			,tipoGanado
			,CASE 
				WHEN indicador = 2
					THEN 'Canal'
				WHEN indicador = 6
					THEN 'Piel'
				WHEN indicador = 7
					THEN 'Visceras'
				END AS 'Descripcion'
			,FolioSalida
			,lote
			,sum(piezas) Cabezas
			,sum(kilos) Kilos
			,SUM(costo) Costos
		FROM [srvppisomty].basculas_sl.dbo.CierreSacrificio a
		INNER JOIN [srvppisomty].basculas_sl.dbo.CosteoSacrificio b ON a.CierreSacrificioId = b.CierreSacrificioId
		INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		GROUP BY a.FechaOperacion
			,tipoGanado
			,indicador
			,FolioSalida
			,lote
		ORDER BY a.FechaOperacion
			,foliosalida
			,b.lote

		INSERT INTO #Costos
		SELECT canal.FechaOperacion
			,canal.Lote
			,canal.Cabezas
			,noqueo.Peso AS PesoNoqueo
			,canal.Kilos AS PesoCanal
			,piel.Kilos AS PesoPiel
			,viscera.Kilos AS PesoViscera
			,canal.Costos AS CostoCanal
			,piel.Costos AS CostoPiel
			,viscera.Costos AS CostoViscera
		FROM (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Canal'
			) canal
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Piel'
			) piel ON canal.FolioSalida = piel.FolioSalida
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Visceras'
			) viscera ON canal.FolioSalida = viscera.FolioSalida
		INNER JOIN (
			SELECT Lote_Padre
				,sum(Cantidad_Primaria) AS Peso
			FROM [srvppisomty].basculas_sl.dbo.Layout_Subida
			WHERE Fecha_de_Produccion IN (
					SELECT Fecha
					FROM #Fechas
					)
				AND Indicador = 1
			GROUP BY Lote_Padre
			) noqueo ON canal.Lote = noqueo.Lote_Padre

		INSERT INTO #Sacrificio
		SELECT NUM_SALI
			,ss.NUM_CORR
			,NUM_PRO
			,FEC_SACR
			, Count(1) 
		FROM [srvppisomty].basculas_sl.dbo.salida_sacrificio ss
		INNER JOIN [srvppisomty].basculas_sl.dbo.layout_subida ls on
			ss.fec_sacr = ls.fecha_de_produccion
			and ss.num_corr + ss.num_pro = ls.lote_padre
			and ls.indicador = 1
		INNER JOIN #Fechas f ON ss.FEC_SACR = f.fecha
		WHERE origen = 'I' AND Origen_Ganado = 'E'
		GROUP BY NUM_SALI,ss.NUM_CORR,NUM_PRO,FEC_SACR
	END

	IF @organizacionId = 4
	BEGIN
		INSERT INTO #CostosSacrificio
		SELECT a.FechaOperacion
			,tipoGanado
			,CASE 
				WHEN indicador = 4
					THEN 'Canal'
				WHEN indicador = 6
					THEN 'Piel'
				WHEN indicador = 7
					THEN 'Visceras'
				END AS 'Descripcion'
			,FolioSalida
			,lote
			,sum(piezas) Cabezas
			,sum(kilos) Kilos
			,SUM(costo) Costos
		FROM [srvppisomon].basculas_sl.dbo.CierreSacrificio a
		INNER JOIN [srvppisomon].basculas_sl.dbo.CosteoSacrificio b ON a.CierreSacrificioId = b.CierreSacrificioId
		INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		GROUP BY a.FechaOperacion
			,tipoGanado
			,indicador
			,FolioSalida
			,lote
		ORDER BY a.FechaOperacion
			,foliosalida
			,b.lote

		INSERT INTO #Costos
		SELECT canal.FechaOperacion
			,canal.Lote
			,canal.Cabezas
			,noqueo.Peso AS PesoNoqueo
			,canal.Kilos AS PesoCanal
			,piel.Kilos AS PesoPiel
			,isnull(viscera.Kilos, 0) AS PesoViscera
			,canal.Costos AS CostoCanal
			,piel.Costos AS CostoPiel
			,isnull(viscera.Costos, 0) AS CostoViscera
		FROM (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Canal'
			) canal
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Piel'
			) piel ON canal.FolioSalida = piel.FolioSalida
		LEFT OUTER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Visceras'
			) viscera ON canal.FolioSalida = viscera.FolioSalida
		INNER JOIN (
			SELECT Lote_Padre
				,sum(CAntidad_PRimaria) AS Peso
			FROM [srvppisomon].basculas_sl.dbo.Layout_Subida
			WHERE Fecha_de_Produccion IN (
					SELECT Fecha
					FROM #Fechas
					)
				AND Indicador = 1
			GROUP BY Lote_Padre
			) noqueo ON canal.Lote = noqueo.Lote_Padre

		INSERT INTO #Sacrificio
		SELECT NUM_SALI
			,ss.NUM_CORR
			,NUM_PRO
			,FEC_SACR
			, Count(1) 
		FROM [srvppisomon].basculas_sl.dbo.salida_sacrificio ss
		INNER JOIN [srvppisomon].basculas_sl.dbo.layout_subida ls on
			ss.fec_sacr = ls.fecha_de_produccion
			and ss.num_corr + ss.num_pro = ls.lote_padre
			and ls.indicador = 1
		INNER JOIN #Fechas f ON ss.FEC_SACR = f.fecha
		WHERE origen = 'I' AND Origen_Ganado = 'E'
		GROUP BY NUM_SALI,ss.NUM_CORR,NUM_PRO,FEC_SACR
	END

	IF @organizacionId = 5
	BEGIN
		INSERT INTO #CostosSacrificio
		SELECT a.FechaOperacion
			,tipoGanado
			,CASE 
				WHEN indicador = 2
					THEN 'Canal'
				WHEN indicador = 6
					THEN 'Piel'
				WHEN indicador = 7
					THEN 'Visceras'
				END AS 'Descripcion'
			,FolioSalida
			,lote
			,sum(piezas) Cabezas
			,sum(kilos) Kilos
			,SUM(costo) Costos
		FROM [srvppisomty].basculas_sl.dbo.CierreSacrificio a
		INNER JOIN [srvppisomty].basculas_sl.dbo.CosteoSacrificio b ON a.CierreSacrificioId = b.CierreSacrificioId
		INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		GROUP BY a.FechaOperacion
			,tipoGanado
			,indicador
			,FolioSalida
			,lote
		ORDER BY a.FechaOperacion
			,foliosalida
			,b.lote

		INSERT INTO #Costos
		SELECT canal.FechaOperacion
			,canal.Lote
			,canal.Cabezas
			,noqueo.Peso AS PesoNoqueo
			,canal.Kilos AS PesoCanal
			,piel.Kilos AS PesoPiel
			,viscera.Kilos AS PesoViscera
			,canal.Costos AS CostoCanal
			,piel.Costos AS CostoPiel
			,viscera.Costos AS CostoViscera
		FROM (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Canal'
			) canal
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Piel'
			) piel ON canal.FolioSalida = piel.FolioSalida
		INNER JOIN (
			SELECT *
			FROM #CostosSacrificio
			WHERE tipoGanado = 'I'
				AND Descripcion = 'Visceras'
			) viscera ON canal.FolioSalida = viscera.FolioSalida
		INNER JOIN (
			SELECT Lote_Padre
				,sum(CAntidad_PRimaria) AS Peso
			FROM [srvppisomty].basculas_sl.dbo.Layout_Subida
			WHERE Fecha_de_Produccion IN (
					SELECT Fecha
					FROM #Fechas
					)
				AND Indicador = 1
			GROUP BY Lote_Padre
			) noqueo ON canal.Lote = noqueo.Lote_Padre

		INSERT INTO #Sacrificio
		SELECT NUM_SALI
			,ss.NUM_CORR
			,NUM_PRO
			,FEC_SACR
			, Count(1) 
		FROM [srvppisomty].basculas_sl.dbo.salida_sacrificio ss
		INNER JOIN [srvppisomty].basculas_sl.dbo.layout_subida ls on
			ss.fec_sacr = ls.fecha_de_produccion
			and ss.num_corr + ss.num_pro = ls.lote_padre
			and ls.indicador = 1
		INNER JOIN #Fechas f ON ss.FEC_SACR = f.fecha
		WHERE origen = 'I' AND Origen_Ganado = 'E'
		GROUP BY NUM_SALI,ss.NUM_CORR,NUM_PRO,FEC_SACR
	END

	SELECT 0 AS LoteID
		,ss.NUM_SALI AS FolioOrdenSacrificio
		,ss.NUM_CORR AS Corral
		,ss.NUM_PRO AS Lote
		,ss.FEC_SACR AS Fecha
		,cs.CostoCanal AS ImporteCanal
		,cs.CostoPiel AS ImportePiel
		,cs.CostoViscera AS ImporteVisera
		,NULL AS Serie
		,NULL AS Folio
		,'' AS Observaciones
		,1 AS Activo
		,GETDATE() AS FechaCreacion
		,1 AS UsuarioCreacionID
		,NULL AS FechaModificacion
		,NULL AS UsuarioModificacion
		,NULL AS ClienteID
		,ss.NUM_CAB AS Cabezas
		,cs.PesoNoqueo
		,cs.PesoCanal
		,cs.PesoPiel
		,cs.PesoViscera
	FROM #Sacrificio ss
	INNER JOIN #Costos cs ON ss.NUM_CORR + ss.NUM_PRO = cs.Lote
		AND ss.FEC_SACR = cs.FechaOperacion

	DROP TABLE #Fechas
	DROP TABLE #CostosSacrificio
	DROP TABLE #Costos
	DROP TABLE #Sacrificio
END

GO

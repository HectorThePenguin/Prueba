USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaSacrificio_ObtenerPorOrdenSacrificioID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaSacrificio_ObtenerPorOrdenSacrificioID]
GO
/****** Object:  StoredProcedure [dbo].[SalidaSacrificio_ObtenerPorOrdenSacrificioID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: José Gilberto Quintero López
-- Create date: 31/07/2014
-- Description: Obtiene la lista de Parametros por organizacion y tipo de parametro
-- Empresa: Apinterfaces
-- EXEC SalidaSacrificio_ObtenerPorOrdenSacrificioID 163
--001 Jorge Luis Velazquez Araujo 22/05/2015 ***Se agrega que regrese el Tipo de Ganado y el Sexo desde la tabla OrdenSacrificionDetalle
-- =============================================
CREATE PROCEDURE [dbo].[SalidaSacrificio_ObtenerPorOrdenSacrificioID] @OrdenSacrificioID INT
AS
BEGIN
	DECLARE @TipoCorralIntensivoID INT
		,@ProveedorSukarne INT

	SET @TipoCorralIntensivoID = 4
	SET @ProveedorSukarne = (
			SELECT Valor
			FROM ParametroGeneral
			WHERE ParametroID = 90
			) --Parametro del Codigo del proveedor Sukarne

	DECLARE @OrdenSacrificio AS TABLE (
		OrdenSacrificioID INT
		,OrganizacionID INT
		,FolioOrdenSacrificio INT
		,FechaOrden SMALLDATETIME
		,Observaciones VARCHAR(1024)
		,CabezasLote INT
		,CabezasSacrificio INT
		,Clasificacion VARCHAR(100)
		,Orden INT
		,FechaSalida SMALLDATETIME
		,LoteID INT
		,Lote VARCHAR(20)
		,FechaInicio SMALLDATETIME
		,CabezasInicio INT
		,CabezasSalida INT
		,CorralID INT
		,Codigo VARCHAR(10)
		,CostoCabeza DECIMAL(16, 2)
		,CostoCorral DECIMAL(16, 2)
		,EstadoComederoID CHAR(1)
		,DiasEngordaGrano INT
		,TipoCorralID INT
		,TipoGanadoID INT --001
		,Sexo char(1) --001
		)
	DECLARE @UltimoReparto AS TABLE (
		RepartoId INT
		,Fecha SMALLDATETIME
		,LoteID INT
		)

	INSERT INTO @OrdenSacrificio
	SELECT o.OrdenSacrificioID
		,o.OrganizacionID
		,od.FolioSalida
		,o.FechaOrden
		,o.Observaciones
		,od.CabezasLote
		,od.CabezasSacrificio
		,od.Clasificacion
		,od.Orden
		,od.FechaCreacion AS [FechaSalida]
		,l.LoteID
		,l.Lote
		,l.FechaInicio
		,l.CabezasInicio
		,l.CabezasInicio - l.Cabezas AS [CabezasSalida]
		,c.CorralID
		,c.Codigo AS [CodigoCorral]
		,[CostoAnimal] = 0
		,[CostoCorral] = 0
		,[EstadoComederoID] = 0
		,od.DiasEngordaGrano
		,l.TipoCorralID
		,od.TipoGanadoID --001
		,tg.Sexo --001
	FROM OrdenSacrificio o(NOLOCK)
	INNER JOIN OrdenSacrificioDetalle od(NOLOCK) ON od.OrdenSacrificioID = o.OrdenSacrificioID
	INNER JOIN Lote l ON l.LoteID = od.LoteID
	--AND l.Activo = 1
	INNER JOIN Corral c ON c.CorralID = l.CorralID
	left join TipoGanado tg on od.TipoGanadoID = tg.TipoGanadoID
	WHERE o.OrdenSacrificioID = @OrdenSacrificioID

	UPDATE os
	SET CostoCabeza = isnull(c.CostoCabeza, os.CostoCabeza)
		,CostoCorral = isnull(c.CostoCorral, os.CostoCorral)
	FROM @OrdenSacrificio os
	LEFT JOIN (
		SELECT am.CorralId
			,am.LoteID
			,Count(DISTINCT a.AnimalID) AS [Animales]
			,SUM(Importe) / COUNT(DISTINCT a.AnimalID) AS [CostoCabeza]
			,SUM(ac.Importe) AS [CostoCorral]
		FROM AnimalMovimiento am(NOLOCK)
		INNER JOIN Animal a(NOLOCK) ON a.AnimalID = am.AnimalID
		INNER JOIN AnimalCosto ac(NOLOCK) ON ac.AnimalID = am.AnimalID
		INNER JOIN @OrdenSacrificio os ON os.CorralID = am.CorralID
			AND os.LoteID = am.LoteID
		--where am.LoteId = 1767
		--And a.AnimalID = 22408
		GROUP BY am.CorralId
			,am.LoteID
			,a.AnimalID
			,a.Arete
			,ac.CostoID
		) c ON c.CorralID = os.CorralID
		AND c.LoteId = os.LoteId

	INSERT INTO @UltimoReparto
	SELECT r.RepartoID
		,r.Fecha
		,r.LoteID
	FROM (
		SELECT r.RepartoId
			,Fecha
			,r.LoteID
			,ROW_NUMBER() OVER (
				PARTITION BY r.LoteID ORDER BY r.Fecha DESC
				) AS Orden
		FROM Reparto r
		INNER JOIN @OrdenSacrificio os ON os.LoteID = r.LoteID
			--Where LoteID = 1298
		) r
	WHERE r.Orden = 1

	UPDATE os
	SET EstadoComederoID = isnull(r.EstadoComederoID, os.EstadoComederoID)
	FROM @OrdenSacrificio os
	LEFT JOIN (
		SELECT r.LoteID
			,rd.EstadoComederoID
		FROM @UltimoReparto r
		INNER JOIN RepartoDetalle rd ON rd.RepartoID = r.RepartoID
		) r ON r.LoteId = os.LoteID

	SELECT FEC_SACR = Replace(convert(VARCHAR(10), os.FechaOrden, 111), '/', '-')
		,NUM_SALI = cast(os.FolioOrdenSacrificio AS VARCHAR(10))
		,NUM_CORR = substring('000' + os.Codigo, LEN('000' + os.codigo) - 2, 3)
		,NUM_PRO = substring('0000' + os.Lote, LEN('0000' + os.Lote) - 3, 4)
		,FEC_SALC = Replace(convert(VARCHAR(10), os.FechaSalida, 111), '/', '-')
		,HORA_SAL = convert(VARCHAR(5), os.FechaSalida, 114)
		,EDO_COME = cast(os.EstadoComederoID AS VARCHAR(6))
		,NUM_CAB = os.CabezasSacrificio
		,TIP_ANI = CAST(os.TipoGanadoID AS VARCHAR(2)) --001
		,KGS_SAL = NULL
		,PRECIO = 1
		,CASE 
			WHEN os.TipoCorralID = @TipoCorralIntensivoID
				THEN 'I'
			ELSE 'P'
			END AS ORIGEN
		,CTA_PROVIN = NULL
		,PRE_EST = NULL
		,ID_SalidaSacrificio = 0
		,CASE 
			WHEN os.TipoCorralID = @TipoCorralIntensivoID
				THEN 'C'
			ELSE 'P'
			END AS VENTA_PARA
		,CASE 
			WHEN os.TipoCorralID = @TipoCorralIntensivoID
				THEN CAST(@ProveedorSukarne AS VARCHAR(8))
			ELSE NULL
			END AS COD_PROVEEDOR
		,NOTAS = os.Observaciones
		,CASE 
			WHEN os.TipoCorralID = @TipoCorralIntensivoID
				THEN '0'
			ELSE cast(os.CostoCabeza AS VARCHAR(20))
			END AS COSTO_CABEZA
		,CABEZAS_PROCESADAS = 0
		,FICHA_INICIO = 0
		,CASE 
			WHEN os.TipoCorralID = @TipoCorralIntensivoID
				THEN '0'
			ELSE cast(os.CostoCorral AS VARCHAR(20))
			END AS COSTO_CORRAL
		,UNI_ENT = cast(os.CabezasInicio AS VARCHAR(8))
		,UNI_SAL = cast(os.CabezasSalida AS VARCHAR(8))
		,SYNC = 'N'
		,ID_S = 0
		,CASE 
			WHEN os.Sexo = 'M'
			THEN 1
			else 2
			end AS 	SEXO --001
		,DIAS_ENG = cast(os.DiasEngordaGrano AS VARCHAR(8))
		,FOLIO_ENTRADA_I = NULL
		,CASE 
			WHEN os.TipoCorralID = @TipoCorralIntensivoID
				THEN 'E'
			ELSE 'P'
			END AS ORIGEN_GANADO
		,TIPO_SALIDA = NULL
		,os.OrganizacionID
		,os.CorralID
		,os.LoteID
		,@OrdenSacrificioID AS OrdenSacrificioID
	FROM @OrdenSacrificio os
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteSalidasConCosto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteSalidasConCosto]
GO
/****** Object:  StoredProcedure [dbo].[ReporteSalidasConCosto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReporteSalidasConCosto]
@OrganizacionID INT
, @FechaInicial DATE	
, @FechaFinal DATE	
, @TipoSalida INT	
, @TipoProceso INT	
, @EsDetallado BIT	
AS
/*
=============================================
-- Author     : Daniel Benitez
-- Create date: 2015/06/01
-- Description: Obtiene los costos de salida
-- exec ReporteSalidasConCosto 4, '20150526',  '20150626', 8, 1,0
--=============================================
*/
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @IDTipoSalidaPorSacrificio INT
	DECLARE @IDTipoCorralProduccion INT
	SET @IDTipoSalidaPorSacrificio = 16
	SET @IDTipoCorralProduccion = 2


	SELECT A.AnimalID,A.Arete,AM.TipoMovimientoID, TM.Descripcion AS TipoSalida,AM.FechaMovimiento,C.Codigo AS Corral,C.TipoCorralID,TG.Descripcion AS TipoGanado,
				A.PesoCompra,
				CASE WHEN AM.TipoMovimientoID = @IDTipoSalidaPorSacrificio THEN
					A.PesoNoqueo 
				ELSE
					AM.Peso
				END AS PesoNoqueo,CAST(A.PesoCanal AS DECIMAL(16,2)) AS PesoCanal,A.PesoPiel,
				CASE WHEN AM.TipoMovimientoID = @IDTipoSalidaPorSacrificio THEN 
							1.0
				ELSE
							0.0
				END AS PesoVisceras,
				AM.LoteID, 
				CASE WHEN AM.TipoMovimientoID = @IDTipoSalidaPorSacrificio AND LS.ImporteCanal > 0 AND LS.PesoCanal > 0 THEN 
							(LS.ImporteCanal/LS.PesoCanal)* A.PesoCanal
				ELSE
							0
				END AS ImporteCanal,
				CASE WHEN AM.TipoMovimientoID = @IDTipoSalidaPorSacrificio AND LS.ImportePiel > 0 AND LS.PesoPiel > 0 THEN 
							(LS.ImportePiel/LS.PesoPiel)* A.PesoPiel
				ELSE
							0
				END AS ImportePiel,
				CASE WHEN AM.TipoMovimientoID = @IDTipoSalidaPorSacrificio AND LS.ImporteVisera > 0 AND OSD.CabezasSacrificio > 0 THEN 
							(LS.ImporteVisera/OSD.CabezasSacrificio)
				ELSE
							0
				END AS ImporteVisceras
	INTO #Animales
	FROM AnimalHistorico A (NOLOCK)
	INNER JOIN AnimalMovimientoHistorico AM (NOLOCK) ON (A.AnimalID = AM.AnimalID)
	INNER JOIN Corral C (NOLOCK) ON (AM.CorralID = C.CorralID)
	INNER JOIN Lote L (NOLOCK) ON (L.LoteID = AM.LoteID)
	INNER JOIN TipoMovimiento TM (NOLOCK) ON (AM.TipoMovimientoID = TM.TipoMovimientoID)
	INNER JOIN TipoGanado TG (NOLOCK) ON (TG.TipoGanadoID = A.TipoGanadoID)
	LEFT JOIN LoteSacrificio LS (NOLOCK) ON (LS.LoteID = L.LoteID)
	LEFT JOIN OrdenSacrificioDetalle OSD (NOLOCK) ON (OSD.OrdenSacrificioID = LS.OrdenSacrificioID AND OSD.LoteID = L.LoteID)
	WHERE AM.TipoMovimientoID = @TipoSalida AND 
	L.TipoProcesoID = @TipoProceso AND
	CONVERT(VARCHAR(10),AM.FechaMovimiento,112) >= CONVERT(VARCHAR(10),@FechaInicial,112) AND 
	CONVERT(VARCHAR(10),AM.FechaMovimiento,112) <= CONVERT(VARCHAR(10),@FechaFinal,112) AND 
	AM.OrganizacionID = @OrganizacionID

	IF @TipoSalida = @IDTipoSalidaPorSacrificio 
	BEGIN
		DELETE FROM #Animales WHERE TipoMovimientoID = @IDTipoSalidaPorSacrificio AND TipoCorralID <> @IDTipoCorralProduccion
	END

	SELECT 
		A.AnimalID,
		ACH.CostoID,
		SUM(Importe) AS Importe
	INTO #Costos
	FROM #Animales A 
	INNER JOIN AnimalCostoHistorico ACH (NOLOCK) ON (A.AnimalID = ACH.AnimalID)
	GROUP BY 
		A.AnimalID,
		ACH.CostoID
	ORDER BY A.AnimalID

	IF @EsDetallado = 1
	BEGIN
		SELECT 
			A.TipoMovimientoID,
			A.TipoSalida,
			A.FechaMovimiento,
			A.Corral,
			A.TipoGanado,
			A.Arete,
			A.PesoCompra,
			A.PesoNoqueo,
			A.PesoCanal,
			A.ImporteCanal,
			A.PesoPiel,
			A.ImportePiel,
			A.PesoVisceras,
			A.ImporteVisceras,
			C.Importe AS ImporteCosto,
			CT.Descripcion AS DescripcionCosto
		FROM #Animales A
		INNER JOIN #Costos C ON (A.AnimalID = C.AnimalID)
		INNER JOIN Costo CT(NOLOCK) ON (C.CostoID = CT.CostoID)
		WHERE C.Importe > 0
		ORDER BY A.Corral,
				 A.Arete,
				 TipoCostoID
	END
	ELSE
	BEGIN
		SELECT 
			A.TipoMovimientoID,
			A.TipoSalida,
			MAX(A.FechaMovimiento) AS FechaMovimiento,
			A.Corral,
			A.TipoGanado,
			CAST((
					SELECT COUNT(1) FROM #Animales ATMP WHERE ATMP.LoteID = A.LoteID AND ATMP.TipoGanado = A.TipoGanado
			)AS VARCHAR(6)) 'Arete',
			SUM(A.PesoCompra) 'PesoCompra',
			SUM(A.PesoNoqueo) 'PesoNoqueo',
			SUM(A.PesoCanal) 'PesoCanal',
			SUM(A.ImporteCanal) 'ImporteCanal', 
			SUM(A.PesoPiel) 'PesoPiel',
			SUM(A.ImportePiel) 'ImportePiel',
			SUM(A.PesoVisceras) 'PesoVisceras',
			SUM(A.ImporteVisceras) 'ImporteVisceras',
			SUM(C.Importe) 'ImporteCosto',
			'COSTOS' AS DescripcionCosto
		FROM #Animales A
		INNER JOIN #Costos C ON (A.AnimalID = C.AnimalID)
		INNER JOIN Costo CT(NOLOCK) ON (C.CostoID = CT.CostoID)
		WHERE C.Importe > 0
		GROUP BY A.TipoMovimientoID,
				A.TipoSalida,
				A.TipoGanado,
				A.LoteID,
				--A.FechaMovimiento,
				A.Corral
		ORDER BY A.Corral,
				 DescripcionCosto
					
	END

	DROP TABLE #Animales
	DROP TABLE #Costos

END

GO

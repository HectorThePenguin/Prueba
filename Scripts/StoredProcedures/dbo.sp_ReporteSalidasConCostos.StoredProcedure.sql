USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteSalidasConCostos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_ReporteSalidasConCostos]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteSalidasConCostos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Reporte Salida Sacrificio Agrupado
CREATE PROCEDURE [dbo].[sp_ReporteSalidasConCostos]
 @OrganizacionID INT
,@TipoProcesoID INT
,@TipoSalidaID INT
,@FechaInicial DATETIME
,@FechaFinal DATETIME
,@Costos VARCHAR(20)
AS
BEGIN
IF  @Costos = 'Agrupados'
	BEGIN
		SELECT
		 CONVERT(VARCHAR(10),amh.FechaMovimiento,103) AS [Fecha]
		,tm.DescripciON AS [Salida]
		,amh.CorralID AS [Corral]
		,tg.descripcion AS [TipoGanado]
		,COUNT(*) AS [Cant]
		,'0' AS [Arete]
		,SUM(ah.PesoCompra)   AS [KilosOrigen]
		,SUM(ah.PesoNoqueo)   AS [KilosSalida]
		,SUM(ah.PesoCanal)    AS [CanalKilos]
		,SUM(ls.ImporteCanal)/CASE WHEN SUM(PesoCanal) = 0 THEN 1 ELSE SUM(ah.PesoPiel) END  AS [CanalPrecio]
		,SUM(ls.ImporteCanal) AS [CanalImporte]
		,SUM(ah.PesoPiel)     AS [PielKilos]
		,SUM(ls.ImportePiel)/CASE WHEN SUM(ah.PesoPiel) = 0 THEN 1 ELSE SUM(ah.PesoPiel) END AS [PielPrecio]
		,SUM(ls.ImportePiel) AS [PielImporte]
		,SUM(ls.ImporteVisera)/COUNT(1) AS [ViscerASPrecio]
		,SUM(ls.ImporteVisera) AS [ViscerASImporte]
		,'Costos' AS [CostosCostos]
		,SUM(ach.Importe) AS [CostosImporte]
		
		FROM TipoGanado TG
		INNER JOIN AnimalHistorico AH ON TG.TipoGanadoID = AH.TipoGanadoID
		INNER JOIN AnimalCostoHistorico ach ON ah.AnimalID = ach.AnimalID
		INNER JOIN AnimalMovimientoHistorico amh ON ach.AnimalID = amh.AnimalID
		INNER JOIN LoteSacrificio ls ON amh.LoteID = ls.LoteID
		INNER JOIN Lote l ON ls.loteid = l.loteID
		INNER JOIN Corral c ON l.CorralID = c.CorralID
		INNER JOIN TipoMovimiento tm ON amh.TipoMovimientoID = tm.TipoMovimientoID And tm.EsGanado = 1 And tm.EsSalida = 1 And tm.Activo = 1
		WHERE amh.TipoMovimientoID = @TipoSalidaID  And amh.OrganizacionID = 1
		AND CONVERT(VARCHAR(10),amh.FechaMovimiento,112) Between CONVERT(VARCHAR(10),@FechaInicial,112) And CONVERT(VARCHAR(10),@FechaFinal,112)
		GROUP BY amh.corralID, amh.loteID, amh.FechaMovimiento,tm.Descripcion,tg.descripcion		
	END
ELSE
	begin
		SELECT 
		  CONVERT(VARCHAR(10),amh.FechaMovimiento,103) AS [Fecha]
		 ,tm.Descripcion AS [Salida]
		 ,amh.CorralID AS [Corral]
		 ,TG.DescripciON AS [TipoGanado]
		 ,0 AS [Cant]
		 ,AH.Arete AS [Arete]
		 ,AVG(AH.PesoCompra) AS [KilosOrigen]
		 ,AVG(AH.PesoNoqueo) AS [KilosSalida]   
		,0 AS [CanalKilos] ,0.0 AS [CanalPrecio] ,0.0 AS [CanalImporte]					 -- Canal
		,0 AS [PielKilos] ,0.0 AS [PielPrecio] ,0.0 AS [PielImporte]					 -- Piel 
		,0.0 AS [ViscerasPrecio] ,0.0 AS [ViscerasImporte]								 -- ViscerAS
		,C.Descripcion AS [CostosCostos], SUM(ACH.Importe) AS [CostosImporte]	 -- Costos e Importe
		,C.CostoID 
		from TipoGanado TG
		INNER JOIN AnimalHistorico AH 
			ON TG.TipoGanadoID = AH.TipoGanadoID
		INNER JOIN AnimalCostoHistorico ACH
			ON AH.AnimalID = ACH.AnimalID
		INNER JOIN Costo C
			ON ACH.CostoID = C.CostoID
		INNER JOIN AnimalMovimientoHistorico AMH
			ON AH.AnimalID = AMH.AnimalID
		INNER JOIN TipoMovimiento tm
		    ON amh.TipomovimientoID = tm.TipomovimientoID	
		And CONVERT(VARCHAR(10),amh.FechaMovimiento,112) Between CONVERT(VARCHAR(10),@FechaInicial,112) And CONVERT(VARCHAR(10),@FechaFinal,112)
		AND AMH.TipoMovimientoID = @TipoSalidaID And amh.OrganizacionID = @OrganizacionID
		Group By AH.Arete,amh.CorralID , AMH.LoteID,AMH.FechaMovimiento,C.Descripcion,C.CostoID, TG.Descripcion,tm.Descripcion
		Order By AH.Arete,C.CostoID
	END
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanadoReporteEjecutivo_ObtenerPorFechas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanadoReporteEjecutivo_ObtenerPorFechas]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanadoReporteEjecutivo_ObtenerPorFechas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 13/02/2014
-- Description: Obtiene los datos para el reporte ejecutivo
-- SpName     : TipoGanadoReporteEjecutivo_ObtenerPorFechas 1, '20140901' , '20140927'
--======================================================
CREATE  PROCEDURE [dbo].[TipoGanadoReporteEjecutivo_ObtenerPorFechas]
@OrganizacionId INT,
@FechaInicio DATETIME, 
@FechaFin  DATETIME
AS
BEGIN
	SET NOCOUNT ON
		SET @FechaFin = DATEADD(HH, 23, @FechaFin)
		SET @FechaFin = CAST (@FechaFin as DATE)
		SET @FechaInicio = CAST (@FechaInicio as DATE)
		SELECT	DISTINCT TG.TipoGanadoID
			,  TG.Descripcion AS TipoGanado
			,  TG.Sexo
			,  ED.Cabezas
			,  ED.PesoOrigen
			,  EG.PesoBruto
			,  EG.PesoTara
			,  ED.PesoLlegada
			,  EG.CabezasRecibidas
			,  ED.PrecioKilo
			,  EGCosteo.EntradaGanadoCosteoID
			, (((ED.PesoOrigen - ED.PesoLlegada ) / ED.PesoOrigen) * 100) as Merma
			,(Select (sum(Importe) )
				from EntradaGanadoCosto (NOLOCK)
				where EntradaGanadoCosto.EntradaGanadoCosteoID = EGCosteo.EntradaGanadoCosteoID)
				/
			 (Select sum(ED2.PesoOrigen) 
						FROM EntradaDetalle (NOLOCK) ED2		
						Where ED2.EntradaGanadoCosteoID = EGCosteo.EntradaGanadoCosteoID) * ED.Cabezas as CostoIntegrado
		FROM EntradaGanado (NOLOCK) EG
		INNER JOIN EntradaGanadoCalidad (NOLOCK) EGC
			ON (EG.EntradaGanadoID = EGC.EntradaGanadoID)
		INNER JOIN EntradaGanadoCosteo (NOLOCK) EGCosteo
			ON (EGC.EntradaGanadoID = EGCosteo.EntradaGanadoID
				AND EG.EntradaGanadoID = EGCosteo.EntradaGanadoID
				AND EG.OrganizacionID = EGCosteo.OrganizacionID
				AND EGCosteo.Activo = 1)
		INNER JOIN EntradaDetalle (NOLOCK) ED
			ON (EGCosteo.EntradaGanadoCosteoID = ED.EntradaGanadoCosteoID )
		INNER JOIN EntradaGanadoCosto (NOLOCK) EGCosto
			ON (EGCosteo.EntradaGanadoCosteoID = EGCosto.EntradaGanadoCosteoID
				AND ED.EntradaGanadoCosteoID = EGCosto.EntradaGanadoCosteoID
				)
		INNER JOIN TipoGanado (NOLOCK) TG
			ON (ED.TipoGanadoID = TG.TipoGanadoID)
		WHERE (CAST (EG.FechaEntrada AS DATE) BETWEEN @FechaInicio AND @FechaFin)
		AND EG.OrganizacionID = @OrganizacionId
		GROUP BY TG.TipoGanadoID
			,  TG.Descripcion
			,  TG.Sexo
			,  ED.Cabezas
			,  ED.PesoOrigen
			,  EG.PesoBruto
			,  EG.PesoTara
			,  EG.CabezasRecibidas
			,  ED.PrecioKilo
			,  EGCosteo.EntradaGanadoCosteoID
			,  ED.PesoLlegada
			Order By TG.Sexo desc, TG.TipoGanadoID
	SET NOCOUNT OFF
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostosReporteEjecutivo_ObtenerPorFechas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostosReporteEjecutivo_ObtenerPorFechas]
GO
/****** Object:  StoredProcedure [dbo].[CostosReporteEjecutivo_ObtenerPorFechas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 13/02/2014
-- Description: Obtiene los Costos por Entrada Ganado Costeo
-- SpName     : CostosReporteEjecutivo_ObtenerPorFechas '20140220' , '20140223'
--======================================================
CREATE PROCEDURE [dbo].[CostosReporteEjecutivo_ObtenerPorFechas]
@OrganizacionId INT
,@FechaInicio DATETIME
, @FechaFin  DATETIME
AS
BEGIN
	SET NOCOUNT ON		
		SET @FechaFin = DATEADD(HH, 24, @FechaFin)
		SELECT C.CostoID
			,  C.Descripcion
			,  EGC.Importe
			,  EGC.EntradaGanadoCosteoID
		FROM EntradaGanadoCosto EGC
		INNER JOIN Costo C
			ON (EGC.CostoID = C.CostoID
				AND C.CostoID <> 1)
		INNER JOIN EntradaGanadoCosteo EGCosteo
			ON (EGC.EntradaGanadoCosteoID = EGCosteo.EntradaGanadoCosteoID
				AND EGCosteo.Activo = 1)
		INNER JOIN EntradaGanado EG
			ON (EGCosteo.EntradaGanadoID = EG.EntradaGanadoID
				AND EGCosteo.OrganizacionID = EG.OrganizacionID
				AND EG.FechaEntrada >= @FechaInicio AND EG.FechaEntrada <= @FechaFin)
		WHERE EG.OrganizacionID = @OrganizacionId
	SET NOCOUNT OFF
END

GO

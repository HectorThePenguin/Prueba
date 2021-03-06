USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_ObtenerPorFiltros]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimento_ObtenerPorFiltros]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_ObtenerPorFiltros]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 15/07/2014
-- Description:  Consulta los Repartos por los filtros proporcionados
-- RepartoAlimento_ObtenerPorFiltros 1,6,'20140508',2
-- ===============================================================
CREATE PROCEDURE [dbo].[RepartoAlimento_ObtenerPorFiltros] @TipoServicioID INT
	,@OperadorID INT
	,@Fecha SMALLDATETIME
	,@CamionRepartoID INT
AS
CREATE TABLE #REPARTOS (
	RepartoAlimentoID INT
	,TipoServicioID INT
	,CamionRepartoID INT
	,UsuarioIDReparto INT
	,HorometroInicial INT
	,HorometroFinal INT
	,OdometroInicial INT
	,OdometroFinal INT
	,LitrosDiesel INT
	,FechaReparto DATETIME
	)
CREATE TABLE #REPARTOSDETALLE (
	RepartoAlimentoID INT
	,RepartoAlimentoDetalleID INT
	,Tolva VARCHAR(10)
	,FolioReparto INT
	,FormulaIDRacion INT
	,KilosEmbarcados INT
	,KilosRepartidos INT
	,Sobrante INT
	,PesoFinal INT
	,CorralInicio VARCHAR(10)
	,CorralFinal VARCHAR(10)
	,HoraRepartoInicio VARCHAR(5)
	,HoraRepartoFinal VARCHAR(5)
	,Observaciones VARCHAR(255)
	)
CREATE TABLE #TIEMPOSMUERTOS (
	TiempoMuertoID INT
	,RepartoAlimentoID INT
	,HoraInicio VARCHAR(5)
	,HoraFin VARCHAR(5)
	,CausaTiempoMuertoID INT
	,CausaTiempoMuerto VARCHAR(50)
	)
INSERT INTO #REPARTOS
SELECT RepartoAlimentoID
	,TipoServicioID
	,CamionRepartoID
	,UsuarioIDReparto
	,HorometroInicial
	,HorometroFinal
	,OdometroInicial
	,OdometroFinal
	,LitrosDiesel
	,FechaReparto
FROM RepartoAlimento
WHERE TipoServicioID = @TipoServicioID
	AND UsuarioIDReparto = @OperadorID
	AND Convert(VARCHAR(10), FechaReparto, 112) = Convert(VARCHAR(10), @Fecha, 112)
	AND CamionRepartoID = @CamionRepartoID
INSERT INTO #REPARTOSDETALLE
SELECT rad.RepartoAlimentoID
	,rad.RepartoAlimentoDetalleID
	,rad.Tolva
	,rad.FolioReparto
	,rad.FormulaIDRacion
	,rad.KilosEmbarcados
	,rad.KilosRepartidos
	,rad.Sobrante
	,rad.PesoFinal
	,co1.Codigo AS CorralInicio
	,co2.Codigo AS CorralFinal
	,rad.HoraRepartoInicio
	,rad.HoraRepartoFinal
	,rad.Observaciones
FROM RepartoAlimentoDetalle rad
INNER JOIN Corral co1 ON rad.CorralIDInicio = co1.CorralID
INNER JOIN Corral co2 ON rad.CorralIDFinal = co2.CorralID
INNER JOIN #REPARTOS re ON rad.RepartoAlimentoID = re.RepartoAlimentoID
INSERT INTO #TIEMPOSMUERTOS
SELECT TiempoMuertoID
	,re.RepartoAlimentoID
	,HoraInicio
	,HoraFin
	,ctm.CausaTiempoMuertoID
	,ctm.Descripcion AS CausaTiempoMuerto
FROM TiempoMuerto tm
INNER JOIN CausaTiempoMuerto ctm ON tm.CausaTiempoMuertoID = ctm.CausaTiempoMuertoID
INNER JOIN #REPARTOS re ON tm.RepartoAlimentoID = re.RepartoAlimentoID
select * from #REPARTOS
select * from #REPARTOSDETALLE
select * from #TIEMPOSMUERTOS

GO

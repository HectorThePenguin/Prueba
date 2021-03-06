USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_ObtenerImpresion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimento_ObtenerImpresion]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_ObtenerImpresion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 17/07/2014
-- Description:  Consulta los Repartos para la impresion del CheckList
-- RepartoAlimento_ObtenerImpresion 6,'20140717',2
-- ===============================================================
CREATE PROCEDURE [dbo].[RepartoAlimento_ObtenerImpresion] @OperadorID INT
	,@Fecha SMALLDATETIME
	,@CamionRepartoID INT
AS
CREATE TABLE #Repartos (
	RepartoAlimentoID INT
	,TipoServicioID INT
	,CamionRepartoID INT
	,NumeroEconomico VARCHAR(10)
	,UsuarioIDReparto INT
	,UsuarioReparto VARCHAR(250)
	,HorometroInicial INT
	,HorometroFinal INT
	,OdometroInicial INT
	,OdometroFinal INT
	,LitrosDiesel INT
	,FechaReparto DATETIME
	)
CREATE TABLE #ReportesDetalle (
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
CREATE TABLE #TiemposMuertos (
	RepartoAlimentoID INT
	,HoraInicio VARCHAR(5)
	,HoraFin VARCHAR(5)
	,CausaTiempoMuertoID INT
	,CausaTiempoMuerto VARCHAR(50)
	)
INSERT INTO #Repartos
SELECT RepartoAlimentoID
	,TipoServicioID
	,cr.CamionRepartoID
	,cr.NumeroEconomico
	,ra.UsuarioIDReparto
	,us.Nombre
	,HorometroInicial
	,HorometroFinal
	,OdometroInicial
	,OdometroFinal
	,LitrosDiesel
	,FechaReparto
FROM RepartoAlimento ra
INNER JOIN Usuario us ON ra.UsuarioIDReparto = us.UsuarioID
INNER JOIN CamionReparto cr ON ra.CamionRepartoID = cr.CamionRepartoID
WHERE UsuarioIDReparto = @OperadorID
	AND Convert(VARCHAR(10), FechaReparto, 112) = Convert(VARCHAR(10), @Fecha, 112)
	AND cr.CamionRepartoID = @CamionRepartoID
INSERT INTO #ReportesDetalle
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
INNER JOIN #Repartos re ON rad.RepartoAlimentoID = re.RepartoAlimentoID
INNER JOIN RepartoAlimento ra ON rad.RepartoAlimentoID = ra.RepartoAlimentoID
INNER JOIN Corral co1 ON rad.CorralIDInicio = co1.CorralID
INNER JOIN Corral co2 ON rad.CorralIDFinal = co2.CorralID
INSERT INTO #TiemposMuertos
SELECT re.RepartoAlimentoID
	,HoraInicio
	,HoraFin
	,ctm.CausaTiempoMuertoID
	,ctm.Descripcion AS CausaTiempoMuerto
FROM TiempoMuerto tm
INNER JOIN CausaTiempoMuerto ctm ON tm.CausaTiempoMuertoID = ctm.CausaTiempoMuertoID
INNER JOIN #Repartos re ON tm.RepartoAlimentoID = re.RepartoAlimentoID
select * from #Repartos
select * from #ReportesDetalle
select * from #TiemposMuertos

GO

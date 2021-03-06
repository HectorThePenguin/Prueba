USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_ObtenerPorRepartoAlimento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimentoDetalle_ObtenerPorRepartoAlimento]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_ObtenerPorRepartoAlimento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 15/07/2014
-- Description:  Consulta los Repartos por los filtros proporcionados
-- RepartoAlimentoDetalle_ObtenerPorRepartoAlimento 11
-- ===============================================================
CREATE PROCEDURE [dbo].[RepartoAlimentoDetalle_ObtenerPorRepartoAlimento] @RepartoAlimentoID INT
AS
SELECT RepartoAlimentoDetalleID
	,ra.RepartoAlimentoID
	,FolioReparto
	,FormulaIDRacion
	,Tolva
	,KilosEmbarcados
	,KilosRepartidos
	,Sobrante
	,PesoFinal
	,CorralIDInicio
	,co1.Codigo AS  CorralInicio
	,CorralIDFinal
	,co2.Codigo AS CorralFinal
	,HoraRepartoInicio
	,HoraRepartoFinal
	,Observaciones
	,ra.Activo
FROM RepartoAlimentoDetalle rad
INNER JOIN RepartoAlimento ra ON rad.RepartoAlimentoID = ra.RepartoAlimentoID
INNER JOIN Corral co1 on rad.CorralIDInicio = co1.CorralID
INNER JOIN Corral co2 on rad.CorralIDFinal = co2.CorralID
WHERE ra.RepartoAlimentoID = @RepartoAlimentoID

GO

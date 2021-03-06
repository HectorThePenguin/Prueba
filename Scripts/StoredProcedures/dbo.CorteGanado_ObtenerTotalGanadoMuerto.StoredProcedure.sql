USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalGanadoMuerto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerTotalGanadoMuerto]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalGanadoMuerto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		ricardo.lopez
-- Create date: 2013/12/17
-- Description: SP para obtener el total de animales Muertos
-- Origen     : APInterfaces
-- EXEC  [dbo].CorteGanado_ObtenerTotalGanadoMuerto '<ROOT><PartidasCorte><NoPartida>4475</NoPartida></PartidasCorte><PartidasCorte><NoPartida>4493</NoPartida></PartidasCorte></ROOT>', 1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerTotalGanadoMuerto]
  @NoPartida XML,
  @OrganizacionID INT
AS
BEGIN
	DECLARE @PartidasCorte AS TABLE ([NoPartida] INT)

	INSERT @PartidasCorte ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
	FROM @NoPartida.nodes('ROOT/PartidasCorte') AS T(item)

	/* Se regresan los animales muertos de las partidas para corte*/
	SELECT COALESCE(COUNT(CEG.AnimalID),0) AS Muertas 
	FROM ControlEntradaGanado AS CEG
	INNER JOIN EntradaGanado EG 
	ON CEG.EntradaGanadoID = EG.EntradaGanadoID
	WHERE EG.FolioEntrada IN (SELECT NoPartida FROM @PartidasCorte)
	AND CEG.Activo = 1
	AND EG.Activo = 1 
	AND EG.OrganizacionID = @OrganizacionID;

	SELECT NoPartida FROM @PartidasCorte

END

GO
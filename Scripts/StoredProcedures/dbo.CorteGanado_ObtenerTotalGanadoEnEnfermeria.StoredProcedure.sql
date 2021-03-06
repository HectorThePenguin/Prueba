USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalGanadoEnEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerTotalGanadoEnEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalGanadoEnEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		ricardo.lopez
-- Create date: 2013/12/17
-- Description: SP para obtener obtener el total de animales que se encuentran en corral de enfermeria
-- Origen     : APInterfaces
-- EXEC  [dbo].[CorteGanado_ObtenerTotalGanadoEnEnfermeria] 1, 2, 1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerTotalGanadoEnEnfermeria]
  @NoPartida XML,
  @GrupoCorralID INT,
  @OrganizacionID INT
AS
BEGIN

	DECLARE @PartidasCorte AS TABLE ([NoPartida] INT)

	INSERT @PartidasCorte ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
	FROM @NoPartida.nodes('ROOT/PartidasCorte') AS T(item)

	SELECT COUNT(*) AS [Muertas] 
	FROM Animal a (NOLOCK)
	INNER JOIN AnimalMovimiento am(NOLOCK) ON a.AnimalID = am.AnimaLID 
	WHERE AnimalMovimientoID IN (
				SELECT MIN(am.AnimalMovimientoID) AnimalMovimientoID 
				FROM Animal a(NOLOCK)
				INNER JOIN AnimalMovimiento am(NOLOCK) ON a.AnimalID = am.AnimaLID 
				WHERE a.OrganizacionIDEntrada = @OrganizacionID
				AND a.FolioEntrada IN (SELECT NoPartida FROM @PartidasCorte )
				GROUP BY am.AnimalID)
	AND am.TipoMovimientoID IN (7)
	
/*  SELECT COUNT(1) AS [EnEnfermeria]
	  FROM AnimalMovimiento am 
	 INNER JOIN Corral c ON am.CorralID = c.CorralID 
	 INNER JOIN TipoCorral tc ON c.TipoCorralID = tc.TipoCorralID
	 WHERE am.Activo = 1 --AND tc.GrupoCorralID = @GrupoCorralID
	   AND am.AnimalID IN (SELECT AnimaLID
							 FROM Animal a
							WHERE a.OrganizacionIDEntrada = @OrganizacionID
							 AND a.FolioEntrada IN (SELECT NoPartida FROM @PartidasCorte )
							  AND a.Activo = 1 )*/


END


GO

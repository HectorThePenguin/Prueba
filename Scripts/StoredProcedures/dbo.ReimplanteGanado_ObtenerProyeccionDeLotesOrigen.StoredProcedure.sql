USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerProyeccionDeLotesOrigen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerProyeccionDeLotesOrigen]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerProyeccionDeLotesOrigen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/10/22
-- Description: SP para Obtener los loteProyeccion de los corrales Origenes para el corral-lote destino
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ObtenerProyeccionDeLotesOrigen 1,1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerProyeccionDeLotesOrigen]
	@CorralDestinoID INT,
	@LoteDestinoID INT
AS
BEGIN
	
	DECLARE @CorralDescartado CHAR(3) = 'R99';
	
	SELECT PR.LoteID, LP.ConsumoBaseHumeda, LP.Conversion, LP.PesoMaduro, LP.PesoSacrificio, LR.NumeroReimplante
	  FROM ProgramacionReimplante PR
	 INNER JOIN ProgramacionReimplanteDetalle PRD ON PR.FolioProgramacionID = PRD.FolioProgramacionID
	 INNER JOIN LoteProyeccion LP ON LP.LoteID = PR.LoteID
	 INNER JOIN LoteReimplante LR ON LR.LoteProyeccionID = LP.LoteProyeccionID
	 INNER JOIN Lote L ON L.LoteID = PR.LoteID
	 INNER JOIN Corral C ON C.CorralID = L.CorralID
	 INNER JOIN Corral Cd ON Cd.CorralID = PRD.CorralDestinoID
  -- INNER JOIN Lote Ld ON Ld.LoteID = 590
	 WHERE PRD.CorralDestinoID = @CorralDestinoID
	   AND CONVERT(CHAR(8),LR.FechaProyectada,112) 
			BETWEEN CONVERT(CHAR(8),GETDATE()-3,112) AND CONVERT(CHAR(8),GETDATE()+7,112)
	   AND PR.LoteID IN (
			SELECT AM.LoteIDOrigen
			  FROM Animal A(NOLOCK)
			 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
			 INNER JOIN Lote Lo ON Lo.LoteID = AM.LoteIDOrigen
			 INNER JOIN Corral C ON C.CorralID = Lo.CorralID
			 WHERE AM.Activo = 1 
			   AND C.Codigo != @CorralDescartado
			   AND AM.LoteID = @LoteDestinoID
			   AND AM.CorralID = @CorralDestinoID
			 GROUP BY AM.LoteIDOrigen
		)
	   ORDER BY LR.FechaProyectada DESC, Fecha DESC;
	
END


GO

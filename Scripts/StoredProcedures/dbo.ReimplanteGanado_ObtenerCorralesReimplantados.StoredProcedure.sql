USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCorralesReimplantados]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerCorralesReimplantados]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCorralesReimplantados]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/10/11
-- Description: SP para Obtener los lotes reimplantados al dia
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ObtenerCorralesReimplantados 1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerCorralesReimplantados]
	@OrganizacionID INT
AS
BEGIN
	
	/* Obtener los corrales reimplantados al dia */
	SELECT AM.LoteID, AM.CorralID, SUM(A.PesoCompra) AS PesoOrigen, SUM(AM.Peso) AS PesoReimplante, COUNT(1) TotalCabezas
	  FROM Animal A(NOLOCK)
	 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	 INNER JOIN Lote L ON L.LoteID = AM.LoteID
	 WHERE AM.Activo = 1
	   AND CONVERT(CHAR(8),AM.FechaMovimiento,112) BETWEEN CONVERT(CHAR(8),GETDATE()-3,112) AND CONVERT(CHAR(8),GETDATE(),112)
	   AND L.Activo = 1
	   AND L.CorralID = AM.CorralID
	   AND AM.TipoMovimientoID = 6
	   AND A.OrganizacionIDEntrada = @OrganizacionID
	   AND AM.CorralID IN (
			 SELECT DISTINCT CorralDestinoID
			   FROM ProgramacionReimplante PR
			  INNER JOIN ProgramacionReimplanteDetalle PRD ON PR.FolioProgramacionID = PRD.FolioProgramacionID
			  WHERE CONVERT(CHAR(8),PR.Fecha,112) BETWEEN CONVERT(CHAR(8),GETDATE()-2,112) AND CONVERT(CHAR(8),GETDATE()-1,112)
			     -- CONVERT(CHAR(8),PR.Fecha,112) = CONVERT(CHAR(8),GETDATE()-1,112)
				AND PR.OrganizacionID = @OrganizacionID
			)
	 GROUP BY AM.LoteID,AM.CorralID;
	 
END

GO

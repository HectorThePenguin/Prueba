USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenReimplante_ObtenerCorralesProgramar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenReimplante_ObtenerCorralesProgramar]
GO
/****** Object:  StoredProcedure [dbo].[OrdenReimplante_ObtenerCorralesProgramar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Leonel Ayala
-- Create date: 17/12/2013
-- Description:  Obtener listado de Corrales Cerrados para programacion de reimplante de ganado
-- OrdenReimplante_ObtenerCorralesProgramar
-- =============================================
CREATE PROCEDURE [dbo].[OrdenReimplante_ObtenerCorralesProgramar]
@OrganizacionID INT
AS
  BEGIN
      SET NOCOUNT ON;
		DECLARE @DIASRANGO INT;
		SET @DIASRANGO = 7;
		SELECT LP.loteId,
			   LT.OrganizacionId, 
			   LT.CorralId, 
			   LT.TipoProcesoID,
			   CR.Codigo, 
			   LR.FechaProyectada, 
			   LR.NumeroReimplante, 
			   LR.PesoProyectado,
			   LT.Cabezas AS CabezasRecibidas,
			  (SELECT TOP 1 TipoGanadoId 
				 FROM Animal A 
				INNER JOIN AnimalMovimiento AM ON AM.AnimalId = A.AnimalId 
				WHERE AM.OrganizacionId = LP.OrganizacionId
				  AND AM.CorralId = LT.CorralId 
				  AND AM.LoteId = LT.LoteId) AS TipoGanadoId
		 FROM LoteProyeccion (NOLOCK) LP
		INNER JOIN Lote (NOLOCK) LT ON (LP.LoteID = LT.LoteID AND LP.OrganizacionId = LT.OrganizacionId)
		INNER JOIN Corral (NOLOCK) CR ON (LT.CorralId = CR.CorralId AND LT.OrganizacionId = CR.OrganizacionId)
		INNER JOIN LoteReimplante (NOLOCK) LR ON LR.LoteProyeccionID = LP.LoteProyeccionID
		WHERE LT.FechaCierre IS NOT NULL AND LT.Activo = 1 --validar que el lote este cerrado, teniendo fecha de cierre y este activo
		  AND LT.OrganizacionID = @organizacionid
		  AND LR.FechaReal IS NULL
		  AND ( CONVERT(CHAR(8),LR.FechaProyectada,112) > CONVERT(CHAR(8),GETDATE(),112) AND 
		        CONVERT(CHAR(8),LR.FechaProyectada,112) <= CONVERT(CHAR(8),DATEADD(DAY, 7, GETDATE()),112))
		  AND NOT EXISTS(SELECT LoteId 
						   FROM ProgramacionReimplante 
						  WHERE LP.LoteId = LoteId 
						    AND OrganizacionID = LT.OrganizacionID
							AND Activo = 1)
		ORDER BY LR.FechaProyectada;
      SET NOCOUNT OFF;
  END

GO

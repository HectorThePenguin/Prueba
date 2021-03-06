USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerUltimaFechaLectorComedero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerUltimaFechaLectorComedero]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerUltimaFechaLectorComedero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jos� Gilberto Quintero L�pes 
-- Create date: 02-04-2014  
-- Description:  Obtiene la ultima fecha del historial de reparto
-- Reparto_ObtenerUltimaFechaLectorComedero 1
-- =============================================  
CREATE PROCEDURE [dbo].[Reparto_ObtenerUltimaFechaLectorComedero]
@OrganizacionID int
AS
BEGIN
	SELECT max(Fecha) as [Fecha] 
	FROM Reparto
	INNER JOIN dbo.RepartoDetalle rd ON rd.RepartoID = Reparto.RepartoID
	WHERE OrganizacionID = @OrganizacionID
		AND rd.Servido = 1
END

GO

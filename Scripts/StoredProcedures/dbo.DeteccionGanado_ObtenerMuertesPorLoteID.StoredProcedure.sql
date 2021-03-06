USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerMuertesPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ObtenerMuertesPorLoteID]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ObtenerMuertesPorLoteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 03/11/2014
-- Description:	Obtiene las muertes por lote
-- DeteccionGanado_ObtenerMuertesPorLoteID 1
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ObtenerMuertesPorLoteID] @LoteID INT	
AS
BEGIN
	SELECT MuerteID
		,Arete
		,AreteMetalico
		,Observaciones
		,LoteID
		,OperadorDeteccion
		,FechaDeteccion
		,FotoDeteccion
		,OperadorRecoleccion
		,FechaRecoleccion
		,OperadorNecropsia
		,FechaNecropsia
		,FotoNecropsia
		,FolioSalida
		,OperadorCancelacion
		,FechaCancelacion
		,MotivoCancelacion
		,EstatusID
		,ProblemaID
		,Comentarios
	FROM Muertes
	where LoteID = @LoteID
	and CAST(FechaDeteccion AS DATE) = CAST(GETDATE() AS DATE)
END

GO

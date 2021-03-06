USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_DecrementarAnimalMuerto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_DecrementarAnimalMuerto]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_DecrementarAnimalMuerto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/05/21
-- Description: SP para Inactivar un registro en la tabla de Animal
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_DecrementarAnimalMuerto 1
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_DecrementarAnimalMuerto]
	@AnimalID BIGINT
AS
BEGIN
	--Se disminuyen las cabezas del lote en todas las ordenes de sacrificio
	UPDATE OSD
	   SET CabezasLote = CabezasLote - 1
	  FROM OrdenSacrificioDetalle (NOLOCK) OSD
	 INNER JOIN AnimalSalida (NOLOCK) ASA ON (OSD.OrdenSacrificioID = ASA.OrdenSacrificioID AND OSD.LoteID = ASA.LoteID)
	 WHERE ASA.AnimalID = @AnimalID
	--Se disminuyen las cabezas de sacrificio de la orden con el mayor numero de cabezas de sacrificio
	UPDATE OSD
	   SET CabezasSacrificio = CabezasSacrificio - 1
	  FROM OrdenSacrificioDetalle(NOLOCK) OSD
	 /*INNER JOIN AnimalSalida (NOLOCK) ASA ON (OSD.OrdenSacrificioID = ASA.OrdenSacrificioID AND OSD.LoteID = ASA.LoteID)*/
	WHERE OSD.OrdenSacrificioDetalleID = (	SELECT TOP 1 OSD.OrdenSacrificioDetalleID 
									  FROM OrdenSacrificioDetalle (NOLOCK) OSD 
									 INNER JOIN AnimalSalida (NOLOCK) ASA ON (OSD.OrdenSacrificioID = ASA.OrdenSacrificioID AND OSD.LoteID = ASA.LoteID)
									 WHERE ASA.AnimalID = @AnimalID 
									 ORDER BY OSD.CabezasSacrificio DESC)
	/*
	--Se disminuyen las cabezas del lote
	UPDATE L
	   SET L.Cabezas = L.Cabezas - 1,
	       L.FechaSalida = GETDATE()
	  FROM Lote (NOLOCK) L
	 INNER JOIN OrdenSacrificioDetalle (NOLOCK) OSD	ON (L.LoteID = OSD.LoteID)
	 INNER JOIN AnimalSalida (NOLOCK) ASA ON (OSD.OrdenSacrificioID = ASA.OrdenSacrificioID AND OSD.LoteID = ASA.LoteID)
	 WHERE ASA.AnimalID = @AnimalId*/
END

GO

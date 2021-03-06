USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListGanadoMuerto_ObtenerAretesMuertosRecoleccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListGanadoMuerto_ObtenerAretesMuertosRecoleccion]
GO
/****** Object:  StoredProcedure [dbo].[CheckListGanadoMuerto_ObtenerAretesMuertosRecoleccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 19/02/2013
-- Description: Obtiene la lista de cabezas muertas para recoleccion de ganado muerto
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[CheckListGanadoMuerto_ObtenerAretesMuertosRecoleccion]
 @OrganizacionId int
AS
BEGIN
SELECT MU.MuerteId, MU.Arete, ISNULL(MU.AreteMetalico, '') as AreteMetalico, ISNULL(MU.Observaciones, '') as Observaciones, MU.LoteId, MU.OperadorDeteccion, 
			ISNULL(MU.FechaDeteccion, '1900-01-01 00:00:00)') as FechaDeteccion,
			MU.FotoDeteccion,
			MU.EstatusID, ISNULL(MU.ProblemaID, 0) as ProblemaID, MU.FechaCreacion,
			LT.Lote, LT.OrganizacionId, CR.Codigo, CR.CorralId, MU.Activo, ISNULL(MU.Comentarios, '') as Comentarios
		FROM Muertes MU
		INNER JOIN Lote LT on MU.LoteId = LT.LoteID
		INNER JOIN Corral CR on LT.CorralID = CR.CorralID
		where LT.OrganizacionID = @OrganizacionId
		and MU.Activo = 1
		and MU.EstatusID = 4
END

GO

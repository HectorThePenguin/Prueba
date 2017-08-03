USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertosParaRecepcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertosParaRecepcion]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertosParaRecepcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 17/02/2013
-- Description: Obtiene la lista de cabezas muertas para recepcion en necropsia
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertosParaRecepcion]
 @OrganizacionId int
AS
BEGIN
select MU.MuerteId, MU.Arete, 
	ISNULL(MU.AreteMetalico, '') as AreteMetalico, ISNULL(MU.Observaciones, '') AS Observaciones, MU.LoteId, MU.OperadorDeteccion, MU.FechaDeteccion, MU.FotoDeteccion,
			MU.OperadorRecoleccion, MU.FechaRecoleccion, MU.OperadorNecropsia, MU.FechaNecropsia, MU.EstatusID, 
			ISNULL(MU.ProblemaID, 0) AS ProblemaID, MU.FechaCreacion,
		LT.Lote, LT.OrganizacionId, CR.Codigo, MU.Activo
from muertes MU
INNER JOIN Lote LT on MU.LoteId = LT.LoteID
INNER JOIN Corral CR on LT.CorralID = CR.CorralID
where LT.OrganizacionID = @OrganizacionId
and MU.Activo = 1
and EstatusID = 5
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Muerte_ObtenerMuertePorArete]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Muerte_ObtenerMuertePorArete]
GO
/****** Object:  StoredProcedure [dbo].[Muerte_ObtenerMuertePorArete]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 12/02/2013
-- Description: Obtiene la informacion de un arete muerto
--	Muerte_ObtenerMuertePorArete 1, '348583'
-- =============================================
CREATE PROCEDURE [dbo].[Muerte_ObtenerMuertePorArete] @OrganizacionId INT
	,@Arete VARCHAR(15)
AS
BEGIN
	SELECT MU.MuerteId
		,MU.Arete
		,MU.AreteMetalico
		,ISNULL(MU.Observaciones, '') AS Observaciones
		,MU.LoteId
		,MU.OperadorDeteccion
		,MU.FechaDeteccion
		,MU.FotoDeteccion
		,MU.OperadorRecoleccion
		,MU.FechaRecoleccion
		,MU.OperadorNecropsia
		,MU.FechaNecropsia
		,MU.EstatusID
		,MU.ProblemaID
		,MU.FechaCreacion
		,LT.Lote
		,LT.OrganizacionId
		,CR.Codigo
		,CR.CorralId
		,MU.Activo
	FROM muertes MU	
	INNER JOIN Lote LT ON MU.LoteId = LT.LoteID
	INNER JOIN Corral CR ON LT.CorralID = CR.CorralID
	WHERE MU.Arete = @Arete
		AND LT.OrganizacionID = @OrganizacionId
		AND MU.Activo = 1
END

GO

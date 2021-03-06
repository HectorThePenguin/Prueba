USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertePorArete]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertePorArete]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertePorArete]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Andres Vejar
-- Create date: 12/02/2013
-- Description: Obtiene la informacion de un arete muerto
-- Empresa: Apinterfaces 
-- SalidaPorMuerte_ObtenerMuertePorArete 4,'2255230'
-- =============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertePorArete] @OrganizacionId INT
	,@Arete VARCHAR(16)
AS
BEGIN
	DECLARE @animalid BIGINT
	DECLARE @peso INT

	SET @animalid = (
			SELECT TOP 1 animalId
			FROM Animal(NOLOCK)
			WHERE Arete = @Arete /*OR AreteMetalico = @Arete*/
			and OrganizacionIDEntrada = @OrganizacionId
			)
	SET @peso = (
			SELECT TOP 1 peso
			FROM AnimalMovimiento(NOLOCK)
			WHERE AnimalId = @animalid
			ORDER BY AnimalMovimientoId DESC
			)

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
		,COALESCE(A.AnimalID, 0) AS AnimalID
		,@peso AS Peso
	FROM muertes MU
	LEFT JOIN Animal A(NOLOCK) ON (
			(A.Arete = MU.Arete AND OrganizacionIDEntrada = @OrganizacionId)
			OR Mu.Arete = ''
			)
		AND (
			(A.AreteMetalico = MU.AreteMetalico AND OrganizacionIDEntrada = @OrganizacionId)
			OR MU.AreteMetalico = ''
			)
	INNER JOIN Lote LT ON MU.LoteId = LT.LoteID
	INNER JOIN Corral CR ON LT.CorralID = CR.CorralID
	WHERE (MU.Arete = @Arete /*OR MU.AreteMetalico = @Arete*/)
		AND LT.OrganizacionID = @OrganizacionId
		AND MU.Activo = 1
		AND MU.EstatusID = 8
END

GO

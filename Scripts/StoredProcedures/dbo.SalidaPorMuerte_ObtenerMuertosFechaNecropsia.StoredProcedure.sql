USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertosFechaNecropsia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertosFechaNecropsia]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertosFechaNecropsia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 2014/08/01
-- Description: Obtiene las muertes por fecha de Necropsia
-- EXEC SalidaPorMuerte_ObtenerMuertosFechaNecropsia 1,'2014-08-02'
--=============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertosFechaNecropsia] @OrganizacionID INT
	,@FechaNecropsia DATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @MuertesSiniestro INT
	SET @MuertesSiniestro = 26
	SELECT MuerteID
		,Corral
		,Arete
		,AreteTestigo
		,Sexo
		,TipoGanado
		,ISNULL(Peso, 0) AS Peso
		,Causa
		,FolioSalida
	INTO #MuertesTmp
	FROM (
		SELECT M.MuerteID
			,C.Codigo AS Corral
			,A.Arete
			,A.AreteMetalico AS AreteTestigo
			,CASE TG.Sexo
				WHEN 'H'
					THEN 'Hembra'
				WHEN 'M'
					THEN 'Macho'
				END AS Sexo
			,TG.Descripcion AS TipoGanado
			,dbo.ObtenerPesoProyectado(@OrganizacionID,L.LoteID,L.Cabezas) AS Peso--AM.Peso
			,P.Descripcion AS Causa
			,ISNULL(M.FolioSalida, 0) AS FolioSalida
		FROM Muertes(NOLOCK) M
		INNER JOIN Lote(NOLOCK) L ON (M.LoteID = L.LoteID)
		INNER JOIN Corral(NOLOCK) C ON (L.CorralID = C.CorralID)
		INNER JOIN Animal(NOLOCK) A ON (
				M.Arete = A.Arete
				OR M.AreteMetalico = A.Arete
				)
		INNER JOIN AnimalMovimiento(NOLOCK) AM ON (
				A.AnimalID = AM.AnimalID
				AND AM.TipoMovimientoID = 8
				) -- Muerte
		INNER JOIN TipoGanado(NOLOCK) TG ON (A.TipoGanadoID = TG.TipoGanadoID)
		INNER JOIN Problema(NOLOCK) P ON (M.ProblemaID = P.ProblemaID)
		WHERE L.OrganizacionID = @OrganizacionID
			AND CAST(M.FechaNecropsia AS DATE) = CAST(@FechaNecropsia AS DATE)
			AND AM.Activo = 1
			AND A.Activo = 0
			AND M.ProblemaID <> @MuertesSiniestro
		UNION
		SELECT M.MuerteID
			,C.Codigo AS Corral
			,AH.Arete
			,AH.AreteMetalico AS AreteTestigo
			,CASE TG.Sexo
				WHEN 'H'
					THEN 'Hembra'
				WHEN 'M'
					THEN 'Macho'
				END AS Sexo
			,TG.Descripcion AS TipoGanado
			,AM.Peso
			,P.Descripcion AS Causa
			,ISNULL(M.FolioSalida, 0) AS FolioSalida
		FROM Muertes(NOLOCK) M
		INNER JOIN Lote(NOLOCK) L ON (M.LoteID = L.LoteID)
		INNER JOIN Corral(NOLOCK) C ON (L.CorralID = C.CorralID)
		INNER JOIN AnimalHistorico(NOLOCK) AH ON (
				M.Arete = AH.Arete
				OR M.AreteMetalico = AH.Arete
				)
		INNER JOIN AnimalMovimientoHistorico(NOLOCK) AM ON (
				AH.AnimalID = AM.AnimalID
				AND AM.TipoMovimientoID = 8
				) -- Muerte
		INNER JOIN TipoGanado(NOLOCK) TG ON (AH.TipoGanadoID = TG.TipoGanadoID)
		INNER JOIN Problema(NOLOCK) P ON (M.ProblemaID = P.ProblemaID)
		WHERE L.OrganizacionID = @OrganizacionID
			AND CAST(M.FechaNecropsia AS DATE) = CAST(@FechaNecropsia AS DATE)
			AND AM.Activo = 1
			AND AH.Activo = 0
			AND M.ProblemaID <> @MuertesSiniestro
		) AS Tmp
	SELECT #MuertesTmp.*
	FROM #MuertesTmp
	UNION
	SELECT M.MuerteID
		,C.Codigo AS Corral
		,M.Arete
		,M.AreteMetalico AS AreteTestigo
		,CASE dbo.ObtenerSexoPrimeroEnLote(L.LoteID)
			WHEN 'H'
				THEN 'Hembra'
			ELSE 'Macho'
			END AS Sexo
		,dbo.ObtenerTipoGanadoPrimeroEnLote(L.LoteID) AS TipoGanado
		,ISNULL(dbo.ObtenerPesoPromedioLote(M.LoteID), 0) AS Peso
		,P.Descripcion AS Causa
		,ISNULL(M.FolioSalida, 0) AS FolioSalida
	FROM Muertes(NOLOCK) M
	INNER JOIN Lote(NOLOCK) L ON (M.LoteID = L.LoteID)
	INNER JOIN Corral(NOLOCK) C ON (L.CorralID = C.CorralID)
	INNER JOIN Problema(NOLOCK) P ON (M.ProblemaID = P.ProblemaID)
	WHERE L.OrganizacionID = @OrganizacionID
		AND CAST(M.FechaNecropsia AS DATE) = CAST(@FechaNecropsia AS DATE)
		AND M.Arete NOT IN (
			SELECT Arete
			FROM #MuertesTmp
			)
		AND M.ProblemaID <> @MuertesSiniestro
	DROP TABLE #MuertesTmp
	SET NOCOUNT OFF;
END

GO

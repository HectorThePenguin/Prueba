USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_ObtenerPorAnimalXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalCosto_ObtenerPorAnimalXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_ObtenerPorAnimalXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene el ultimo movimiento de los animales
-- AnimalCosto_ObtenerPorAnimalXML '<ROOT><Animales><AnimalID>76728</AnimalID></Animales></ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[AnimalCosto_ObtenerPorAnimalXML]
@XmlAnimales XML
AS
BEGIN

	SET NOCOUNT ON

		DECLARE @tAnimales TABLE
		(
			AnimalID BIGINT
			, OrganizacionID INT
		)

		INSERT INTO @tAnimales
		SELECT AnimalID = T.item.value('./AnimalID[1]', 'BIGINT')
			,  OrganizacionID = T.item.value('./OrganizacionID[1]', 'INT')
		FROM  @XmlAnimales.nodes('ROOT/Animales') AS T(item)

		SELECT DISTINCT A.AnimalID
			,  A.CostoID
			,  A.Importe
			,  A.TipoReferencia
			,  A.FolioReferencia
			,  A.FechaCosto
			,  A.OrganizacionID
			,  A.Arete
		FROM (
		SELECT AC.AnimalID
			,  AC.CostoID
			,  AC.Importe
			,  AC.TipoReferencia
			,  AC.FolioReferencia
			,  AC.FechaCosto
			,  A.OrganizacionID
			,  An.Arete
		FROM AnimalCosto AC(NOLOCK)
		INNER JOIN @tAnimales A
			ON (AC.AnimalID = A.AnimalID)
			INNER JOIN Animal An
			ON (A.AnimalID = An.AnimalID)
		UNION
		SELECT AC.AnimalID
			,  AC.CostoID
			,  AC.Importe
			,  AC.TipoReferencia
			,  AC.FolioReferencia
			,  AC.FechaCosto
			,  A.OrganizacionID
			,  An.Arete
		FROM AnimalCostoHistorico AC(NOLOCK)
		INNER JOIN @tAnimales A
			ON (AC.AnimalID = A.AnimalID)
			INNER JOIN AnimalHistorico An
			ON (A.AnimalID = An.AnimalID)
		) A

	SET NOCOUNT OFF

END

GO

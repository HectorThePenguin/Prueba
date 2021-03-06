USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerUltimoMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerUltimoMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerUltimoMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene el ultimo movimiento de los animales
-- Animal_ObtenerUltimoMovimiento '1957911', 1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerUltimoMovimiento]
@Arete  VARCHAR(50)
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #tAnimal
		(
			AnimalID		BIGINT
			, Arete			VARCHAR(20)
		)

		DECLARE @Muerte INT
		SET @Muerte = 8

		INSERT INTO #tAnimal
		(
			AnimalID
			, Arete
		)
		SELECT AnimalID
			,  Arete
		FROM Animal(NOLOCK)
		WHERE Arete = @Arete
		UNION
		SELECT AnimalID
			,  Arete
		FROM AnimalHistorico(NOLOCK)
		WHERE Arete = @Arete

		SELECT tA.AnimalID
			,  tA.Arete
		FROM
		(
			SELECT MAX(AM.FechaMovimiento) AS FechaMovimiento
				,  AM.AnimalID				AS AnimalID
				,  AM.OrganizacionID
				,  MAX(AM.TipoMovimientoID) AS TipoMovimientoID
			FROM AnimalMovimiento AM(NOLOCK)
			WHERE AM.TipoMovimientoID = @Muerte
				AND AM.OrganizacionID = @OrganizacionID
			GROUP BY AM.AnimalID
				,	 AM.OrganizacionID
			UNION 
			SELECT MAX(AM.FechaMovimiento) AS FechaMovimiento
				,  AM.AnimalID				AS AnimalID
				,  AM.OrganizacionID
				,  MAX(AM.TipoMovimientoID) AS TipoMovimientoID
			FROM AnimalMovimientoHistorico AM(NOLOCK)
			WHERE AM.TipoMovimientoID = @Muerte
				AND AM.OrganizacionID = @OrganizacionID
			GROUP BY AM.AnimalID
				,	 AM.OrganizacionID
		) A 
		INNER JOIN #tAnimal tA
			ON (A.AnimalID = tA.AnimalID)		

		DROP TABLE #tAnimal

	SET NOCOUNT OFF

END

GO

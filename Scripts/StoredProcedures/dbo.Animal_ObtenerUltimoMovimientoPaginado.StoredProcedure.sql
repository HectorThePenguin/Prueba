USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerUltimoMovimientoPaginado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerUltimoMovimientoPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerUltimoMovimientoPaginado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 07/07/2014
-- Description:  Obtiene el ultimo movimiento de los animales
-- Animal_ObtenerUltimoMovimientoPaginado '', 1, 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerUltimoMovimientoPaginado]
@Arete  VARCHAR(50)
, @OrganizacionID INT
, @Inicio INT
, @Limite INT 
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #tAnimal
		(
			AnimalID		BIGINT
		)

		DECLARE @Muerte INT
		SET @Muerte = 8

		INSERT INTO #tAnimal
		(
			AnimalID
		)
		SELECT A.AnimalID
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

		SELECT ROW_NUMBER() OVER (ORDER BY Arete ASC) AS [RowNum]
				,  AnimalID
				,  Arete
				,  AreteMetalico
		INTO #Animales
		FROM
		(
			SELECT A.AnimalID
				,  A.Arete
				,  A.AreteMetalico
			FROM Animal A(NOLOCK)
			INNER JOIN #tAnimal tA
				ON (A.AnimalID = tA.AnimalID)
			UNION
			SELECT A.AnimalID
				,  A.Arete
				,  A.AreteMetalico
			FROM AnimalHistorico A(NOLOCK)
					INNER JOIN #tAnimal tA
				ON (A.AnimalID = tA.AnimalID)
		) A
		WHERE @Arete = '' OR A.Arete LIKE '%' + @Arete + '%'

		SELECT
			AnimalID
			,  Arete
			,  AreteMetalico
			,  @OrganizacionID AS OrganizacionID
		FROM #Animales a
		WHERE RowNum BETWEEN @Inicio AND @Limite

		SELECT
		COUNT(AnimalID) AS [TotalReg]
		FROM #Animales

		DROP TABLE #Animales
		DROP TABLE #tAnimal

	SET NOCOUNT OFF

END

GO

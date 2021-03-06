USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorFolio]
	@TipoOrganizacionID INT,
	@OrganizacionID INT,
	@Descripcion NVARCHAR(50),	
	@EmbarqueID INT,
	@Inicio INT, 
	@Limite INT
/* 
=============================================
-- Author: Gilberto Carranza
-- Create date: 23-10-2013
-- Description:	Otiene un listado de organizaciones por Folio de Entrada
-- Organizacion_ObtenerPorFolio 1, 0, '', 2 , 1, 10
=============================================
*/
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #Organizacion
	(
		RowNum INT
		, OrganizacionID INT
		, Descripcion VARCHAR(250)
		, Activo BIT
	)
	IF (@EmbarqueID = 0)
	BEGIN
		INSERT INTO #Organizacion
		SELECT 
			ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,
			O.OrganizacionID,
			O.Descripcion,
			O.Activo				
			FROM Organizacion O
			LEFT OUTER JOIN EntradaGanado EG
				ON (O.OrganizacionID = EG.OrganizacionID)
			WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
			  AND O.Activo = 1
			  AND @TipoOrganizacionID IN (O.TipoOrganizacionID, 0)
			  AND @OrganizacionID IN (O.OrganizacionID, 0)		  
			GROUP BY O.OrganizacionID,
					O.Descripcion,
					O.Activo
	END
	ELSE
	BEGIN
		INSERT INTO #Organizacion
		SELECT ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,
				O.OrganizacionID,
				O.Descripcion,
				O.Activo				
		FROM Organizacion O
		INNER JOIN EmbarqueDetalle PED
			ON (O.OrganizacionID = PED.OrganizacionOrigenID
				AND PED.EmbarqueID = @EmbarqueID)
		WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
			AND O.Activo = 1
			AND @TipoOrganizacionID IN (O.TipoOrganizacionID, 0)
			AND @OrganizacionID IN (O.OrganizacionID, 0)		  
		GROUP BY O.OrganizacionID,
				O.Descripcion,
				O.Activo
	END
	SELECT
		OrganizacionID, 
		Descripcion,		
		Activo
	FROM    #Organizacion	
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(OrganizacionID)AS TotalReg 
	FROM #Organizacion	
	SET NOCOUNT OFF;
END

GO

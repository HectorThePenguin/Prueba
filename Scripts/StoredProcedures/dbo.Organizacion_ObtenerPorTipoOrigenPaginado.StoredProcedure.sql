USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorTipoOrigenPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorTipoOrigenPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorTipoOrigenPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorTipoOrigenPaginado]
	@TipoOrganizacionID INT,
	@OrganizacionID INT,
	@EmbarqueID INT,
	@Descripcion NVARCHAR(50),
	@Inicio INT, 
	@Limite INT
/* 
=============================================
-- Author: Gilberto Carranza
-- Create date: 26-11-2013
Organizacion_ObtenerPorEmbarqueTipoOrigen
-- Description:	Otiene un listado de organizaciones por Folio de Entrada
-- Organizacion_ObtenerPorTipoOrigenPaginado 0, 0, 0,'',1,10
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
		,TipoOrganizacionID INT
		,TipoOrganizacion varchar(50)
		, Activo BIT
	)
	INSERT INTO #Organizacion
	SELECT ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,
			O.OrganizacionID,
			O.Descripcion,
			too.TipoOrganizacionID,
			too.Descripcion AS TipoOrganizacion,
			O.Activo				
	FROM Organizacion O
	INNER JOIN TipoOrganizacion too ON O.TipoOrganizacionID = too.TipoOrganizacionID
	LEFT JOIN EmbarqueDetalle PED
		ON (O.OrganizacionID = PED.OrganizacionOrigenID
			AND PED.EmbarqueID = @EmbarqueID)
	WHERE O.Activo = 1
		AND @TipoOrganizacionID IN (O.TipoOrganizacionID, 0)
		AND @OrganizacionID IN (O.OrganizacionID, 0)		
		AND (
			o.Descripcion LIKE '%' + @Descripcion + '%'
			OR @Descripcion = ''
			)  
	GROUP BY O.OrganizacionID,
			O.Descripcion,
			O.Activo,
			too.TipoOrganizacionID,
			too.Descripcion
	SELECT
		OrganizacionID, 
		Descripcion,	
		TipoOrganizacionID,
		TipoOrganizacion,	
		Activo
	FROM #Organizacion
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(OrganizacionID)AS TotalReg 
	FROM #Organizacion	
	DROP TABLE #Organizacion
	SET NOCOUNT OFF;
END

GO

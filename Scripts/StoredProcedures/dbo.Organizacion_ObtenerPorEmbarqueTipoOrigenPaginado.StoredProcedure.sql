USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author: Gilberto Carranza
-- Create date: 26-11-2013
-- Description:	Otiene un listado de organizaciones por Folio de Entrada
-- Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado 1, 0, 2,1,10
--=============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigenPaginado]
	@TipoOrganizacionID INT,
	@OrganizacionID INT,
	@EmbarqueID INT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #Organizacion
	(
		RowNum INT
		, OrganizacionID INT
		, Descripcion VARCHAR(250)
		, Activo BIT
		, Direccion VARCHAR(250)
	)
	INSERT INTO #Organizacion
	SELECT ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,
			O.OrganizacionID,
			O.Descripcion,
			O.Activo,
			O.Direccion	
	FROM Organizacion O
	INNER JOIN EmbarqueDetalle PED
		ON (O.OrganizacionID = PED.OrganizacionOrigenID
			AND PED.EmbarqueID = @EmbarqueID
			AND PED.Activo = 1
			AND PED.Recibido = 0)
	WHERE O.Activo = 1
		AND @TipoOrganizacionID IN (O.TipoOrganizacionID, 0)
		AND @OrganizacionID IN (O.OrganizacionID, 0)		  
	GROUP BY O.OrganizacionID,
			O.Descripcion,
			O.Activo,
			O.Direccion	
	SELECT
		OrganizacionID, 
		Descripcion,		
		Activo,
		Direccion
	FROM #Organizacion
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(OrganizacionID)AS TotalReg 
	FROM #Organizacion	
	DROP TABLE #Organizacion
	SET NOCOUNT OFF;
END

GO

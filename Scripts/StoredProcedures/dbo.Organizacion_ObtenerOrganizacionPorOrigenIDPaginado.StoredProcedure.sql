USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerOrganizacionPorOrigenIDPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerOrganizacionPorOrigenIDPaginado]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerOrganizacionPorOrigenIDPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Organizacion_ObtenerOrganizacionPorOrigenIDPaginado] @OrganizacionOrigenID INT
	,@OrganizacionID INT
	,@Descripcion VARCHAR(50)
	,@Inicio INT
	,@Limite INT
	/* 
=============================================
-- Author: Jorge Velazquez
-- Create date: 26-11-2013
-- Description:	Otiene un listado de organizaciones por Origen Entrada ID
-- Organizacion_ObtenerOrganizacionPorOrigenIDPaginado 3,0,'',1,10
=============================================
*/
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #Organizacion (
		RowNum INT
		,OrganizacionID INT
		,Descripcion VARCHAR(250)
		,Activo BIT
		)
	INSERT INTO #Organizacion
	SELECT ROW_NUMBER() OVER (
			ORDER BY O.descripcion ASC
			) AS RowNum
		,O.OrganizacionID
		,O.Descripcion
		,O.Activo
	FROM Organizacion O
	INNER JOIN ConfiguracionEmbarque ce ON O.OrganizacionID = ce.OrganizacionDestinoID
	WHERE O.Activo = 1
		AND ce.OrganizacionOrigenID = @OrganizacionOrigenID
		AND (
			Descripcion LIKE '%' + @Descripcion + '%'
			OR @Descripcion = ''
			)
		AND @OrganizacionID IN (O.OrganizacionID ,0)
	GROUP BY O.OrganizacionID
		,O.Descripcion
		,O.Activo
	SELECT OrganizacionID
		,Descripcion
		,Activo
	FROM #Organizacion
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(OrganizacionID) AS TotalReg
	FROM #Organizacion
	DROP TABLE #Organizacion
	SET NOCOUNT OFF;
END

GO

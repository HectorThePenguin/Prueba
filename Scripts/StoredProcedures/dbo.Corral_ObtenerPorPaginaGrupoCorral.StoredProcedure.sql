USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPaginaGrupoCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorPaginaGrupoCorral]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPaginaGrupoCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Gilberto Carranza
-- Create date: 07/04/2014
-- Description:	Obtener listado de Corrales.
-- Corral_ObtenerPorPaginaGrupoCorral '', 0,0,1 , 1, 10 
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorPaginaGrupoCorral]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@GrupoCorralID INT,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY c.Codigo ASC) AS RowNum,
		C.CorralID
		,C.OrganizacionID
		,C.Codigo
		,C.TipoCorralID
		,C.Capacidad
		,C.MetrosLargo
		,C.MetrosAncho
		,C.Seccion
		,C.Orden
		,C.Activo
		, TC.Descripcion AS TipoCorral
		, GC.Descripcion AS GrupoCorral
		, GC.GrupoCorralID
		INTO #Corral
		FROM Corral C
		INNER JOIN TipoCorral TC
			ON (C.TipoCorralID = TC.TipoCorralID)
		INNER JOIN GrupoCorral GC
			ON (TC.GrupoCorralID = GC.GrupoCorralID
				AND @GrupoCorralID IN (GC.GrupoCorralID, 0))
		WHERE (c.Codigo LIKE '%' + @Codigo + '%' OR @Codigo = '')
			AND @OrganizacionID in (0, OrganizacionID)			
			AND c.Activo = @Activo
	SELECT 
		c.CorralID,
		c.OrganizacionID,
		c.Codigo,
		c.TipoCorralID,
		TipoCorral,
		c.Capacidad,
		c.MetrosLargo,
		c.MetrosAncho,
		c.Seccion,
		c.Orden,
		c.Activo,
		TipoCorral
		, GrupoCorral
		, GrupoCorralID
	FROM  #Corral c	
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(CorralID) AS TotalReg 
	FROM #Corral
	DROP TABLE #Corral
END

GO

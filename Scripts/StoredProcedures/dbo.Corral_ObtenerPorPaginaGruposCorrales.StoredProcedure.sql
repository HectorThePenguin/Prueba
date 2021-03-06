USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPaginaGruposCorrales]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorPaginaGruposCorrales]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPaginaGruposCorrales]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Andres Vejar
-- Create date: 16/07/2014
-- Description:	Obtener listado de Corrales con lote activo de una organizacion pertenecientes a uno o mas grupo de corral
-- Corral_ObtenerPorPaginaGruposCorrales '', 0,0,1 , 1, 10 
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorPaginaGruposCorrales]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@XmlCodigosCorral XML,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #GRUPOSCORRAL
	(
		GrupoCorralID int
	)	
	INSERT #GRUPOSCORRAL (GrupoCorralID)
	SELECT grupo = t.item.value('./GrupoCorral[1]', 'int')
	FROM @XmlCodigosCorral.nodes('ROOT/Grupos') AS t(item)
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY c.Codigo ASC) AS RowNum, 
	C.CorralID, C.OrganizacionID, C.Codigo, GC.GrupoCorralID, C.TipoCorralID, C.Capacidad, C.MetrosLargo,
	C.MetrosAncho, C.Seccion, C.Orden, C.Activo
	INTO #Corral
	from Corral (NOLOCK) AS C 
	INNER JOIN Lote (NOLOCK) AS L ON (L.CorralID = C.CorralID)
	INNER JOIN TipoCorral (NOLOCK) AS TC ON (C.TipoCorralID = TC.TipoCorralID)
	INNER JOIN GrupoCorral (NOLOCK) AS GC ON (TC.GrupoCorralID = GC.GrupoCorralID)
	WHERE
	C.OrganizacionID = @OrganizacionID
	AND (c.Codigo LIKE '%' + @Codigo + '%' OR @Codigo = '')
	AND  GC.GrupoCorralID IN (SELECT GrupoCorralID from #GRUPOSCORRAL) 
	AND TC.Activo = 1 AND GC.Activo = 1 AND L.Activo = 1 AND C.Activo = 1
	SELECT 
	CX.CorralID, CX.OrganizacionID, CX.Codigo, CX.GrupoCorralID, CX.TipoCorralID, CX.Capacidad, CX.MetrosLargo,
	CX.MetrosAncho,CX.Seccion, CX.Orden, CX.Activo
	FROM  #Corral CX
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(CorralID) AS TotalReg 
	FROM #Corral
	DROP TABLE #Corral
END

GO

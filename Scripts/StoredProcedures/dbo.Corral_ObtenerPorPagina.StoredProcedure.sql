USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Gilberto Carranza
-- Create date: 24/10/2013
-- Description:	Obtener listado de Corrales.
-- Corral_ObtenerPorPagina '', 0,0,1 , 1, 10 
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorPagina]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@TipoCorralID INT,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY c.Codigo ASC) AS RowNum,
		CorralID
		,OrganizacionID
		,Codigo
		,TipoCorralID
		,Capacidad
		,MetrosLargo
		,MetrosAncho
		,Seccion
		,Orden
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		,FechaModificacion
		,UsuarioModificacionID
		INTO #Corral
		FROM Corral C(NOLOCK)
		WHERE (c.Codigo LIKE '%' + @Codigo + '%' OR @Codigo = '')
			AND @OrganizacionID in (0, OrganizacionID)
			AND @TipoCorralID in (0, TipoCorralID)
			AND c.Activo = @Activo
	SELECT 
		c.CorralID,
		c.OrganizacionID,
		o.Descripcion as [Organizacion],
		c.Codigo,
		c.TipoCorralID,
		t.Descripcion as [TipoCorral],
		c.Capacidad,
		c.MetrosLargo,
		c.MetrosAncho,
		c.Seccion,
		c.Orden,
		c.Activo,
		c.FechaCreacion,
		c.UsuarioCreacionID,
		c.FechaModificacion,
		c.UsuarioModificacionID
	FROM  #Corral c
	INNER JOIN Organizacion o on o.OrganizacionID = c.OrganizacionId
	INNER JOIN TipoCorral t on t.TipoCorralID = c.TipoCorralID
	WHERE   RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(CorralID) AS TotalReg 
	FROM #Corral
	DROP TABLE #Corral
END

GO

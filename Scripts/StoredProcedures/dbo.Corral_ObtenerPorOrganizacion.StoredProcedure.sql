USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Gilberto Carranza
-- Create date: 24/10/2013
-- Description:	Obtener listado de Corrales Por Organizacion.
-- Corral_ObtenerPorOrganizacion '', 4, 1, 1, 15, 0
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorOrganizacion]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT, 
	@TipoCorralID INT
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
		,C.FechaCreacion
		,C.UsuarioCreacionID
		,C.FechaModificacion
		,C.UsuarioModificacionID
		INTO #Corral
		FROM Corral C(NOLOCK)
		INNER JOIN Organizacion O(NOLOCK)
			ON (C.OrganizacionID = O.OrganizacionID)
		WHERE (c.Codigo LIKE '%' + @Codigo + '%' OR @Codigo = '')
		  AND C.OrganizacionID =  @OrganizacionID
		  AND c.Activo = @Activo
		  AND @TipoCorralID IN (TipoCorralID, 0)
	SELECT 
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
	FROM  #Corral
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(CorralID) AS TotalReg 
	FROM #Corral
	DROP TABLE #Corral
END

GO

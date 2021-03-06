USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_FormulaObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_FormulaObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Corral_FormulaObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Edgar Villarreal
-- Create date: 24/10/2013
-- Description:	Obtener listado de Corrales Por Organizacion.
-- Corral_FormulaObtenerPorOrganizacion '', 1, 1, 1, 15, 2
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_FormulaObtenerPorOrganizacion]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT, 
	@FormulaID INT
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
		INNER JOIN Organizacion O(NOLOCK) ON (C.OrganizacionID = O.OrganizacionID)
		INNER JOIN Lote as l on l.CorralID = c.CorralID AND l.Activo = 1
		INNER JOIN Reparto r on r.CorralID = c.CorralID AND r.Activo = 1
		LEFT JOIN RepartoDetalle rd on rd.RepartoID = r.RepartoID
		WHERE (c.Codigo LIKE '%' + @Codigo + '%' OR @Codigo = '')
		  AND C.OrganizacionID =  @OrganizacionID
		  AND c.Activo = @Activo
      AND rd.FormulaIDServida = @FormulaID 
			AND CAST (r.Fecha as date) = cast(getdate() as date)
	GROUP BY C.CorralID,C.OrganizacionID
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

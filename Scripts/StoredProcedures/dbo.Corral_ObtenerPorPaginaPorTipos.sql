USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPaginaPorTipos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorPaginaPorTipos]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorPaginaPorTipos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Edgar Villarreal
-- Create date: 01/12/2015
-- Description:	Obtener listado de Corrales por tipos.
-- Corral_ObtenerPorPaginaPorTipos '30',1,
'<ROOT>
 		<TiposCorral><TipoCorralID>4</TipoCorralID></TiposCorral>
 		<TiposCorral><TipoCorralID>6</TipoCorralID></TiposCorral>
 </ROOT>',1,1,100
=============================================
*/
CREATE PROCEDURE [dbo].[Corral_ObtenerPorPaginaPorTipos]
	@Codigo NVARCHAR(10),
	@OrganizacionID INT,
	@XmlTiposCorral xml,
	@Activo BIT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TiposCorral AS TABLE ([TipoCorralID] INT)
	INSERT @TiposCorral ([TipoCorralID])
	SELECT [TipoCorralID] = t.item.value('./TipoCorralID[1]', 'INT')
	FROM @XmlTiposCorral.nodes('ROOT/TiposCorral') AS t(item)


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
			AND c.Activo = @Activo
			AND TipoCorralID IN (SELECT TipoCorralID FROM @TiposCorral);
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

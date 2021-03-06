USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorFiltroPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorFiltroPagina]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorFiltroPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionSemana_ObtenerPorFiltroPagina
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorFiltroPagina] @OrganizacionID INT
	,@Activo BIT
	,@Inicio INT
	,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY cs.ConfiguracionSemanaID ASC
			) AS [RowNum]
		,cs.ConfiguracionSemanaID
		,o.OrganizacionID
		,o.Descripcion Organizacion
		,too.TipoOrganizacionID
		,too.Descripcion TipoOrganizacion
		,cs.InicioSemana
		,cs.FinSemana
		,cs.Activo
	INTO #ConfiguracionSemana
	FROM ConfiguracionSemana cs
	INNER JOIN Organizacion o ON cs.OrganizacionID = o.OrganizacionID
	INNER JOIN TipoOrganizacion too ON o.TipoOrganizacionID = too.TipoOrganizacionID
	WHERE @OrganizacionID IN (
			o.OrganizacionID
			,0
			)
		AND cs.Activo = @Activo
	SELECT ConfiguracionSemanaID
		,OrganizacionID
		,Organizacion
		,TipoOrganizacionID
		,TipoOrganizacion
		,InicioSemana
		,FinSemana
		,Activo
	FROM #ConfiguracionSemana
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(ConfiguracionSemanaID) AS [TotalReg]
	FROM #ConfiguracionSemana
	DROP TABLE #ConfiguracionSemana
	SET NOCOUNT OFF;
END

GO

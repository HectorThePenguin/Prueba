USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorFiltroPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorFiltroPagina]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorFiltroPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroOrganizacion_ObtenerPorFiltroPagina 0, 68, 1, 1, 15
-- 001 Jorge Luis Velazquez Araujo 01/12/2015 **Se agrega el id del parametro
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorFiltroPagina]
@OrganizacionID INT,
@ParametroID INT, --001
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY po.ParametroOrganizacionID ASC) AS [RowNum],
		po.ParametroOrganizacionID,
		pa.ParametroID,
		pa.Descripcion AS Parametro,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		po.Valor,
		po.Activo
	INTO #ParametroOrganizacion
	FROM ParametroOrganizacion po
	INNER JOIN Organizacion o on po.OrganizacionID = o.OrganizacionID
	INNER JOIN Parametro pa on po.ParametroID = pa.ParametroID
	WHERE 
	@OrganizacionID IN (o.OrganizacionID,0)
	and @ParametroID IN (pa.ParametroID,0) --001
	AND po.Activo = @Activo
	SELECT
		ParametroOrganizacionID,
		ParametroID,
		Parametro,
		OrganizacionID,
		Organizacion,
		Valor,
		Activo
	FROM #ParametroOrganizacion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ParametroOrganizacionID) AS [TotalReg]
	FROM #ParametroOrganizacion
	DROP TABLE #ParametroOrganizacion
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 27/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionTraspasoAlmacen_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorPagina]
@ConfiguracionTraspasoAlmacenID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY ConfiguracionTraspasoAlmacenID ASC) AS [RowNum],
		ConfiguracionTraspasoAlmacenID,
		TipoAlmacenOrigenID,
		TipoAlmacenDestinoID,
		DiferenteOrganizacion,
		Activo
	INTO #ConfiguracionTraspasoAlmacen
	FROM ConfiguracionTraspasoAlmacen
	WHERE Activo = @Activo
	SELECT
		ConfiguracionTraspasoAlmacenID,
		TipoAlmacenOrigenID,
		TipoAlmacenDestinoID,
		DiferenteOrganizacion,
		Activo
	FROM #ConfiguracionTraspasoAlmacen
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ConfiguracionTraspasoAlmacenID) AS [TotalReg]
	FROM #ConfiguracionTraspasoAlmacen
	DROP TABLE #ConfiguracionTraspasoAlmacen
	SET NOCOUNT OFF;
END

GO

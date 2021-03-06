USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionSemana_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorPagina]
@ConfiguracionSemanaID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY ConfiguracionSemanaID ASC) AS [RowNum],
		ConfiguracionSemanaID,
		OrganizacionID,
		InicioSemana,
		FinSemana,
		Activo
	INTO #ConfiguracionSemana
	FROM ConfiguracionSemana
	WHERE 
	Activo = @Activo
	SELECT
		ConfiguracionSemanaID,
		OrganizacionID,
		InicioSemana,
		FinSemana,
		Activo
	FROM #ConfiguracionSemana
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ConfiguracionSemanaID) AS [TotalReg]
	FROM #ConfiguracionSemana
	DROP TABLE #ConfiguracionSemana
	SET NOCOUNT OFF;
END

GO

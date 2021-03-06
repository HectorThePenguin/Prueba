USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteProyeccion_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccion_ObtenerPorPagina]
@LoteProyeccionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY LoteProyeccionID ASC) AS [RowNum],
		LoteProyeccionID,
		LoteID,
		OrganizacionID,
		Frame,
		GananciaDiaria,
		ConsumoBaseHumeda,
		Conversion,
		PesoMaduro,
		PesoSacrificio,
		DiasEngorda,
		FechaEntradaZilmax	INTO #Datos
	FROM LoteProyeccion	
	SELECT
		LoteProyeccionID,
		LoteID,
		OrganizacionID,
		Frame,
		GananciaDiaria,
		ConsumoBaseHumeda,
		Conversion,
		PesoMaduro,
		PesoSacrificio,
		DiasEngorda,
		FechaEntradaZilmax
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(LoteProyeccionID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

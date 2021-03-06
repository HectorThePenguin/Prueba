USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TiempoMuerto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TiempoMuerto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TiempoMuerto_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TiempoMuerto_ObtenerPorPagina]
@TiempoMuertoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY TiempoMuertoID ASC) AS [RowNum],
		TiempoMuertoID,
		ProduccionDiariaID,
		RepartoAlimentoID,
		HoraInicio,
		HoraFin,
		CausaTiempoMuertoID,
		Activo
	INTO #TiempoMuerto
	FROM TiempoMuerto
	WHERE  Activo = @Activo
	SELECT
		TiempoMuertoID,
		ProduccionDiariaID,
		RepartoAlimentoID,
		HoraInicio,
		HoraFin,
		CausaTiempoMuertoID,
		Activo
	FROM #TiempoMuerto
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TiempoMuertoID) AS [TotalReg]
	FROM #TiempoMuerto
	DROP TABLE #TiempoMuerto
	SET NOCOUNT OFF;
END

GO

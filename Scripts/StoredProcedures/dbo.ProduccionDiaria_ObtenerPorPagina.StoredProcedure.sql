USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionDiaria_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionDiaria_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 24/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProduccionDiaria_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ProduccionDiaria_ObtenerPorPagina]
@ProduccionDiariaID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY ProduccionDiariaID ASC) AS [RowNum],
		ProduccionDiariaID,
		Turno,
		LitrosInicial,
		LitrosFinal,
		HorometroInicial,
		HorometroFinal,
		FechaProduccion,
		UsuarioIDAutorizo,
		Observaciones,
		Activo
	INTO #ProduccionDiaria
	FROM ProduccionDiaria
	WHERE Activo = @Activo
	SELECT
		ProduccionDiariaID,
		Turno,
		LitrosInicial,
		LitrosFinal,
		HorometroInicial,
		HorometroFinal,
		FechaProduccion,
		UsuarioIDAutorizo,
		Observaciones,
		Activo
	FROM #ProduccionDiaria
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ProduccionDiariaID) AS [TotalReg]
	FROM #ProduccionDiaria
	DROP TABLE #ProduccionDiaria
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LoteReimplante_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[LoteReimplante_ObtenerPorPagina]
@LoteReimplanteID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY LoteReimplanteID ASC) AS [RowNum],
		LoteReimplanteID,
		LoteProyeccionID,
		LoteProyeccionID,
		NumeroReimplante,
		FechaProyectada,
		PesoProyectado,
		FechaReal,
		PesoReal	INTO #Datos
	FROM LoteReimplante	
	SELECT
		LoteReimplanteID,
		LoteProyeccionID,
		LoteProyeccionID,
		NumeroReimplante,
		FechaProyectada,
		PesoProyectado,
		FechaReal,
		PesoReal
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(LoteReimplanteID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

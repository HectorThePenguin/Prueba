USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Condicion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Condicion_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Condicion_ObtenerPorPagina]
@CondicionID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		CondicionID,
		Descripcion,
		Activo
	INTO #Condicion
	FROM Condicion
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		CondicionID,
		Descripcion,
		Activo
	FROM #Condicion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CondicionID) AS [TotalReg]
	FROM #Condicion
	DROP TABLE #Condicion
	SET NOCOUNT OFF;
END

GO

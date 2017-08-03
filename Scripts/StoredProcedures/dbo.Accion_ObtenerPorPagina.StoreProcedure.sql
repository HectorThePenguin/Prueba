USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Accion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Accion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Accion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarria
-- Create date: 15/03/2016
-- Description: SP para tomar las acciones por pagina para el grid de datos.
-- SpName     : dbo.Accion_ObtenerPorPagina
-- --======================================================
CREATE PROCEDURE [dbo].[Accion_ObtenerPorPagina]
@Descripcion varchar(255),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		Ac.AccionID,
		Ac.Descripcion,
		Ac.Activo
	INTO #Accion
	FROM Accion Ac
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND Activo = @Activo
	SELECT
		AccionID,
		Descripcion,
		Activo
	FROM #Accion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(AccionID) AS [TotalReg]
	FROM #Accion
	DROP TABLE #Accion
	SET NOCOUNT OFF;
END
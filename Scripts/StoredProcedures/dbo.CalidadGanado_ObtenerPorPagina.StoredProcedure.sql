USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CalidadGanado_ObtenerPorPagina 0,'',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[CalidadGanado_ObtenerPorPagina]
@CalidadGanadoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		CalidadGanadoID,
		Descripcion,
		Sexo,
		Activo, 
		Calidad
	INTO #CalidadGanado
	FROM CalidadGanado
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		CalidadGanadoID,
		Descripcion,
		Sexo,
		Activo, 
		Calidad
	FROM #CalidadGanado
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CalidadGanadoID) AS [TotalReg]
	FROM #CalidadGanado
	DROP TABLE #CalidadGanado
	SET NOCOUNT OFF;
END

GO

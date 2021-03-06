USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DescripcionGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DescripcionGanado_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[DescripcionGanado_ObtenerPorPagina]
@DescripcionGanadoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		DescripcionGanadoID,
		Descripcion,
		Activo
	INTO #DescripcionGanado
	FROM DescripcionGanado
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		DescripcionGanadoID,
		Descripcion,
		Activo
	FROM #DescripcionGanado
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(DescripcionGanadoID) AS [TotalReg]
	FROM #DescripcionGanado
	DROP TABLE #DescripcionGanado
	SET NOCOUNT OFF;
END

GO

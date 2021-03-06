USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Moneda_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Moneda_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Moneda_ObtenerPorPagina]
@MonedaID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		MonedaID,
		Descripcion,
		Abreviatura,
		Activo
	INTO #Moneda
	FROM Moneda
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		MonedaID,
		Descripcion,
		Abreviatura,
		Activo
	FROM #Moneda
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(MonedaID) AS [TotalReg]
	FROM #Moneda
	DROP TABLE #Moneda
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CentroCosto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CentroCosto_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[CentroCosto_ObtenerPorPagina]
@CentroCostoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		CentroCostoID,
		CentroCostoSAP,
		Descripcion,
		AreaDepartamento,
		Activo
	INTO #CentroCosto
	FROM CentroCosto
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		CentroCostoID,
		CentroCostoSAP,
		Descripcion,
		AreaDepartamento,
		Activo
	FROM #CentroCosto
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CentroCostoID) AS [TotalReg]
	FROM #CentroCosto
	DROP TABLE #CentroCosto
	SET NOCOUNT OFF;
END

GO

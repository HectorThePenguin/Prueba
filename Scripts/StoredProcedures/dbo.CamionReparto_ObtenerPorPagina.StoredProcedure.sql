USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CamionReparto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CamionReparto_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[CamionReparto_ObtenerPorPagina]
@CamionRepartoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY CamionRepartoID ASC) AS [RowNum],
		CamionRepartoID,
		OrganizacionID,
		CentroCostoID,
		NumeroEconomico,
		Activo
	INTO #CamionReparto
	FROM CamionReparto
	WHERE Activo = @Activo
	SELECT
		CamionRepartoID,
		OrganizacionID,
		CentroCostoID,
		NumeroEconomico,
		Activo
	FROM #CamionReparto
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CamionRepartoID) AS [TotalReg]
	FROM #CamionReparto
	DROP TABLE #CamionReparto
	SET NOCOUNT OFF;
END

GO

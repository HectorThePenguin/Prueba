USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CostoOrganizacion_ObtenerPorPagina 0, 0, 0, 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorPagina]
@CostoOrganizacionID INT,
@TipoOrganizacionID INT,
@CostoID INT, 
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY C.Descripcion ASC) AS [RowNum],
		CO.CostoOrganizacionID,
		CO.TipoOrganizacionID,
		CO.CostoID,
		CO.Automatico,
		CO.Activo
		, C.Descripcion AS Costo
		, C.ClaveContable
		, TOR.Descripcion AS TipoOrganizacion
		, R.RetencionID
		, R.Descripcion AS Retencion
	INTO #CostoOrganizacion
	FROM CostoOrganizacion CO
	INNER JOIN Costo C
		ON (CO.CostoID = C.CostoID
			AND @CostoID IN (CO.CostoID, 0))
	INNER JOIN TipoOrganizacion TOR
		ON (CO.TipoOrganizacionID = TOR.TipoOrganizacionID
			AND @TipoOrganizacionID IN (CO.TipoOrganizacionID, 0))
	LEFT OUTER JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	WHERE @CostoOrganizacionID IN (CO.CostoOrganizacionID, 0)			
			AND CO.Activo = @Activo
	SELECT
		CostoOrganizacionID,
		TipoOrganizacionID,
		CostoID,
		Automatico,
		Activo
		, Costo
		, ClaveContable
		, TipoOrganizacion
		, RetencionID
		, Retencion
	FROM #CostoOrganizacion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CostoOrganizacionID) AS [TotalReg]
	FROM #CostoOrganizacion
	DROP TABLE #CostoOrganizacion
	SET NOCOUNT OFF;
END

GO

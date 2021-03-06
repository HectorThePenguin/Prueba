USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_ObtenerPorPagina 0, 0, 0, 4, 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_ObtenerPorPagina]
@ParametroID INT,
@TrampaID INT,
@TipoParametroID INT,
@OrganizacionID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS [RowNum],
		PT.ParametroTrampaID,
		PT.ParametroID,
		PT.TrampaID,
		PT.Valor,
		PT.Activo,
		P.Descripcion AS Parametro,
		T.Descripcion AS Trampa,
		TP.TipoParametroID,
		TP.Descripcion AS TipoParametro
	INTO #ParametroTrampa
	FROM ParametroTrampa PT
	INNER JOIN Parametro P
		ON (PT.ParametroID = P.ParametroID
			AND @ParametroID IN (PT.ParametroID, 0)
			AND @TipoParametroID IN (P.TipoParametroID, 0))
	INNER JOIN Trampa T
		ON (PT.TrampaID = T.TrampaID
			AND @TrampaID IN (PT.TrampaID, 0)
			AND T.OrganizacionID = @OrganizacionID)
	INNER JOIN TipoParametro TP
		ON (P.TipoParametroID = TP.TipoParametroID)
	WHERE PT.Activo = @Activo
	SELECT
		ParametroTrampaID,
		ParametroID,
		TrampaID,
		Valor,
		Activo,
		Parametro,
		Trampa,
		TipoParametroID,
		TipoParametro
	FROM #ParametroTrampa
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ParametroTrampaID) AS [TotalReg]
	FROM #ParametroTrampa
	DROP TABLE #ParametroTrampa
	SET NOCOUNT OFF;
END

GO

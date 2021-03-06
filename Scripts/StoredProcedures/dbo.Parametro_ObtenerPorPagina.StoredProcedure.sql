USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Obtiene una lista de parametros paginada
-- SpName     : Parametro_ObtenerPorPagina '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Parametro_ObtenerPorPagina]
@Descripcion varchar(50),
@TipoParametroID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS [RowNum],
		ParametroID,
		P.TipoParametroID,
		TP.Descripcion TipoParametro,
		P.Descripcion,
		Clave,
		P.Activo
	INTO #Parametro
	FROM Parametro P 
	INNER JOIN TipoParametro TP 
		ON P.TipoParametroID = TP.TipoParametroID 
	WHERE (P.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
		AND @TipoParametroID IN (P.TipoParametroID, 0)
		AND P.Activo = @Activo
	SELECT
		ParametroID,
		TipoParametroID,
		TipoParametro,
		Descripcion,
		Clave,
		Activo
	FROM #Parametro
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ParametroID) AS [TotalReg]
	FROM #Parametro
	DROP TABLE #Parametro
	SET NOCOUNT OFF;
END

GO

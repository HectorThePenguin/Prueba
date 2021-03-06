USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Retencion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Retencion_ObtenerPorPagina 0, '', 1, 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Retencion_ObtenerPorPagina]
@RetencionID INT,
@Descripcion VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		RetencionID
		,Descripcion
		,TipoRetencion
		,IndicadorRetencion
		,IndicadorImpuesto
		,Tasa
		,Activo
	INTO #Datos
	FROM Retencion	
	WHERE @RetencionID IN (RetencionID, 0)
		AND (@Descripcion = '' OR Descripcion LIKE '%' + @Descripcion + '%')
	SELECT
		RetencionID
		,Descripcion
		,TipoRetencion
		,IndicadorRetencion
		,IndicadorImpuesto
		,Tasa
		,Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(RetencionID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

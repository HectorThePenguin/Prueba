USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaFiltroTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorPaginaFiltroTipoCosto]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaFiltroTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 23/05/2014
-- Description: Sp para obtener los Costos por Pagina con filtro por tipo de costo
-- Costo_ObtenerPorPaginaFiltroTipoCosto 0, '', 'A', 0, 1 , 1, 10
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorPaginaFiltroTipoCosto] 
@CostoID INT
,@Descripcion VARCHAR(50)
,@ClaveContable CHAR(3)
,@TipoCostoID INT
,@Activo BIT
,@Inicio INT
,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY C.Descripcion ASC
			) AS RowNum
		, C.CostoID,
		C.ClaveContable,
		C.Descripcion,
		C.TipoCostoID,
		C.TipoProrrateoID,
		ISNULL(C.RetencionID, 0) AS RetencionID,
		C.AbonoA,
		C.Activo,
		TC.Descripcion AS TipoCosto,
		TP.Descripcion AS TipoProrrateo,
		ISNULL(R.Descripcion,'')  AS Retencion
		, R.TipoRetencion
		, R.IndicadorImpuesto
		, R.IndicadorRetencion
	INTO #Datos
	FROM Costo C
	INNER JOIN TipoCosto TC
		ON (C.TipoCostoID = TC.TipoCostoID) 
	INNER JOIN TipoProrrateo TP
		ON (C.TipoProrrateoID = TP.TipoProrrateoID)
	LEFT OUTER JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	WHERE (
			C.Descripcion LIKE '%' + @Descripcion + '%'
			OR @Descripcion = ''
			)
		AND @CostoID IN (C.CostoID, 0)
		AND @TipoCostoID IN (C.TipoCostoID, 0)
		AND C.Activo = @Activo
		AND C.TipoCostoID = @TipoCostoID
	SELECT CostoID
		,Descripcion
		,Activo
		, RetencionID
		, ClaveContable
		, TipoCostoID,
		TipoProrrateoID,
		RetencionID,
		AbonoA,
		Activo,
		TipoCosto,
		TipoProrrateo,
		Retencion
		, TipoRetencion
		, IndicadorImpuesto
		, IndicadorRetencion
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(CostoID) AS TotalReg
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

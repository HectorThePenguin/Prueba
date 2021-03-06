USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaCostoPorTiposGasto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorPaginaCostoPorTiposGasto]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaCostoPorTiposGasto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
=============================================
-- Author:		Andres Vejar
-- Create date: 16/07/2014
-- Description:	Obtener listado de costos contenidos en uno o mas grupos costo
-- Costo_ObtenerPorPaginaCostoPorTiposGasto '', 0,0,1 , 1, 10 
=============================================
*/
CREATE PROCEDURE [dbo].[Costo_ObtenerPorPaginaCostoPorTiposGasto]
	@DescripcionCosto NVARCHAR(150),
	@XmlTipoCostos XML,
	@Activo INT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #GRUPOSCOSTO
	(
		CostoID int
	)	
	INSERT #GRUPOSCOSTO (CostoID)
	SELECT grupo = t.item.value('./TipoCostoID[1]', 'int')
	FROM @XmlTipoCostos.nodes('ROOT/TiposCosto') AS t(item)
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
			C.Descripcion LIKE '%' + @DescripcionCosto + '%'
			)
		AND C.TipoCostoID IN(SELECT CostoId from #GRUPOSCOSTO)
		AND C.Activo = @Activo
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

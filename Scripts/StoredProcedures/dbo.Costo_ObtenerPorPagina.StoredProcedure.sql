USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener los Costos por Pagina
-- EXEC Costo_ObtenerPorPagina 0, '', '', 0, 1 , 1, 50
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorPagina] 
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
	DECLARE @Clave CHAR(3)
	IF (ISNUMERIC(@ClaveContable) <> 0)
	BEGIN
		SET @Clave= dbo.RellenaCeros(@ClaveContable,3)
	END
	ELSE
	BEGIN
		SET @Clave= @ClaveContable
	END
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
		,C.CompraIndividual
		,C.Compra
		,C.Recepcion
		,C.Gasto
		,C.Costo
		,TCC.TipoCostoCentroID
		,TCC.Descripcion AS DescripcionTipoCostoCentro
	INTO #Datos
	FROM Costo C
	INNER JOIN TipoCosto TC
		ON (C.TipoCostoID = TC.TipoCostoID) 
	INNER JOIN TipoProrrateo TP
		ON (C.TipoProrrateoID = TP.TipoProrrateoID)
	LEFT OUTER JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	LEFT OUTER JOIN TipoCostoCentro TCC ON (TCC.TipoCostoCentroID= C.TipoCostoIDCentro)
	WHERE (
			C.Descripcion LIKE '%' + @Descripcion + '%'
			OR @Descripcion = ''
			)
		AND @CostoID IN (C.CostoID, 0)
		AND @TipoCostoID IN (C.TipoCostoID, 0)
		AND (@Clave = '' OR C.ClaveContable LIKE '%' + LTRIM(RTRIM(@Clave)) + '%')
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
		,CompraIndividual
		,Compra
		,Recepcion
		,Gasto
		,Costo
		,TipoCostoCentroID
		,DescripcionTipoCostoCentro
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(CostoID) AS TotalReg
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

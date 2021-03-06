USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorPaginaTipoCosto]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener los Costos por Pagina
-- Costo_ObtenerPorPaginaTipoCosto 1, '',1,'',1,10
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorPaginaTipoCosto] 
@CostoID INT
,@Descripcion VARCHAR(50)
,@XmlTiposCosto XML
,@ClaveContable CHAR(3)
,@Inicio INT
,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TiposCosto AS TABLE ([TipoCostoID] INT)
	INSERT @TiposCosto ([TipoCostoID])
	SELECT [TipoCostoID] = t.item.value('./TipoCostoID[1]', 'INT')
	FROM @XmlTiposCosto.nodes('ROOT/TiposCosto') AS T(item)
	SELECT ROW_NUMBER() OVER (
			ORDER BY C.Descripcion ASC
			) AS RowNum
		, C.CostoID
		,C.TipoCostoID
		, C.ClaveContable
		, C.Descripcion
		, C.Activo
		, C.RetencionID
	INTO #Datos
	FROM Costo C
	INNER JOIN @TiposCosto tc1 ON c.TipoCostoID = tc1.TipoCostoID
	WHERE (
			C.Descripcion LIKE '%' + @Descripcion + '%'
			OR @Descripcion = ''
			)
		AND C.Activo = 1
		AND @CostoID IN (C.CostoID, 0)
		AND @ClaveContable IN (C.ClaveContable, '')		
	SELECT CostoID
		,ClaveContable
		,Descripcion		
		,Activo
		, RetencionID
		,TipoCostoID
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(CostoID) AS TotalReg
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

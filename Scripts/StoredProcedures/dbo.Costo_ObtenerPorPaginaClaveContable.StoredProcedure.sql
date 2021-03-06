USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaClaveContable]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorPaginaClaveContable]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorPaginaClaveContable]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener los Costos por Pagina
-- Costo_ObtenerPorPaginaClaveContable '',1,1,10
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorPaginaClaveContable] 
@ClaveContable INT
,@Descripcion VARCHAR(50)
,@Inicio INT
,@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY C.Descripcion ASC
			) AS RowNum
		,C.CostoID
		,C.ClaveContable
		,C.Descripcion		
		,C.Activo
		, C.RetencionID
	INTO #Datos
	FROM Costo C
	WHERE (
			C.Descripcion LIKE '%' + @Descripcion + '%'
			OR @Descripcion = ''
			)
		AND C.Activo = 1
		AND @ClaveContable IN (ClaveContable, '')
	SELECT CostoID
		,ClaveContable
		,Descripcion
		,Activo
		, RetencionID
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio
			AND @Limite
	SELECT COUNT(CostoID) AS TotalReg
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

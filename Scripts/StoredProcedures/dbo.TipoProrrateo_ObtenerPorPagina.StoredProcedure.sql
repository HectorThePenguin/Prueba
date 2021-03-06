USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProrrateo_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener Tipos de Prorrateo por Pagina
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoProrrateo_ObtenerPorPagina]
@TipoProrrateoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS RowNum,
		TipoProrrateoID,
		Descripcion,
		Activo
		INTO #Datos
	FROM TipoProrrateo
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND Activo = @Activo
	SELECT
		TipoProrrateoID,
		Descripcion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(TipoProrrateoID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

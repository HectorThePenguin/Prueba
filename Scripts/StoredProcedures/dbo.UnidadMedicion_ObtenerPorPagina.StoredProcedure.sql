USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[UnidadMedicion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[UnidadMedicion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[UnidadMedicion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/01/14
-- Description: Obtiene Unidad Medicion paginada
-- UnidadMedicion_ObtenerPorPagina '', '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[UnidadMedicion_ObtenerPorPagina]
@Descripcion varchar(50),
@ClaveUnidad CHAR(3),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS RowNum
		, UnidadID
		, Descripcion
		, ClaveUnidad
		, Activo
	INTO #Datos
	FROM UnidadMedicion
	WHERE (Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion = '')
		AND (ClaveUnidad LIKE '%' + @ClaveUnidad + '%' OR @ClaveUnidad = '')
		AND Activo = @Activo
	SELECT		
		UnidadID
		, Descripcion
		, ClaveUnidad
		, Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(UnidadID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

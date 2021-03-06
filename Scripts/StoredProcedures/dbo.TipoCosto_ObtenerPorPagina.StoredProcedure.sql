USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCosto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para consultar Tipos de Costo por Pagina
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoCosto_ObtenerPorPagina]
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS RowNum,
		TipoCostoID,
		Descripcion,
		Activo
		INTO #Datos
	FROM TipoCosto
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND Activo = @Activo
	SELECT
		TipoCostoID,
		Descripcion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(TipoCostoID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

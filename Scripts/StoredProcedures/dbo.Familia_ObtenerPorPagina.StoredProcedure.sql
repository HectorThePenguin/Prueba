USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Familia_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- Familia_ObtenerPorPagina '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Familia_ObtenerPorPagina]
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS RowNum,
		FamiliaID,
		Descripcion,
		Activo
		INTO #Datos
	FROM Familia
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
		AND Activo = @Activo
	SELECT
		FamiliaID,
		Descripcion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite ORDER BY FamiliaID
	SELECT 
		COUNT(FamiliaID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

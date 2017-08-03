USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_ObtenerPorPagina]    Script Date: 22/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Marcas_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Marcas_ObtenerPorPagina]    Script Date: 22/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 22-05-2017
-- Description: Procedimiento almacenado que obtiene por página las marcas registradas.
-- SpName     : Marcas_ObtenerPorPagina '',1,1,15
--======================================================  
CREATE PROCEDURE [dbo].[Marcas_ObtenerPorPagina]
@Descripcion VARCHAR(255),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		MarcaID,
		Descripcion,
		Activo,
		Tracto
	INTO #Marca
	FROM Marca
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		MarcaID,
		Descripcion,
		Activo,
		Tracto
	FROM #Marca
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(MarcaID) AS [TotalReg]
	FROM #Marca
	DROP TABLE #Marca
	SET NOCOUNT OFF;
END
GO
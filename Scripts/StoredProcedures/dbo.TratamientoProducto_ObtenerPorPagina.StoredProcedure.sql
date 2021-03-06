USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_ObtenerPorPagina]
@TratamientoProductoID int,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY TratamientoProductoID ASC) AS [RowNum],
		TratamientoProductoID,
		TratamientoID,
		ProductoID,
		Dosis,
		Activo	INTO #Datos
	FROM TratamientoProducto
	WHERE 
	Activo = @Activo
	SELECT
		TratamientoProductoID,
		TratamientoID,
		ProductoID,
		Dosis,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TratamientoProductoID) AS [TotalReg]
	FROM #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO

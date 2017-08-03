USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Guillermo Osuna Covarrubias
-- Create date: 22 may 2017
-- Description: Sp para obtener los gastos fijos para el grid
-- EXEC AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina @Descripcion = "", @Activo = 1, @Inicio = 1, @Limite = 15
--=============================================
CREATE PROCEDURE [dbo].[AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina] 
@Descripcion varchar(250),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		GastoFijoID,
		Descripcion,
	 	Importe,
		Activo
	INTO #GastosFijos
	FROM GastosFijos
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		GastoFijoID,
		Descripcion,
		Importe,
		Activo
	FROM #GastosFijos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(GastoFijoID) AS [TotalReg]
	FROM #GastosFijos
	DROP TABLE #GastosFijos
	SET NOCOUNT OFF;
END

GO


--========================05_CatParametroBanco_ObtenerPorPagina========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroBanco_ObtenerPorPagina' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroBanco_ObtenerPorPagina
END
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 22/09/2016
-- Description: SP para obtener los datos de la CatParametroBanco
-- Origen: APInterfaces
-- SpName     : dbo.CatParametroBanco_ObtenerPorPagina '',1,1,0,10
-- --======================================================
CREATE PROCEDURE [dbo].[CatParametroBanco_ObtenerPorPagina]
@Descripcion varchar(255),
@TipoParametro INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY CatParametroBancoID ASC) AS [RowNum],
		CatParametroBancoID,
		Descripcion,
		Clave,
		TipoParametro,
		Valor,
		Activo
	INTO #CatParametroBanco
	FROM CatParametroBanco 
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND @TipoParametro IN (TipoParametro, 0)
	AND Activo = @Activo
	ORDER BY CatParametroBancoID ASC
	
	SELECT
		CatParametroBancoID,
		Descripcion,
		Clave,
		TipoParametro,
		Valor,
		Activo
	FROM #CatParametroBanco
	WHERE RowNum BETWEEN @Inicio AND @Limite
	ORDER BY CatParametroBancoID ASC
	
	SELECT
	COUNT(CatParametroBancoID) AS [TotalReg]
	FROM #CatParametroBanco
	
	DROP TABLE #CatParametroBanco
	SET NOCOUNT OFF;
END
GO

IF object_id('dbo.TipoGanadoCentros_ObtenerPorPagina', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TipoGanadoCentros_ObtenerPorPagina
END
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 05/11/2015 12:00:00 a.m.  
-- Description:   
-- SpName     : TipoGanadoCentros_ObtenerPorPagina 0, '',1,1,15  
--======================================================  
CREATE PROCEDURE [dbo].[TipoGanadoCentros_ObtenerPorPagina]  
@TipoGanadoID INT,  
@Descripcion VARCHAR(50),  
@Activo BIT,  
@Inicio INT,  
@Limite INT   
AS  
BEGIN  
SET NOCOUNT ON;  
	SELECT  
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],  
		TipoGanadoID,  
		Descripcion,  
		Sexo,  
		PesoMinimo,  
		PesoMaximo,  
		PesoSalida,  
		Activo  
	INTO #TipoGanado  
	FROM Sukarne.dbo.CatTipoGanado
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')   
	AND Activo = @Activo
  
	SELECT  
		TipoGanadoID,  
		Descripcion,  
		Sexo,  
		PesoMinimo,  
		PesoMaximo,  
		PesoSalida,  
		Activo  
	FROM #TipoGanado  
	WHERE RowNum BETWEEN @Inicio AND @Limite
  
	SELECT  
	COUNT(TipoGanadoID) AS [TotalReg]  
	FROM #TipoGanado  

	DROP TABLE #TipoGanado  

SET NOCOUNT OFF;  
END
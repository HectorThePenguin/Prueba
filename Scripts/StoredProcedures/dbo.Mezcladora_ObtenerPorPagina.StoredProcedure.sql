USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Mezcladora_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Mezcladora_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Mezcladora_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Mezcladora_ObtenerPorPagina 'M2',0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Mezcladora_ObtenerPorPagina]
@NumeroEconomico VARCHAR(10),
@mezcladoraID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY NumeroEconomico ASC) AS [RowNum],
		mezcladoraID,
		NumeroEconomico,
		Descripcion,
		Activo
	INTO #Mezcladora
	FROM Mezcladora
	WHERE (Activo = @Activo OR @Activo is null)
				AND NumeroEconomico LIKE '%' + @NumeroEconomico + '%'
				AND @mezcladoraID IN (mezcladoraID, 0)
				AND Activo = @Activo
	SELECT
				mezcladoraID,
				NumeroEconomico,
				Descripcion,
				Activo
	FROM #Mezcladora 
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(mezcladoraID) AS [TotalReg]
	FROM #Mezcladora
	DROP TABLE #Mezcladora
	SET NOCOUNT OFF;
END

GO

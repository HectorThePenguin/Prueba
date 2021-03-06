USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_ObtenerPorPagina 0,'',0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_ObtenerPorPagina]
@CausaSalidaID int,
@Descripcion varchar(50),
@TipoMovimientoID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY cs.Descripcion ASC) AS [RowNum],
		CausaSalidaID,
		cs.Descripcion,
		tm.TipoMovimientoID,
		tm.Descripcion AS TipoMovimiento,
		cs.Activo
	INTO #CausaSalida
	FROM CausaSalida cs
	INNER JOIN TipoMovimiento tm on cs.TipoMovimientoID = tm.TipoMovimientoID
	WHERE (cs.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND cs.Activo = @Activo
	AND @TipoMovimientoID in (tm.TipoMovimientoID,0)
	SELECT
		CausaSalidaID,
		Descripcion,
		TipoMovimientoID,
		TipoMovimiento,
		Activo		
	FROM #CausaSalida
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CausaSalidaID) AS [TotalReg]
	FROM #CausaSalida
	DROP TABLE #CausaSalida
	SET NOCOUNT OFF;
END

GO

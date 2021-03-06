USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioGanado_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : PrecioGanado_ObtenerPorPagina 0,0,0,1,1,50000
--======================================================
CREATE PROCEDURE [dbo].[PrecioGanado_ObtenerPorPagina]
@PrecioGanadoID int,
@OrganizacionID int,
@TipoGanadoID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY OrganizacionID ASC) AS [RowNum],
		PrecioGanadoID,
		OrganizacionID,
		TipoGanadoID,
		Precio,
		FechaVigencia,
		Activo
	INTO #PrecioGanado
	FROM PrecioGanado
	WHERE @OrganizacionID in (0, OrganizacionID)
	AND @TipoGanadoID in (0, TipoGanadoID)
	AND Activo = @Activo
	SELECT
		p.PrecioGanadoID,
		p.OrganizacionID,
		o.Descripcion as [Organizacion],
		p.TipoGanadoID,
		t.Descripcion as [TipoGanado],
		p.Precio,
		p.FechaVigencia,
		p.Activo
	FROM #PrecioGanado p
	INNER JOIN Organizacion o on o.OrganizacionID = p.OrganizacionID
	INNER JOIN TipoGanado t on t.TipoGanadoID = p.TipoGanadoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(PrecioGanadoID) AS [TotalReg]
	FROM #PrecioGanado
	DROP TABLE #PrecioGanado
	SET NOCOUNT OFF;
END

GO

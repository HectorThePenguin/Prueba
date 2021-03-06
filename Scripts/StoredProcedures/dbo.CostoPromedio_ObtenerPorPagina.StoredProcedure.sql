USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoPromedio_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CostoPromedio_ObtenerPorPagina 0, 0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[CostoPromedio_ObtenerPorPagina]
@OrganizacionID int,
@CostoID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY OrganizacionID,CostoPromedioID ASC) AS [RowNum],
		CostoPromedioID,
		OrganizacionID,
		CostoID,
		Importe,
		Activo
	INTO #CostoPromedio
	FROM CostoPromedio
	WHERE @OrganizacionID in (OrganizacionID, 0)
	AND @CostoID in (CostoID, 0)
	AND Activo = @Activo
	SELECT
		cp.CostoPromedioID,
		cp.OrganizacionID,
		o.Descripcion as [Organizacion],
		cp.CostoID,
		c.Descripcion as [Costo],
		c.ClaveContable,
		cp.Importe,
		cp.Activo
	FROM #CostoPromedio cp
	INNER JOIN Organizacion o on o.OrganizacionID = cp.OrganizacionID
	INNER JOIN Costo c on c.CostoID = cp.CostoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CostoPromedioID) AS [TotalReg]
	FROM #CostoPromedio
	DROP TABLE #CostoPromedio
	SET NOCOUNT OFF;
END

GO

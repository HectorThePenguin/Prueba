USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rotomix_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rotomix_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Rotomix_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : Rotomix_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Rotomix_ObtenerPorPagina]
@RotomixID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY r.Descripcion ASC) AS [RowNum],
		r.RotomixID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		r.Descripcion,
		r.Activo
	INTO #Rotomix
	FROM Rotomix r
	INNER JOIN Organizacion o on r.OrganizacionID = o.OrganizacionID
	WHERE (r.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND r.Activo = @Activo
	SELECT
		RotomixID,
		OrganizacionID,
		Organizacion,
		Descripcion,
		Activo
	FROM #Rotomix
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(RotomixID) AS [TotalReg]
	FROM #Rotomix
	DROP TABLE #Rotomix
	SET NOCOUNT OFF;
END

GO

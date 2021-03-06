USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Trampa_ObtenerPorPagina 0, '', 0, '',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Trampa_ObtenerPorPagina]
@TrampaID int,
@Descripcion varchar(50),
@OrganizacionID int,
@TipoTrampa char,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		TrampaID,
		Descripcion,
		OrganizacionID,
		TipoTrampa,
		HostName,
		Activo
	INTO #Trampa
	FROM Trampa
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND @OrganizacionID in (OrganizacionID, 0)
	AND @TipoTrampa in (TipoTrampa , ' ')
	AND Activo = @Activo
	SELECT
		t.TrampaID,
		t.Descripcion,
		t.OrganizacionID,
		o.Descripcion as [Organizacion],
		t.TipoTrampa,
		t.HostName,
		t.Activo
	FROM #Trampa t
	inner join Organizacion o on o.OrganizacionID = t.OrganizacionID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TrampaID) AS [TotalReg]
	FROM #Trampa
	DROP TABLE #Trampa
	SET NOCOUNT OFF;
END

GO

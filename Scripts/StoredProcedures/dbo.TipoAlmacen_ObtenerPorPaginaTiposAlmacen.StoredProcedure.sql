USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorPaginaTiposAlmacen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAlmacen_ObtenerPorPaginaTiposAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_ObtenerPorPaginaTiposAlmacen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 27/11/2014
-- Description: 
-- SpName     : TipoAlmacen_ObtenerPorPaginaTiposAlmacen 0, '',1,1,10
--======================================================
CREATE PROCEDURE [dbo].[TipoAlmacen_ObtenerPorPaginaTiposAlmacen]
@TipoAlmacenID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT,
@XmlTiposAlmacen XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TmpTiposAlmacen TABLE(TipoAlmacenID INT)
	INSERT INTO @TmpTiposAlmacen
	SELECT TipoAlmacenID  = T.item.value('./TipoAlmacenID[1]', 'INT')
	  FROM @XmlTiposAlmacen.nodes('ROOT/TiposAlmacen') AS T(item) 
	SELECT
		ROW_NUMBER() OVER (ORDER BY ta.Descripcion ASC) AS [RowNum],
		ta.TipoAlmacenID,
		ta.Descripcion,
		ta.Activo
	INTO #TipoAlmacen
	FROM TipoAlmacen ta
	INNER JOIN @TmpTiposAlmacen tmp ON ta.TipoAlmacenID = tmp.TipoAlmacenID
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND Activo = @Activo
	SELECT
		TipoAlmacenID,
		Descripcion,
		Activo
	FROM #TipoAlmacen
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TipoAlmacenID) AS [TotalReg]
	FROM #TipoAlmacen
	DROP TABLE #TipoAlmacen
	SET NOCOUNT OFF;
END

GO

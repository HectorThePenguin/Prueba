USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerPorFiltros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_ObtenerPorFiltros]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerPorFiltros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 15/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorObjetivo_ObtenerPorFiltros 26,1,1
--======================================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_ObtenerPorFiltros]
@IndicadorProductoCalidadID INT,
@OrganizacionID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		iob.IndicadorObjetivoID,
		ipc.IndicadorProductoCalidadID,
		i.IndicadorID,
		i.Descripcion AS Indicador,
		pr.ProductoID,
		pr.Descripcion AS Producto,		
		toc.TipoObjetivoCalidadID,
		toc.Descripcion As TipoObjetivoCalidad,
		o.OrganizacionID,
		o.Descripcion As Organizacion,
		iob.ObjetivoMinimo,
		iob.ObjetivoMaximo,
		iob.Tolerancia,
		iob.Medicion,
		iob.Activo
	FROM IndicadorObjetivo iob	
	INNER JOIN IndicadorProductoCalidad ipc on iob.IndicadorProductoCalidadID = ipc.IndicadorProductoCalidadID
	INNER JOIN Indicador i on ipc.IndicadorID = i.IndicadorID
	INNER JOIN Producto pr on ipc.ProductoID = pr.ProductoID
	INNER JOIN TipoObjetivoCalidad toc on iob.TipoObjetivoCalidadID = toc.TipoObjetivoCalidadID
	INNER JOIN Organizacion o on iob.OrganizacionID = o.OrganizacionID
	WHERE iob.OrganizacionID = @OrganizacionID
	AND iob.IndicadorProductoCalidadID = @IndicadorProductoCalidadID
	AND iob.Activo = @Activo
	SET NOCOUNT OFF;
END

GO

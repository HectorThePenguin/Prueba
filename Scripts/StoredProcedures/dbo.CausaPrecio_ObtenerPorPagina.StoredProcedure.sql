USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaPrecio_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CausaPrecio_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaPrecio_ObtenerPorPagina 0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[CausaPrecio_ObtenerPorPagina]
@CausaPrecioID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY cp.CausaPrecioID ASC) AS [RowNum],
		cp.CausaPrecioID,
		cp.OrganizacionID,
		o.Descripcion AS Organizacion,
		cs.CausaSalidaID,
		cs.Descripcion AS CausaSalida,
		tm.TipoMovimientoID,
		tm.Descripcion AS TipoMovimiento,
		cp.Precio,		
		cp.Activo
	INTO #CausaPrecio
	FROM CausaPrecio cp	
	INNER JOIN CausaSalida cs on cp.CausaSalidaID = cs.CausaSalidaID
	INNER JOIN TipoMovimiento tm ON cs.TipoMovimientoID = tm.TipoMovimientoID
	INNER JOIN Organizacion o on cp.OrganizacionID = o.OrganizacionID
	WHERE 
	cp.Activo = @Activo
	SELECT
		CausaPrecioID,
		OrganizacionID,
		Organizacion,
		CausaSalidaID,
		CausaSalida,		
		Precio,
		TipoGanadoID,
		TipoGanado,		
		Activo
	FROM #CausaPrecio
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CausaPrecioID) AS [TotalReg]
	FROM #CausaPrecio
	DROP TABLE #CausaPrecio
	SET NOCOUNT OFF;
END

GO

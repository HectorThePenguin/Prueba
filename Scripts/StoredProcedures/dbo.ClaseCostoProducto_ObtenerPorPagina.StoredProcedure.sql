USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ClaseCostoProducto_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_ObtenerPorPagina]
@ClaseCostoProductoID int,
@AlmacenID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY ClaseCostoProductoID ASC) AS [RowNum],
		ClaseCostoProductoID,
		AlmacenID,
		ProductoID,
		CuentaSAPID,
		Activo
	INTO #ClaseCostoProducto
	FROM ClaseCostoProducto
	WHERE Activo = @Activo
	AND @AlmacenID in (AlmacenID,0)
	SELECT
		ccp.ClaseCostoProductoID,
		a.AlmacenID,
		a.Descripcion AS Almacen,
		pr.ProductoID,
		pr.Descripcion AS Producto,
		cs.CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion AS CuentaSAPDescripcion,		
		ccp.Activo
	FROM #ClaseCostoProducto ccp
	INNER JOIN Almacen a on ccp.AlmacenID = a.AlmacenID
	inner join CuentaSAP cs on ccp.CuentaSAPID = cs.CuentaSAPID
	inner join Producto pr on pr.ProductoID = ccp.ProductoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ClaseCostoProductoID) AS [TotalReg]
	FROM #ClaseCostoProducto
	DROP TABLE #ClaseCostoProducto
	SET NOCOUNT OFF;
END

GO

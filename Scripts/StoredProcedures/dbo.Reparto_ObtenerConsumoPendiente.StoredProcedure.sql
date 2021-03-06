USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerConsumoPendiente]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerConsumoPendiente]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerConsumoPendiente]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Jorge Luis Velazquez	
-- Create date:08/03/2015
-- Description:	Obtener Reparto
-- Reparto_ObtenerConsumoPendiente '20150822', 1
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerConsumoPendiente]
@Fecha DATE
,@OrganizacionID INT
AS
declare @TipoAlmacenPlanta int = 8
SELECT
	pr.ProductoID,
	pr.Descripcion AS Producto,
	re.Fecha,
	COUNT(rd.RepartoDetalleID) AS TotalRegistros,	
	SUM(rd.CantidadServida) AS CantidadReparto,
	(SELECT
		Cantidad
	FROM AlmacenInventario ai
	INNER JOIN Almacen a
		ON ai.AlmacenID = a.AlmacenID
	WHERE ai.ProductoID = pr.ProductoID
	AND a.OrganizacionID = @OrganizacionID
	AND a.TipoAlmacenID = @TipoAlmacenPlanta)
	AS CantidadInventario,
	(SELECT
		Cantidad
	FROM AlmacenInventario ai
	INNER JOIN Almacen a
		ON ai.AlmacenID = a.AlmacenID
	WHERE ai.ProductoID = pr.ProductoID
	AND a.OrganizacionID = @OrganizacionID
	AND a.TipoAlmacenID = @TipoAlmacenPlanta)
	- SUM(rd.CantidadServida) AS Diferencia
FROM Reparto re (NOLOCK)
INNER JOIN RepartoDetalle rd (NOLOCK)
	ON re.RepartoID = rd.RepartoID
INNER JOIN Formula fo (NOLOCK)
	ON rd.FormulaIDServida = fo.FormulaID
	inner join Producto pr on fo.ProductoID = pr.ProductoID
WHERE re.Fecha = @Fecha
AND rd.Servido = 1
AND rd.CostoPromedio = 0
AND rd.AlmacenMovimientoID IS NULL
AND re.OrganizacionID = @OrganizacionID
GROUP BY	re.Fecha,			
			pr.ProductoID,
			pr.Descripcion
ORDER BY pr.Descripcion

GO

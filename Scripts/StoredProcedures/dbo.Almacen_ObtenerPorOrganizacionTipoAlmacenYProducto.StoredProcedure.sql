USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenYProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenYProducto]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenYProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 02/06/2014
-- Description: Obtiene todos los almacenes por Organizacion
-- SpName     : Almacen_ObtenerPorOrganizacionTipoAlmacenYProducto 1, 85, 0, 223, 324
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorOrganizacionTipoAlmacenYProducto]
@Activo INT,
@OrganizacionID INT,
@TipoAlmacen INT,
@ProductoId INT,
@AlmacenID INT = 0
AS
BEGIN
	SET NOCOUNT ON;
	
	IF EXISTS(SELECT * FROM Organizacion (NOLOCK) WHERE OrganizacionID = @OrganizacionID AND TipoOrganizacionID NOT IN(4,6,7))
	BEGIN
		SELECT
			ai.AlmacenInventarioID, ai.AlmacenID, ai.Cantidad, ai.Importe, ai.ProductoID, ai.Minimo, ai.Maximo, ai.PrecioPromedio
		FROM AlmacenInventario AS ai (NOLOCK)
		INNER JOIN Almacen AS a (NOLOCK) ON (ai.AlmacenID = a.AlmacenID)
		WHERE a.Activo = @Activo AND a.OrganizacionID = @OrganizacionID 
		AND @TipoAlmacen IN (a.TipoAlmacenID, 0) AND ai.ProductoId = @ProductoId
			AND @AlmacenID IN (AI.AlmacenID, 0)	
	END
	ELSE
	BEGIN
		SELECT 
			ai.AlmacenInventarioID, ai.AlmacenID, CAST(ai.Cantidad AS DECIMAL(18,2)) AS Cantidad, CAST(ai.Importe AS DECIMAL(24,2)) AS Importe, ai.ProductoID, ai.Minimo, ai.Maximo, CAST(ai.PrecioPromedio AS DECIMAL(18,4)) AS PrecioPromedio
		FROM Sukarne.dbo.CatAlmacenInventario ai (NOLOCk)
		INNER JOIN Almacen AS a (NOLOCK) ON (ai.AlmacenID = a.AlmacenID)
		WHERE a.Activo = @Activo AND a.OrganizacionID = @OrganizacionID 
		AND @TipoAlmacen IN (a.TipoAlmacenID, 0) AND ai.ProductoId = @ProductoId
			AND @AlmacenID IN (AI.AlmacenID, 0)
	END
		
	SET NOCOUNT OFF;
END
GO
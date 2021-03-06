USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerExistencia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerExistencia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerExistencia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2014/06/23
-- Description: Obtiene la existenc�a de un producto deun producto en existencia.
-- [dbo].[Producto_ObtenerExistencia]  0,0
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerExistencia] @XmlProductos XML 
AS
BEGIN
	SET NOCOUNT ON;
	declare @Existencia decimal(14,4)
	set @Existencia = 0;
	declare @Productos as TABLE
	(
		AlmacenInventarioID int	
		,AlmacenID int
		,ProductoID int
		,Maximo	int
		,PrecioPromedio	decimal(14,2)
		,Cantidad decimal(14,2)
		,Importe decimal(14,2)
	)
	INSERT @Productos(AlmacenID,ProductoID, Cantidad )
	SELECT 
		[AlmacenID] = t.item.value('./AlmacenID[1]', 'INT'),
		[ProductoID] = t.item.value('./ProductoID[1]', 'INT'),
		[Cantidad] = 0
	FROM @XmlProductos.nodes('ROOT/Producto') AS T(item)
	Update p 	
		set AlmacenInventarioID = ex.AlmacenInventarioID
		,Cantidad = ex.Cantidad
	From @Productos p
	INNER JOIN(
		Select ai.AlmacenInventarioID,ai.almacenID, ai.ProductoID, SUM(ai.Cantidad) as [Cantidad]
		FROM AlmacenInventario ai 
		INNER JOIN @Productos p ON p.AlmacenID = ai.AlmacenID 
			AND p.ProductoID = ai.ProductoID
		Group by ai.AlmacenInventarioID,ai.almacenID, ai.ProductoID
	)ex  on ex.AlmacenID = p.AlmacenID 
		AND  ex.ProductoID = p.ProductoID
	SELECT * From @Productos
	SET NOCOUNT OFF;
END

GO

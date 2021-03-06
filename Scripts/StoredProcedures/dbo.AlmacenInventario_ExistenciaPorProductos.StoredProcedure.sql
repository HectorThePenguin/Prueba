USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ExistenciaPorProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ExistenciaPorProductos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ExistenciaPorProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2014/06/23
-- Description: Obtiene la existenc�a de un producto deun producto en existencia.
-- [dbo].[AlmacenInventario_ExistenciaPorProductos]  0,0
--=============================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ExistenciaPorProductos] @XmlProductos XML 
AS
BEGIN
	SET NOCOUNT ON;
	declare @Existencia decimal(14,4)
	set @Existencia = 0;
	declare @Productos as TABLE
	(
		AlmacenID int
		,ProductoID int
		,Cantidad decimal(14,2)
		,primary key (AlmacenID, ProductoID)
	)
	INSERT @Productos(AlmacenID,ProductoID, Cantidad )
	SELECT 
		[AlmacenID] = t.item.value('./AlmacenID[1]', 'INT'),
		[ProductoID] = t.item.value('./ProductoID[1]', 'INT'),
		[Cantidad] = 0
	FROM @XmlProductos.nodes('ROOT/Producto') AS T(item)
	select  
		ai.AlmacenInventarioID
		,ai.AlmacenID
		,ta.TipoAlmacenID
		,ta.Descripcion AS TipoAlmacen
		,ai.ProductoID
		,pro.Descripcion as Producto
		,sf.SubFamiliaID
		,sf.Descripcion AS SubFamilia
		,um.UnidadID
		,um.Descripcion AS Unidad
		,um.ClaveUnidad
		,ai.Minimo
		,ai.Maximo
		,ai.PrecioPromedio
		,ai.Cantidad
		,ai.Importe
		,ai.FechaCreacion
		,ai.UsuarioCreacionID
		,ai.FechaModificacion
		,ai.UsuarioModificacionID
	From AlmacenInventario ai
	INNER JOIN @Productos p on (p.AlmacenID = ai.AlmacenID  AND p.ProductoID = ai.ProductoID)
	inner join Almacen a on ai.AlmacenID = a.AlmacenID
	inner join TipoAlmacen ta on a.TipoalmacenID = ta.TipoAlmacenID
	inner join Producto pro on p.ProductoID = pro.ProductoID
	inner join SubFamilia sf on pro.SubFamiliaID = sf.SubFamiliaID
	inner join UnidadMedicion um on pro.UnidadID = um.UnidadID
	SET NOCOUNT OFF;
END

GO

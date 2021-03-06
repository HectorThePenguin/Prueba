USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ActualizarPorProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ActualizarPorProductos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ActualizarPorProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 08/07/2014
-- Description: Actualiza la existencia del almacen 
-- SpName     : AlmacenInventario_ActualizarPorProductos
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ActualizarPorProductos] @AlmacenInventarioXML XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #AlmacenInventario (
		AlmacenInventarioID INT
		,AlmacenID INT
		,ProductoID INT
		,PrecioPromedio DECIMAL(18, 4)
		,Cantidad DECIMAL(18, 2)
		,Importe DECIMAL(18, 2)
		,UsuarioModificacionID INT
		)
	INSERT #AlmacenInventario (
		AlmacenInventarioID
		,AlmacenID
		,ProductoID
		,PrecioPromedio
		,Cantidad
		,Importe
		,UsuarioModificacionID
		)
	SELECT AlmacenInventarioID = t.item.value('./AlmacenInventarioID[1]', 'INT')
		,AlmacenID = t.item.value('./AlmacenID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,PrecioPromedio = t.item.value('./PrecioPromedio[1]', 'decimal(18,4)')
		,Cantidad = t.item.value('./Cantidad[1]', 'decimal(18,2)')
		,Importe = t.item.value('./Importe[1]', 'decimal(18,2)')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM @AlmacenInventarioXML.nodes('ROOT/AlmacenInventario') AS T(item)
	UPDATE ai
	SET ai.Cantidad = ad.Cantidad
		,ai.PrecioPromedio = ad.PrecioPromedio
		,ai.Importe = ad.importe
		,ai.UsuarioModificacionID = ad.UsuarioModificacionID
		,ai.FechaModificacion = GETDATE()
	FROM AlmacenInventario ai
	INNER JOIN #AlmacenInventario ad ON (ai.AlmacenInventarioID = ad.AlmacenInventarioID)
	/* Cambio por Cesar Vega: Insertar producto en almacen destino cuando este no existe */
	IF EXISTS (
			SELECT *
			FROM #AlmacenInventario
			WHERE AlmacenInventarioID = - 1
			)
	BEGIN
		INSERT AlmacenInventario (
			AlmacenID
			,ProductoID
			,Minimo
			,Maximo
			,PrecioPromedio
			,Cantidad
			,Importe
			,FechaCreacion
			,UsuarioCreacionID
			,FechaModificacion
			,UsuarioModificacionID
			,DiasReorden
			,CapacidadAlmacenaje
			)
		SELECT aim.AlmacenID
			,aim.ProductoID
			,0 AS Minimo
			,0 AS Maximo
			,aim.PrecioPromedio
			,aim.Cantidad
			,aim.Importe
			,GETDATE()
			,aim.UsuarioModificacionID
			,NULL AS FechaModificacion
			,NULL AS UsuarioModificacionID
			,0 AS DiasReorden
			,0 AS CapacidadAlmacenaje
		FROM #AlmacenInventario aim
		WHERE aim.AlmacenInventarioID = - 1
	END
	SET NOCOUNT OFF;
	DROP TABLE #AlmacenInventario
END

GO

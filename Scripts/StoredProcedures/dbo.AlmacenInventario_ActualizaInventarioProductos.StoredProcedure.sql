USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ActualizaInventarioProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ActualizaInventarioProductos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ActualizaInventarioProductos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 07/04/2014
-- Description:	Actualiza el inventario de productos por almacen
/*AlmacenInventario_ActualizaInventarioProductos '
	<ROOT>
		<AlmacenInventario>
      <AlmacenID>1</AlmacenID>
      <ProductoID>1</ProductoID>
      <Cantidad>1.1</Cantidad>
      <Importe>1.1</Importe>
      <UsuarioCreacionID>19</UsuarioCreacionID>
		</AlmacenInventario>
		<AlmacenInventario>
      <AlmacenID>1</AlmacenID>
      <ProductoID>2</ProductoID>
      <Cantidad>1.1</Cantidad>
      <Importe>1.1</Importe>
      <UsuarioCreacionID>19</UsuarioCreacionID>
		</AlmacenInventario>
	</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ActualizaInventarioProductos]
@XmlAlmacenInventario XML
AS
BEGIN
	DECLARE @TmpAlmacenInventario TABLE(AlmacenID INT,ProductoID INT,Cantidad DECIMAL(14,2),Importe DECIMAL(17,2),UsuarioModificacionID INT)
	INSERT INTO @TmpAlmacenInventario
	SELECT 
			AlmacenID  = T.item.value('./AlmacenID[1]', 'INT'),
			ProductoID  = T.item.value('./ProductoID[1]', 'INT'),
			Cantidad    = T.item.value('./Cantidad[1]', 'DECIMAL(14,2)'),
			Importe    = T.item.value('./Importe[1]', 'DECIMAL(17,2)'),
			Usuario = T.item.value('./UsuarioCreacionID[1]', 'INT')
		FROM  @XmlAlmacenInventario.nodes('ROOT/AlmacenInventario') AS T(item)
	UPDATE AI
	SET AI.Cantidad = TMP.Cantidad,
			AI.Importe = TMP.Importe,
			AI.UsuarioModificacionID = TMP.UsuarioModificacionID,
			AI.FechaModificacion = GETDATE()
	FROM AlmacenInventario AI
	INNER JOIN @TmpAlmacenInventario TMP ON (AI.AlmacenID = TMP.AlmacenID AND AI.ProductoID = TMP.ProductoID) 
	WHERE TMP.Cantidad >= 0
	UPDATE AI
	SET AI.Cantidad = 0,
			AI.Importe = 0,
			AI.UsuarioModificacionID = TMP.UsuarioModificacionID,
			AI.FechaModificacion = GETDATE()
	FROM AlmacenInventario AI
	INNER JOIN @TmpAlmacenInventario TMP ON (AI.AlmacenID = TMP.AlmacenID AND AI.ProductoID = TMP.ProductoID) 
	WHERE TMP.Cantidad < 0
END

GO

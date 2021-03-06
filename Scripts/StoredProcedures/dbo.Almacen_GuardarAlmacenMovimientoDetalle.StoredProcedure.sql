USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimientoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimientoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimientoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 20/02/2014
-- Description:  Almacena el detalle de AlmcenMovimiento y descuenta los consumos del inventario
-- Origen: APInterfaces
-- Almacen_GuardarAlmacenMovimientoDetalle 
--			<ROOT>
--			  <AlmacenMovimientoDetalle>
--				<AlmacenID>3</AlmacenID>
--				<TratamientoID>0</TratamientoID>
--				<ProductoID>67</ProductoID>
--				<Precio>0</Precio>
--				<Cantidad>4</Cantidad>
--				<Importe>0</Importe>
--				<AlmacenMovimientoID>3</AlmacenMovimientoID>
--				<UsuarioCreacionID>6</UsuarioCreacionID>
--			  </AlmacenMovimientoDetalle>
--			</ROOT>
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimientoDetalle]
 @XmlAlmacenMovimientoDetalle XML
AS
BEGIN
	/* Se crea tabla temporal para almacenar el XML */
	DECLARE @AlmacenMovimientoDetalleTem AS TABLE
	(
		AlmacenID INT,
		TratamientoID INT,
		ProductoID INT,
		Precio DECIMAL(10,4),
		Cantidad DECIMAL(14,2),
		Importe DECIMAL(17,2),
		AlmacenMovimientoID BIGINT,
		UsuarioCreacionID INT
	)
	/* Se llena tabla temporal con info del XML */
	INSERT @AlmacenMovimientoDetalleTem(
			AlmacenID,
			TratamientoID,
			ProductoID,
			Precio,
			Cantidad,
			Importe,
			AlmacenMovimientoID,
			UsuarioCreacionID
		)
	SELECT 
		AlmacenID  = T.item.value('./AlmacenID[1]', 'INT'),
		TratamientoID  = T.item.value('./TratamientoID[1]', 'INT'),
		ProductoID    = T.item.value('./ProductoID[1]', 'INT'),
		Precio    = T.item.value('./Precio[1]', 'DECIMAL(10,4)'),
		Cantidad  = T.item.value('./Cantidad[1]', 'DECIMAL(14,2)'),
		Importe   = T.item.value('./Importe[1]', 'DECIMAL(17,2)'),
		AlmacenMovimientoID    = T.item.value('./AlmacenMovimientoID[1]', 'BIGINT'),
		UsuarioCreacionID  = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlAlmacenMovimientoDetalle.nodes('ROOT/AlmacenMovimientoDetalle') AS T(item)
	/* Se actualizan los importes de los productos en base al inventario */
	UPDATE temp SET temp.Precio=AI.PrecioPromedio,
					temp.Importe=AI.PrecioPromedio*temp.Cantidad
	FROM @AlmacenMovimientoDetalleTem temp
	INNER JOIN AlmacenInventario AI(NOLOCK) ON AI.AlmacenID = temp.AlmacenID AND AI.ProductoID = temp.ProductoID 
    /* Se insertan los costos en el AlmacenMovimientoDetalle */
	INSERT INTO AlmacenMovimientoDetalle 
	( 
		AlmacenMovimientoID,
		TratamientoID,
		ProductoID,
		Precio,
		Cantidad,
		Importe,
		FechaCreacion,
		UsuarioCreacionID
	)
	SELECT
		AlmacenMovimientoID,
		CASE WHEN TratamientoID = 0 THEN NULL ELSE TratamientoID END AS "TratamientoID",
		ProductoID,
		Precio,
		Cantidad,
		Importe,
		GETDATE(),
		UsuarioCreacionID
	FROM  @AlmacenMovimientoDetalleTem 
	/* WHERE  No tiene filtro por que ya se valido que los productos existan */
	/* Se decrementan las existencias del inventario*/
	UPDATE AI SET AI.Cantidad=AI.Cantidad-temp.Cantidad,
				  AI.Importe=AI.PrecioPromedio*(AI.Cantidad-temp.Cantidad),
				  AI.FechaModificacion = GETDATE(),
				  AI.UsuarioModificacionID = temp.UsuarioCreacionID
	FROM AlmacenInventario AI(NOLOCK)
	INNER JOIN @AlmacenMovimientoDetalleTem temp ON temp.AlmacenID = AI.AlmacenID AND temp.ProductoID = AI.ProductoID
	/* Se obtiene el Costo total para general el registro en AnimalCosto */
	SELECT SUM(Importe) AS ImporteCosto	FROM @AlmacenMovimientoDetalleTem 
END

GO

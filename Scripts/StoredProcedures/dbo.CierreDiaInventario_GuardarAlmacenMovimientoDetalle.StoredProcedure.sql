USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_GuardarAlmacenMovimientoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventario_GuardarAlmacenMovimientoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_GuardarAlmacenMovimientoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 20/02/2014
-- Description:  Almacena el detalle de AlmcenMovimiento y descuenta los consumos del inventario
-- Origen: APInterfaces
-- EXEC CierreDiaInventario_GuardarAlmacenMovimientoDetalle 
--			<ROOT>
--			  <AlmacenMovimientoDetalle>
--				<AlmacenID>3</AlmacenID>
--				<ProductoID>67</ProductoID>
--				<Precio>0</Precio>
--				<Cantidad>4</Cantidad>
--				<Importe>0</Importe>
--				<AlmacenMovimientoID>3</AlmacenMovimientoID>
--				<UsuarioCreacionID>6</UsuarioCreacionID>
--			  </AlmacenMovimientoDetalle>
--			</ROOT>
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventario_GuardarAlmacenMovimientoDetalle]
 @XmlGuardarProductosCierreDiaInventario XML
AS
BEGIN
	/* Se crea tabla temporal para almacenar el XML */
	DECLARE @AlmacenMovimientoDetalleTem AS TABLE
	(
		AlmacenID INT,
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
			ProductoID,
			Precio,
			Cantidad,
			Importe,
			AlmacenMovimientoID,
			UsuarioCreacionID
		)
	SELECT 
		AlmacenID  = T.item.value('./AlmacenID[1]', 'INT'),
		ProductoID    = T.item.value('./ProductoID[1]', 'INT'),
		Precio    = T.item.value('./Precio[1]', 'DECIMAL(10,4)'),
		Cantidad  = T.item.value('./Cantidad[1]', 'DECIMAL(14,2)'),
		Importe   = T.item.value('./Importe[1]', 'DECIMAL(17,2)'),
		AlmacenMovimientoID    = T.item.value('./AlmacenMovimientoID[1]', 'BIGINT'),
		UsuarioCreacionID  = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlGuardarProductosCierreDiaInventario.nodes('ROOT/CierreDiaInventario') AS T(item)
    /* Se insertan los costos en el AlmacenMovimientoDetalle */
	INSERT INTO AlmacenMovimientoDetalle 
	( 
		AlmacenMovimientoID,
		ProductoID,
		Precio,
		Cantidad,
		Importe,
		FechaCreacion,
		UsuarioCreacionID
	)
	SELECT
		AlmacenMovimientoID,
		ProductoID,
		Precio,
		Cantidad,
		Importe,
		GETDATE(),
		UsuarioCreacionID
	FROM  @AlmacenMovimientoDetalleTem ;
	SELECT
		AlmacenMovimientoID,
		ProductoID,
		Precio,
		Cantidad,
		Importe,
		GETDATE(),
		UsuarioCreacionID
	FROM  @AlmacenMovimientoDetalleTem ;
END

GO

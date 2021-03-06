USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_GuardarDetalleXml]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_GuardarDetalleXml]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_GuardarDetalleXml]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 02/07/2014
-- Description:  Guardar la lista de Almacen MovimientoDetalle
-- AlmacenMovimientoDetalle_GuardarDetalleXml
-- ===============================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_GuardarDetalleXml] @XmlAlmacenMovimientoDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @AlmacenMovimientoDetalle AS TABLE (
		AlmacenMovimientoID bigint
		,AlmacenInventarioLoteID INT
		,ProductoID INT
		,Precio decimal(18,4)
		,Cantidad decimal(18,2)
		,Importe decimal(24,2)
		,Piezas int		
		,UsuarioCreacionID INT				
		)
	INSERT @AlmacenMovimientoDetalle (
		AlmacenMovimientoID
		,AlmacenInventarioLoteID 
		,ProductoID 
		,Precio 
		,Cantidad 
		,Importe 	
		,Piezas
		,UsuarioCreacionID 
		)
	SELECT AlmacenMovimientoID = t.item.value('./AlmacenMovimientoID[1]', 'INT')
		,AlmacenInventarioLoteID = t.item.value('./AlmacenInventarioLoteID[1]', 'INT')
		,ProductoID = t.item.value('./ProductoID[1]', 'INT')
		,Precio = t.item.value('./Precio[1]', 'decimal(18,4)')
		,Cantidad = t.item.value('./Cantidad[1]', 'decimal(18,2)')
		,Importe = t.item.value('./Importe[1]', 'decimal(24,2)')
		,Piezas = COALESCE(t.item.value('./Piezas[1]', 'INT'),0)
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlAlmacenMovimientoDetalle.nodes('ROOT/AlmacenMovimientoDetalle') AS T(item)	
	update @AlmacenMovimientoDetalle set AlmacenInventarioLoteID = null
	where AlmacenInventarioLoteID = 0
	INSERT AlmacenMovimientoDetalle (
		AlmacenMovimientoID
		,AlmacenInventarioLoteID 
		,ProductoID 
		,Precio 
		,Cantidad 
		,Importe 	
		,Piezas
		,UsuarioCreacionID 
		,FechaCreacion
		)
	SELECT AlmacenMovimientoID
		,AlmacenInventarioLoteID 
		,ProductoID 
		,Precio 
		,Cantidad 
		,Importe 	
		,COALESCE(Piezas,0)
		,UsuarioCreacionID 
		,GETDATE()
	FROM @AlmacenMovimientoDetalle
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ActualizaAlmacenInventario]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ActualizaAlmacenInventario]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ActualizaAlmacenInventario]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Octavio Quintero
-- Create date: 24/06/2014
-- Description: Actualiza al almacen, almacen inventario lote y las piezas para la salida del producto.
-- EXEC SalidaProducto_ActualizaAlmacenInventario 1,1,5,60
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ActualizaAlmacenInventario]
	@SalidaProductoID INT,
	@AlmacenID INT,
	@AlmacenInventarioLoteID INT,
	@Piezas INT,
	@Observaciones VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.SalidaProducto
		SET AlmacenID = @AlmacenID,
			AlmacenInventarioLoteID = @AlmacenInventarioLoteID,
			Piezas = @Piezas,
			Observaciones =@Observaciones
	    WHERE SalidaProductoID = @SalidaProductoID;
	SET NOCOUNT OFF;
END

GO

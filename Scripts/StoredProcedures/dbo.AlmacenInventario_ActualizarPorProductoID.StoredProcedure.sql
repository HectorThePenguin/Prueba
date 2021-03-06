USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ActualizarPorProductoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ActualizarPorProductoID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ActualizarPorProductoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 14/05/2014
-- Description: Actualiza el inventario de un producto
-- SpName     : exec AlmacenInventario_ActualizarPorProductoID 1,1,10.0,10.0,1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ActualizarPorProductoID]
@AlmacenID INT,
@ProductoID INT,
@PrecioPromedio DECIMAL(18,4),
@Cantidad DECIMAL(18,2),
@Importe DECIMAL(24,2),
@UsuarioModificacionID INT
AS
BEGIN
	UPDATE AlmacenInventario
	SET Cantidad = @Cantidad, 
		Importe = @Importe,
		PrecioPromedio = @PrecioPromedio,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE ProductoID = @ProductoID AND AlmacenID = @AlmacenID
END

GO

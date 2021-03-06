USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 05/06/2014
-- Description: Actualiza el almacen inventario lote
-- SpName     : exec AlmacenInventarioLote_Actualizar 1, 0, 0, 0, 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_Actualizar]
@AlmacenInventarioLoteID INT,
@Cantidad DECIMAL(18, 2),
@PrecioPromedio DECIMAL(18,4),
@Piezas INT,
@Importe DECIMAL(24, 2),
@UsuarioModificacionID INT
AS 
BEGIN
	UPDATE AlmacenInventarioLote 
	SET Cantidad = @Cantidad,
	PrecioPromedio = @PrecioPromedio,
	Piezas = @Piezas, 
	Importe = @Importe,
	FechaModificacion = GETDATE(),
	UsuarioModificacionID = @UsuarioModificacionID
	WHERE AlmacenInventarioLoteID = @AlmacenInventarioLoteId
END

GO

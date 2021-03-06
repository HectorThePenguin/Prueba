USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 05/06/2014
-- Description: Actualiza un registro de almacen inventario
-- AlmacenMovimientoDetalle_Actualizar 
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_Actualizar]
@AlmacenMovimientoDetalleID BIGINT,	
@Precio DECIMAL(18,4),
@Cantidad DECIMAL(18,2),
@Importe DECIMAL(24,2),
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE AlmacenMovimientoDetalle
			SET
			Precio = @Precio,
			Cantidad = @Cantidad,
			Importe = @Importe,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE AlmacenMovimientoDetalleID = @AlmacenMovimientoDetalleID
	SET NOCOUNT OFF;
END

GO

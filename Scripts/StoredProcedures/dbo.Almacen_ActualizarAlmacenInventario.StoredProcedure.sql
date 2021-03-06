USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ActualizarAlmacenInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ActualizarAlmacenInventario]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ActualizarAlmacenInventario]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 28/03/2014
-- Description: Actualiza un registro en AlmacenInventario por ID
-- SpName     : Almacen_ActualizarAlmacenInventario 37, 190.00, 7200.15, 1
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ActualizarAlmacenInventario]
@AlmacenInventarioID INT,
@Cantidad DECIMAL(14,2),
@Importe DECIMAL(17,2),
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE AlmacenInventario SET
		Cantidad = @Cantidad,
		Importe = @Importe,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE AlmacenInventarioID = @AlmacenInventarioID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_Cancelar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_Cancelar]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_Cancelar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 12/12/2014
-- Description: Cancela una salida de producto
-- SpName     : exec SalidaProducto_Cancelar 1,1
--======================================================
CREATE PROCEDURE [dbo].[SalidaProducto_Cancelar]
@SalidaProductoID BIGINT,
@UsuarioModificacionID INT
AS
BEGIN
	UPDATE SalidaProducto 
		SET Activo = 0,
				FechaModificacion = GETDATE(),
				UsuarioModificacionID = @UsuarioModificacionID
	WHERE SalidaProductoID = @SalidaProductoID
END

GO

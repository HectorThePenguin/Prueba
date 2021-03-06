USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LiquidacionDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LiquidacionDetalle_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[LiquidacionDetalle_Actualizar]
@LiquidacionDetalleID int,
@LiquidacionID int,
@EntradaProductoID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE LiquidacionDetalle SET
		LiquidacionID = @LiquidacionID,
		EntradaProductoID = @EntradaProductoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE LiquidacionDetalleID = @LiquidacionDetalleID
	SET NOCOUNT OFF;
END

GO

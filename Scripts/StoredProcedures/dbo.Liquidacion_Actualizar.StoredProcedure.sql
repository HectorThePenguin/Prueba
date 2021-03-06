USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Liquidacion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_Actualizar]
@LiquidacionID int,
@TipoCambio decimal(10,4),
@Fecha smalldatetime,
@Factura varchar(50),
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Liquidacion SET
		TipoCambio = @TipoCambio,
		Fecha = @Fecha,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE LiquidacionID = @LiquidacionID
	SET NOCOUNT OFF;
END

GO

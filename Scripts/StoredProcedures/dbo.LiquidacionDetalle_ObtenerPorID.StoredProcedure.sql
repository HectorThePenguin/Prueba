USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LiquidacionDetalle_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LiquidacionDetalle_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[LiquidacionDetalle_ObtenerPorID]
@LiquidacionDetalleID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LiquidacionDetalleID,
		LiquidacionID,
		EntradaProductoID,
		Activo
	FROM LiquidacionDetalle
	WHERE LiquidacionDetalleID = @LiquidacionDetalleID
	SET NOCOUNT OFF;
END

GO

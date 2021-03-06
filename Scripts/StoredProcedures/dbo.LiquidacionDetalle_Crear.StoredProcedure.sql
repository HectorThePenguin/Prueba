USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LiquidacionDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : LiquidacionDetalle_Crear
--======================================================
CREATE PROCEDURE [dbo].[LiquidacionDetalle_Crear]
@LiquidacionID int,
@EntradaProductoID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT LiquidacionDetalle (
		LiquidacionID,
		EntradaProductoID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@LiquidacionID,
		@EntradaProductoID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProducto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProducto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProducto_Actualizar]
@IndicadorProductoID int,
@IndicadorID int,
@ProductoID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE IndicadorProducto SET
		IndicadorID = @IndicadorID,
		ProductoID = @ProductoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE IndicadorProductoID = @IndicadorProductoID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoCalidad_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoCalidad_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoCalidad_Actualizar]
@IndicadorProductoCalidadID int,
@IndicadorID int,
@ProductoID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE IndicadorProductoCalidad SET
		IndicadorID = @IndicadorID,
		ProductoID = @ProductoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE IndicadorProductoCalidadID = @IndicadorProductoCalidadID
	SET NOCOUNT OFF;
END

GO

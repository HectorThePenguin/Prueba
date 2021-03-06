USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoProducto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoProducto_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TratamientoProducto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TratamientoProducto_Actualizar]
@TratamientoProductoID int,
@TratamientoID int,
@ProductoID int,
@Dosis int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TratamientoProducto SET
		TratamientoID = @TratamientoID,
		ProductoID = @ProductoID,
		Dosis = @Dosis,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TratamientoProductoID = @TratamientoProductoID
	SET NOCOUNT OFF;
END

GO

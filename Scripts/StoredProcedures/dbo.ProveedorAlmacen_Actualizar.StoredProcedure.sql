USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorAlmacen_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 26/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorAlmacen_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ProveedorAlmacen_Actualizar]
@ProveedorAlmacenID int,
@ProveedorID int,
@AlmacenID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProveedorAlmacen SET
		ProveedorID = @ProveedorID,
		AlmacenID = @AlmacenID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ProveedorAlmacenID = @ProveedorAlmacenID
	SET NOCOUNT OFF;
END

GO

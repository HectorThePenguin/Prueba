USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoAlmacen_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoAlmacen_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoAlmacen_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoAlmacen_Actualizar]
@TipoAlmacenID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoAlmacen SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoAlmacenID = @TipoAlmacenID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObservacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObservacion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoObservacion_Actualizar]
@TipoObservacionID int,
@Descripcion varchar(50),
@UsuarioModificacionID int,
@Activo	BIT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoObservacion SET
		Descripcion = @Descripcion,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
		, Activo = @Activo
	WHERE TipoObservacionID = @TipoObservacionID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObjetivoCalidad_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObjetivoCalidad_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoObjetivoCalidad_Actualizar]
@TipoObjetivoCalidadID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoObjetivoCalidad SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoObjetivoCalidadID = @TipoObjetivoCalidadID
	SET NOCOUNT OFF;
END

GO

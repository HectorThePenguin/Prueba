USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCorral_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCorral_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoCorral_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCorral_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoCorral_Actualizar]
@TipoCorralID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoCorral SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoCorralID = @TipoCorralID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoParametro_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Actualiza un tipo de parametro
-- SpName     : TipoParametro_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoParametro_Actualizar]
	@TipoParametroID int,
	@Descripcion varchar(50),
	@Activo bit,
	@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoParametro SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoParametroID = @TipoParametroID
	SET NOCOUNT OFF;
END

GO

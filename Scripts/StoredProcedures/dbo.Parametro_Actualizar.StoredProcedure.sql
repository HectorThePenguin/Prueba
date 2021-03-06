USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Parametro_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Parametro_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: Actualiza los datos de un parametro
-- SpName     : Parametro_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Parametro_Actualizar]
@ParametroID int,
@TipoParametroID int,
@Descripcion varchar(50),
@Clave varchar(30),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Parametro SET
		TipoParametroID = @TipoParametroID,
		Descripcion = @Descripcion,
		Clave = @Clave,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ParametroID = @ParametroID
	SET NOCOUNT OFF;
END

GO

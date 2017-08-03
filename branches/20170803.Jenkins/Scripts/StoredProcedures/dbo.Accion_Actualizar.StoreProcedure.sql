USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Accion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Accion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Accion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramón Abel Atondo Echavarria
-- Create date: 15/03/2016
-- Description: SP para actualizar Acciones.
-- SpName     : dbo.Accion_Actualizar
-- --======================================================
CREATE PROCEDURE [dbo].[Accion_Actualizar]
@AccionID int,
@Descripcion varchar(150),
@Activo varchar(50),
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Accion SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE AccionID = @AccionID
	SET NOCOUNT OFF;
END


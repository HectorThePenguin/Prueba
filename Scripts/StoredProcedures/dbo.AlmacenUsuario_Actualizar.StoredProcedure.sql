USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenUsuario_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_Actualizar]
@AlmacenUsuarioID int,
@AlmacenID int,
@UsuarioID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE AlmacenUsuario SET
		AlmacenID = @AlmacenID,
		UsuarioID = @UsuarioID,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE AlmacenUsuarioID = @AlmacenUsuarioID
	SET NOCOUNT OFF;
END

GO

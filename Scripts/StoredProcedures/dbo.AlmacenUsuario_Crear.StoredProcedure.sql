USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenUsuario_Crear
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_Crear]
@AlmacenUsuarioID int,
@AlmacenID int,
@UsuarioID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT AlmacenUsuario (
		AlmacenUsuarioID,
		AlmacenID,
		UsuarioID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@AlmacenUsuarioID,
		@AlmacenID,
		@UsuarioID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Accion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Accion_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Accion_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramón Abel Atondo Echavarría
-- Create date: 14/03/2016 03:00:00 p.m.
-- Description: Crear las Acciones de incidencias
-- SpName     : Accion_Crear
--======================================================
CREATE PROCEDURE [dbo].[Accion_Crear]
@Descripcion varchar(255),
@Activo BIT,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Accion (
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@Descripcion,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SET NOCOUNT OFF;
END

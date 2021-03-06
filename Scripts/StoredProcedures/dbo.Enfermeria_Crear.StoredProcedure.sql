USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 07/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Enfermeria_Crear
--======================================================
CREATE PROCEDURE [dbo].[Enfermeria_Crear]
@OrganizacionID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Enfermeria (
		OrganizacionID,
		Descripcion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@Descripcion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

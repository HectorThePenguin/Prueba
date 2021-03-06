USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Usuario_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 21/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Usuario_Crear
--001 Jorge Luis Velazquez 22/04/2015 *Se agrega columna Corporativo
-- 002 Jorge Luis Velazquez 07/12/2015 ** Se agrega columna NivelAcceso
--======================================================
CREATE PROCEDURE [dbo].[Usuario_Crear]
@Nombre varchar(200),
@OrganizacionID int,
@UsuarioActiveDirectory varchar(100),
@Corporativo BIT,
@Activo BIT,
@UsuarioCreacionID INT,
@NivelAcceso INT --002
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Usuario (
		Nombre,
		OrganizacionID,
		UsuarioActiveDirectory,
		Corporativo,
		Activo,
		NivelAcceso --002
	)
	VALUES(
		@Nombre,
		@OrganizacionID,
		@UsuarioActiveDirectory,
		@Corporativo,
		@Activo,
		@NivelAcceso --002
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

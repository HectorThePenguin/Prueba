USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Usuario_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Usuario_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 21/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Usuario_Actualizar
-- 001 Jorge Luis Velazquez Araujo 07/12/2015 ** Se agrega la columna NivelAcceso
--======================================================
CREATE PROCEDURE [dbo].[Usuario_Actualizar]
@UsuarioID int,
@Nombre varchar(200),
@OrganizacionID int,
@UsuarioActiveDirectory varchar(100),
@Corporativo BIT,
@Activo BIT,
@NivelAcceso INT --001
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Usuario SET
		Nombre = @Nombre,
		OrganizacionID = @OrganizacionID,
		UsuarioActiveDirectory = @UsuarioActiveDirectory,
		Corporativo = @Corporativo,
		Activo = @Activo,
		NivelAcceso = @NivelAcceso
	WHERE UsuarioID = @UsuarioID
	SET NOCOUNT OFF;
END

GO

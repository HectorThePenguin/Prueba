IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[Usuario_ObtenerPorActiveDirectory]'))
 DROP PROCEDURE [dbo].[Usuario_ObtenerPorActiveDirectory]
GO
--=============================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 2013/11/26
-- Description: Obtiene un usuario por usuario de active directory
-- Usuario_ObtenerPorActiveDirectory 'luis.velazquez'
-- 001 Jorge Luis Velazquez Araujo 06/08/2015 **Se agrega columna NivelAcceso
--=============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorActiveDirectory]
	@UsuarioActiveDirectory VARCHAR(100) 
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		UsuarioID,
		Nombre,
		OrganizacionID,
		UsuarioActiveDirectory,
		Corporativo,
		NivelAcceso
	FROM Usuario
	WHERE UsuarioActiveDirectory = @UsuarioActiveDirectory AND Activo = 1
	
	SET NOCOUNT OFF;
END
go

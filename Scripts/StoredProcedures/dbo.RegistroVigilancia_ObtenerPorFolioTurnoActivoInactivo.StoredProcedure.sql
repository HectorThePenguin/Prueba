USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[RegistroVigilancia_ObtenerPorFolioTurnoActivoInactivo]'))
 DROP PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorFolioTurnoActivoInactivo]; 
GO

--======================================================
-- Author     : Luis Garcia
-- Create date: 05/10/2016
-- Description: Obtiene un registro de vigilancia por folioturno independientemente si esta activo o inactivo.
-- SpName     : RegistroVigilancia_ObtenerPorFolioTurnoActivoInactivo
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorFolioTurnoActivoInactivo]
@FolioTurno INT,
@OrganizacionID INT
AS
BEGIN
	SELECT 
		RegistroVigilanciaID,
		OrganizacionID,
		ProveedorIDMateriasPrimas,
		ContratoID,
		ProveedorChoferID,
		Transportista,
		Chofer,
		ProductoID,
		CamionID,
		Camion,
		Marca,
		Color,
		FolioTurno,
		FechaLlegada,
		FechaSalida,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM RegistroVigilancia (NOLOCK)
	WHERE FolioTurno = @FolioTurno AND OrganizacionID = @OrganizacionID
END

GO

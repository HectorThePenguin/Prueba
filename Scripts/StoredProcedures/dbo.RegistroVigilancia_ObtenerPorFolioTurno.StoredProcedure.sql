USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerPorFolioTurno]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorFolioTurno]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerPorFolioTurno]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 16/05/2014
-- Description: Obtiene un registro de vigilancia por folioturno
-- SpName     : RegistroVigilancia_ObtenerPorFolioTurno
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorFolioTurno]
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
	WHERE FolioTurno = @FolioTurno AND OrganizacionID = @OrganizacionID AND Activo = 1
END

GO

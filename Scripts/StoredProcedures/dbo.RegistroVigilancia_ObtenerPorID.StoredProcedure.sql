USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 16/05/2014
-- Description:  Obtiene un registro vigilancia por id
-- Origen: APInterfaces
-- RegistroVigilancia_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_ObtenerPorID]
@RegistroVigilanciaID INT
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
	WHERE RegistroVigilanciaID = @RegistroVigilanciaID
END

GO

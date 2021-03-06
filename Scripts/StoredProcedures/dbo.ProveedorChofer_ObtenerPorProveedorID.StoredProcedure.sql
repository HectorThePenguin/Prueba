USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorChofer_ObtenerPorProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pedro Delgado
-- Create date: 19/05/2014
-- Description:  Obtiene la lista de proveedorchofer por proveedor
-- ProveedorChofer_ObtenerPorProveedorID 1
-- =============================================
CREATE PROCEDURE [dbo].[ProveedorChofer_ObtenerPorProveedorID]
@ProveedorID INT
AS
BEGIN
	SELECT 
		ProveedorChoferID ,
		ProveedorID,
		ChoferID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM ProveedorChofer (NOLOCK)
	WHERE Activo = 1 AND ProveedorID = @ProveedorID
END

GO

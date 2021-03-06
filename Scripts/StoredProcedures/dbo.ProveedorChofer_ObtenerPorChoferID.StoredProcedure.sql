USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorChoferID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorChofer_ObtenerPorChoferID]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorChoferID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Eduardo Cota
-- Create date: 25/05/2014
-- Description:  Obtiene de los campos de la tabla ProveedorChofer" por ChoferID
-- ProveedorChofer_ObtenerPorChoferID 12
-- =============================================
CREATE PROCEDURE [dbo].[ProveedorChofer_ObtenerPorChoferID]
@ProveedorID INT,
@ChoferID INT
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
	WHERE Activo = 1 AND ProveedorID = @ProveedorID AND ChoferID = @ChoferID
END

GO

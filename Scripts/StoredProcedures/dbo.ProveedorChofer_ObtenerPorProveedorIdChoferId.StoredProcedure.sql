USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorProveedorIdChoferId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorChofer_ObtenerPorProveedorIdChoferId]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorProveedorIdChoferId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 27/06/2014
-- Description: Obtiene el proveedorChofer por chofer y proveedor
-- SpName     : ProveedorChofer_ObtenerPorProveedorIdChoferId 375, 1
--======================================================
CREATE PROCEDURE [dbo].[ProveedorChofer_ObtenerPorProveedorIdChoferId]
@ProveedorId INT,
@ChoferId INT
AS 
BEGIN
	SELECT 
		ProveedorChoferID, ProveedorID, ChoferID, Activo
	FROM ProveedorChofer (NOLOCK)
	WHERE ProveedorID = @ProveedorId AND ChoferID = @ChoferId
END

GO

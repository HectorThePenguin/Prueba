USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorChofer_ObtenerPorId]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_ObtenerPorId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 15/05/2014
-- Description: Obtiene el chofer proveedor
-- SpName     : ProveedorChofer_ObtenerPorId
--======================================================
CREATE PROCEDURE [dbo].[ProveedorChofer_ObtenerPorId]
@ProveedorChoferId INT
AS 
BEGIN
	SELECT 
		ProveedorChoferID, ProveedorID, ChoferID, Activo
	FROM ProveedorChofer (NOLOCK)
	WHERE ProveedorChoferID = @ProveedorChoferId
END

GO

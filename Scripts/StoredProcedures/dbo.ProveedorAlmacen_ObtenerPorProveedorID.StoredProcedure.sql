USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorAlmacen_ObtenerPorProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Obtiene un proveedor almacen por ProveedorID
-- SpName     : ProveedorAlmacen_ObtenerPorProveedorID 1
--======================================================
CREATE PROCEDURE [dbo].[ProveedorAlmacen_ObtenerPorProveedorID]
@ProveedorID INT
AS
BEGIN
	SELECT 
		ProveedorAlmacenID,
		ProveedorID,
		AlmacenID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM ProveedorAlmacen (NOLOCK) PA
	WHERE PA.ProveedorID = @ProveedorID
	and Activo = 1
END

GO

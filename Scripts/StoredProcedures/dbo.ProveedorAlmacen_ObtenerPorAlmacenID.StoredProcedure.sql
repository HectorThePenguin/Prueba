USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorAlmacen_ObtenerPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/07/2014
-- Description: Obtiene un proveedor almacen por Almacen ID
-- SpName     : ProveedorAlmacen_ObtenerPorAlmacenID 1
--======================================================
CREATE PROCEDURE [dbo].[ProveedorAlmacen_ObtenerPorAlmacenID]
@AlmacenID INT
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
	WHERE PA.AlmacenID = @AlmacenID
END

GO

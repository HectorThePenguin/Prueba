USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_ObtenerPorProveedorTipoAlmacen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorAlmacen_ObtenerPorProveedorTipoAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorAlmacen_ObtenerPorProveedorTipoAlmacen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 10/11/2014
-- Description: Obtiene un proveedor almacen por ProveedorID y por el tipo de Almacen
-- SpName     : ProveedorAlmacen_ObtenerPorProveedorTipoAlmacen 1,1
--======================================================
CREATE PROCEDURE [dbo].[ProveedorAlmacen_ObtenerPorProveedorTipoAlmacen]
@ProveedorID INT
,@TipoAlmacenID INT
,@Activo BIT
AS
BEGIN
	SELECT 
		pa.ProveedorAlmacenID,
		pa.ProveedorID,
		pa.AlmacenID,
		pa.Activo,
		pa.FechaCreacion,
		pa.UsuarioCreacionID
	FROM ProveedorAlmacen (NOLOCK) PA
	INNER JOIN Almacen a on PA.AlmacenID = a.AlmacenID
	WHERE PA.ProveedorID = @ProveedorID
	and a.TipoAlmacenID = @TipoAlmacenID
	and pa.Activo = @Activo
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoId]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 26/05/2014
-- Description: Obtiene un almacen movimiento detalle
-- SpName     : AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoId 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoId]
@AlmacenMovimientoID BIGINT
AS
BEGIN
	SELECT
		AM.AlmacenMovimientoCostoID,
		AM.AlmacenMovimientoID,
		AM.ProveedorID,
		P.CodigoSAP,
		P.Descripcion AS ProveedorDescripcion,
		AM.CuentaSAPID,
		CS.CuentaSAP,
		CS.Descripcion AS CuentaSapDescripcion,
		AM.CostoID,
		C.Descripcion AS CostoDescripcion,
		C.ClaveContable,
		AM.Iva,
		AM.Retencion,
		AM.Importe,
		AM.Cantidad,
		AM.Activo,
		AM.TieneCuenta,
		AM.FechaCreacion,
		AM.UsuarioCreacionID
	FROM AlmacenMovimientoCosto (NOLOCK) AM
	INNER JOIN Costo C ON C.CostoID = AM.CostoID
	LEFT JOIN Proveedor P ON P.ProveedorID = AM.ProveedorID
	LEFT JOIN CuentaSAP CS ON CS.CuentaSAPID = AM.CuentaSAPID
	WHERE AM.AlmacenMovimientoID = @AlmacenMovimientoID
END

GO

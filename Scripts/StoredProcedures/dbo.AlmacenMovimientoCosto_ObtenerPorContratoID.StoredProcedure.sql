USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 20/11/2014
-- Description: Obtiene los costos de un contrato
-- SpName     : AlmacenMovimientoCosto_ObtenerPorContratoID 57
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoID]
@ContratoID INT
AS
BEGIN
	SELECT DISTINCT
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
	inner join AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	WHERE amd.ContratoID = @ContratoID
END

GO

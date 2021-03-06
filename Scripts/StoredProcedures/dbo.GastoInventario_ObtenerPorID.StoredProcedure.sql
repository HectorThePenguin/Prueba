USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoInventario_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GastoInventario_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[GastoInventario_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GastoInventario_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[GastoInventario_ObtenerPorID]
@GastoInventarioID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		gi.GastoInventarioID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		gi.TipoGasto,
		gi.FolioGasto,
		gi.FechaGasto,
		co.CostoID,
		co.Descripcion AS Costo,
		co.ClaveContable,
		gi.TieneCuenta,
		cs.CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion AS DescripcionCuentaSAP,
		pro.ProveedorID,
		pro.CodigoSAP,
		pro.Descripcion AS Proveedor,
		gi.Factura,
		gi.Importe,
		gi.IVA,
		gi.Observaciones,
		gi.Retencion,
		gi.Activo
	FROM GastoInventario gi
	INNER JOIN Costo co on gi.CostoID = co.CostoID
	INNER JOIN Organizacion o on gi.OrganizacionID = o.OrganizacionID
	LEFT JOIN CuentaSAP cs on gi.CuentaSAPID = cs.CuentaSAPID
	LEFT JOIN Proveedor pro on gi.ProveedorID = pro.ProveedorID
	WHERE GastoInventarioID = @GastoInventarioID
	and gi.Activo = 1
	SET NOCOUNT OFF;
END

GO

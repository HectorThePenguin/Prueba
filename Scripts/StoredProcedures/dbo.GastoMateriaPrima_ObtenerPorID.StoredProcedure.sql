USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoMateriaPrima_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GastoMateriaPrima_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[GastoMateriaPrima_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GastoMateriaPrima_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[GastoMateriaPrima_ObtenerPorID] @GastoMateriaPrimaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT gmp.GastoMateriaPrimaID
		,o.OrganizacionID
		,o.Descripcion AS Organizacion
		,gmp.FolioGasto
		,tm.TipoMovimientoID
		,tm.Descripcion AS TipoMovimiento
		,gmp.Fecha
		,pro.ProductoID
		,pro.Descripcion AS Producto
		,gmp.TieneCuenta
		,cs.CuentaSAPID
		,cs.CuentaSAP
		,pr.ProveedorID
		,pr.Descripcion AS Proveedor
		,gmp.AlmacenMovimientoID
		,gmp.AlmacenInventarioLoteID
		,gmp.Importe
		,gmp.IVA
		,gmp.Observaciones
		,gmp.Activo
	FROM GastoMateriaPrima gmp
	INNER JOIN Organizacion o ON gmp.OrganizacionID = o.OrganizacionID
	INNER JOIN TipoMovimiento tm ON gmp.TipoMovimientoID = tm.TipoMovimientoID
	INNER JOIN Producto pro ON gmp.ProductoID = pro.ProductoID
	LEFT JOIN CuentaSAP cs ON gmp.CuentaSAPID = cs.CuentaSAPID
	LEFT JOIN Proveedor pr ON gmp.ProveedorID = pr.ProveedorID
	WHERE GastoMateriaPrimaID = @GastoMateriaPrimaID
	SET NOCOUNT OFF;
END

GO

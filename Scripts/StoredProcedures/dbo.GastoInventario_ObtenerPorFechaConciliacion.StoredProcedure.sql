USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoInventario_ObtenerPorFechaConciliacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GastoInventario_ObtenerPorFechaConciliacion]
GO
/****** Object:  StoredProcedure [dbo].[GastoInventario_ObtenerPorFechaConciliacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 13/11/2014
-- Description: 
-- SpName     : GastoInventario_ObtenerPorFechaConciliacion 1, '20141113', '20141113'
--======================================================
CREATE PROCEDURE [dbo].[GastoInventario_ObtenerPorFechaConciliacion]
@OrganizacionID	INT
, @FechaInicial DATE
, @FechaFinal	DATE
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
		ISNULL(cs.CuentaSAPID, 0) AS CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion AS DescripcionCuentaSAP,
		ISNULL(pro.ProveedorID, 0) AS ProveedorID,
		pro.CodigoSAP,
		pro.Descripcion AS Proveedor,
		gi.Factura,
		gi.Importe,
		gi.IVA,
		gi.Observaciones,
		gi.Retencion,
		gi.Activo
		, GI.CuentaGasto
		, GI.CentroCosto
		, GI.TotalCorrales
		, ISNULL(GI.CorralID, 0) AS CorralID
		, C.Codigo		
	FROM GastoInventario gi
	INNER JOIN Costo co 
		ON (gi.CostoID = co.CostoID)
	INNER JOIN Organizacion o 
		ON (gi.OrganizacionID = o.OrganizacionID)
	LEFT JOIN CuentaSAP cs 
		ON (gi.CuentaSAPID = cs.CuentaSAPID)
	LEFT JOIN Proveedor pro 
		ON (gi.ProveedorID = pro.ProveedorID)
	LEFT JOIN Corral C
		ON (GI.CorralID = C.CorralID)
	WHERE GI.Activo = 1
		AND CAST(FechaGasto AS DATE) BETWEEN @FechaInicial AND @FechaFinal
		AND O.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

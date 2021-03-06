USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP
-- =============================================
CREATE PROCEDURE [dbo].[GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT SP.GastoMateriaPrimaID
			,  SP.OrganizacionID
			,  SP.FolioGasto
			,  SP.TipoMovimientoID
			,  SP.Fecha
			,  SP.ProductoID
			,  SP.TieneCuenta
			,  ISNULL(SP.CuentaSAPID, 0)	AS CuentaSAPID
			,  ISNULL(SP.ProveedorID, 0)	AS ProveedorID
			,  SP.AlmacenMovimientoID
			,  ISNULL(SP.AlmacenInventarioLoteID, 0)	AS AlmacenInventarioLoteID
			,  SP.Importe
			,  SP.IVA
			,  SP.Observaciones
			,  Pro.Descripcion	AS Producto
			,  P.CodigoSAP		AS Proveedor
			,  CS.CuentaSAP
			,  CS.Descripcion	AS Cuenta
			,  AM.AlmacenID
		FROM 
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @XmlAlmacenMovimiento.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x
		INNER JOIN GastoMateriaPrima SP
		 ON (X.AlmacenMovimientoID = SP.AlmacenMovimientoID)		
		INNER JOIN AlmacenMovimiento AM
			ON (SP.AlmacenMovimientoID = AM.AlmacenMovimientoID)		
		INNER JOIN Producto Pro
			ON (SP.ProductoID = Pro.ProductoID)
		LEFT OUTER JOIN AlmacenInventarioLote AIL
			ON (SP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		LEFT OUTER JOIN Proveedor P
			ON (SP.ProveedorID = P.ProveedorID)
		LEFT OUTER JOIN CuentaSAP CS
			ON (SP.CuentaSAPID = CS.CuentaSAPID)
	SET NOCOUNT OFF;
END

GO

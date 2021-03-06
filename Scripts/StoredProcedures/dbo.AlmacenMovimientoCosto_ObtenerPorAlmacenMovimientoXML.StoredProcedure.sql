USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT AMC.AlmacenMovimientoCostoID
			,  AMC.AlmacenMovimientoID
			,  AMC.TieneCuenta
			,  ISNULL(AMC.ProveedorID,0)	AS ProveedorID
			,  ISNULL(AMC.CuentaSAPID, 0)	AS CuentaSAPID
			,  ISNULL(AMC.CostoID, 0)		AS CostoID
			,  AMC.Cantidad
			,  AMC.Importe
			,  P.CodigoSAP
			,  P.Descripcion				AS Proveedor
			,  ISNULL(CS.CuentaSAP, '')		AS CuentaSAP
			,  C.Descripcion				AS Costo
			,  AMC.FechaCreacion
		FROM AlmacenMovimientoCosto AMC
		INNER JOIN
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @XmlAlmacenMovimiento.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x	ON (AMC.AlmacenMovimientoID = X.AlmacenMovimientoID)
		LEFT OUTER JOIN Proveedor P
			ON (AMC.ProveedorID = P.ProveedorID)
		LEFT OUTER JOIN Costo C
			ON (AMC.CostoID = C.CostoID)
		LEFT OUTER JOIN CuentaSAP CS
			ON (AMC.CuentaSAPID = CS.CuentaSAPID)		
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoXML]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- AlmacenMovimientoCosto_ObtenerPorContratoXML
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoCosto_ObtenerPorContratoXML]
@XmlContrato XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT AMC.AlmacenMovimientoCostoID
			,  AMC.AlmacenMovimientoID
			,  AMC.TieneCuenta
			,  ISNULL(AMC.ProveedorID, 0)		AS ProveedorID
			,  ISNULL(AMC.CuentaSAPID, 0)	AS CuentaSAPID
			,  AMC.CostoID
			,  AMC.Cantidad
			,  AMC.Importe
			,  P.CodigoSAP
			,  P.Descripcion				AS Proveedor
			,  ISNULL(CS.CuentaSAP, '')		AS CuentaSAP
			,  C.Descripcion				AS Costo
			,  x.ContratoID
			,  AMD.FechaCreacion
		FROM AlmacenMovimientoCosto AMC
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (AMC.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		INNER JOIN
		(
			SELECT T.N.value('./ContratoID[1]','INT') AS ContratoID
			FROM @XmlContrato.nodes('/ROOT/Contrato') as T(N)
		) x	ON (AMD.ContratoID = X.ContratoID)
		INNER JOIN Costo C
			ON (AMC.CostoID = C.CostoID)
		LEFT OUTER JOIN Proveedor P
			ON (AMC.ProveedorID = P.ProveedorID)
		LEFT OUTER JOIN CuentaSAP CS
			ON (AMC.CuentaSAPID = CS.CuentaSAPID)		
	SET NOCOUNT OFF;
END

GO

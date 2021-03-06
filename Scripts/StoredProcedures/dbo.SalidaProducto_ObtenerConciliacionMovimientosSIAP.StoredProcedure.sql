USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- SalidaProducto_ObtenerConciliacionMovimientosSIAP
-- =============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerConciliacionMovimientosSIAP]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT SP.SalidaProductoID
			,  SP.OrganizacionID
			,  SP.OrganizacionIDDestino 
			,  SP.TipoMovimientoID
			,  SP.FolioSalida
			,  SP.FolioFactura
			,  SP.AlmacenID
			,  SP.AlmacenInventarioLoteID
			,  ISNULL(SP.ClienteID, 0)		AS ClienteID
			,  ISNULL(SP.CuentaSAPID, 0)	AS CuentaSAPID
			,  SP.Observaciones
			,  SP.Precio
			,  SP.Importe
			,  SP.AlmacenMovimientoID
			,  SP.PesoTara
			,  SP.PesoBruto
			,  SP.Piezas
			,  SP.FechaSalida
			,  A.Descripcion	AS Almacen
			,  C.CodigoSAP		AS Cliente
			,  CS.CuentaSAP
			,  CS.Descripcion	AS Cuenta
			,  AIL.Lote
			,  AMD.ProductoID
		FROM 
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @XmlAlmacenMovimiento.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x
		INNER JOIN SalidaProducto SP
		 ON (X.AlmacenMovimientoID = SP.AlmacenMovimientoID)
		INNER JOIN Almacen A
			ON (SP.AlmacenID = A.AlmacenID)
		INNER JOIN AlmacenInventarioLote AIL
			ON (SP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (SP.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		LEFT OUTER JOIN Cliente C
			ON (SP.ClienteID = C.ClienteID)
		LEFT OUTER JOIN CuentaSAP CS
			ON (SP.CuentaSAPID = CS.CuentaSAPID)
	SET NOCOUNT OFF;
END

GO

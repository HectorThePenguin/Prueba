USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubProductos_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubProductos_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubProductos_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- SubProductos_ObtenerConciliacionMovimientosSIAP
-- =============================================
CREATE PROCEDURE [dbo].[SubProductos_ObtenerConciliacionMovimientosSIAP]
@AlmacenMovimientoXML XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT AMD.AlmacenMovimientoDetalleID
			,  AMD.AlmacenMovimientoID
			,  AMD.FechaModificacion
			,  PreDet.ProductoID
			,  PreDet.Porcentaje
			,  P.Descripcion
		FROM AlmacenMovimientoDetalle AMD(NOLOCK)
		INNER JOIN Premezcla Pre
			ON (AMD.ProductoID = Pre.ProductoID)
		INNER JOIN PremezclaDetalle PreDet
			ON (Pre.PremezclaID = PreDet.PremezclaID)
		INNER JOIN Producto P
			ON (PreDet.ProductoID = P.ProductoID)
		INNER JOIN
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @AlmacenMovimientoXML.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x	ON (AMD.AlmacenMovimientoID = X.AlmacenMovimientoID)
	SET NOCOUNT OFF;
END

GO

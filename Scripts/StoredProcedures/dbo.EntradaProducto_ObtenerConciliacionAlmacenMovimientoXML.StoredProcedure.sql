USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 05/11/2014
-- Description:  Obtiene un proveedor por producto
-- EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML]
@AlmacenMovimientoXML XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT EP.EntradaProductoID
			,  EP.ContratoID
			,  EP.OrganizacionID
			,  EP.ProductoID
			,  EP.RegistroVigilanciaID
			,  EP.Folio
			,  EP.Fecha
			,  EP.FechaDestara
			,  EP.Observaciones
			,  EP.OperadorIDAnalista
			,  EP.PesoBonificacion
			,  EP.PesoOrigen
			,  EP.PesoBruto
			,  EP.PesoTara
			,  CAST(EP.PesoDescuento AS DECIMAL) PesoDescuento
			,  EP.Piezas
			,  EP.TipoContratoID
			,  EP.EstatusID
			,  EP.Justificacion
			,  EP.OperadorIDBascula
			,  EP.OperadorIDAlmacen
			,  EP.OperadorIDAutoriza
			,  EP.FechaInicioDescarga
			,  EP.FechaFinDescarga
			,  EP.AlmacenInventarioLoteID
			,  EP.AlmacenMovimientoID
			,  AM.AlmacenID
			,  P.Descripcion AS Producto
		FROM EntradaProducto EP
		INNER JOIN
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @AlmacenMovimientoXML.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x	ON (EP.AlmacenMovimientoID = X.AlmacenMovimientoID)
		INNER JOIN AlmacenMovimiento AM
			ON (x.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		INNER JOIN Producto P
			ON (EP.ProductoID = P.ProductoID)
		WHERE EP.Activo = 1
	SET NOCOUNT OFF;
END

GO

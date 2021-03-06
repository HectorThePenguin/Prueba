USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerCostosAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerCostosAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerCostosAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 28/07/2014
-- Description: Obtiene las entrada por folio
-- SpName     : EntradaProducto_ObtenerCostosAlmacen 25, 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerCostosAlmacen]
@ContratoID INT
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT EP.EntradaProductoID
			,  EP.OrganizacionID
			,  EP.RegistroVigilanciaID
			,  EP.Folio
			,  EP.Fecha
			,  EPC.Observaciones
			--,  EP.Observaciones
			,  EP.PesoOrigen
			,  EP.PesoBruto
			,  EP.PesoTara
			,  EP.Piezas
			,  EP.TipoContratoID
			,  EP.AlmacenInventarioLoteID
			,  EP.AlmacenMovimientoID
			,  EP.ProductoID
			,  AM.AlmacenMovimientoCostoID
			,  AM.Cantidad AS CantidadAlmacen
			,  AM.CostoID
			,  AM.Importe AS ImporteAlmacen
			,  AM.ProveedorID	
			,  AIL.Lote			
		FROM EntradaProducto EP(NOLOCK)
		INNER JOIN EntradaProductoCosto EPC
			ON (EP.EntradaProductoID = EPC.EntradaProductoID)
		INNER JOIN AlmacenMovimientoCosto AM(NOLOCK)
			ON (EP.AlmacenMovimientoID = AM.AlmacenMovimientoID)
		INNER JOIN Producto P(NOLOCK)
			ON (EP.ProductoID = P.ProductoID)
		INNER JOIN AlmacenInventarioLote AIL(NOLOCK)
			ON (EP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
		WHERE EP.ContratoID = @ContratoID
			AND EP.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF
END

GO

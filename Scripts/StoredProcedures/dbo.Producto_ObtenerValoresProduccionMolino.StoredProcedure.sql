USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerValoresProduccionMolino]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerValoresProduccionMolino]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerValoresProduccionMolino]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 18/06/2014
-- Description: Obtiene los Productos filtrados por la Familia
-- SpName     : Producto_ObtenerValoresProduccionMolino 80
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerValoresProduccionMolino] @ProductoID INT	
AS
SELECT ail.Lote
	,cmp.Resultado as HumedadForraje 
FROM Producto pro
INNER JOIN AlmacenInventario ai on pro.ProductoID = ai.ProductoID
INNER JOIN AlmacenInventarioLote ail on ai.AlmacenInventarioID = ail.AlmacenInventarioID
INNER JOIN PedidoDetalle pd on ail.AlmacenInventarioLoteID = pd.InventarioLoteIDDestino
INNER JOIN CalidadMateriaPrima cmp on pd.PedidoDetalleID = cmp.PedidoDetalleID
WHERE ail.Activo = 1	
	AND ai.ProductoID = @ProductoID

GO

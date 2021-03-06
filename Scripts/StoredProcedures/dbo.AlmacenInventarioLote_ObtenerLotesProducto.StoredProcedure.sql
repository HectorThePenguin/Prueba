USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesProducto]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 23-08-2014 
-- Description: sp para regresar los AlmacenInventarioLote de un producto, que hayan tenido movimientos
-- SpName     : AlmacenInventarioLote_ObtenerLotesProducto 1,4,81,0
--======================================================  
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesProducto] @OrganizacionID INT
	,@AlmacenID INT
	,@ProductoID INT
	,@Lote INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT ail.AlmacenInventarioLoteID
		,ail.Lote
		,ail.Cantidad
	FROM Almacen a
	INNER JOIN AlmacenInventario ai ON a.AlmacenID = ai.AlmacenID
	INNER JOIN AlmacenInventarioLote ail ON ai.AlmacenInventarioID = ail.AlmacenInventarioID
	WHERE a.OrganizacionID = @OrganizacionID
		AND A.AlmacenID = @AlmacenID
		AND @Lote IN (ail.Lote,0)
		AND ai.ProductoID = @ProductoID
		and ail.Activo = 1
		and ail.Cantidad > 0
	GROUP BY ail.AlmacenInventarioLoteID,ail.Lote
		,ail.Cantidad
	SET NOCOUNT OFF
END

GO

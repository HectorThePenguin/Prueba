USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID 72010
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID]
@AlmacenMovimientoID BIGINT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT AMD.AlmacenMovimientoDetalleID
			,  AMD.AlmacenMovimientoID
			,  AMD.AlmacenInventarioLoteID
			,  AMD.ContratoID
			,  AMD.Piezas
			,  AMD.TratamientoID
			,  AMD.ProductoID
			,  AMD.Precio
			,  AMD.Cantidad
			,  AMD.Importe
			,  P.Descripcion AS Producto
		FROM AlmacenMovimientoDetalle AMD
		INNER JOIN Producto P
			ON (AMD.ProductoID = P.ProductoID)
		WHERE AMD.AlmacenMovimientoID = @AlmacenMovimientoID
	SET NOCOUNT OFF;
END

GO

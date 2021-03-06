USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorContrato]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorContrato]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 26/05/2014
-- Description: Obtiene el detalle de movimientos por contrato
-- SpName     : EXEC AlmacenMovimientoDetalle_ObtenerPorContrato 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorContrato]
@ContratoID INT
AS
BEGIN
	SELECT 
		AMD.AlmacenMovimientoDetalleID,
		AMD.AlmacenMovimientoID,
		AMD.AlmacenInventarioLoteID,
		AMD.ContratoID,
		AMD.Piezas,
		AMD.TratamientoID,
		AMD.ProductoID,
		AMD.Precio,
		AMD.Cantidad,
		AMD.Importe,
		AMD.FechaCreacion,
		AMD.UsuarioCreacionID,
		AMD.FechaModificacion,
		AMD.UsuarioModificacionID
	FROM AlmacenMovimientoDetalle (NOLOCK) AMD
	INNER JOIN AlmacenMovimiento (NOLOCK) AM ON(AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID)
	WHERE AMD.ContratoID = @ContratoID 
END

GO

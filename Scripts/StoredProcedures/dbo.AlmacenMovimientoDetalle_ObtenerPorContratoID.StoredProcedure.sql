USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 27/05/2014
-- Description: Obtiene el detalle de movimientos por contrato id
-- SpName     : EXEC AlmacenMovimientoDetalle_ObtenerPorContratoID 8
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorContratoID]
@ContratoID INT
AS
BEGIN
	SELECT TOP 1
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

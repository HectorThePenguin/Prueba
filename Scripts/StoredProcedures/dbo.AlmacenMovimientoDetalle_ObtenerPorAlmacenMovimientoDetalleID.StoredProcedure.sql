USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoDetalleID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoDetalleID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoDetalleID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 30/06/2014
-- Description: Obtiene un registro por almacenmovimientodetalleid
-- SpName     : EXEC AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoDetalleID 8
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoDetalleID]
@AlmacenMovimientoDetalleID INT
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
	WHERE AMD.AlmacenMovimientoDetalleID = @AlmacenMovimientoDetalleID
END

GO

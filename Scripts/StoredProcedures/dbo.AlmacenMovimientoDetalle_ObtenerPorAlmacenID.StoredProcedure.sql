USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 05/04/2014
-- Description: Obtiene el detalle de movimientos por almacenid
-- SpName     : EXEC AlmacenMovimientoDetalle_ObtenerPorAlmacenID 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_ObtenerPorAlmacenID]
@AlmacenID INT
AS
BEGIN
	SELECT 
		AMD.AlmacenMovimientoDetalleID,
		AMD.AlmacenMovimientoID,
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
	WHERE AM.AlmacenID = @AlmacenID 
END

GO

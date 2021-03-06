USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerMovimientosPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ObtenerMovimientosPorID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerMovimientosPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 31/07/2014
-- Description:  Obtiene el movimiento de almacen, con todos sus detalles
-- AlmacenMovimiento_ObtenerMovimientosPorID 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ObtenerMovimientosPorID] @AlmacenMovimientoID INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT AlmacenMovimientoID
		,a.AlmacenID
		,ta.TipoAlmacenID
		,TipoMovimientoID
		,ProveedorID
		,FolioMovimiento
		,Observaciones
		,FechaMovimiento
		,STATUS
		,AnimalMovimientoID
		,PolizaGenerada
	FROM AlmacenMovimiento AM
	INNER JOIN Almacen a on am.AlmacenID = a.AlmacenID
	INNER JOIN TipoAlmacen ta on a.TipoAlmacenID = ta.TipoAlmacenID
	WHERE AM.AlmacenMovimientoID = @AlmacenMovimientoID
	SELECT AMD.AlmacenMovimientoDetalleID
		,AMD.AlmacenMovimientoID
		,AMD.AlmacenInventarioLoteID
		,AMD.ContratoID
		,AMD.Piezas
		,AMD.TratamientoID
		,P.ProductoID
		,P.Descripcion AS Producto
		,SF.SubFamiliaID
		,SF.Descripcion AS SubFamilia
		,F.FamiliaID
		,F.Descripcion AS Familia
		,AMD.Precio
		,AMD.Cantidad
		,AMD.Importe
	FROM AlmacenMovimiento AM
	INNER JOIN AlmacenMovimientoDetalle AMD ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	INNER JOIN Producto P ON (AMD.ProductoID = P.ProductoID)
	INNER JOIN SubFamilia SF ON (P.SubFamiliaID = SF.SubFamiliaID)
	INNER JOIN Familia F ON (SF.FamiliaID = F.FamiliaID)
	WHERE AM.AlmacenMovimientoID = @AlmacenMovimientoID
	SET NOCOUNT OFF
END

GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[AlmacenMovimiento_ObtenerMovimientosPorContrato]')
		)
	DROP PROCEDURE [dbo].AlmacenMovimiento_ObtenerMovimientosPorContrato
GO

-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 28/09/2015
-- Description:  Obtiene los movimientos con sus detalles de un Contrato
-- AlmacenMovimiento_ObtenerMovimientosPorContrato 796
-- =============================================
CREATE PROCEDURE dbo.AlmacenMovimiento_ObtenerMovimientosPorContrato 
@ContratoID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT am.AlmacenMovimientoID
		,a.AlmacenID
		,a.Descripcion AS Almacen
		,ta.TipoAlmacenID
		,ta.Descripcion AS TipoAlmacen
		,am.TipoMovimientoID
		,am.ProveedorID
		,am.FolioMovimiento
		,am.Observaciones
		,am.FechaMovimiento
		,am.STATUS
		,am.AnimalMovimientoID
		,am.PolizaGenerada
	FROM AlmacenMovimiento AM
	INNER JOIN Almacen a on am.AlmacenID = a.AlmacenID
	INNER JOIN TipoAlmacen ta on a.TipoAlmacenID = ta.TipoAlmacenID
	INNER JOIN AlmacenMovimientoDetalle amd on am.AlmacenMovimientoID = amd.AlmacenMovimientoID 
	WHERE amd.ContratoID = @ContratoID

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
	WHERE AMD.ContratoID = @ContratoID

	SET NOCOUNT OFF
END

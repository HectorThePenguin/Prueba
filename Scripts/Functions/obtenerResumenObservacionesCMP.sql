IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[obtenerResumenObservacionesCMP]')
		)
	DROP FUNCTION [dbo].[obtenerResumenObservacionesCMP]
GO

CREATE FUNCTION obtenerResumenObservacionesCMP (
	@OrganizacionID INT
	,@ProductoID INT
	,@IndicadorProductoID INT
	,@Fecha DATE
	)
RETURNS VARCHAR(255)
AS
BEGIN
	DECLARE @cadena VARCHAR(255)

	SELECT TOP 1 @cadena = CMP.Observaciones
	FROM CalidadMateriaPrima CMP
	INNER JOIN PedidoDetalle PD ON CMP.PedidoDetalleID = PD.PedidoDetalleID
	INNER JOIN Pedido Ped ON PD.PedidoID = Ped.PedidoID
	INNER JOIN Producto P ON PD.ProductoID = P.ProductoID
	INNER JOIN IndicadorProducto IP ON P.ProductoID = IP.ProductoID
	INNER JOIN Indicador I ON IP.IndicadorID = I.IndicadorID
	INNER JOIN IndicadorProductoCalidad IPC ON IPC.IndicadorID = i.IndicadorID
	INNER JOIN IndicadorObjetivo IOB ON IPC.IndicadorProductoCalidadID = IOB.IndicadorProductoCalidadID
	WHERE P.ProductoID = @ProductoID
		AND IP.IndicadorProductoID = @IndicadorProductoID
		AND CMP.IndicadorObjetivoID = IOB.IndicadorObjetivoID
		AND Ped.Organizacionid = @OrganizacionID
		AND cast(CMP.FechaCreacion AS DATE) = @Fecha
		AND CMP.Observaciones != ''

	RETURN isnull(@cadena, '')
END

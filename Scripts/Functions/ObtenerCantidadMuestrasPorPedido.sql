 IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerCantidadMuestrasPorPedido]'))
		DROP FUNCTION [dbo].[ObtenerCantidadMuestrasPorPedido]
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/27
-- Description: Obtiene la cantidad de muestras que tiene el folio pedido
-- select dbo.ObtenerCantidadMuestrasPorPedido (1156)
--=============================================
CREATE FUNCTION ObtenerCantidadMuestrasPorPedido(@EntradaProductoId INT)
	RETURNS INT
AS
BEGIN
	DECLARE @CantidadPesaje INT

	 SELECT @CantidadPesaje = COUNT(*) FROM EntradaProducto(NOLOCK) AS EP
		INNER JOIN  EntradaProductoDetalle(NOLOCK) AS EPD
		 ON (EP.EntradaProductoID = EPD.EntradaProductoID)
		 INNER JOIN EntradaProductoMuestra (NOLOCK) AS EPM
		 ON (EPD.EntradaProductoDetalleID = EPM.EntradaProductoDetalleID)
		 AND EP.EntradaProductoID = @EntradaProductoId

	 Return ISNULL(@CantidadPesaje, 0)
END
GO
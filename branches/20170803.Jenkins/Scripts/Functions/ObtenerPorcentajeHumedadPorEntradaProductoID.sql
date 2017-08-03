IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerPorcentajeHumedadPorEntradaProductoID]'))
		DROP FUNCTION [dbo].[ObtenerPorcentajeHumedadPorEntradaProductoID]
GO
CREATE FUNCTION ObtenerPorcentajeHumedadPorEntradaProductoID(@EntradaProductoID BIGINT)
Returns DECIMAL(12,4)
AS
BEGIN
	DECLARE @PorcentajeHumedad DECIMAL(12,4)
	DECLARE @IndicadorHumedad INT
	
	SET @IndicadorHumedad = 4

	SET @PorcentajeHumedad = (SELECT SUM(Porcentaje) / COUNT(1)
														FROM EntradaProducto EP (NOLOCK)
														INNER JOIN EntradaProductoDetalle EPD (NOLOCK) ON (EP.EntradaProductoID = EPD.EntradaProductoID)
														INNER JOIN EntradaProductoMuestra EPM (NOLOCK) ON (EPD.EntradaProductoDetalleID = EPM.EntradaProductoDetalleID)
														WHERE EPD.IndicadorID = @IndicadorHumedad
														AND EP.EntradaProductoID = @EntradaProductoID)

	Return isnull(@PorcentajeHumedad, 0.0)
END

GO
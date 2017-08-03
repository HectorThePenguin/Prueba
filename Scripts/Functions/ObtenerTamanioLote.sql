IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerTamanioLote]')
		)
	DROP FUNCTION [dbo].[ObtenerTamanioLote]
GO

-- =============================================  
-- Autor:  Jorge Luis Velazquez Araujo
-- Create date: 30/06/2014
-- Description: Funcion para consultar el tamanio de un lote
-- select ObtenerTamanioLote 1,1,1
-- =============================================  
CREATE FUNCTION dbo.ObtenerTamanioLote (
	@AlmacenID INT
	,@AlmacenInventarioID INT
	,@Lote INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @TamanioLote INT
	DECLARE @SumaMovimientos INT
	DECLARE @TipoMovimientoSalidaTraspaso INT

	SET @TipoMovimientoSalidaTraspaso = 22
	SET @TamanioLote = (
			SELECT Round(Cantidad,0)
			FROM AlmacenInventarioLote
			WHERE AlmacenInventarioID = @AlmacenInventarioID
				AND Lote = @Lote
				AND Activo = 1
			)
	SET @SumaMovimientos = (
			SELECT Round(sum(amd.Cantidad),0)
			FROM AlmacenMovimiento am
			INNER JOIN AlmacenMovimientoDetalle amd ON am.AlmacenMovimientoID = amd.AlmacenMovimientoID
			WHERE am.AlmacenID = @AlmacenID
				AND am.TipoMovimientoID = @TipoMovimientoSalidaTraspaso
				AND amd.AlmacenInventarioLoteID = @AlmacenInventarioID
			GROUP BY am.AlmacenID
			)

	RETURN isnull(@TamanioLote, 0) + isnull(@SumaMovimientos, 0)
END

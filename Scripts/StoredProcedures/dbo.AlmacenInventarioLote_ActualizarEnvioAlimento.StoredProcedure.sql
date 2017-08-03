--======================================================  
-- Author     : Edwin Martin Angulo Juarez
-- Create date: 22-10-20146
-- Description: sp para acutalizar el inventario del lote cuando se realiza
--				un envio de mercanccía.
-- SpName     : AlmacenInventarioLote_ActualizarEnvioAlimento 
--======================================================  
IF EXISTS (SELECT  object_id FROM    sys.objects WHERE   object_id = OBJECT_ID(N'AlmacenInventarioLote_ActualizarEnvioAlimento') AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE AlmacenInventarioLote_ActualizarEnvioAlimento
END
GO
CREATE PROCEDURE AlmacenInventarioLote_ActualizarEnvioAlimento
	@AlmacenInventarioLoteID INT,
	@Cantidad DECIMAL(18, 2),
	@PrecioPromedio DECIMAL(18,4),
	@Piezas INT,
	@Importe DECIMAL(24, 2),
	@Activo INT,
	@UsuarioModificacionID INT
AS 
BEGIN
	
	UPDATE AlmacenInventarioLote 
	SET Cantidad = @Cantidad,
		PrecioPromedio = @PrecioPromedio,
		Piezas = @Piezas, 
		Importe = @Importe,
		FechaInicio = CASE WHEN @Cantidad = 0  THEN GETDATE() ELSE FechaInicio END,
		FechaFin = CASE WHEN @Cantidad = 0 AND @Importe = 0 THEN GETDATE() ELSE FechaFin END,
		Activo =  CASE WHEN @Cantidad = 0 AND @Importe = 0 THEN @Activo ELSE Activo END,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE AlmacenInventarioLoteID = @AlmacenInventarioLoteId
END
GO
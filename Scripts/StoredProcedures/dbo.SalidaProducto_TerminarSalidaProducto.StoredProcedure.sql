USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[SalidaProducto_TerminarSalidaProducto]'))
 DROP PROCEDURE [dbo].[SalidaProducto_TerminarSalidaProducto]; 
GO

--=============================================
-- Author     : Roque Solis, Franco Jesus inzunza martinez
-- Create date: 26/06/2014
-- Modification date: 06/06/2016
-- Description: Termina la salida del producto con o sin generar factura
-- EXEC SalidaProducto_TerminarSalidaProducto 1,1,200,5.5,1,1
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_TerminarSalidaProducto]
    @SalidaProductoID INT,
	@AlmacenMovimientoID INT,
	@Activo INT,
	@UsuarioModificacionID INT,
	@OrganizacionID INT,
	@RequiereFactura BIT = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FolioFactura VARCHAR(15)
	DECLARE @Serie VARCHAR(5)
	DECLARE @Folio VARCHAR(10)
	
	IF(@RequiereFactura = 1) 
	BEGIN 
		-- Obtiene el numero que sigue de la factura segun el parametro configurado para cada organizacion.
		EXEC FolioFactura_Obtener @OrganizacionID, @FolioFactura OUTPUT, @Serie OUTPUT, @Folio OUTPUT
	END
	
	UPDATE SalidaProducto
	SET
        AlmacenMovimientoID = @AlmacenMovimientoID,
        Activo = @Activo,
		FechaModificacion = GETDATE(),	
		UsuarioModificacionID = @UsuarioModificacionID,
		FolioFactura = @FolioFactura
		WHERE SalidaProductoID = @SalidaProductoID
	SET NOCOUNT OFF;
END

GO
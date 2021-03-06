USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ActualizarSegundoPesaje]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ActualizarSegundoPesaje]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ActualizarSegundoPesaje]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 24/06/2014
-- Description: Actualiza el registro con folio en la tabla "Salida Producto." con el peso bruto
-- EXEC SalidaProducto_ActualizarSegundoPesaje 1,1,200,5.5,1
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ActualizarSegundoPesaje]
    @SalidaProductoID INT,
	@CuentaSAPID INT,
	@PesoBruto INT,
	@Precio decimal(14,2),
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	IF @CuentaSAPID = 0
	BEGIN
		SET @CuentaSAPID = NULL;
	END
	UPDATE SalidaProducto
	SET
        CuentaSAPID = @CuentaSAPID,
        PesoBruto = @PesoBruto,
        Precio = @Precio,
		Importe = (@Precio* (@PesoBruto - PesoTara)),
		FechaModificacion = GETDATE(),	
		UsuarioModificacionID = @UsuarioModificacionID
		WHERE SalidaProductoID = @SalidaProductoID
	SET NOCOUNT OFF;
END

GO

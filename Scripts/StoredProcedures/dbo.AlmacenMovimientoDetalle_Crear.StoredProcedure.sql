USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Crea un nuevo almacen inventario
-- AlmacenMovimientoDetalle_Crear
--=============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoDetalle_Crear]
	@AlmacenMovimientoID BIGINT,
	@AlmacenInventarioLoteID INT,
	@ContratoID INT,
	@Piezas INT,
	@TratamientoID INT,
	@ProductoID INT,
	@Precio DECIMAL(18,4),
	@Cantidad DECIMAL(18,2),
	@Importe DECIMAL(24,2),
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT AlmacenMovimientoDetalle(
		AlmacenMovimientoID,
		AlmacenInventarioLoteID,
		ContratoID,
		Piezas,
		TratamientoID,
		ProductoID,
		Precio,
		Cantidad,
		Importe,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@AlmacenMovimientoID,
		CASE
            WHEN @AlmacenInventarioLoteID = 0
            THEN NULL
			ELSE @AlmacenInventarioLoteID
		END,
		@ContratoID,
		@Piezas,
		CASE
            WHEN @TratamientoID = 0
            THEN NULL
			ELSE @TratamientoID
		END,
		@ProductoID,
		@Precio,
		@Cantidad,
		@Importe,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

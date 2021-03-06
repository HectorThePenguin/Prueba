USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoCosto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Crea un nuevo almacen movimiento costo
-- AlmacenMovimientoCosto_Crear 9, 4842, 35, 1000, 1, 5 
--=============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoCosto_Crear]
	@AlmacenMovimientoID BIGINT,
	@ProveedorID INT,
	@CuentaSAPID INT,
	@CostoID INT,
	@Importe DECIMAL(17,2),
	@Cantidad DECIMAL(14,2),
	@Iva BIT,
	@Retencion BIT,
	@Activo INT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	if @ProveedorID = 0 BEGIN 
	set @ProveedorID = null
	end
	if @CuentaSAPID = 0 BEGIN 
	set @CuentaSAPID = null
	end
	INSERT AlmacenMovimientoCosto(
		AlmacenMovimientoID,
		ProveedorID,
		CuentaSAPID,
		CostoID,
		Importe,
		Cantidad,
		Iva,
		Retencion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@AlmacenMovimientoID,
		@ProveedorID,
		@CuentaSAPID,
		@CostoID,
		@Importe,
		@Cantidad,
		@Iva,
		@Retencion,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

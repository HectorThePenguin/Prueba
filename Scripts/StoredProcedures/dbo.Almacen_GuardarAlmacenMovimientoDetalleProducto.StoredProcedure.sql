USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimientoDetalleProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimientoDetalleProducto]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimientoDetalleProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Ramses Santos
-- Create date: 22/05/2014
-- Description:  Guardar el Almacen Movimiento
-- Origen: APInterfaces
-- Almacen_GuardarAlmacenMovimientoDetalleProducto 1,null,12,1,"Observaciones",1
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimientoDetalleProducto]
	@AlmacenMovimientoID BIGINT,
	@ProductoID INT,
	@Precio DECIMAL(18,4),
	@Cantidad DECIMAL(18,2),
	@Importe DECIMAL(24, 2),
	@AlmacenInventarioLoteID INT,
	@ContratoID INT,
	@Piezas INT,
	@UsuarioCreacionID INT
AS
  BEGIN
    SET NOCOUNT ON
	DECLARE @IdentityID BIGINT;
	INSERT INTO AlmacenMovimientoDetalle(
	AlmacenMovimientoID, AlmacenInventarioLoteID, ContratoID, Piezas, TratamientoID, ProductoID, Precio, Cantidad,
	Importe, FechaCreacion, UsuarioCreacionID) 
	SELECT @AlmacenMovimientoID, CASE WHEN @AlmacenInventarioLoteID > 0 THEN @AlmacenInventarioLoteID ELSE NULL END, @ContratoID, @Piezas, null, @ProductoID, @Precio, @Cantidad,
			@Importe, GETDATE(), @UsuarioCreacionID
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY)
	SELECT AlmacenMovimientoDetalleID, AlmacenMovimientoID, AlmacenInventarioLoteID, ContratoID, Piezas, 
	TratamientoID, ProductoID, Precio, Cantidad, Importe, FechaCreacion, UsuarioCreacionID FROM AlmacenMovimientoDetalle
	WHERE AlmacenMovimientoDetalleID = @IdentityID
	SET NOCOUNT OFF
  END

GO

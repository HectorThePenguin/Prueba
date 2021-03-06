USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_CrearCostos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimientoCosto_CrearCostos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimientoCosto_CrearCostos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Crea un nuevo almacen movimiento costo
-- 001 se agregan las nuevas columnas de Iva y Retencion
-- AlmacenMovimientoCosto_CrearCostos 
--=============================================
CREATE PROCEDURE [dbo].[AlmacenMovimientoCosto_CrearCostos]
@XmlAlmacenMovimientoCosto XML
AS
BEGIN
	DECLARE @tmpAlmacenMovimientoCosto AS TABLE
	(
		AlmacenMovimientoID BIGINT,
		ProveedorID INT,
		CuentaSAPID INT,
		CostoID INT,
		Importe DECIMAL(17,2),
		Cantidad DECIMAL(18,2),
		Activo INT,
		TieneCuenta INT,	
		Iva BIT, --001
		Retencion BIT, --001
		UsuarioCreacionID INT
	)
	INSERT @tmpAlmacenMovimientoCosto(
	    AlmacenMovimientoID,
		ProveedorID,
		CuentaSAPID,
		CostoID,
		Importe,
		Cantidad,
		Activo,
		TieneCuenta,
		Iva, --001
		Retencion, --001
		UsuarioCreacionID
		)
	SELECT 
		AlmacenMovimientoID = T.item.value('./AlmacenMovimientoID[1]', 'BIGINT'),
		CASE WHEN T.item.value('./ProveedorID[1]', 'INT') > 0 THEN T.item.value('./ProveedorID[1]', 'INT') ELSE NULL END,
		CASE WHEN T.item.value('./CuentaSAPID[1]', 'INT') > 0 THEN T.item.value('./CuentaSAPID[1]', 'INT') ELSE NULL END,
		CostoID = T.item.value('./CostoID[1]', 'INT'),
		Importe = T.item.value('./Importe[1]', 'DECIMAL(17,2)'),
		Cantidad = T.item.value('./Cantidad[1]', 'DECIMAL(18,2)'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		TieneCuenta = T.item.value('./TieneCuenta[1]', 'INT'),
		Iva = T.item.value('./Iva[1]', 'BIT'), --001
		Retencion = T.item.value('./Retencion[1]', 'BIT'), --001
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlAlmacenMovimientoCosto.nodes('ROOT/XmlAlmacenMovimientoCosto') AS T(item)
			/* Se crea registro en la tabla de Orden sacrificio*/
			INSERT INTO AlmacenMovimientoCosto(
				AlmacenMovimientoID,
				ProveedorID,
				CuentaSAPID,
				CostoID,
				Importe,
				Cantidad,
				Activo,
				TieneCuenta,
				Iva, --001
				Retencion, --001
				FechaCreacion,
			    UsuarioCreacionID
				)
			SELECT AlmacenMovimientoID,
				   ProveedorID,
				   CuentaSAPID,
				   CostoID,
				   Importe,
				   Cantidad,
				   Activo,
				   TieneCuenta,
				   Iva, --001
				   Retencion, --001
				   GETDATE(),
				   UsuarioCreacionID
			FROM @tmpAlmacenMovimientoCosto
END

GO

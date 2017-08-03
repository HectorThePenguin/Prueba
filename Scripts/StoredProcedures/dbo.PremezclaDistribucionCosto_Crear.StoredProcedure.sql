USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucionCosto_Crear]    ******/
DROP PROCEDURE [dbo].[PremezclaDistribucionCosto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDistribucionCosto_Crear]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Eric García
-- Create date: 01/12/2015
-- Description: 
/* SpName     : PremezclaDistribucionCosto_Crear '<ROOT>
- <PremezclaDistribucionCosto>
  <PremezclaDistribucionID>616</PremezclaDistribucionID> 
  <Costo>4</Costo> 
  <TieneCuenta>true</TieneCuenta> 
  <Proveedor>0</Proveedor> 
  <CuentaProvision>2005281061</CuentaProvision> 
  <Importe>100</Importe> 
  <Iva>false</Iva> 
  <Retencion>false</Retencion> 
  <Activo>1</Activo> 
  <UsuarioCreacionID>5</UsuarioCreacionID> 
  <UsuarioModificacionID /> 
  </PremezclaDistribucionCosto>
  </ROOT>'*/
--======================================================
CREATE PROCEDURE [dbo].[PremezclaDistribucionCosto_Crear]
    @PremezclaDistribucionCostoXML XML
AS
BEGIN
	DECLARE @tmpPremezclaDistribucionCosto AS TABLE
	(
		PremezclaDistribucionID INT,
		Costo INT,
		TieneCuenta BIT,
		Proveedor INT,
		CuentaProvision VARCHAR(10),
		Importe DECIMAL(10,2),
		Iva BIT,
		Retencion BIT,
		Activo BIT,
		UsuarioCreacionID INT
	)
	INSERT @tmpPremezclaDistribucionCosto
	(
		PremezclaDistribucionID,
		Costo,
		TieneCuenta,
		Proveedor,
		CuentaProvision,
		Importe,
		Iva,
		Retencion,
		Activo,
		UsuarioCreacionID
	)
	SELECT
		PremezclaDistribucionID = T.item.value('./PremezclaDistribucionID[1]', 'INT'),
		Costo = T.item.value('./Costo[1]', 'INT'),
		TieneCuenta = T.item.value('./TieneCuenta[1]', 'BIT'),
		Proveedor = CASE WHEN T.item.value('./Proveedor[1]', 'INT') =  0 THEN NULL ELSE T.item.value('./Proveedor[1]', 'INT') END,
		CuentaProvision = T.item.value('./CuentaProvision[1]', 'VARCHAR(10)'),
		Importe = T.item.value('./Importe[1]', 'DECIMAL(10,2)'),
		Iva = T.item.value('./Iva[1]', 'BIT'),
		Retencion = T.item.value('./Retencion[1]', 'BIT'),
		Activo = T.item.value('./Activo[1]', 'BIT'),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @PremezclaDistribucionCostoXML.nodes('ROOT/PremezclaDistribucionCosto') AS T(item)
	INSERT INTO PremezclaDistribucionCosto(
		PremezclaDistribucionID,
		CostoID,
		TieneCuenta,
		ProveedorID,
		CuentaProvision,
		Importe,
		Iva,
		Retencion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
		)
	SELECT PremezclaDistribucionID,
		   Costo,
		   TieneCuenta,
		   Proveedor,
		   CuentaProvision,
		   Importe,
		   Iva,
		   Retencion,
			 Activo,
		   GETDATE(),
		   UsuarioCreacionID
	FROM @tmpPremezclaDistribucionCosto
	
END
GO
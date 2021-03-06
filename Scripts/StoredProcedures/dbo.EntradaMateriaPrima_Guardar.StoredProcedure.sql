USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaMateriaPrima_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/12/10
-- Description: 
-- EXEC EntradaMateriaPrima_Guardar 1,1,'<ROOT><Datos>
--											     <EntradaProductoID>1</EntradaProductoID>
--											     <CostoID>1</CostoID>
--											     <TieneCuenta>1</TieneCuenta>
--												 <ProveedorID>1</ProveedorID>
--												 <CuentaProvision>CUENTA</CuentaProvision>
--												 <Importe>100</Importe>
--												 <Iva>1</Iva>
--												 <Retencion>1</Retencion>
--												 <TipoEntrada>Compra</TipoEntrada>
--												 <Observaciones>OBSERVACION</Observaciones>
--											   </Datos><Datos>
--											     <EntradaProductoID>1</EntradaProductoID>
--											     <CostoID>1</CostoID>
--											     <TieneCuenta>1</TieneCuenta>
--												 <ProveedorID>1</ProveedorID>
--												 <CuentaProvision>CUENTA</CuentaProvision>
--												 <Importe>100</Importe>
--												 <Iva>1</Iva>
--												 <Retencion>1</Retencion>
--												 <TipoEntrada>Compra</TipoEntrada>
--												 <Observaciones>OBSERVACION</Observaciones>
--                                             </Datos></ROOT>
--=============================================
CREATE PROCEDURE [dbo].[EntradaMateriaPrima_Guardar]
@UsuarioID INT,
@Activo BIT,
@xmlTipoCuenta XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #tEntradaProductoCosto
	(
		EntradaProductoID INT,
		CostoID INT,
		TieneCuenta BIT,
		ProveedorID INT,
		CuentaProvision VARCHAR(10),
		Importe DECIMAL(10,2),
		Iva BIT,
		Retencion BIT,
		TipoEntrada varchar(10),
		Observaciones varchar(255),
	)
	INSERT INTO #tEntradaProductoCosto
	SELECT 
			EntradaProductoID  = T.item.value('./EntradaProductoID[1]', 'INT'),
			CostoID  = T.item.value('./CostoID[1]', 'INT'),
			TieneCuenta    = T.item.value('./TieneCuenta[1]', 'BIT'),
			ProveedorID    = T.item.value('./ProveedorID[1]', 'INT'),
			CuentaProvision = T.item.value('./CuentaProvision[1]', 'VARCHAR(10)'),
			Importe = T.item.value('./Importe[1]', 'DECIMAL(10,2)'),
			Iva = T.item.value('./Iva[1]', 'BIT'),
			Retencion = T.item.value('./Retencion[1]', 'BIT'),
			TipoEntrada = T.item.value('./TipoEntrada[1]', 'VARCHAR(10)'),
			Observaciones = T.item.value('./Observaciones[1]', 'VARCHAR(255)')
	FROM @xmlTipoCuenta.nodes('ROOT/Datos') AS T(item)
	UPDATE #tEntradaProductoCosto 
	   SET ProveedorID = NULL 
	 WHERE ProveedorID = 0
	INSERT INTO EntradaProductoCosto (
				EntradaProductoID,
				CostoID,
				TieneCuenta,
				ProveedorID,
				CuentaProvision,
				Importe,
				Iva,
				Retencion,
				TipoEntrada,
				Observaciones,
				Activo,
				FechaCreacion,
				UsuarioCreacionID)
	SELECT EntradaProductoID,
	       CostoID,
		   TieneCuenta,
		   ProveedorID,
		   CuentaProvision,
		   Importe,
		   Iva,
		   Retencion,
		   TipoEntrada,
		   Observaciones,
		   1,
		   GETDATE(),
		   @UsuarioID
	FROM #tEntradaProductoCosto
	SET NOCOUNT OFF;
END

GO

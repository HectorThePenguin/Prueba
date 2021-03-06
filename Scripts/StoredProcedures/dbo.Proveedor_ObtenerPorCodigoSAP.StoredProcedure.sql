USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorCodigoSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorCodigoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorCodigoSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 15/10/2013
-- Description:  Obtener listado de Proveedores
-- Proveedor_ObtenerPorCodigoSAP '  400000  '
-- Proveedor_ObtenerPorCodigoSAP '  40A000  '
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorCodigoSAP]	
	@CodigoSAP VARCHAR(10)
AS
	BEGIN
		SET NOCOUNT ON;
		--Si se detecta que SAP utiliza tambien caracteres alfabeticos para sus codigo sera necesario
		--cambiar este procedimiento almacenado y la funcion RellenaCeros
		DECLARE @Numero bigint
		DECLARE @EsNumero bit
		SELECT @EsNumero = ISNUMERIC(@CodigoSAP)
		IF @EsNumero = 0
			SET @CodigoSAP = '0'
		SET @Numero = convert(bigint, @CodigoSAP)
		SET @CodigoSAP = dbo.RellenaCeros(@Numero, 10)
		SELECT ProveedorID,
			CodigoSAP,
			Descripcion,
			TipoProveedorID,
			ImporteComision,
			Activo
		FROM 
			Proveedor 
		WHERE 
			@CodigoSAP = CodigoSAP     
		SET NOCOUNT OFF;
	END

GO

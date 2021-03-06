USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormulaDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormulaDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormulaDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/06/2014
-- Description:	Crea la lista de produccion formula detalle
/*ProduccionFormulaDetalle_Crear	
'<ROOT>
  <ProduccionFormula>
    <ProduccionFormulaDetalle>
      <ProduccionFormulaID>1</ProduccionFormulaID>
      <ProductoID>1</ProductoID>
      <CantidadProducto>1</CantidadProducto>
      <IngredienteID>1</IngredienteID>
      <UsuarioCreacionID>5</UsuarioCreacionID>
    </ProduccionFormulaDetalle>
    <ProduccionFormulaDetalle>
      <ProduccionFormulaID>1</ProduccionFormulaID>
      <ProductoID>2</ProductoID>
      <CantidadProducto>93</CantidadProducto>
      <IngredienteID>2</IngredienteID>
      <UsuarioCreacionID>5</UsuarioCreacionID>
    </ProduccionFormulaDetalle>
    <ProduccionFormulaDetalle>
      <ProduccionFormulaID>1</ProduccionFormulaID>
      <ProductoID>3</ProductoID>
      <CantidadProducto>6</CantidadProducto>
      <IngredienteID>4</IngredienteID>
      <UsuarioCreacionID>5</UsuarioCreacionID>
    </ProduccionFormulaDetalle>
  </ProduccionFormula>
</ROOT>'*/
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormulaDetalle_Crear]
@XMLProduccionFormulaDetalle XML
AS
BEGIN
	INSERT INTO ProduccionFormulaDetalle 
	(ProduccionFormulaID, ProductoID, CantidadProducto, IngredienteID, Activo, FechaCreacion, UsuarioCreacionID)
	SELECT 
		ProduccionFormulaID = T.item.value('./ProduccionFormulaID[1]','INT'),
		ProductoID = T.item.value('./ProductoID[1]','INT'),
		CantidadProducto = T.item.value('./CantidadProducto[1]','DECIMAL(14,2)'),
		IngredienteID    = T.item.value('./IngredienteID[1]', 'INT'),
		Activo  = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XMLProduccionFormulaDetalle.nodes('ROOT/ProduccionFormula/ProduccionFormulaDetalle') AS T(item)
END

GO

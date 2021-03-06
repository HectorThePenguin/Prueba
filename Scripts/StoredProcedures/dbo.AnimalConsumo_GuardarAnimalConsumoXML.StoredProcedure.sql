USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_GuardarAnimalConsumoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalConsumo_GuardarAnimalConsumoXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_GuardarAnimalConsumoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Ramses Santos
-- Create date: 2014/09/10
-- Description: Procedimiento para almacenar el consumo del animal
-- Origen     : APInterfaces
/* 
EXEC [dbo].[AnimalConsumo_GuardarAnimalConsumoXML] '<ROOT>
  <AnimalConsumo>
    <AnimalID>81799</AnimalID>
    <RepartoID>129554</RepartoID>
	<Fecha>129554</Fecha>
    <FormulaIDServida>10</FormulaIDServida>
    <Cantidad>15.75</Cantidad>
    <TipoServicioID>3</TipoServicioID>
    <UsuarioCreacionId>5</UsuarioCreacionId>
  </AnimalConsumo>
</ROOT>'
*/
-- =============================================
CREATE PROCEDURE [dbo].[AnimalConsumo_GuardarAnimalConsumoXML]
	@AnimalConsumoXML XML
AS
BEGIN
	DECLARE @AnimalConsumo AS TABLE (
		AnimalID INT,
		RepartoID INT,
		Fecha DATETIME,
		FormulaIDServida INT,
		Cantidad decimal(17,2),
		TipoServicioID INT,
		UsuarioCreacionId INT				
	)
	INSERT @AnimalConsumo (
		AnimalID,
		RepartoID,
		Fecha,
		FormulaIDServida,
		Cantidad,
		TipoServicioID,
		UsuarioCreacionId)
	SELECT AnimalID = t.item.value('./AnimalID[1]', 'INT'),
		RepartoID = t.item.value('./RepartoID[1]', 'INT'),
		Fecha = t.item.value('./Fecha[1]', 'DATETIME'),
		FormulaIDServida = t.item.value('./FormulaIDServida[1]', 'INT'),
		Cantidad = t.item.value('./Cantidad[1]', 'DECIMAL(17,2)'),
		TipoServicioID = t.item.value('./TipoServicioID[1]', 'INT'),
		UsuarioCreacionId = t.item.value('./UsuarioCreacionId[1]', 'INT')
	FROM @AnimalConsumoXML.nodes('ROOT/AnimalConsumo') AS T(item)
	/* Se crea registro en la tabla de Animal Movimiento*/
	INSERT INTO AnimalConsumo(
		AnimalID,
		RepartoID,
		FormulaIDServida,
		Cantidad,
		TipoServicioID,
		UsuarioCreacionId,
		Fecha,
		Activo,
		FechaCreacion)
	SELECT
		AnimalID,
		RepartoID,
		FormulaIDServida,
		Cantidad,
		TipoServicioID,
		UsuarioCreacionId,
		COALESCE(Fecha,GETDATE()),
		1,
		GETDATE()
	FROM @AnimalConsumo
END

GO

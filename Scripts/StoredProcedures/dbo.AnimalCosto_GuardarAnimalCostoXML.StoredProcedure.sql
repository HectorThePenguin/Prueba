USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_GuardarAnimalCostoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalCosto_GuardarAnimalCostoXML]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_GuardarAnimalCostoXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/02/20
-- Description: Procedimiento para almacenar el costo del animal
-- Origen     : APInterfaces
-- EXEC [dbo].[AnimalCosto_GuardarAnimalCostoXML] 1,16,123,200.0,1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalCosto_GuardarAnimalCostoXML]
	@AnimalCostoXML XML
AS
BEGIN
	DECLARE @AnimalCosto AS TABLE (
		CostoID INT,
		TipoReferencia INT,
		FolioReferencia BIGINT,
		FechaCosto DATETIME,
		AnimalID BIGINT,
		Importe decimal(17,2),
		UsuarioCreacionID INT				
	)
	INSERT @AnimalCosto (
		CostoID,
		TipoReferencia,
		FolioReferencia,
		FechaCosto,
		AnimalID,
		Importe,
		UsuarioCreacionID)
	SELECT CostoID = t.item.value('./CostoID[1]', 'INT'),
		TipoReferencia = t.item.value('./TipoReferencia[1]', 'INT'),
		FolioReferencia = t.item.value('./FolioReferencia[1]', 'BIGINT'),
		FechaCosto = CASE WHEN ISDATE(t.item.value('./FechaCosto[1]', 'VARCHAR(20)')) = 0 
						  THEN GETDATE() 
						  ELSE COALESCE(t.item.value('./FechaCosto[1]', 'VARCHAR(20)'),GETDATE()) END,
		AnimalID = t.item.value('./AnimalID[1]', 'BIGINT'),
		Importe = t.item.value('./Importe[1]', 'DECIMAL(17,2)'),
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @AnimalCostoXML.nodes('ROOT/AnimalCosto') AS T(item)
	/* Se crea registro en la tabla de Animal Movimiento*/
	INSERT INTO AnimalCosto(
		AnimalID,
		FechaCosto,
		CostoID,
		TipoReferencia,
		FolioReferencia,
		Importe,
		FechaCreacion,
		UsuarioCreacionID)
	SELECT
		AnimalID,
		COALESCE(CONVERT(CHAR(8), FechaCosto ,112),GETDATE()),
		CostoID,
		TipoReferencia,
		FolioReferencia,
		Importe,
		GETDATE(),
		UsuarioCreacionID
	FROM @AnimalCosto
	WHERE Importe > 0
END

GO

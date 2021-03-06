USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_GuardarAnimalCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_GuardarAnimalCosto]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_GuardarAnimalCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/02/20
-- Description: Procedimiento para almacenar el costo del animal
-- Origen     : APInterfaces
-- EXEC [dbo].[CorteGanado_GuardarAnimalCosto] 1,16,123,200.0,1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_GuardarAnimalCosto]
	@AnimalID BIGINT,
	@CostoID INT,
	@FolioReferencia BIGINT,
	@Importe DECIMAL(17,2),
	@UsuarioCreacionID INT
AS
BEGIN
	/* Se crea registro en la tabla de Animal Movimiento*/
	INSERT INTO AnimalCosto(
		AnimalID,
		FechaCosto,
		CostoID,
		FolioReferencia,
		Importe,
		FechaCreacion,
		UsuarioCreacionID)
	VALUES(
		@AnimalID,
		GETDATE(),
		@CostoID,
		@FolioReferencia,
		@Importe,
		GETDATE(),
		@UsuarioCreacionID)
END

GO

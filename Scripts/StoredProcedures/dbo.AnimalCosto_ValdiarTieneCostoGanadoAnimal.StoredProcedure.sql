USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_ValdiarTieneCostoGanadoAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalCosto_ValdiarTieneCostoGanadoAnimal]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_ValdiarTieneCostoGanadoAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description:  Valdiar Si el animal tiene costo de ganado
-- Origen: APInterfaces
-- AnimalCosto_ValdiarTieneCostoGanadoAnimal 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalCosto_ValdiarTieneCostoGanadoAnimal]
	@AnimalID INT,
	@CostoID INT
AS
  BEGIN
    SET NOCOUNT ON
		SELECT COUNT(AnimalID) 
		  FROM AnimalCosto (NOLOCK) 
		 WHERE AnimalId = @AnimalID
		   AND CostoID = @CostoID
	SET NOCOUNT OFF
  END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_EliminarAnimalCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalCosto_EliminarAnimalCosto]
GO
/****** Object:  StoredProcedure [dbo].[AnimalCosto_EliminarAnimalCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description:  Elimina el Animal de AnimalCosto
-- Origen: APInterfaces
-- AnimalCosto_EliminarAnimalCosto 1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalCosto_EliminarAnimalCosto]
	@AnimalID INT
AS
  BEGIN
    SET NOCOUNT ON
		DELETE FROM AnimalCosto
		 WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
  END

GO

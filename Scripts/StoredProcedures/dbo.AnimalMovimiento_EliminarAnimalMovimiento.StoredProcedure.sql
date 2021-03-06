USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_EliminarAnimalMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_EliminarAnimalMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_EliminarAnimalMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description: Elimina el Animal de AnimalMovimiento
-- Origen: APInterfaces
-- AnimalMovimiento_EliminarAnimalMovimiento 1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_EliminarAnimalMovimiento]
	@AnimalID INT
AS
  BEGIN
    SET NOCOUNT ON
		DELETE FROM AnimalMovimiento
		 WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
  END

GO

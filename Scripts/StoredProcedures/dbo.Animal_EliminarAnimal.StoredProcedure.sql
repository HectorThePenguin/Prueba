USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_EliminarAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_EliminarAnimal]
GO
/****** Object:  StoredProcedure [dbo].[Animal_EliminarAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description: Elimina el Animal de Animal
-- Origen: APInterfaces
-- Animal_EliminarAnimal 1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_EliminarAnimal]
	@AnimalID INT
AS
  BEGIN
    SET NOCOUNT ON
		DELETE FROM AnimalSalida WHERE AnimalID = @AnimalID
		DELETE FROM Animal
		 WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
  END

GO

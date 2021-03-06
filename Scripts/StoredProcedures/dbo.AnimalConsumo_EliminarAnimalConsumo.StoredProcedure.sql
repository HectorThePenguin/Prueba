USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_EliminarAnimalConsumo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalConsumo_EliminarAnimalConsumo]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_EliminarAnimalConsumo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 30/12/2014
-- Description:  Eliminar AnimalConsumo
-- AnimalConsumo_EliminarAnimalConsumo
-- =============================================
CREATE PROCEDURE [dbo].[AnimalConsumo_EliminarAnimalConsumo]
@AnimalID BIGINT
AS
  BEGIN
    SET NOCOUNT ON
		DELETE AnimalConsumo
		WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
END

GO

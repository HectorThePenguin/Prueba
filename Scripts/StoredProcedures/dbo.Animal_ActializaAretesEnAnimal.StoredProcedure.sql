USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActializaAretesEnAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ActializaAretesEnAnimal]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActializaAretesEnAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/09/17
-- Description: SP para Actualizar los aretes del animal
-- Origen     : APInterfaces
-- EXEC Animal_ActializaAretesEnAnimal 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ActializaAretesEnAnimal]
	@Arete VARCHAR(15),
	@AreteTestigo VARCHAR(15),
	@AnimalID INT,
	@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Animal 
	   SET Arete = @Arete,
		   AreteMetalico = @AreteTestigo,
		   UsuarioModificacionID = @UsuarioModificacionID, 
		   FechaModificacion = GETDATE()
	WHERE AnimalID = @AnimalID
END

GO

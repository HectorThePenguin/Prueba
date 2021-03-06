USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_InactivarAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_InactivarAnimal]
GO
/****** Object:  StoredProcedure [dbo].[Animal_InactivarAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/05/21
-- Description: SP para Inactivar un registro en la tabla de Animal
-- Origen     : APInterfaces
-- EXEC Animal_InactivarAnimal 1,0,1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_InactivarAnimal]
	@AnimalID BIGINT,
	@Activo INT,
	@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Animal 
	   SET Activo = 0,
		   UsuarioModificacionID = @UsuarioModificacionID, 
		   FechaModificacion = GETDATE()
	WHERE AnimalID = @AnimalID;
END

GO

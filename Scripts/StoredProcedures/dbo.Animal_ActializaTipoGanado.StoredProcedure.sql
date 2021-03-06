USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActializaTipoGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ActializaTipoGanado]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActializaTipoGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/09/16
-- Description: SP para Actualizar el tipo de ganado de un animal
-- Origen     : APInterfaces
-- EXEC Animal_ActializaTipoGanado 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ActializaTipoGanado]
	@AnimalID BIGINT,
	@TipoGanadoID INT,
	@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Animal 
	   SET TipoGanadoID = @TipoGanadoID,
		   UsuarioModificacionID = @UsuarioModificacionID, 
		   FechaModificacion = GETDATE()
	WHERE AnimalID = @AnimalID
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActualizaClasificacionGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ActualizaClasificacionGanado]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ActualizaClasificacionGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Edgar . villarreal
-- Create date: 2014/09/16
-- Description: SP para Actualizar Clasificacion Ganado
-- Origen     : APInterfaces
-- EXEC Animal_ActualizaClasificacionGanado 40536,6,1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ActualizaClasificacionGanado]
	@AnimalID INT,
	@ClasificacionGanado INT,
	@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Animal 
	   SET ClasificacionGanadoID = @ClasificacionGanado,
		   UsuarioModificacionID = @UsuarioModificacionID, 
		   FechaModificacion = GETDATE()
	WHERE AnimalID = @AnimalID
END

GO

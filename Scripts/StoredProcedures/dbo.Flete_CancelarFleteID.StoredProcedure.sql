USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Flete_CancelarFleteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Flete_CancelarFleteID]
GO
/****** Object:  StoredProcedure [dbo].[Flete_CancelarFleteID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 26/05/2014
-- Description: Cancela los fletes
-- Flete_CancelarFleteID 1,1
--=============================================
CREATE PROCEDURE [dbo].[Flete_CancelarFleteID]
@FleteID INT,
@UsuarioModificacionID INT
AS
BEGIN
	UPDATE Flete 
	SET Activo = 0,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE FleteID = @FleteID
	UPDATE FD
	SET Activo = 0, 
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	FROM FleteDetalle FD
	WHERE FleteID = @FleteID
END

GO

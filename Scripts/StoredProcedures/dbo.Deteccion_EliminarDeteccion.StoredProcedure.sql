USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Deteccion_EliminarDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Deteccion_EliminarDeteccion]
GO
/****** Object:  StoredProcedure [dbo].[Deteccion_EliminarDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Fecha: 20-02-2014
-- Descripci�n:	Eliminar una deteccion
-- EXEC Deteccion_EliminarDeteccion 1,1,0
-- =============================================
CREATE PROCEDURE [dbo].[Deteccion_EliminarDeteccion]
	@DeteccionID INT, 
	@Arete VARCHAR(15),
	@UsuarioModificacion INT,
	@Estatus INT
AS
BEGIN
	IF @DeteccionID = 0
	BEGIN
	UPDATE Deteccion
		   SET Activo = @Estatus,
			   FechaModificacion = GETDATE(),
			   UsuarioModificacionID = @UsuarioModificacion
		 WHERE Arete = @Arete
	END
	ELSE
	BEGIN
		UPDATE Deteccion
		   SET Activo = @Estatus,
			   FechaModificacion = GETDATE(),
			   UsuarioModificacionID = @UsuarioModificacion
		 WHERE DeteccionID = @DeteccionID
	END
END

GO

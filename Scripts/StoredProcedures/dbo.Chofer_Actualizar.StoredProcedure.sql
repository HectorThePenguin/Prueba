USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chofer_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar un Chofer.
-- Chofer_Actualizar 'Chofer001', 1
-- =============================================
CREATE PROCEDURE [dbo].[Chofer_Actualizar]		
 @ChoferID				INT,
 @Nombre                VARCHAR(50),
 @ApellidoPaterno       VARCHAR(50),
 @ApellidoMaterno       VARCHAR(50),
 @Activo                BIT,
 @UsuarioModificacionID INT,
 @Boletinado			BIT,
 @Observaciones			VARCHAR(500)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Chofer 
		SET Nombre = @Nombre,
			ApellidoPaterno = @ApellidoPaterno,
			ApellidoMaterno = @ApellidoMaterno,
			Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID,
			Boletinado = @Boletinado,
			Observaciones = @Observaciones
	WHERE ChoferID = @ChoferID
	SET NOCOUNT OFF;
END

GO

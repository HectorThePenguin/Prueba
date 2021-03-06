USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Grado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 29/04/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Grado_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Grado_Actualizar]
@GradoID	INT,
@Descripcion VARCHAR(50),
@NivelGravedad CHAR(1),
@Activo BIT,
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Grado
	SET Descripcion = @Descripcion
		, NivelGravedad = @NivelGravedad
		, Activo = @Activo
		, UsuarioModificacionID = @UsuarioModificacionID
		, FechaModificacion = GETDATE()
	WHERE GradoID = @GradoID
	SET NOCOUNT OFF;
END

GO

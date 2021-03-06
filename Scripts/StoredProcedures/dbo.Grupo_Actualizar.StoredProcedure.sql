USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 30/09/2013
-- Description:	Actualizar un Grupo.
-- Grupo_Actualizar 1, 'grupo17', 1
-- =============================================
CREATE PROCEDURE [dbo].[Grupo_Actualizar]		
	@GrupoID INT,
	@Descripcion VARCHAR(50),
	@Activo BIT	
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Grupo 
		SET Descripcion = @Descripcion, 
			Activo = @Activo
	WHERE GrupoID = @GrupoID
END

GO

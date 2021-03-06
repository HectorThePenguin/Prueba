USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 28/09/2013
-- Description:	Guardar un Grupo.
-- Grupo_Crear 'grupo17', 1
-- =============================================
CREATE PROCEDURE [dbo].[Grupo_Crear]		
	@Descripcion VARCHAR(50),
	@Activo BIT	
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Grupo (Descripcion, Activo) 
	VALUES (@Descripcion, @Activo)
	SELECT SCOPE_IDENTITY()
END

GO

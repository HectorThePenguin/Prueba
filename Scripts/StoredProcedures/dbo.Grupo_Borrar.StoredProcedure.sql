USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_Borrar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_Borrar]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_Borrar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 30/09/2013
-- Description:	Actualizar un Grupo.
-- Grupo_Borrar 1
-- =============================================
CREATE PROCEDURE [dbo].[Grupo_Borrar]		
	@GrupoID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Grupo 
		SET Activo = 0
	WHERE GrupoID = @GrupoID
END

GO

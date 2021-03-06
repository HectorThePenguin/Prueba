USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 30/09/2013
-- Description:	Actualizar un Grupo.
-- Grupo_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[Grupo_ObtenerPorID]		
	@GrupoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		GrupoID, 
		Descripcion, 
		Activo
	FROM Grupo 		
	WHERE GrupoID = @GrupoID
END

GO

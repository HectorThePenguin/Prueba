USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Grupo_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Grupo_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		GrupoID,
		Descripcion,
		Activo
	FROM Grupo
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

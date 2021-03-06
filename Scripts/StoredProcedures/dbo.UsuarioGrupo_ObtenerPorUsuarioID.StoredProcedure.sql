USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGrupo_ObtenerPorUsuarioID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[UsuarioGrupo_ObtenerPorUsuarioID]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGrupo_ObtenerPorUsuarioID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : UsuarioGrupo_ObtenerPorUsuarioID 0
--======================================================
CREATE PROCEDURE [dbo].[UsuarioGrupo_ObtenerPorUsuarioID] @UsuarioId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COALESCE(UsuarioGrupoID, 0) AS [UsuarioGrupoID]
		,@UsuarioId AS [UsuarioID]
		,g.GrupoID
		,g.Descripcion AS [Grupo]
		,Activo = CAST(COALESCE(ug.UsuarioGrupoID, 0) AS BIT)
	FROM Grupo g
	LEFT JOIN UsuarioGrupo ug ON ug.GrupoID = g.GrupoID
		AND ug.UsuarioID = @UsuarioId
	SET NOCOUNT OFF;
END

GO

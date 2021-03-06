USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGrupo_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[UsuarioGrupo_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGrupo_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : UsuarioGrupo_Guardar 
--======================================================
CREATE PROCEDURE [dbo].[UsuarioGrupo_Guardar] @XmlUsuarioGrupo XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Detalle AS TABLE (
		[UsuarioGrupoID] INT
		,[UsuarioID] INT
		,[GrupoID] INT
		,[Activo] BIT
		)
	INSERT @Detalle (
		[UsuarioGrupoID]
		,[UsuarioID]
		,[GrupoID]
		,[Activo]
		)
	SELECT [UsuarioGrupoID] = t.item.value('./UsuarioGrupoID[1]', 'INT')
		,[UsuarioID] = t.item.value('./UsuarioID[1]', 'INT')
		,[GrupoID] = t.item.value('./GrupoID[1]', 'INT')
		,[Activo] = t.item.value('./Activo[1]', 'BIT')
	FROM @XmlUsuarioGrupo.nodes('ROOT/UsuarioGrupo') AS T(item)
	DELETE ug
	FROM UsuarioGrupo ug
	INNER JOIN @Detalle dt ON dt.UsuarioGrupoID = ug.UsuarioGrupoID
		AND dt.Activo = 0
	INSERT UsuarioGrupo (
		[UsuarioID]
		,[GrupoID]
		)
	SELECT [UsuarioID]
		,[GrupoID]
	FROM @Detalle
	WHERE UsuarioGrupoID = 0
		AND Activo = 1
	SET NOCOUNT OFF;
END

GO

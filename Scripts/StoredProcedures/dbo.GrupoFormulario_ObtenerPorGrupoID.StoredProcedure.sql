USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorGrupoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_ObtenerPorGrupoID]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorGrupoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_ObtenerPorGrupoID 0
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_ObtenerPorGrupoID]
@GrupoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT F.FormularioID
		,  F.Descripcion AS Formulario
		,  M.Descripcion AS Modulo
		,  M.ModuloID
	FROM Formulario F
	INNER JOIN Modulo M
		ON (F.ModuloID = M.ModuloID)
	ORDER BY F.Descripcion
	SELECT
		  G.GrupoID
		, G.Descripcion AS Grupo
		, G.Activo
	FROM Grupo G
	WHERE GrupoID = @GrupoID
	SELECT GF.FormularioID		
		,  GF.AccesoID
	FROM Formulario F
	INNER JOIN GrupoFormulario GF
		ON (F.FormularioID = GF.FormularioID
			AND GF.GrupoID = @GrupoID)
	ORDER BY F.Descripcion
	SET NOCOUNT OFF;
END

GO

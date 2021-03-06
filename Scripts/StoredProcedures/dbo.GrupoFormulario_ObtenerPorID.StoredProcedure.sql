USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_ObtenerPorID]
@GrupoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		gf.GrupoID,
		g.Descripcion as [Grupo],
		gf.FormularioID,
		f.Descripcion as [Formulario], 
		gf.AccesoID,
		a.Descripcion as [Acceso]
	FROM GrupoFormulario gf
	inner join Grupo g on g.GrupoID = gf.GrupoID
	inner join Formulario f on f.FormularioID = gf.FormularioID
	inner join Acceso a on a.AccesoID = gf.AccesoID
	WHERE gf.GrupoID = @GrupoID
	SET NOCOUNT OFF;
END

GO

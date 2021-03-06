USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grupo_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Grupo_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 23/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Grupo_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Grupo_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		GrupoID,
		Descripcion,
		Activo
	FROM Grupo
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO

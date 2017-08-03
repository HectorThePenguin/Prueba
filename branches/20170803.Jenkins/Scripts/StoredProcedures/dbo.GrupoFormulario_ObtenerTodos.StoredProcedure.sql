USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		GrupoID,
		FormularioID,
		AccesoID
	FROM GrupoFormulario	
	SET NOCOUNT OFF;
END

GO

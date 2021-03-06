USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_ObtenerPorDescripcion]
@GrupoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		GrupoID,
		FormularioID,
		AccesoID
	FROM GrupoFormulario
	WHERE GrupoID = @GrupoID
	SET NOCOUNT OFF;
END

GO

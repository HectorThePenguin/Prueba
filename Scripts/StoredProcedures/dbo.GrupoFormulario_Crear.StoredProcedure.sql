USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GrupoFormulario_Crear]
GO
/****** Object:  StoredProcedure [dbo].[GrupoFormulario_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 10/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : GrupoFormulario_Crear
--======================================================
CREATE PROCEDURE [dbo].[GrupoFormulario_Crear]
@FormularioID int,
@AccesoID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT GrupoFormulario (
		FormularioID,
		AccesoID
	)
	VALUES(
		@FormularioID,
		@AccesoID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

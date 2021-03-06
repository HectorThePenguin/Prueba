USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SubFamilia_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		SF.FamiliaID,
		SF.SubFamiliaID,
		SF.Descripcion,
		SF.Activo,
		F.Descripcion AS Familia
	FROM SubFamilia SF
	INNER JOIN Familia F
		ON (SF.FamiliaID = F.FamiliaID) 
	WHERE SF.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

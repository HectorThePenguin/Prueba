USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Familia_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Familia_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Familia_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Familia_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FamiliaID,
		Descripcion,
		Activo
	FROM Familia
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

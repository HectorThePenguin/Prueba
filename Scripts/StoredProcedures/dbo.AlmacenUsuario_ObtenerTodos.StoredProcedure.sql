USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenUsuario_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		AlmacenUsuarioID,
		AlmacenID,
		UsuarioID,
		Activo
	FROM AlmacenUsuario
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

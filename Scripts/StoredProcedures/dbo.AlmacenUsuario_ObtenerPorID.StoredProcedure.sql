USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenUsuario_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenUsuario_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[AlmacenUsuario_ObtenerPorID]
@AlmacenUsuarioID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		AlmacenUsuarioID,
		AlmacenID,
		UsuarioID,
		Activo
	FROM AlmacenUsuario
	WHERE AlmacenUsuarioID = @AlmacenUsuarioID
	SET NOCOUNT OFF;
END

GO

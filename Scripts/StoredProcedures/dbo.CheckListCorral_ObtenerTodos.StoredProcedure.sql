USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListCorral_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListCorral_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[CheckListCorral_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CheckListCorralID,
		OrganizacionID,
		LoteID,
		PDF,
		Activo
	FROM CheckListCorral
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraHorometro_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraHorometro_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraHorometro_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CheckListRoladoraHorometroID,
		CheckListRoladoraGeneralID,
		RoladoraID,
		HorometroInicial,
		HorometroFinal,
		Activo
	FROM CheckListRoladoraHorometro
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

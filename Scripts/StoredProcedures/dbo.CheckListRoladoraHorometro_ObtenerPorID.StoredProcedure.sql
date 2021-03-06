USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraHorometro_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraHorometro_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraHorometro_ObtenerPorID]
@CheckListRoladoraHorometroID int
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
	WHERE CheckListRoladoraHorometroID = @CheckListRoladoraHorometroID
	SET NOCOUNT OFF;
END

GO

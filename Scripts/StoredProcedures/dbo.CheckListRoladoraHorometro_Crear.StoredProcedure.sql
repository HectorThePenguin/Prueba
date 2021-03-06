USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraHorometro_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraHorometro_Crear
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraHorometro_Crear]
@CheckListRoladoraHorometroID int,
@CheckListRoladoraGeneralID int,
@RoladoraID int,
@HorometroInicial varchar(5),
@HorometroFinal varchar(5),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CheckListRoladoraHorometro (
		CheckListRoladoraHorometroID,
		CheckListRoladoraGeneralID,
		RoladoraID,
		HorometroInicial,
		HorometroFinal,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@CheckListRoladoraHorometroID,
		@CheckListRoladoraGeneralID,
		@RoladoraID,
		@HorometroInicial,
		@HorometroFinal,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

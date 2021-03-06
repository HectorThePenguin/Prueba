USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraHorometro_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraHorometro_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraHorometro_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraHorometro_Actualizar]
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
	UPDATE CheckListRoladoraHorometro SET
		CheckListRoladoraGeneralID = @CheckListRoladoraGeneralID,
		RoladoraID = @RoladoraID,
		HorometroInicial = @HorometroInicial,
		HorometroFinal = @HorometroFinal,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE CheckListRoladoraHorometroID = @CheckListRoladoraHorometroID
	SET NOCOUNT OFF;
END

GO

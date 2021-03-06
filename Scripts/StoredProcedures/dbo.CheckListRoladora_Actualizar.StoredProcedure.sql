USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladora_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladora_Actualizar]
@CheckListRoladoraID int,
@CheckListRoladoraGeneralID int,
@RoladoraID int,
@UsuarioIDResponsable int,
@FechaCheckList smalldatetime,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CheckListRoladora SET
		CheckListRoladoraGeneralID = @CheckListRoladoraGeneralID,
		RoladoraID = @RoladoraID,
		UsuarioIDResponsable = @UsuarioIDResponsable,		
		FechaCheckList = GETDATE(), --@FechaCheckList,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CheckListRoladoraID = @CheckListRoladoraID
	SET NOCOUNT OFF;
END

GO

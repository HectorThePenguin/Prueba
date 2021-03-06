USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladora_Crear
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladora_Crear]
@CheckListRoladoraGeneralID int,
@RoladoraID int,
@UsuarioIDResponsable int,
@FechaCheckList smalldatetime,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CheckListRoladora (
		CheckListRoladoraGeneralID,
		RoladoraID,
		UsuarioIDResponsable,		
		FechaCheckList,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@CheckListRoladoraGeneralID,
		@RoladoraID,
		@UsuarioIDResponsable,		
		GETDATE(),--@FechaCheckList,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

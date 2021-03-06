USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladora_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladora_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 23/06/2014
-- Description:  Obtener CheckList
-- CheckListRoladora_Guardar
-- =============================================
CREATE PROCEDURE [dbo].[CheckListRoladora_Guardar]
@CheckListRoladoraGeneralID INT
, @RoladoraID INT
, @UsuarioIDResponsable INT
, @FechaCheckList SMALLDATETIME
, @Activo BIT
, @UsuarioCreacion INT
AS
BEGIN
	INSERT INTO CheckListRoladora
	(
		CheckListRoladoraGeneralID
		,RoladoraID
		,UsuarioIDResponsable		
		,FechaCheckList
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
	)
	VALUES
	(
		@CheckListRoladoraGeneralID
		, @RoladoraID
		, @UsuarioIDResponsable		
		, @FechaCheckList
		, @Activo
		, GETDATE()
		, @UsuarioCreacion
	)
END

GO

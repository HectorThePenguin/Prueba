USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraDetalle_Crear
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraDetalle_Crear]
@CheckListRoladoraDetalleID int,
@CheckListRoladoraID int,
@CheckListRoladoraRangoID int,
@CheckListRoladoraAccionID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CheckListRoladoraDetalle (
		CheckListRoladoraDetalleID,
		CheckListRoladoraID,
		CheckListRoladoraRangoID,
		CheckListRoladoraAccionID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@CheckListRoladoraDetalleID,
		@CheckListRoladoraID,
		@CheckListRoladoraRangoID,
		@CheckListRoladoraAccionID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

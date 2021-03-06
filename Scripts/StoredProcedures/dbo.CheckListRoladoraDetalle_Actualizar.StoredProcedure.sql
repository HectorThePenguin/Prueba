USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListRoladoraDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListRoladoraDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListRoladoraDetalle_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CheckListRoladoraDetalle_Actualizar]
@CheckListRoladoraDetalleID int,
@CheckListRoladoraID int,
@CheckListRoladoraRangoID int,
@CheckListRoladoraAccionID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CheckListRoladoraDetalle SET
		CheckListRoladoraID = @CheckListRoladoraID,
		CheckListRoladoraRangoID = @CheckListRoladoraRangoID,
		CheckListRoladoraAccionID = @CheckListRoladoraAccionID,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE CheckListRoladoraDetalleID = @CheckListRoladoraDetalleID
	SET NOCOUNT OFF;
END

GO

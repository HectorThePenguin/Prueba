USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListCorral_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListCorral_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CheckListCorral_Actualizar]
@CheckListCorralID int,
@OrganizacionID int,
@LoteID int,
@PDF varbinary(max),
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CheckListCorral SET
		OrganizacionID = @OrganizacionID,
		LoteID = @LoteID,
		PDF = @PDF,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CheckListCorralID = @CheckListCorralID
	SET NOCOUNT OFF;
END

GO

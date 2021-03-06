USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rol_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rol_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Rol_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- Modificado : Torres Lugo Manuel
--		Se agrego el campo de NivelAlertaID
-- SpName     : Rol_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Rol_Actualizar]
@RolID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int,
@NivelAlertaID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Rol SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE(),
		NivelAlertaID = CASE WHEN @NivelAlertaID = 0 THEN NULL ELSE @NivelAlertaID END 
	WHERE RolID = @RolID
	SET NOCOUNT OFF;
END


GO

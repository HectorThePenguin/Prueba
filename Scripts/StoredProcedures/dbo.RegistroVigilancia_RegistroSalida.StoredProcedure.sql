USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_RegistroSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilancia_RegistroSalida]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilancia_RegistroSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Eduardo COta
-- Create date: 25/05/2014
-- Description: Actualiza fecha Salida y campo activo = 0, -- significa que el camion ya salio y folio esta cerrado
-- RegistroVigilancia_RegistroSalida 1, 22, 0, 5
--=============================================
CREATE PROCEDURE [dbo].[RegistroVigilancia_RegistroSalida]
	@OrganizacionID INT,
	@FolioTurno INT,
	@Activo BIT,
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE RegistroVigilancia SET 
	    FechaSalida = GETDATE(),
		Activo = @Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE FolioTurno = @FolioTurno AND OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

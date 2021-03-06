USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rol_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rol_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Rol_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--==============================================================
-- Author      : José Gilberto Quintero López/Manuel Torres Lugo 
-- Create date : 05/03/2014 12:00:00 a.m.
-- Description :  
-- Modificacion: Se le agrego el campo NivelAlerta al SP
-- SpName      : Rol_Crear
--==============================================================
CREATE PROCEDURE [dbo].[Rol_Crear]
@Descripcion varchar(50),
@Activo bit,
@UsuarioCreacionID int,
@NivelAlertaID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Rol (
		Descripcion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion,
		NivelAlertaID
	)
	VALUES(
		@Descripcion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE(),
		CASE WHEN @NivelAlertaID = 0 THEN NULL ELSE @NivelAlertaID END
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

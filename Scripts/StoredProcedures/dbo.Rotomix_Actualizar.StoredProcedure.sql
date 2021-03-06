USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rotomix_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rotomix_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Rotomix_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : Rotomix_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Rotomix_Actualizar]
@RotomixID int,
@OrganizacionID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Rotomix SET
		OrganizacionID = @OrganizacionID,
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE RotomixID = @RotomixID
	SET NOCOUNT OFF;
END

GO

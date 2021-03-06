USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SubFamilia_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[SubFamilia_Actualizar]
@FamiliaID int,
@SubFamiliaID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE SubFamilia SET
		FamiliaID = @FamiliaID,
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE SubFamiliaID = @SubFamiliaID
	SET NOCOUNT OFF;
END

GO

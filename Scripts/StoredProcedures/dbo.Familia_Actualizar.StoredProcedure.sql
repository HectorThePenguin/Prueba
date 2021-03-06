USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Familia_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Familia_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Familia_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Familia_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Familia_Actualizar]
@FamiliaID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Familia SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE FamiliaID = @FamiliaID
	SET NOCOUNT OFF;
END

GO

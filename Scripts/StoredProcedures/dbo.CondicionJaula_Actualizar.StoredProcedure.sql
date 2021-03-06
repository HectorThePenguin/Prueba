USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CondicionJaula_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CondicionJaula_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 22/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CondicionJaula_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CondicionJaula_Actualizar]
@CondicionJaulaID int,
@Descripcion varchar(255), 
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CondicionJaula SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CondicionJaulaID = @CondicionJaulaID
	SET NOCOUNT OFF;
END

GO

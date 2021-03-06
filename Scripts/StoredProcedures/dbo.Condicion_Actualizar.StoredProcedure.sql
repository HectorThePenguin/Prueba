USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Condicion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Condicion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Condicion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Condicion_Actualizar]
@CondicionID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Condicion SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CondicionID = @CondicionID
	SET NOCOUNT OFF;
END

GO

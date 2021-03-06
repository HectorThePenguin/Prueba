USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralDetector_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CorralDetector_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CorralDetector_Actualizar]
@CorralDetectorID int,
@OperadorID int,
@CorralID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CorralDetector SET
		OperadorID = @OperadorID,
		CorralID = @CorralID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CorralDetectorID = @CorralDetectorID
	SET NOCOUNT OFF;
END

GO

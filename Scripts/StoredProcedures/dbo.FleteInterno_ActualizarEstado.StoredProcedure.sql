USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInterno_ActualizarEstado]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: 
-- SpName     : FleteInterno_ActualizarEstado
--======================================================
CREATE PROCEDURE [dbo].[FleteInterno_ActualizarEstado]
@FleteInternoID INT,
@Activo INT,
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE FleteInterno SET
		Activo = @Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE FleteInternoID = @FleteInternoID
	SET NOCOUNT OFF;
END

GO

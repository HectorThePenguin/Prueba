USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_EliminarInformacionDatos]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_EliminarInformacionDatos]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_EliminarInformacionDatos]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 19/06/2017 11:00:00 a.m.
-- Description: Procedimiento realiza eliminado logico
-- de la informacion de registrada en la seccion de datos
-- SpName: 
/* 
ProgramacionEmbarque_EliminarInformacionDatos 14974, 1
*/
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_EliminarInformacionDatos]
@EmbarqueID 				INT,
@UsuarioModificacionID  	INT
AS
BEGIN
	SET NOCOUNT ON;

	-- Revertir los cambios realizados en la seccion de datos
	UPDATE Embarque SET
	  JaulaID = null,
	  CamionID = null,
	  FolioEmbarque = 0,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	WHERE EmbarqueID = @EmbarqueID;

	-- Eliminado logico de la informacion del operador
	UPDATE EmbarqueOperador SET
	  Activo = 0,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	WHERE EmbarqueID = @EmbarqueID
	AND Activo = 1;

	SET NOCOUNT OFF;
END

GO
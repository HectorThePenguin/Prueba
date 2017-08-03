USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_EliminarEmbarqueProgramacion]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_EliminarEmbarqueProgramacion]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_EliminarEmbarqueProgramacion]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 05/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que eliminar la
-- informacion ingresada en programacion embarque.
-- SpName     : ProgramacionEmbarque_EliminarEmbarqueProgramacion 5, 3, 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_EliminarEmbarqueProgramacion]
@EmbarqueID 			INT,
@EstausCancelado		INT,
@UsuarioModificacionID  INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Descativado INT = 0; /* Indica activo a 0 */

	/* Eliminar Información De Embarque */
	UPDATE Embarque SET
		Activo 					= @Descativado,
		Estatus					= @EstausCancelado,
		FechaModificacion 		= GETDATE(),
		UsuarioModificacionID 	= @UsuarioModificacionID
	WHERE EmbarqueID = @EmbarqueID;

	/* Eliminar en EmbarqueRuteo */
	IF EXISTS (SELECT EmbarqueID FROM EmbarqueRuteo WHERE EmbarqueID = @EmbarqueID) 
	BEGIN
		DELETE FROM EmbarqueRuteo
		WHERE EmbarqueID = @EmbarqueID;
	END

	/* Eliminar en EmbarqueObservaciones */
	IF EXISTS (SELECT EmbarqueID FROM EmbarqueObservaciones WHERE EmbarqueID = @EmbarqueID) 
	BEGIN
		UPDATE EmbarqueObservaciones SET
			Activo 					= @Descativado,
			FechaModificacion 		= GETDATE(),
			UsuarioModificacionID 	= @UsuarioModificacionID
		WHERE EmbarqueID = @EmbarqueID;
	END

	/* Eliminar en EmbarqueOperador */
	IF EXISTS (SELECT EmbarqueID FROM EmbarqueOperador WHERE EmbarqueID = @EmbarqueID) 
	BEGIN
		UPDATE EmbarqueOperador SET
			Activo 					= @Descativado,
			FechaModificacion 		= GETDATE(),
			UsuarioModificacionID 	= @UsuarioModificacionID
		WHERE EmbarqueID = @EmbarqueID;
	END

	/* Eliminar en EmbarqueGastoFijo */
	IF EXISTS (SELECT EmbarqueID FROM EmbarqueGastoFijo WHERE EmbarqueID = @EmbarqueID) 
	BEGIN
		UPDATE EmbarqueGastoFijo SET
			Activo 					= @Descativado,
			FechaModificacion 		= GETDATE(),
			UsuarioModificacionID 	= @UsuarioModificacionID
		WHERE EmbarqueID = @EmbarqueID;
	END

	/* Eliminar en EmbarqueCosto */
	IF EXISTS (SELECT EmbarqueID FROM EmbarqueCosto WHERE EmbarqueID = @EmbarqueID) 
	BEGIN
		UPDATE EmbarqueCosto SET
			Activo 					= @Descativado,
			FechaModificacion 		= GETDATE(),
			UsuarioModificacionID 	= @UsuarioModificacionID
		WHERE EmbarqueID = @EmbarqueID;
	END

	SET NOCOUNT OFF;
END

GO
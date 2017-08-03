USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarObservaciones]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarObservaciones]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarObservaciones]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 05/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que guarda la
-- informacion ingresada en la seccion de programacion.
-- SpName     : ProgramacionEmbarque_ActualizarObservaciones 1, 1, 'Mis Observaciones', 1
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarObservaciones]
@Activo					BIT,
@EmbarqueID 			INT,
@Observacion			VARCHAR (255),
@UsuarioModificacionID  INT
AS
BEGIN
	SET NOCOUNT ON;
	
   	BEGIN
       	INSERT EmbarqueObservaciones (
			Activo,
			EmbarqueID,		
			Observacion,	
			UsuarioCreacionID
		)
		VALUES (
			@Activo,
			@EmbarqueID,			
			@Observacion,	
			@UsuarioModificacionID
		);
   	END

	SET NOCOUNT OFF;
END

GO
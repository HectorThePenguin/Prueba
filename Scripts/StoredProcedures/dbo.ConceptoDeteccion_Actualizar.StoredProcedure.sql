USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConceptoDeteccion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConceptoDeteccion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ConceptoDeteccion_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConceptoDeteccion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ConceptoDeteccion_Actualizar]
@ConceptoDeteccionID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ConceptoDeteccion SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ConceptoDeteccionID = @ConceptoDeteccionID
	SET NOCOUNT OFF;
END

GO

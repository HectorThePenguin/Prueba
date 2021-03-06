USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoTratamiento_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoTratamiento_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoTratamiento_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoTratamiento_Actualizar]
@TipoTratamientoID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoTratamiento SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoTratamientoID = @TipoTratamientoID
	SET NOCOUNT OFF;
END

GO

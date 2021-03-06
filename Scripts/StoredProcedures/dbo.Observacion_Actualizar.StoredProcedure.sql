USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Observacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Observacion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Observacion_Actualizar]
@ObservacionID int,
@Descripcion varchar(50),
@TipoObservacionID int,
@UsuarioModificacionID int,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Observacion SET
		Descripcion = @Descripcion,
		TipoObservacionID = @TipoObservacionID,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE(),
		Activo =  @Activo
	WHERE ObservacionID = @ObservacionID
	SET NOCOUNT OFF;
END

GO

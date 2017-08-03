USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoOrganizacion_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoOrganizacion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[TipoOrganizacion_Actualizar]
@TipoOrganizacionID int,
@TipoProcesoID int,
@Descripcion varchar(50),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TipoOrganizacion SET
		TipoProcesoID = @TipoProcesoID,
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE TipoOrganizacionID = @TipoOrganizacionID
	SET NOCOUNT OFF;
END

GO

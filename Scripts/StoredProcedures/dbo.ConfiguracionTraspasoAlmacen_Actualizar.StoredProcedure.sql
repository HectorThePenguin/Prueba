USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 27/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionTraspasoAlmacen_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_Actualizar]
@ConfiguracionTraspasoAlmacenID int,
@TipoAlmacenOrigenID int,
@TipoAlmacenDestinoID int,
@Activo bit,
@UsuarioModificacionID int,
@DiferenteOrganizacion BIT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ConfiguracionTraspasoAlmacen SET
		TipoAlmacenOrigenID = @TipoAlmacenOrigenID,
		TipoAlmacenDestinoID = @TipoAlmacenDestinoID,
		DiferenteOrganizacion = @DiferenteOrganizacion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE ConfiguracionTraspasoAlmacenID = @ConfiguracionTraspasoAlmacenID
	SET NOCOUNT OFF;
END

GO

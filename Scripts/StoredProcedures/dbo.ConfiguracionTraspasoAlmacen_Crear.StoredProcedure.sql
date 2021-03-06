USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 27/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionTraspasoAlmacen_Crear
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_Crear]
@ConfiguracionTraspasoAlmacenID int,
@TipoAlmacenOrigenID int,
@TipoAlmacenDestinoID int,
@Activo bit,
@UsuarioCreacionID int,
@DiferenteOrganizacion bit
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ConfiguracionTraspasoAlmacen (
		ConfiguracionTraspasoAlmacenID,
		TipoAlmacenOrigenID,
		TipoAlmacenDestinoID,
		DiferenteOrganizacion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@ConfiguracionTraspasoAlmacenID,
		@TipoAlmacenOrigenID,
		@TipoAlmacenDestinoID,
		@DiferenteOrganizacion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO

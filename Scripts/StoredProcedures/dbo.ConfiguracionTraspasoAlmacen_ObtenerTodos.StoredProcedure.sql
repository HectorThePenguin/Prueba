USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 27/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionTraspasoAlmacen_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cta.ConfiguracionTraspasoAlmacenID,
		cta.TipoAlmacenOrigenID,
		ta1.Descripcion TipoAlmacenOrigen,
		cta.TipoAlmacenDestinoID,
		ta2.Descripcion AS TipoAlmacenDestino,
		cta.DiferenteOrganizacion,
		cta.Activo
	FROM ConfiguracionTraspasoAlmacen cta
	INNER JOIN TipoAlmacen ta1 on cta.TipoAlmacenOrigenID = ta1.TipoAlmacenID
	INNER JOIN TipoAlmacen ta2 on cta.TipoAlmacenDestinoID = ta2.TipoAlmacenID
	WHERE cta.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 27/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionTraspasoAlmacen_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionTraspasoAlmacen_ObtenerPorID]
@ConfiguracionTraspasoAlmacenID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ConfiguracionTraspasoAlmacenID,
		TipoAlmacenOrigenID,
		TipoAlmacenDestinoID,
		DiferenteOrganizacion,
		Activo
	FROM ConfiguracionTraspasoAlmacen
	WHERE ConfiguracionTraspasoAlmacenID = @ConfiguracionTraspasoAlmacenID
	SET NOCOUNT OFF;
END

GO

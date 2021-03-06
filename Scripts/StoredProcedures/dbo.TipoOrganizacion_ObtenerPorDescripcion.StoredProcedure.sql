USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoOrganizacion_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoOrganizacion_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoOrganizacion_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		o.TipoOrganizacionID,
		o.Descripcion,
		o.TipoProcesoID,
		tp.Descripcion as [TipoProceso],
		o.Activo
	FROM TipoOrganizacion o
	INNER JOIN TipoProceso tp on tp.TipoProcesoID = o.TipoProcesoID
	WHERE o.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO

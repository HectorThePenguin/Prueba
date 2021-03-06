USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoOrganizacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoOrganizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 27/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoOrganizacion_ObtenerPorID 7
--======================================================
CREATE PROCEDURE [dbo].[TipoOrganizacion_ObtenerPorID]
@TipoOrganizacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ot.TipoOrganizacionID,
		ot.TipoProcesoID,
		tp.Descripcion as [TipoProceso],		
		ot.Descripcion,
		ot.Activo
	FROM TipoOrganizacion ot
	inner join TipoProceso tp on  tp.TipoProcesoID=ot.TipoProcesoID
	WHERE TipoOrganizacionID = @TipoOrganizacionID
	SET NOCOUNT OFF;
END

GO

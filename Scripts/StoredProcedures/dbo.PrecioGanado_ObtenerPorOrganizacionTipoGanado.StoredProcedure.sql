USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerPorOrganizacionTipoGanado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioGanado_ObtenerPorOrganizacionTipoGanado]
GO
/****** Object:  StoredProcedure [dbo].[PrecioGanado_ObtenerPorOrganizacionTipoGanado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : PrecioGanado_ObtenerPorOrganizacionTipoGanado 4,1
--======================================================
CREATE PROCEDURE [dbo].[PrecioGanado_ObtenerPorOrganizacionTipoGanado]
@OrganizacionID int,
@TipoGanadoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		PrecioGanadoID,
		OrganizacionID,
		TipoGanadoID,
		Precio,
		FechaVigencia,
		Activo
	FROM PrecioGanado
	WHERE OrganizacionID = @OrganizacionID
	AND TipoGanadoID = @TipoGanadoID
	SET NOCOUNT OFF;
END

GO

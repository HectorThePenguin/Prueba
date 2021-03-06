USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerPorOrganizacionCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoPromedio_ObtenerPorOrganizacionCosto]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerPorOrganizacionCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CostoPromedio_ObtenerPorOrganizacionCosto 4,1
--======================================================
CREATE PROCEDURE [dbo].[CostoPromedio_ObtenerPorOrganizacionCosto]
@OrganizacionID int,
@CostoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CostoPromedioID,
		OrganizacionID,
		CostoID,
		Importe,
		Activo
	FROM CostoPromedio
	WHERE OrganizacionID = @OrganizacionID
	AND CostoID = @CostoID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CamionReparto_ObtenerPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[CamionReparto_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CamionReparto_ObtenerPorOrganizacionID
--======================================================
CREATE PROCEDURE [dbo].[CamionReparto_ObtenerPorOrganizacionID]
@OrganizacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cr.CamionRepartoID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		cc.CentroCostoID,
		cc.Descripcion AS CentroCosto,
		cr.NumeroEconomico,
		cr.Activo
	FROM CamionReparto cr
	inner join Organizacion o on cr.OrganizacionID = o.OrganizacionID
	inner join CentroCosto cc on cr.CentroCostoID = cc.CentroCostoID
	WHERE o.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

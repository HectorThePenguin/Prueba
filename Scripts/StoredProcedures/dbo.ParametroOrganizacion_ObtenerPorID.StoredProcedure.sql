USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroOrganizacion_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorID] @ParametroOrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT po.ParametroOrganizacionID
		,pa.ParametroID
		,pa.Descripcion AS Parametro
		,o.OrganizacionID
		,o.Descripcion AS Organizacion
		,po.Valor
		,po.Activo
	--INTO #ParametroOrganizacion
	FROM ParametroOrganizacion po
	INNER JOIN Organizacion o ON po.OrganizacionID = o.OrganizacionID
	INNER JOIN Parametro pa ON po.ParametroID = pa.ParametroID
	WHERE ParametroOrganizacionID = @ParametroOrganizacionID
	SET NOCOUNT OFF;
END

GO

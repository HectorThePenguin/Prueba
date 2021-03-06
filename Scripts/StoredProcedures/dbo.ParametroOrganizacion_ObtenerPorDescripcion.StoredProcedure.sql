USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroOrganizacion_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_ObtenerPorDescripcion]
@ParametroOrganizacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		PO.ParametroOrganizacionID,
		PO.ParametroID,
		PO.OrganizacionID,
		PO.Valor,
		PO.Activo
		, P.Descripcion AS Parametro
		, O.Descripcion AS Organizacion
	FROM ParametroOrganizacion PO
	INNER JOIN Parametro P
		ON (PO.ParametroID = P.ParametroID)
	INNER JOIN Organizacion O
		ON (PO.OrganizacionID = O.OrganizacionID)
	WHERE PO.ParametroOrganizacionID = @ParametroOrganizacionID
	SET NOCOUNT OFF;
END

GO

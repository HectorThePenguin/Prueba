USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroOrganizacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ParametroOrganizacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroOrganizacion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ParametroOrganizacion_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
			po.ParametroOrganizacionID,
		pa.ParametroID,
		pa.Descripcion AS Parametro,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		po.Valor,
		po.Activo
	FROM ParametroOrganizacion po
	INNER JOIN Organizacion o on po.OrganizacionID = o.OrganizacionID
	INNER JOIN Parametro pa on po.ParametroID = pa.ParametroID
	WHERE po.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

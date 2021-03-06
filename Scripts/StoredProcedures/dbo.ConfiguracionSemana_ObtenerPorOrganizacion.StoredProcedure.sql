USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionSemana_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorOrganizacion]
@OrganizacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cs.ConfiguracionSemanaID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		cs.InicioSemana,
		cs.FinSemana,
		cs.Activo
	FROM ConfiguracionSemana cs
	INNER JOIN Organizacion o ON cs.OrganizacionID = o.OrganizacionID
	WHERE cs.OrganizacionID = @OrganizacionID
	AND cs.Activo = 1
	SET NOCOUNT OFF;
END

GO

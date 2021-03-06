USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/02/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionSemana_ObtenerPorID 1
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorID]
@ConfiguracionSemanaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cs.ConfiguracionSemanaID,
		cs.OrganizacionID,
		o.Descripcion as [Organizacion],
		cs.InicioSemana,
		cs.FinSemana,
		cs.Activo
	FROM ConfiguracionSemana cs 
	inner join Organizacion o on o.OrganizacionID = cs.OrganizacionID
	WHERE ConfiguracionSemanaID = @ConfiguracionSemanaID
	SET NOCOUNT OFF;
END

GO

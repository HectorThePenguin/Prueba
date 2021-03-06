USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionSemana_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[ConfiguracionSemana_ObtenerPorDescripcion]
@ConfiguracionSemanaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ConfiguracionSemanaID,
		OrganizacionID,
		InicioSemana,
		FinSemana,
		Activo
	FROM ConfiguracionSemana
	WHERE ConfiguracionSemanaID = @ConfiguracionSemanaID
	SET NOCOUNT OFF;
END

GO

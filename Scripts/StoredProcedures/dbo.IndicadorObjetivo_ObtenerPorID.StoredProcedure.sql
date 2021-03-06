USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorObjetivo_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_ObtenerPorID]
@IndicadorObjetivoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IndicadorObjetivoID,
		IndicadorProductoCalidadID,
		TipoObjetivoCalidadID,
		OrganizacionID,
		ObjetivoMinimo,
		ObjetivoMaximo,
		Tolerancia,
		Medicion,
		Activo
	FROM IndicadorObjetivo
	WHERE IndicadorObjetivoID = @IndicadorObjetivoID
	SET NOCOUNT OFF;
END

GO

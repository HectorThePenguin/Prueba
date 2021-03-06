USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorObjetivo_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_ObtenerTodos]
@Activo BIT = NULL
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
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

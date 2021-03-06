USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObjetivoCalidad_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObjetivoCalidad_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TipoObjetivoCalidad_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoObjetivoCalidadID,
		Descripcion,
		Activo
	FROM TipoObjetivoCalidad
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

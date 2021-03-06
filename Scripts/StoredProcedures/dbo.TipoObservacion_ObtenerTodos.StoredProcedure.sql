USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObservacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObservacion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TipoObservacion_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoObservacionID,
		Descripcion
		, Activo
	FROM TipoObservacion
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

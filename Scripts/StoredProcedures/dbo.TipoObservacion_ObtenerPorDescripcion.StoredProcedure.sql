USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObservacion_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObservacion_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[TipoObservacion_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoObservacionID,
		Descripcion,
		Activo
	FROM TipoObservacion
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO

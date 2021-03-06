USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObservacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoObservacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObservacion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoObservacion_ObtenerPorID]
@TipoObservacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoObservacionID,
		Descripcion
		, Activo
	FROM TipoObservacion
	WHERE TipoObservacionID = @TipoObservacionID
	SET NOCOUNT OFF;
END

GO

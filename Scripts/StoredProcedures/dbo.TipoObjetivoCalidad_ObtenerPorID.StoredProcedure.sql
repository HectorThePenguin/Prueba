USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoObjetivoCalidad_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoObjetivoCalidad_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoObjetivoCalidad_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[TipoObjetivoCalidad_ObtenerPorID]
@TipoObjetivoCalidadID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoObjetivoCalidadID,
		Descripcion,
		Activo
	FROM TipoObjetivoCalidad
	WHERE TipoObjetivoCalidadID = @TipoObjetivoCalidadID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Observacion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Observacion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[Observacion_ObtenerPorID]
@ObservacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		o.ObservacionID,
		o.Descripcion,
		tob.TipoObservacionID,
		tob.Descripcion [TipoObservacion],
		o.Activo
	FROM Observacion o
	INNER JOIN TipoObservacion tob on o.TipoObservacionID = tob.TipoObservacionID
	WHERE ObservacionID = @ObservacionID
	SET NOCOUNT OFF;
END

GO

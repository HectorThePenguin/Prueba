USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Observacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Observacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Observacion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Observacion_ObtenerTodos]
@Activo BIT = NULL
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
	WHERE o.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

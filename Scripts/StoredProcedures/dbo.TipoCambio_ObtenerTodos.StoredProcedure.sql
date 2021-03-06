USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCambio_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TipoCambio_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoCambioID,
		Descripcion,
		Cambio,
		Fecha,
		Activo
	FROM TipoCambio
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

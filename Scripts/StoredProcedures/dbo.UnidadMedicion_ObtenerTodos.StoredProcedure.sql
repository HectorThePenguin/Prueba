USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[UnidadMedicion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[UnidadMedicion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[UnidadMedicion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 15/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : UnidadMedicion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[UnidadMedicion_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		UnidadID,
		Descripcion,
		ClaveUnidad,
		Activo
	FROM UnidadMedicion
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

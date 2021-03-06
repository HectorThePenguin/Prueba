USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoParametro_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoParametro_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoParametro_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TipoParametro_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoParametroID,
		Descripcion,
		Activo
	FROM TipoParametro
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

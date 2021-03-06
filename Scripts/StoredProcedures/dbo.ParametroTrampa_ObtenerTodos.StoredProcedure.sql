USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ParametroTrampaID,
		ParametroID,
		TrampaID,
		Valor,
		Activo
	FROM ParametroTrampa
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO

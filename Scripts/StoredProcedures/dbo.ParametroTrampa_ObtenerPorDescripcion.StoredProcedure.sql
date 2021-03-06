USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_ObtenerPorDescripcion]
@ParametroTrampaID int
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
	WHERE ParametroTrampaID = @ParametroTrampaID
	SET NOCOUNT OFF;
END

GO

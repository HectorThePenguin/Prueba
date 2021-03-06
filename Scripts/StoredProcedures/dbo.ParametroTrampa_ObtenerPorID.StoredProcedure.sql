USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_ObtenerPorID]
@ParametroTrampaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		PT.ParametroTrampaID,
		PT.ParametroID,
		PT.TrampaID,
		PT.Valor,
		PT.Activo,
		P.Descripcion AS Parametro
		, T.Descripcion AS Trampa
	FROM ParametroTrampa PT
	INNER JOIN Parametro P
		ON (PT.ParametroID = P.ParametroID)
	INNER JOIN Trampa T
		ON (PT.TrampaID = T.TrampaID)
	WHERE PT.ParametroTrampaID = @ParametroTrampaID
	SET NOCOUNT OFF;
END

GO

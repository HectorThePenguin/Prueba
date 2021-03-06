USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorParametroTrampa]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ParametroTrampa_ObtenerPorParametroTrampa]
GO
/****** Object:  StoredProcedure [dbo].[ParametroTrampa_ObtenerPorParametroTrampa]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ParametroTrampa_ObtenerPorParametroTrampa 9, 1
--======================================================
CREATE PROCEDURE [dbo].[ParametroTrampa_ObtenerPorParametroTrampa]
@ParametroID INT,
@TrampaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		PT.ParametroTrampaID,
		PT.ParametroID,
		PT.TrampaID,
		PT.Valor,
		PT.Activo,
		P.Descripcion AS Parametro,
		T.Descripcion AS Trampa,
		TP.TipoParametroID,
		TP.Descripcion AS TipoParametro
	FROM ParametroTrampa PT
	INNER JOIN Parametro P
		ON (PT.ParametroID = P.ParametroID
			AND PT.ParametroID = @ParametroID)
	INNER JOIN Trampa T
		ON (PT.TrampaID = T.TrampaID
			AND PT.TrampaID = @TrampaID)
	INNER JOIN TipoParametro TP
		ON (P.TipoParametroID = TP.TipoParametroID)
	SET NOCOUNT OFF;
END

GO

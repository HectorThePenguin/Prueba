USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 05/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Trampa_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[Trampa_ObtenerPorID]
@TrampaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		T.TrampaID,
		T.Descripcion,
		T.OrganizacionID,
		T.TipoTrampa,
		T.HostName,
		T.Activo
		, O.Descripcion AS Organizacion
	FROM Trampa T
	INNER JOIN Organizacion O
		ON (T.OrganizacionID = O.OrganizacionID)
	WHERE T.TrampaID = @TrampaID
	SET NOCOUNT OFF;
END

GO

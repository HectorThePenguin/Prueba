USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Trampa_ObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Trampa_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 06/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Trampa_ObtenerPorOrganizacion 4
--======================================================
CREATE PROCEDURE [dbo].[Trampa_ObtenerPorOrganizacion]
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		T.TrampaID,
		T.Descripcion,
		T.OrganizacionID,
		T.TipoTrampa,
		T.HostName,
		T.Activo,
		O.Descripcion AS Organizacion
	FROM Trampa T
	INNER JOIN Organizacion O
		ON (T.OrganizacionID = O.OrganizacionID)
	WHERE T.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

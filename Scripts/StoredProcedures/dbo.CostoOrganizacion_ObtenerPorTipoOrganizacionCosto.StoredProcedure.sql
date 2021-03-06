USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorTipoOrganizacionCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorTipoOrganizacionCosto]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorTipoOrganizacionCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CostoOrganizacion_ObtenerPorTipoOrganizacionCosto 1, 1
--======================================================
CREATE PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorTipoOrganizacionCosto]
@TipoOrganizacionID INT,
@CostoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY C.Descripcion ASC) AS [RowNum],
		CO.CostoOrganizacionID,
		CO.TipoOrganizacionID,
		CO.CostoID,
		CO.Automatico,
		CO.Activo
		, C.Descripcion AS Costo
		, C.ClaveContable
		, TOR.Descripcion AS TipoOrganizacion
		, R.RetencionID
		, R.Descripcion AS Retencion
	FROM CostoOrganizacion CO
	INNER JOIN Costo C
		ON (CO.CostoID = C.CostoID
			AND @CostoID = CO.CostoID)
	INNER JOIN TipoOrganizacion TOR
		ON (CO.TipoOrganizacionID = TOR.TipoOrganizacionID
			AND @TipoOrganizacionID = CO.TipoOrganizacionID)
	LEFT OUTER JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	SET NOCOUNT OFF;
END

GO

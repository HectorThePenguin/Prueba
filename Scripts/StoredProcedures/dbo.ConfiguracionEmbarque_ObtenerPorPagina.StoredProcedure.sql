USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionEmbarque_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionEmbarque_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 13/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionEmbarque_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionEmbarque_ObtenerPorPagina]
@ConfiguracionEmbarqueID INT,
@OrganizacionOrigenID INT,
@OrganizacionDestinoID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY OrganizacionOrigenID,OrganizacionDestinoID ASC) AS [RowNum],
		ConfiguracionEmbarqueID,
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		Kilometros,
		Horas,
		Activo
	INTO #ConfiguracionEmbarque
	FROM ConfiguracionEmbarque
	WHERE @OrganizacionOrigenID in (0, OrganizacionOrigenID)
	AND @OrganizacionDestinoID in (0, OrganizacionDestinoID)
	AND Activo = @Activo
	--(Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '' 
	SELECT
		c.ConfiguracionEmbarqueID,
		c.OrganizacionOrigenID,
		oo.Descripcion as [OrganizacionOrigen],
		c.OrganizacionDestinoID,
		od.Descripcion as [OrganizacionDestino],
		c.Kilometros,
		c.Horas,
		c.Activo
	FROM #ConfiguracionEmbarque c
	INNER JOIN Organizacion oo on oo.OrganizacionID = c.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = c.OrganizacionDestinoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ConfiguracionEmbarqueID) AS [TotalReg]
	FROM #ConfiguracionEmbarque
	DROP TABLE #ConfiguracionEmbarque
	SET NOCOUNT OFF;
END

GO

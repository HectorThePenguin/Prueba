USE [SIAP]
GO
/**** Object:  StoredProcedure [dbo].[AdministracionRuteoDetalle_ObtenerPorPagina]    Script Date: 19/05/2017 09:31:44 a.m. ****/
DROP PROCEDURE [dbo].[AdministracionRuteoDetalle_ObtenerPorPagina]
GO
/**** Object:  StoredProcedure [dbo].[AdministracionRuteoDetalle_ObtenerPorPagina]    Script Date: 19/05/2017 09:31:44 a.m. ****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Luis Manuel García López
-- Create date: 22/05/2017 12:00:00 a.m.
-- Description: Obtiene el detalle del ruteo por pagina
-- SpName     : AdministracionRuteoDetalle_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[AdministracionRuteoDetalle_ObtenerPorPagina]
@RuteoDetalleID INT,
@RuteoID INT,
@OrganizacionOrigenID INT,
@OrganizacionDestinoID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #RuteoDetalle(
	[RowNum] int NOT NULL,
	[RuteoDetalleID] int NOT NULL,
	[OrganizacionOrigenID] int NOT NULL ,
	[OrganizacionDestinoID] int NOT NULL ,
	[Kilometros] DECIMAL(10,2) NOT NULL ,
	[Horas] DECIMAL(4,1) NOT NULL ,
	[Activo] bit NOT NULL DEFAULT ((1))
	);
	INSERT INTO #RuteoDetalle 
	SELECT
		ROW_NUMBER() OVER (ORDER BY OrganizacionOrigenID,OrganizacionDestinoID ASC) AS [RowNum],
		RuteoDetalleID,
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		Kilometros,
		Horas,
		Activo
	FROM RuteoDetalle
	WHERE @RuteoDetalleID in (0,RuteoDetalleID)
	AND @RuteoID in (0,RuteoID)
	AND @OrganizacionOrigenID in (0, OrganizacionOrigenID)
	AND @OrganizacionDestinoID in (0, OrganizacionDestinoID)
	AND Activo = @Activo
	
	SELECT
		e.RuteoDetalleID,
		e.OrganizacionOrigenID,
		e.Kilometros,
		e.Horas,
		oo.Descripcion as [OrganizacionOrigen],
		e.OrganizacionDestinoID,
		od.Descripcion as [OrganizacionDestino],
		e.Activo
	FROM #RuteoDetalle e
	INNER JOIN Organizacion oo on oo.OrganizacionID = e.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = e.OrganizacionDestinoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(RuteoDetalleID) AS [TotalReg]
	FROM #RuteoDetalle
	DROP TABLE #RuteoDetalle
	SET NOCOUNT OFF;
END

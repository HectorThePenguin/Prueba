USE [SIAP]
GO
/**** Object:  StoredProcedure [dbo].[AdministracionRuteo_ObtenerPorPagina]    Script Date: 19/05/2017 09:31:44 a.m. ****/
DROP PROCEDURE [dbo].[AdministracionRuteo_ObtenerPorPagina]
GO
/**** Object:  StoredProcedure [dbo].[AdministracionRuteo_ObtenerPorPagina]    Script Date: 19/05/2017 09:31:44 a.m. ****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Luis Manuel García López
-- Create date: 19/05/2017 12:00:00 a.m.
-- Description: Obtiene los registros de para la administracion de ruteo paginado
-- SpName     : AdministracionRuteo_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[AdministracionRuteo_ObtenerPorPagina]
@RuteoID INT,
@OrganizacionOrigenID INT,
@OrganizacionDestinoID INT,
@NombreRuteo VARCHAR(255),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #Ruteo(
	[RowNum] int NOT NULL,
	[RuteoID] int NOT NULL,
	[OrganizacionOrigenID] int NOT NULL ,
	[OrganizacionDestinoID] int NOT NULL ,
	[NombreRuteo] VARCHAR(255) NOT NULL ,
	[Activo] bit NOT NULL DEFAULT ((1))
	);
	INSERT INTO #Ruteo 
	SELECT
		ROW_NUMBER() OVER (ORDER BY OrganizacionOrigenID,OrganizacionDestinoID ASC) AS [RowNum],
		RuteoID,
		OrganizacionOrigenID,
		OrganizacionDestinoID,
		NombreRuteo,
		Activo
	FROM Ruteo
	WHERE @RuteoID in (0,RuteoID)
	AND (NombreRuteo like '%' + @NombreRuteo + '%' OR @NombreRuteo = '')
	AND @OrganizacionOrigenID in (0, OrganizacionOrigenID)
	AND @OrganizacionDestinoID in (0, OrganizacionDestinoID)
	AND Activo = @Activo
	--(Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '' 
	SELECT
		e.RuteoID,
		e.OrganizacionOrigenID,
		e.NombreRuteo,
		oo.Descripcion as [OrganizacionOrigen],
		e.OrganizacionDestinoID,
		od.Descripcion as [OrganizacionDestino],
		substring(
        (
            SELECT oor.Descripcion + ' - '  AS [text()]
            FROM RuteoDetalle ST1
						INNER JOIN Organizacion oor ON oor.OrganizacionID=ST1.OrganizacionOrigenID
            WHERE ST1.RuteoID = e.RuteoID
            ORDER BY ST1.RuteoDetalleID
            FOR XML PATH ('')
        )+
				(
            SELECT TOP (1)ooD.Descripcion  AS [text()]
            FROM RuteoDetalle ST1
						INNER JOIN Organizacion ooD ON ooD.OrganizacionID=ST1.OrganizacionDestinoID
            WHERE ST1.RuteoID = e.RuteoID
            ORDER BY ST1.RuteoDetalleID DESC
            FOR XML PATH ('')
        ), 1, 1000) [Rutas],
		e.Activo
	FROM #Ruteo e
	INNER JOIN Organizacion oo on oo.OrganizacionID = e.OrganizacionOrigenID
	INNER JOIN Organizacion od on od.OrganizacionID = e.OrganizacionDestinoID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(RuteoID) AS [TotalReg]
	FROM #Ruteo
	DROP TABLE #Ruteo
	SET NOCOUNT OFF;
END



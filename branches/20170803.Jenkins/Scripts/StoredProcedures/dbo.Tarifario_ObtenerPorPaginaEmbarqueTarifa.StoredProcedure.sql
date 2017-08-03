USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tarifario_ObtenerPorPaginaEmbarqueTarifa]    Script Date: 22/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Tarifario_ObtenerPorPaginaEmbarqueTarifa]
GO
/****** Object:  StoredProcedure [dbo].[Tarifario_ObtenerPorPaginaEmbarqueTarifa]    Script Date: 22/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 22-05-2017
-- Description: sp para regresar el listado de embarque tarifa
-- SpName     : Tarifario_ObtenerPorPaginaEmbarqueTarifa 0,0,0,1,1,15
--======================================================  
CREATE PROCEDURE [dbo].[Tarifario_ObtenerPorPaginaEmbarqueTarifa]
@proveedorId INT,
@organizacionOrigenId INT,
@organizacionDestinoId INT,
@Activo BIT,
@Inicio int,
@Limite int
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #EmbarqueTarifa(
	[RowNum] int NOT NULL,
	[embarqueTarifaId] int NOT NULL,
	[proveedor] VARCHAR(255) NOT NULL,
	[origen] VARCHAR(255) NOT NULL ,
	[destino] VARCHAR(255) NOT NULL ,
	[ruta] VARCHAR(255) NOT NULL ,
	[kilometros] DECIMAL(10,2) NOT NULL ,
	[tarifa] DECIMAL(10,2) NOT NULL,
	[activo] bit NOT NULL DEFAULT ((1))
	);
	INSERT INTO #EmbarqueTarifa 
	SELECT
		ROW_NUMBER() OVER (ORDER BY OrganizacionOrigenID,OrganizacionDestinoID ASC) AS [RowNum],
		ET.EmbarqueTarifaID as embarqueTarifaId,
		P.Descripcion AS proveedor,
		OO.Descripcion AS origen,
		OD.Descripcion AS destino,
		CED.Descripcion AS ruta,
		CED.Kilometros AS kilometros,
		ET.ImporteTarifa AS tarifa,
		ET.Activo AS activo
	FROM EmbarqueTarifa AS ET
	INNER JOIN ConfiguracionEmbarqueDetalle AS CED ON ET.ConfiguracionEmbarqueDetalleID = CED.ConfiguracionEmbarqueDetalleID
	INNER JOIN ConfiguracionEmbarque AS CE ON CED.ConfiguracionEmbarqueId = CE.ConfiguracionEmbarqueID
	INNER JOIN Proveedor AS P ON ET.ProveedorID = P.ProveedorID
	INNER JOIN Organizacion AS OO ON CE.OrganizacionOrigenID = OO.OrganizacionID
	INNER JOIN Organizacion AS OD ON CE.OrganizacionDestinoID = OD.OrganizacionID
	WHERE
	(@proveedorId = 0 or ET.ProveedorID = @proveedorId) AND
	(@organizacionOrigenId = 0 or CE.OrganizacionOrigenID = @organizacionOrigenId) AND
	(@organizacionDestinoId = 0 or CE.OrganizacionDestinoID = @organizacionDestinoId) AND
	ET.Activo = @Activo

	SELECT 
	[embarqueTarifaId],
	[proveedor],
	[origen],
	[destino],
	[ruta],
	[kilometros],
	[tarifa],
	[activo]
	FROM #EmbarqueTarifa
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(embarqueTarifaId) AS [TotalReg]
	FROM #EmbarqueTarifa

	DROP TABLE #EmbarqueTarifa
	SET NOCOUNT OFF;
END

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Camion_ObtenerPorPagina 0,'038',1,1,15
--======================================================
--======================================================
-- Edito     : Guillermo Osuna Covarrubias
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: Se agregaron los siguientes campos: NumEconomico, Color, Modelo, mARCAid, Descripcion, Boletinado y Observaciones.
-- SpName     : Camion_ObtenerPorPagina 0,'038',1,1,15
--======================================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorPagina]
@PlacaCamion VARCHAR(10),
@ProveedorID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY PlacaCamion ASC) AS [RowNum],
		c.CamionID,
		c.ProveedorID,
		c.PlacaCamion,
		c.NumEconomico,
		c.Color,
		c.Modelo,
		c.MarcaID,
		cm.Descripcion as [MarcaDescripcion],
		c.Boletinado,
		c.Observaciones,
		c.Activo
	INTO #Camion
	FROM Camion c
	LEFT JOIN Marca cm ON cm.MarcaID = c.MarcaID AND cm.Activo = 1 AND cm.Tracto = 1
	WHERE (c.Activo = @Activo OR @Activo is null)
				AND PlacaCamion LIKE '%' + @PlacaCamion + '%'
				AND @ProveedorID IN (ProveedorID, 0)
				AND c.Activo = @Activo
	SELECT
		c.CamionID,
		c.ProveedorID,
		p.Descripcion as [Proveedor],
		p.CodigoSAP,
		c.PlacaCamion,
		c.NumEconomico,
		c.Color,
		c.Modelo,
		c.MarcaID,
		c.MarcaDescripcion,
		c.Boletinado,
		c.Observaciones,
		c.Activo
	FROM #Camion c
	INNER JOIN Proveedor p on p.ProveedorID = c.ProveedorID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CamionID) AS [TotalReg]
	FROM #Camion
	DROP TABLE #Camion
	SET NOCOUNT OFF;
END

GO
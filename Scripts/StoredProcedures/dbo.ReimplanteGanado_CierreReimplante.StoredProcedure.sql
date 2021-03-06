USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_CierreReimplante]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_CierreReimplante]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_CierreReimplante]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 26/06/2015
-- Description: SP para cerrar las Programaciones de Reimplante
-- EXEC ReimplanteGanado_CierreReimplante 4,1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_CierreReimplante] 
@OrganizacionID INT,
@UsuarioID INT
AS
BEGIN
	
	DECLARE @TipoMovimientoReimplante INT = 6

	CREATE TABLE #ReimplantesProgramados
	(
		FolioProgramacionID INT,		
		LoteID INT,
		FechaProgramacion DATE,
		PesoReal INT,
		FechaReal DATE,
		Procesado BIT
	)

	CREATE TABLE #PesosReimplantes
	(
		LoteID INT,
		PesoReal INT,
		FechaReal DATE
	)

INSERT INTO #ReimplantesProgramados
	SELECT
		pr.FolioProgramacionID,
		pr.LoteID,
		pr.Fecha AS FechaProgramacion,
		0,
		NULL,
		0
	FROM ProgramacionReimplante pr
	WHERE pr.Activo = 1
	AND pr.OrganizacionID = @OrganizacionID

INSERT INTO #PesosReimplantes
	SELECT
		am.LoteIDOrigen AS LoteID,
		AVG(am.Peso) AS PesoReal,
		CAST(ROUND(AVG(ROUND(CAST(am.FechaMovimiento AS FLOAT), 0, 1)), 0) AS SMALLDATETIME) AS FechaReal
	FROM AnimalMovimiento am
	INNER JOIN #ReimplantesProgramados rp ON am.LoteIDOrigen = rp.LoteID
	WHERE am.TipoMovimientoID = @TipoMovimientoReimplante
	AND (CAST(am.FechaMovimiento AS DATE) >= (CAST(rp.FechaProgramacion AS DATETIME) - 7) AND CAST(am.FechaMovimiento AS DATE) <= (CAST(rp.FechaProgramacion AS DATETIME) + 7))
	GROUP BY am.LoteIDOrigen

UPDATE rp
SET	rp.PesoReal = pr.PesoReal,
	rp.FechaReal = pr.FechaReal,
	rp.Procesado = 1
FROM #ReimplantesProgramados rp
INNER JOIN #PesosReimplantes pr ON rp.LoteID = pr.LoteID

UPDATE lr
SET	lr.FechaReal = rp.FechaReal,
	lr.PesoReal = rp.PesoReal,
	lr.FechaModificacion = GETDATE(),
	lr.UsuarioModificacionID = @UsuarioID
FROM LoteProyeccion lp
INNER JOIN #ReimplantesProgramados rp ON lp.LoteID = rp.LoteID
INNER JOIN LoteReimplante lr ON lp.LoteProyeccionID = lr.LoteProyeccionID	
WHERE rp.Procesado = 1
AND (CAST(lr.FechaProyectada AS DATE) >= (CAST(rp.FechaReal AS DATETIME) - 7) AND CAST(lr.FechaProyectada AS DATE) <= (CAST(rp.FechaReal AS DATETIME) + 7))

UPDATE pr
SET	pr.Activo = 0,
	pr.Trabajado = 1,
	pr.FechaModificacion = GETDATE(),
	pr.UsuarioModificacionID = @UsuarioID
FROM ProgramacionReimplante pr
INNER JOIN #ReimplantesProgramados rp ON pr.FolioProgramacionID = rp.FolioProgramacionID
WHERE rp.Procesado = 1
AND (CAST(pr.Fecha AS DATE) >= (CAST(rp.FechaReal AS DATETIME) - 7) AND CAST(pr.Fecha AS DATE) <= (CAST(rp.FechaReal AS DATETIME) + 7))

UPDATE pr
SET	pr.Activo = 0,
	pr.Trabajado = 0,
	pr.FechaModificacion = GETDATE(),
	pr.UsuarioModificacionID = @UsuarioID
FROM ProgramacionReimplante pr
WHERE CAST(pr.Fecha AS DATE) < CAST(GETDATE() - 7 AS DATE)
AND pr.Activo = 1
and pr.OrganizacionID = @OrganizacionID


update re
set re.PesoRepeso = ISNULL(rp.PesoReal,0),
re.UsuarioModificacionID = @UsuarioID,
re.FechaModificacion = GETDATE()
from Reparto re
inner join #ReimplantesProgramados rp on re.LoteID = rp.LoteID
where rp.Procesado = 1
and CAST(re.Fecha AS DATE) = CAST(rp.FechaReal AS DATE)

END
GO

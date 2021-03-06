USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDetalleReimplante]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteDetalleReimplante]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDetalleReimplante]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gumaro Alberto Lugo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ReporteDetalleReimplante 1, '20150615'
--======================================================
CREATE PROCEDURE [dbo].[ReporteDetalleReimplante]
   @OrganizacionID AS INT ,
   @Fecha DATE 

AS
  

  --Obtenemos los movimientos de Reimplante
	CREATE TABLE #Reimplantes (
		AnimalId bigint,
		MaxReimplante bigint,
		CodigoCorral varchar(10),
		xPeso int,
		xFecha smalldatetime,
		AnimalMovimientoIDAnterior  BIGINT
	)

	CREATE TABLE #Cortes (
		AnimalId bigint,
		MaxCorte bigint,
		CodigoCorral varchar(10),
		xPeso int,
		xFecha smalldatetime
	)

INSERT INTO #Reimplantes
	SELECT
		AnimalID,
		MaxReimplante = MAX(AnimalMovimientoID),
		CodigoCorral = '',
		xPeso = 0,
		xFecha = GETDATE(),
		AnimalMovimientoIDAnterior
	FROM dbo.AnimalMovimiento
	WHERE TipoMovimientoID = 6
	AND OrganizacionID = @OrganizacionID
	AND CAST(FechaMovimiento AS DATE) = @Fecha
	GROUP BY AnimalID,AnimalMovimientoIDAnterior
--obtenemos los movimientos de corte
INSERT INTO #Cortes
	SELECT
		ac.AnimalID,
		MaxCorte = MAX(AnimalMovimientoID),
		CodigoCorral = '',
		xPeso = 0,
		xFecha = GETDATE()
	FROM AnimalMovimiento ac
	INNER JOIN #Reimplantes ar
		ON ar.AnimalID = ac.AnimalID AND ac.AnimalMovimientoID = ar.AnimalMovimientoIDAnterior	
	GROUP BY ac.AnimalID

UPDATE A
SET	A.CodigoCorral = CR.codigo,
	A.xPeso = mov.Peso,
	A.xFecha = fechamovimiento
FROM #Reimplantes A
INNER JOIN dbo.AnimalMovimiento mov
	ON mov.AnimalID = A.AnimalID
INNER JOIN corral CR
	ON mov.CorralId = CR.CorralId
WHERE CR.OrganizacionId = @OrganizacionId
AND A.MaxReimplante = mov.AnimalMovimientoID

UPDATE A
SET	A.CodigoCorral = CR.Codigo,
	A.xPeso = an.pesoCompra,
	A.xFecha = fechamovimiento
FROM #Cortes A
INNER JOIN dbo.AnimalMovimiento mov
	ON mov.AnimalID = A.AnimalID
INNER JOIN Animal an
	ON mov.AnimalID = an.animalId
INNER JOIN corral CR
	ON mov.CorralId = CR.CorralId
WHERE CR.OrganizacionId = @OrganizacionId
AND A.MaxCorte = mov.AnimalMovimientoID


SELECT
DISTINCT
	a.Arete,
	tg.Descripcion,
	cor.CodigoCorral AS CorralOrigen,
	r.CodigoCorral AS CorralDestino,
	cor.xPeso AS PesoCorte,
	r.xPeso AS PesoReimplante,
	GananciaDiaria = CAST((CAST((r.xPeso - cor.xPeso) AS INT) / CASE
		WHEN CAST(DATEDIFF(DAY,
			cor.xFecha,
			r.xFecha) AS DECIMAL(10, 4)) = 0 THEN CAST(1 AS DECIMAL(10, 4))
		ELSE CAST(DATEDIFF(DAY,
			cor.xFecha,
			r.xFecha) AS DECIMAL(10, 4))
	END) AS DECIMAL(10, 4)),
	DATEDIFF(DAY, cor.xFecha, r.xFecha) AS DiasEngorda,
	Fecha_Consulta = @Fecha,
	Titulo = '',
	Organizacion = ''
FROM AnimalMovimiento am
INNER JOIN #Reimplantes r
	ON r.AnimalID = am.AnimalID
INNER JOIN #Cortes cor
	ON cor.AnimalID = am.AnimalID
INNER JOIN Animal a
	ON a.AnimalID = am.AnimalID
INNER JOIN TipoGanado tg
	ON a.TipoGanadoID = tg.TipoGanadoID
WHERE cor.CodigoCorral <> 'ZZZ'
ORDER BY Arete

DROP TABLE #Reimplantes
DROP TABLE #Cortes


GO

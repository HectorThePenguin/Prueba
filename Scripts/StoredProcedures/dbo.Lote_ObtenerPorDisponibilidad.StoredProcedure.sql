USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorDisponibilidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorDisponibilidad]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorDisponibilidad]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/02/11
-- Description: Obtiene Lotes por Disponibilidad
-- Lote_ObtenerPorDisponibilidad 1,2
-- Modificado : Roque Solis
-- Fecha      : 28/11/2014
-- Se agrego la consulta para obtener los datos del reporte proyector
--=============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerPorDisponibilidad] 
--@FechaInicio DATETIME
--	,@FechaFin DATETIME
	--,
	@OrganizacionID INT,
	@TipoCorralProduccion INT
AS
BEGIN
	
	SET NOCOUNT ON

	DECLARE @TipoCorralImproductivo INT
	SET  @TipoCorralImproductivo = 5

	CREATE TABLE #LOTES (
		LoteID INT
		,Lote VARCHAR(20)
		,Cabezas INT
		,FechaDisponibilidad SMALLDATETIME
		,Codigo VARCHAR(10)
		,PesoSacrificio INT
		,DiasEngorda INT
		,FechaInicio SMALLDATETIME
		,GananciaDiaria DECIMAL(10,2)
		,Revision BIT
		)
		
	CREATE TABLE #TMPPROYECTOR (
		 CodigoCorral VARCHAR(10)
		,LoteID INT
		,CodigoLote VARCHAR(10)
		,Cabezas INT
		,TipoGanado VARCHAR(50)
		,Clasificacion VARCHAR(50)
		,PesoOrigen INT
		,Merma DECIMAL(18, 2)
		,PesoProyectado INT
		,GananciaDiaria DECIMAL(18, 2)
		,DiasEngorda INT
		,FechaReimplante1 DATE
		,PesoReimplante1 INT
		,GananciaReimplante1 DECIMAL(18,2)
		,FechaReimplante2 DATE
		,PesoReimplante2 INT
		,GananciaReimplante2 DECIMAL(18,2)
		,FechaReimplante3 DATE
		,PesoReimplante3 INT
		,GananciaReimplante3 DECIMAL(18,2)
		,DiasF4 INT
		,DiasZilmax INT
		,FechaSacrificio DATE
	)

	INSERT INTO #LOTES
	SELECT L.LoteID
		,L.Lote
		,L.Cabezas
		,L.FechaDisponibilidad
		,C.Codigo
		,LP.PesoSacrificio
		,LP.DiasEngorda
		,L.FechaInicio
		,LP.GananciaDiaria
		,LP.Revision
	FROM Lote L	
	INNER JOIN LoteProyeccion LP ON (
			L.LoteID = LP.LoteID
			AND L.OrganizacionID = LP.OrganizacionID
			)
	INNER JOIN Corral C ON (
			L.CorralID = C.CorralID
			AND L.OrganizacionID = C.OrganizacionID
			)
	WHERE L.DisponibilidadManual = 0	
	AND l.OrganizacionID = @OrganizacionID	
		AND l.Activo = 1
		AND (C.TipoCorralID = @TipoCorralProduccion OR C.TipoCorralID = @TipoCorralImproductivo OR LP.Revision = 1)

	INSERT INTO #LOTES
	SELECT L.LoteID
		,L.Lote
		,L.Cabezas
		,L.FechaDisponibilidad
		,C.Codigo
		,LP.PesoSacrificio
		,LP.DiasEngorda
		,L.FechaInicio
		,LP.GananciaDiaria
		,LP.Revision
	FROM Lote L	
	INNER JOIN LoteProyeccion LP ON (
			L.LoteID = LP.LoteID
			AND L.OrganizacionID = LP.OrganizacionID
			)
	INNER JOIN Corral C ON (
			L.CorralID = C.CorralID
			AND L.OrganizacionID = C.OrganizacionID
			)
	WHERE L.DisponibilidadManual = 0		
		AND l.OrganizacionID = @OrganizacionID
		AND l.FechaDisponibilidad IS NULL
		AND l.Activo = 1
		AND (C.TipoCorralID = @TipoCorralProduccion OR C.TipoCorralID = @TipoCorralImproductivo OR LP.Revision = 1)
		
		
		INSERT INTO #TMPPROYECTOR
		(CodigoCorral, LoteID, CodigoLote, Cabezas, TipoGanado,
		 Clasificacion, PesoOrigen, Merma, PesoProyectado,GananciaDiaria, 
		 DiasEngorda, FechaReimplante1, PesoReimplante1, GananciaReimplante1, FechaReimplante2, 
		 PesoReimplante2, GananciaReimplante2, FechaReimplante3, PesoReimplante3, GananciaReimplante3, 
		 DiasF4, DiasZilmax ,FechaSacrificio)
		EXEC ReporteProyectorComportamiento_ObtenerDatosReporte @OrganizacionID

	SELECT DISTINCT L.LoteID
		,L.Lote
		,L.Cabezas
		,L.FechaDisponibilidad
		,L.Codigo
		,L.PesoSacrificio
		,L.DiasEngorda
		,L.FechaInicio
		,L.GananciaDiaria
		,L.Revision
		,P.Clasificacion
		,P.Merma
		,P.PesoOrigen
		,P.PesoProyectado
		,P.FechaReimplante1 
		,P.PesoReimplante1 
		,P.GananciaReimplante1 
		,P.FechaReimplante2 
		,P.PesoReimplante2 
		,P.GananciaReimplante2 
		,P.FechaReimplante3 
		,P.PesoReimplante3 
		,P.GananciaReimplante3 
		,P.DiasF4
		,P.DiasZilmax
	FROM #LOTES L
INNER JOIN #TMPPROYECTOR P ON L.LoteID = P.LoteID
	

	SET NOCOUNT OFF
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteTabularDisponibilidadSemanal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteTabularDisponibilidadSemanal]
GO
/****** Object:  StoredProcedure [dbo].[ReporteTabularDisponibilidadSemanal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar - Anabel Rojas
-- Create date: 29/07/2014
-- Description: Consulta para obtener la informacion base para el reporte Tabular Disponibilidad Semanal
-- Empresa: Apinterfaces
-- Uso: ReporteTabularDisponibilidadSemanal 2 , 2 , '20150623'
-- =============================================|
CREATE PROCEDURE [dbo].[ReporteTabularDisponibilidadSemanal] 
@OrganizacionID int, 
@TipoCorral int, 
@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON

	declare @FechaValor DATE
	declare @TipoCorralImproductivosID INT
	
	set @TipoCorralImproductivosID = 5
	set @FechaValor = CAST(@Fecha AS DATETIME) - 7

	SET DATEFIRST 1

	
	CREATE TABLE #Lotes
	(
		CorralId int,
		Codigo varchar(10),
		LoteId int,
		FechaCierre smalldatetime,
		Cabezas int,
		FechaDisponibilidadProyectada smalldatetime,
		FechaDisponibilidad smalldatetime,
		DisponibilidadManual bit,
		PesoTotalLote int,
		PesoPromedio int,
		Sexo varchar(1),
		FechaInicioSemana smalldatetime,
		FechaFinSemana smalldatetime
	);


	INSERT INTO #Lotes 
	Select C.CorralId, C.Codigo, L.LoteID, L.FechaCierre, L.Cabezas, 
	DATEADD(DAY, LP.DiasEngorda, L.FechaInicio) AS FechaDisponibilidadProyectada,
	isnull(L.FechaDisponibilidad, DATEADD(DAY, LP.DiasEngorda, L.FechaInicio)) as FechaDisponibilidad, 
	L.DisponibilidadManual,
	SUM(A.pesoCompra) as pesoTotalLote,
	SUM(A.pesoCompra) / L.Cabezas as pesoPromedio,
	(SELECT TOP 1 Sexo 
			FROM TipoGanado 
			where TipoGanadoID = 
			(SELECT TOP 1 AA.TipoGanadoId
				FROM Animal AA 
				INNER JOIN AnimalMovimiento AAMM ON AA.AnimalID = AAMM.AnimalID
				WHERE AAMM.LoteID = L.LoteID AND AAMM.Activo = 1) )
				as Sexo, NULL, NULL

	from Corral C
	Inner join Lote L on L.CorralID = C.CorralID
	Inner join AnimalMovimiento AM on L.LoteID = AM.LoteId
	INNER JOIN Animal A on (A.AnimalID = AM.AnimalID AND a.Activo = 1)
	INNER JOIN LoteProyeccion LP ON L.LoteID = LP.LoteID
	WHERE AM.Activo = 1 
	AND C.Codigo <> 'ZZZ'
	AND L.Activo = 1 
	AND L.OrganizacionID = @OrganizacionId 
	AND  L.TipoCorralID in (@TipoCorral ,@TipoCorralImproductivosID)
	group by C.CorralId, C.Codigo, L.LoteID, L.FechaCierre, L.Cabezas, LP.DiasEngorda, L.FechaInicio, L.FechaDisponibilidad,
	L.DisponibilidadManual
	ORDER BY 1
	--Colocamos el inicio de la semana
	update #Lotes set FechaInicioSemana = DATEADD(dd, -(DATEPART(dw, L.FechaDisponibilidad))+1, L.FechaDisponibilidad)		  
	FROM #Lotes L
	--Colocamos el fin de la semana
	update #Lotes set FechaFinSemana = DATEADD(dd, (7 - DATEPART(dw, L.FechaInicioSemana)), L.FechaInicioSemana)
	From #Lotes L

	select LTR.Codigo, LTR.LoteId, LTR.Cabezas, TP.Descripcion,LTR.FechaCierre, LTR.FechaDisponibilidadProyectada, 
	LTR.FechaDisponibilidad , LTR.DisponibilidadManual,
	LTR.PesoTotalLote, LTR.PesoPromedio, LTR.Sexo, 
	(
		SELECT f.Descripcion
		FROM RepartoDetalle repdet(NOLOCK)
		INNER JOIN Formula f(NOLOCK)
			ON repdet.FormulaIdServida = f.FormulaID
		WHERE repdet.RepartoDetalleId = MAX(RD.repartoDetalleId) 
	) AS FormulaIdServida, 
	--CAST(CAST(DATEPART(year, LTR.FechaDisponibilidad ) as VARCHAR) +  CAST( FORMAT(DATEPART(WW, LTR.FechaDisponibilidad ), '00') as VARCHAR) as INT) as Semana,
	CASE WHEN (DATEDIFF(day, GETDATE() - 7, LTR.FechaFinSemana) / 7) < 0
	THEN 0
	ELSE
	DATEDIFF(day, GETDATE() - 7, LTR.FechaFinSemana) / 7
	END AS Semana, 
	LTR.FechaInicioSemana, 
	LTR.FechaFinSemana
	from #Lotes LTR
	INNER JOIN TipoGanado TP ON LTR.PesoPromedio BETWEEN TP.PesoMinimo AND TP.PesoMaximo AND LTR.Sexo = TP.Sexo
	INNER JOIN reparto R ON R.LoteID = LTR.LoteId
	INNER JOIN RepartoDetalle RD on R.RepartoID = RD.RepartoID AND RD.FormulaIdServida is not null  
	WHERE  1=1
	-- and CAST(FechaDisponibilidad as DATE) >= @FechaValor
	group by LTR.CorralId,LTR.LoteId, LTR.Codigo, LTR.FechaCierre, LTR.Cabezas, LTR.FechaDisponibilidadProyectada, 
	LTR.FechaDisponibilidad, LTR.DisponibilidadManual, LTR.PesoTotalLote, LTR.PesoPromedio, LTR.Sexo, TP.Descripcion, LTR.FechaInicioSemana, LTR.FechaFinSemana
	Order by Semana, FechaDisponibilidad, RIGHT('00000' + LTRIM(RTRIM(LTR.Codigo)), 5) asc

	DROP TABLE #Lotes;

	SET NOCOUNT OFF
END

GO

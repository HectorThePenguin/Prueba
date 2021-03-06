USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_Generar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteEstadoComederos_Generar]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_Generar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roberto Aguilar Pozos
-- Create date: 2014-09-26
-- Origen: APInterfaces
-- Description:	Genera el reporte en estado de comedero para la fecha actual
-- EXEC ReporteEstadoComederos_Generar 1
--=============================================
create Procedure [dbo].[ReporteEstadoComederos_Generar]
	@OrganizacionID int
as
begin
declare @Fecha DATETIME = cast(getdate() as date);
Declare @TablaResultado table(
	Corral varchar(10), ---- Codigo del corral
	Lote varchar(20) , ---LoteID del lote que esta asignado al corral �es realmente el codigo de Lote?
	TipoGanado varchar(50), --Tipo de ganado promedio que corresponde el ganado
	Cabezas int, --numero de cabezas en el lote activo del corral
	DiasEngorda int, ---numero promedio de dias de engorda en ese corral segun la cantidad de animales
	PesoProyectado int,
	DiasUltimaFormula int,
	Promedio5Dias decimal(10,4),
	EstadoComederoHoy int,
	AlimentacionProgramadaMatutinaHoy int,
	FormulaProgramadaMatutinaHoy varchar(50),
	AlimentacionProgramadaVespertinaHoy int,
	FormulaProgramadaVespertinaHoy varchar(50),
	TotalProgramadoHoy int,
	EstadoComederoRealAyer int,
	AlimentacionRealMatutinaAyer int,
	FormulaRealMatutinaAyer varchar(50),
	AlimentacionRealVespertinoAyer int,
	FormulaRealVespertinoAyer varchar(50),
	TotalRealAyer int,
	Kilogramos3Dias int,
	ConsumoCabeza3Dias decimal(10,4),
	EstadoComedero3Dias int,
	Kilogramos4Dias int,
	ConsumoCabeza4Dias decimal(10,4),
	EstadoComedero4Dias int,
	Kilogramos5Dias int,
	ConsumoCabeza5Dias decimal(10,4),
	EstadoComedero5Dias int,
	Kilogramos6Dias int,
	ConsumoCabeza6Dias decimal(10,4),
	EstadoComedero6Dias int,
	Kilogramos7Dias int,
	ConsumoCabeza7Dias decimal(10,4),
	EstadoComedero7Dias int
)
INSERT INTO @TablaResultado 
	SELECT C.Codigo,
		ISNULL(L.lote,''), 
		ISNULL(dbo.obtenerTipoGanadoLote(L.loteID),''),
		ISNULL(L.Cabezas,0), 
		ISNULL(dbo.ObtenerDiasEngordaLote(L.LoteID),0),
		ISNULL(dbo.obtenerPesoProyectadoLote(L.LoteID),0),
		ISNULL(dbo.obtenerDiasUltimaFormulaCorral(C.CorralID),0), 
		CASE WHEN sel1.Promedio5Dias > 0 and L.Cabezas > 0 
			THEN sel1.Promedio5Dias/L.Cabezas 
			ELSE 0 
			END,
		ISNULL(hoy.EstadoComederoID,-1),
		ISNULL(hoy.CantidadServidaMatutino ,0),
		ISNULL(hoy.FormulaServidaMatutino,''),
		ISNULL(hoy.CantidadServidaVespertino,0),
		ISNULL(hoy.FormulaServidaVespertino,''),
		ISNULL(hoy.TotalServido,0),
		ISNULL(Ayer.EstadoComederoID,-1),
		ISNULL(Ayer.CantidadServidaMatutino,0),
		ISNULL(Ayer.FormulaServidaMatutino,''),
		ISNULL(Ayer.CantidadServidaVespertino,0),
		ISNULL(Ayer.FormulaServidaVespertino,''),
		ISNULL(Ayer.TotalServido,0),
		ISNULL(Dia3.TotalServido,0),
		CASE WHEN Dia3.TotalServido > 0 AND L.Cabezas > 0
			THEN ISNULL(CAST(Dia3.TotalServido  as decimal(10,4))/CAST(L.Cabezas as decimal(10,4)),0)
			ELSE 0
			END,
		ISNULL(Dia3.EstadoComederoID,-1),
		ISNULL(Dia4.TotalServido,0),
		CASE WHEN Dia4.TotalServido > 0 AND L.Cabezas > 0
			THEN ISNULL(CAST(Dia4.TotalServido as decimal(10,4))/CAST(L.Cabezas as decimal(10,4)),0)
			ELSE 0
			END,
		ISNULL(Dia4.EstadoComederoID,-1),
		ISNULL(Dia5.TotalServido,0),
		CASE WHEN Dia5.TotalServido > 0 AND L.Cabezas > 0 THEN
			ISNULL(CAST(Dia5.TotalServido as decimal(10,4))/CAST(L.Cabezas as decimal(10,4)),0)
			ELSE 0
			END,
		ISNULL(Dia5.EstadoComederoID,-1),
		ISNULL(Dia6.TotalServido,0),
		CASE WHEN Dia6.TotalServido > 0 AND L.Cabezas > 0 THEN
			ISNULL(CAST(Dia6.TotalServido as decimal(10,4))/CAST(L.Cabezas as decimal(10,4)),0)
			ELSE 0
			END,
		ISNULL(Dia6.EstadoComederoID,-1),
		ISNULL(Dia7.TotalServido,0),
		CASE WHEN Dia7.TotalServido > 0 AND L.Cabezas > 0 THEN 
			ISNULL(CAST(Dia7.TotalServido as decimal(10,4))/CAST(L.Cabezas as decimal(10,4)),0)
			ELSE 0
			END,
		ISNULL(Dia7.EstadoComederoID,-1)
	from Corral(nolock) C
		inner join Reparto(nolock) Re on Re.CorralID = C.CorralID 
		left join Lote(nolock) L on L.CorralID = C.CorralID and L.Activo = 1
		left join (select Re1.LoteID, case  
										when sum(RD1.CantidadServida) > 0 
										then CAST(sum(RD1.CantidadServida) as decimal(10,4))/CAST(count(distinct Re1.RepartoID) as decimal(10,4)) 
										else 0 
										END as Promedio5Dias
					from RepartoDetalle(nolock) RD1
					inner join Reparto(nolock) Re1 on Re1.RepartoID = RD1.RepartoID
					where Re1.Fecha between dateadd(D,-4,@Fecha) and @Fecha
					group by Re1.LoteID) sel1 on Sel1.LoteID = Re.loteID
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,@Fecha) hoy on hoy.CorralID = C.CorralID 
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,dateadd(D,-1,@Fecha)) Ayer on Ayer.CorralID = C.CorralID 
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,dateadd(D,-2,@Fecha)) Dia3 on Dia3.CorralID = C.CorralID 
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,dateadd(D,-3,@Fecha)) Dia4 on Dia4.CorralID = C.CorralID 
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,dateadd(D,-4,@Fecha)) Dia5 on Dia5.CorralID = C.CorralID 
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,dateadd(D,-5,@Fecha)) Dia6 on Dia6.CorralID = C.CorralID 
		left join dbo.obtenerEstadoReparto(@OrganizacionID ,dateadd(D,-6,@Fecha)) Dia7 on Dia7.CorralID = C.CorralID 
		where C.OrganizacionID = @OrganizacionID and Re.Fecha = @Fecha
--Enviamos el detalle
Select * from @TablaResultado 
--Enviamos la fecha de generacion del sistema
select @Fecha as FechaReporte
end

GO

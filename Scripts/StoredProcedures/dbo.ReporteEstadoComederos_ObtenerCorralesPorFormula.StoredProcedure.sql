USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_ObtenerCorralesPorFormula]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteEstadoComederos_ObtenerCorralesPorFormula]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_ObtenerCorralesPorFormula]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: 25/04/2013
-- Description: Consulta para obtener corrales por formula
-- Empresa: SuKarne
-- Uso: ReporteEstadoComederos_ObtenerCorralesPorFormula 1
-- =============================================
CREATE procedure [dbo].[ReporteEstadoComederos_ObtenerCorralesPorFormula]
	@OrganizacionID int
as
begin
	declare @hoy bigint;
	declare @TipoServicio_AM int;
	declare @TipoServicio_PM int;
	declare @Fecha DATETIME = cast(getdate() as date);
	set @hoy = cast(convert(varchar(10), @Fecha, 112) as bigint)
	set @TipoServicio_AM = 1
	set @TipoServicio_PM = 2
	declare @detallado table(
		loteID int
		, formulaID int
	);
	insert into @detallado
		select
			r.loteid
			, rdam.FormulaIDProgramada
		from
			reparto r 
			inner join repartodetalle rdam on r.repartoid = rdam.repartoid and rdam.tipoServicioId = @TipoServicio_AM
			inner join repartodetalle rdpm on r.repartoid = rdpm.repartoid and rdpm.tipoServicioId = @TipoServicio_PM
		where
			cast(convert(varchar(10), r.fecha, 112) as bigint) = @hoy 
			and r.OrganizacionID = @OrganizacionID
			and rdam.FormulaIDProgramada = rdpm.FormulaIDProgramada
		GROUP BY
			r.loteid
			, rdam.FormulaIDProgramada
	select 
		f.formulaID as FormulaID
		, f.Descripcion as FormulaDescripcion
		, count(d.loteID) as TotalCorrales
	from
		@detallado d 
		right join formula f on d.formulaID = f.formulaID
	where
		 d.loteID > 0
	group by
		f.formulaID
		, f.Descripcion
	order by f.FormulaID asc
end

GO

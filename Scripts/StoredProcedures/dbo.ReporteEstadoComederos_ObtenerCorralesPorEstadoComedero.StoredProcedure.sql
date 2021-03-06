USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_ObtenerCorralesPorEstadoComedero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteEstadoComederos_ObtenerCorralesPorEstadoComedero]
GO
/****** Object:  StoredProcedure [dbo].[ReporteEstadoComederos_ObtenerCorralesPorEstadoComedero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: 25/04/2014
-- Description: Consulta para obtener corrales por estado de comedero
-- Empresa: SuKarne
-- Uso: [ReporteEstadoComederos_ObtenerCorralesPorEstadoComedero] 1
-- =============================================
create procedure [dbo].[ReporteEstadoComederos_ObtenerCorralesPorEstadoComedero]
	@OrganizacionID int
as
begin
	declare @hoy bigint;
	declare	@TipoServicio_AM tinyint;
	declare @Fecha DATETIME = cast(getdate() as date);
	set @hoy = cast(convert(varchar(10), @Fecha, 112) as bigint)
	set @TipoServicio_AM = 1
	declare @detallado table(
		loteID int
		, estadoComederoID int
	);
	insert into @detallado
		select
			r.loteid
			, rd.estadoComederoID
		from
			reparto r 
			inner join repartodetalle rd on r.repartoid = rd.repartoid
		where
			cast(convert(varchar(10), r.fecha, 112) as bigint) = @hoy
			and r.OrganizacionID = @OrganizacionID
			and rd.TipoServicioId = @TipoServicio_AM
		GROUP BY
			r.loteid
			, rd.estadoComederoID		
	select 
		ec.estadoComederoID as EstadoComederoID
		, ec.DescripcionCorta as EstadoComederoDescripcion
		, count(d.loteID) as TotalCorrales
	from
		@detallado d 
		right join EstadoComedero ec on d.estadoComederoID = ec.estadoComederoID
    where
		d.loteID > 0
	group by
		ec.estadoComederoID
		, ec.DescripcionCorta
	order by
		ec.EstadoComederoID asc
end

GO

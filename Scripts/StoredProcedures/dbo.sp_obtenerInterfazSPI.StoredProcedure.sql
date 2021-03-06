USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_obtenerInterfazSPI]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_obtenerInterfazSPI]
GO
/****** Object:  StoredProcedure [dbo].[sp_obtenerInterfazSPI]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_obtenerInterfazSPI]
	@org int			
	, @fec smalldatetime
as
begin
	create table #spi
	(
		OrganizacionId				int
		, Corral					varchar(10)
		, Lote						varchar(20)
		, Arete						varchar(max)
		, FechaSacrificio			smalldatetime
		, Procesado					bit
		, OrganizacionIdSacrificio	int
		, CorralSacrificio			varchar(10)
	)

	if @org = 1
	begin
		insert into #spi (OrganizacionId, Corral, Lote, Arete, FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
		select
			@org
			, ss.num_corr
			, ss.num_pro
			, cast(cast(ls.no_lote as bigint) as varchar) 
			, fec_sacr
			, 0
			, @org
			, ss.num_corr
		from
			[srvppisocln].basculas_sl.dbo.Salida_Sacrificio ss
			inner join [srvppisocln].basculas_sl.dbo.Layout_Subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
		where
			fec_sacr = @fec
			and origen_ganado = 'P'

		goto fin
	end

	if @org = 2
	begin
		insert into #spi (OrganizacionId, Corral, Lote, Arete, FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
		select
			@org
			, ss.num_corr
			, ss.num_pro
			, cast(cast(ls.no_lote as bigint) as varchar) 
			, fec_sacr
			, 0
			, @org
			, ss.num_corr
		from
			[srvppisomxli].basculas_sl.dbo.Salida_Sacrificio ss
			inner join [srvppisomxli].basculas_sl.dbo.Layout_Subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
		where
			fec_sacr = @fec
			and origen_ganado = 'P'

		goto fin
	end

	if @org = 3
	begin
		insert into #spi (OrganizacionId, Corral, Lote, Arete, FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
		select
			@org
			, ss.num_corr
			, ss.num_pro
			, cast(cast(ls.no_lote as bigint) as varchar) 
			, fec_sacr
			, 0
			, @org
			, ss.num_corr
		from
			[srvppisomty].basculas_sl.dbo.Salida_Sacrificio ss
			inner join [srvppisomty].basculas_sl.dbo.Layout_Subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
		where
			fec_sacr = @fec
			and origen_ganado = 'P'

		goto fin
	end

	if @org = 4
	begin
		insert into #spi (OrganizacionId, Corral, Lote, Arete, FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
		select
			@org
			, ss.num_corr
			, ss.num_pro
			, cast(cast(ls.no_lote as bigint) as varchar) 
			, fec_sacr
			, 0
			, @org
			, ss.num_corr
		from
			[srvppisomon].basculas_sl.dbo.Salida_Sacrificio ss
			inner join [srvppisomon].basculas_sl.dbo.Layout_Subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
		where
			fec_sacr = @fec
			and origen_ganado = 'P'

		goto fin
	end

	fin:

	delete InterfazSPI where FechaSacrificio = @fec and OrganizacionId = @org

	insert into InterfazSPI (OrganizacionId, Corral, Lote, Arete, FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
	select OrganizacionId, Corral, Lote, ltrim(rtrim(Arete)), FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio from #spi

	drop table #spi
end

GO

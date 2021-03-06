USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_CierreSacrificioLucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_CierreSacrificioLucero]
GO
/****** Object:  StoredProcedure [dbo].[sp_CierreSacrificioLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_CierreSacrificioLucero]
	@fecha					smalldatetime
	, @organizacion			int
	, @descargarInventario	int = 0
	, @descargarConsumo		int = 0
	, @eliminar				int = 0
	, @activar				int = 0
	, @scp					int = 0
as
begin
	select
		lsl.LoteSacrificioID
		, lsl.Fecha
		, lsl.LoteID
		, lsl.InterfaceSalidaTraspasoDetalleID
		, lsl.ImporteCanal
		, lsl.ImportePiel
		, lsl.ImporteVisera
		, lsl.PesoCanal
		, lsl.PesoPiel
		, lsl.PesoVisceras
		, lsl.Corral
		, lsl.Lote
		, c.Codigo as [Corral Salida]
		, l.Lote as [Lote Salida]
		, ist.OrganizacionIDDestino as OrganizacionID
		, ist.FolioTraspaso
		, istd.Cabezas
	into
		#cabecero
	from
		LoteSacrificioLucero lsl (nolock)
		left outer join InterfaceSalidaTraspasoDetalle istd (nolock) on
			lsl.InterfaceSalidaTraspasoDetalleID = istd.InterfaceSalidaTraspasoDetalleID
		left outer join InterfaceSalidaTraspaso ist (nolock) on
			istd.InterfaceSalidaTraspasoID = ist.InterfaceSalidaTraspasoID
		left outer join Lote l (nolock) on
			istd.LoteID = l.LoteID
		left outer join Corral c (nolock) on
			l.CorralID = c.CorralID
	where	
		lsl.Fecha = @fecha
		and ist.OrganizacionIDDestino = @organizacion

	select
		lsl.LoteSacrificioID
		, lsl.InterfaceSalidaTraspasoDetalleID
		, lsld.AnimalID
	into
		#detalle
	from
		#cabecero lsl (nolock)
		left outer Join LoteSacrificioLuceroDetalle lsld (nolock) on
			lsl.LoteSacrificioID = lsld.LoteSacrificioID

	if @scp = 1
	begin
		exec sp_ObtenerLoteSacrificioLucero @fecha, @organizacion
	end

	if @descargarInventario = 1
	begin
		exec Rendimiento_CalcularRendimientos @fecha, 5, 0
	end

	if @descargarConsumo = 1
	begin
		exec Rendimiento_CalcularRendimientos @fecha, 5, 1
	end

	if @eliminar = 1
	begin
		exec Rendimiento_EliminarTransaccionales_Lucero @organizacion, @fecha
	end

	if @activar = 1
	begin
		update a set activo = 1 from AnimalHistorico a inner join #detalle d on a.AnimalID = d.AnimalID
	end

	select 
		am.AnimalMovimientoID
		, am.AnimalID
		, am.LoteID
		, am.TipoMovimientoID
		, am.Activo
		, am.FechaMovimiento
		, am.Observaciones
		, d.InterfaceSalidaTraspasoDetalleID
		, d.LoteSacrificioID
	into
		#movimientos
	from
		AnimalMovimiento am (nolock)
		inner join #detalle d on
			am.AnimalID = d.AnimalID

	select 
		am.AnimalMovimientoID
		, am.AnimalID
		, am.LoteID
		, am.TipoMovimientoID
		, am.Activo
		, am.FechaMovimiento
		, am.Observaciones
		, d.InterfaceSalidaTraspasoDetalleID
		, d.LoteSacrificioID
	into 
		#historico
	from
		AnimalMovimientoHistorico am (nolock)
		inner join #detalle d on
			am.AnimalID = d.AnimalID

	select
		am.AnimalMovimientoID
		, am.AnimalID
		, am.LoteID
		, am.TipoMovimientoID
		, am.Activo
		, am.FechaMovimiento
		, am.Observaciones
		, c.InterfaceSalidaTraspasoDetalleID
		, c.LoteSacrificioID
	into
		#movimientosSacrificio
	from
		AnimalMovimiento am (nolock)
		inner join #cabecero c on
			am.LoteID = c.LoteID
	where
		am.FechaMovimiento = @fecha
		and am.TipoMovimientoID = 16
		and am.Activo = 1

	select
		am.AnimalMovimientoID
		, am.AnimalID
		, am.LoteID
		, am.TipoMovimientoID
		, am.Activo
		, am.FechaMovimiento
		, am.Observaciones
		, c.InterfaceSalidaTraspasoDetalleID
		, c.LoteSacrificioID
	into
		#historicoSacrificio
	from
		AnimalMovimientoHistorico am (nolock)
		inner join #cabecero c on
			am.LoteID = c.LoteID
		inner join #detalle d on 
			am.AnimalID = d.AnimalID
			and c.LoteSacrificioID = d.LoteSacrificioID
	where
		am.FechaMovimiento = @fecha
		and am.TipoMovimientoID = 16
		and am.Activo = 1

	select
		a.AnimalID
		, a.Arete
		, c.LoteID
		, d.InterfaceSalidaTraspasoDetalleID
		, d.LoteSacrificioID
	into
		#animalesInactivos
	from
		Animal a (nolock)
		inner join #detalle d on
			a.AnimalID = d.AnimalID
		inner join #cabecero c on
			d.LoteSacrificioID = c.LoteSacrificioID
	where
		a.Activo = 0

	select
		a.AnimalID
		, a.Arete
		, c.LoteID
		, d.InterfaceSalidaTraspasoDetalleID
		, d.LoteSacrificioID
	into
		#animalesHistoricoInactivos
	from
		AnimalHistorico a (nolock)
		inner join #detalle d on
			a.AnimalID = d.AnimalID
		inner join #cabecero c on
			d.LoteSacrificioID = c.LoteSacrificioID
	where
		a.Activo = 0
		-- select  1317 - ((75 + 25 + 418 + 75) + 450 + 270)
	select 
		c.* 
		, d.Cabezas as Detalle
		, m.Cabezas as Movimientos
		, ma.Cabezas as [Movimientos Sacrificados]
		, ms.Cabezas as [Movimientos Sacrificados Todos]
		, ai.Cabezas as [Animales Inactivos]
		, h.Cabezas as Historico
		, hs.Cabezas as [Historico Sacrificados]
		, ha.Cabezas as [Historico Sacrificados Todos]
		, ahi.Cabezas as [Animales Historico Inactivos]
	into
		#resultado
	from 
		#cabecero c
		left outer join (select LoteSacrificioID, count(1) as Cabezas from #detalle group by LoteSacrificioID) d on
			c.LoteSacrificioID = d.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(distinct AnimalID) as Cabezas from #movimientos group by LoteSacrificioID) m on
			c.LoteSacrificioID = m.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(1) as Cabezas from #movimientos where Activo = 1 and TipoMovimientoID = 16 group by LoteSacrificioID) ma on
			c.LoteSacrificioID = ma.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(distinct AnimalID) as Cabezas from #movimientosSacrificio group by LoteSacrificioID) ms on
			c.LoteSacrificioID = ms.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(distinct AnimalID) as Cabezas from #animalesInactivos group by LoteSacrificioID) ai on
			c.LoteSacrificioID = ai.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(distinct AnimalID) as Cabezas from #historico group by LoteSacrificioID) h on
			c.LoteSacrificioID = h.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(1) as Cabezas from #historico where Activo = 1 and TipoMovimientoID = 16 group by LoteSacrificioID) ha on
			c.LoteSacrificioID = ha.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(distinct AnimalID) as Cabezas from #historicoSacrificio group by LoteSacrificioID) hs on
			c.LoteSacrificioID = hs.LoteSacrificioID
		left outer join (select LoteSacrificioID, COUNT(distinct AnimalID) as Cabezas from #animalesHistoricoInactivos group by LoteSacrificioID) ahi on
			c.LoteSacrificioID = ahi.LoteSacrificioID
	Order by
		c.LoteSacrificioID

	insert into #resultado (LoteSacrificioID, LoteID, Corral, Lote, Fecha, ImporteCanal, ImportePiel, ImporteVisera, PesoCanal, PesoPiel, PesoVisceras, Cabezas, Detalle, Movimientos, [Movimientos Sacrificados],[Movimientos Sacrificados Todos], Historico, [Historico Sacrificados], [Historico Sacrificados Todos])
	select 0, 0, 'ZZZ', 'ZZZZ', Fecha, SUM(ImporteCanal), SUM(ImportePiel), SUM(ImporteVisera), SUM(PesoCanal), SUM(PesoPiel), SUM(PesoVisceras), SUM(Cabezas), SUM(Detalle), SUM(Movimientos), SUM([Movimientos Sacrificados]), SUM([Movimientos Sacrificados Todos]), SUM(Historico), SUM([Historico Sacrificados]), SUM([Historico Sacrificados Todos]) from #resultado group by Fecha

	select
		cast(PesoCanal / Detalle as int) PesoCanal
		, cast(PesoPiel / Detalle as int) PesoPiel
		, *
	from 
		#resultado
	order by
		Corral

	drop table #cabecero, #detalle, #movimientos, #historico, #movimientosSacrificio, #historicoSacrificio, #animalesInactivos, #animalesHistoricoInactivos, #resultado
end
GO

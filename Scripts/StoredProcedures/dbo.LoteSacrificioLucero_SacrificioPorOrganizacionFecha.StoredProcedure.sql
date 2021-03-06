USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_SacrificioPorOrganizacionFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_SacrificioPorOrganizacionFecha]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_SacrificioPorOrganizacionFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_SacrificioPorOrganizacionFecha 3, '2015-10-01'
create procedure [dbo].[LoteSacrificioLucero_SacrificioPorOrganizacionFecha]
	@organizacion int
	, @fechaSacrificio smalldatetime
as

	select 
		lsl.LoteSacrificioID
		, lsl.LoteID
		, lsl.Corral
		, lsl.Lote
		, lsl.ImporteCanal
		, lsl.ImportePiel
		, lsl.ImporteVisera
		, lsl.InterfaceSalidaTraspasoDetalleID
		, lsl.PesoCanal
		, lsl.PesoPiel
		, lsl.PesoVisceras
		, lsld.AnimalID
		, t.Total
	from 
		LoteSacrificioLucero lsl
		inner join LoteSacrificioLuceroDetalle lsld on
			lsl.LoteSacrificioID = lsld.LoteSacrificioID
		inner join InterfaceSalidaTraspasoDetalle istd on
			lsl.InterfaceSalidaTraspasoDetalleID = istd.InterfaceSalidaTraspasoDetalleID
			and lsl.LoteID = istd.LoteID
		inner join InterfaceSalidaTraspaso ist on 
			istd.InterfaceSalidaTraspasoID = ist.InterfaceSalidaTraspasoID
		inner join (
			select 
				lsl.LoteID
				, COUNT(1) Total
			from 
				LoteSacrificioLucero lsl
				inner join LoteSacrificioLuceroDetalle lsld on
					lsl.LoteSacrificioID = lsld.LoteSacrificioID
				inner join InterfaceSalidaTraspasoDetalle istd on
					lsl.InterfaceSalidaTraspasoDetalleID = istd.InterfaceSalidaTraspasoDetalleID
					and lsl.LoteID = istd.LoteID
				inner join InterfaceSalidaTraspaso ist on 
					istd.InterfaceSalidaTraspasoID = ist.InterfaceSalidaTraspasoID
			where
				lsl.Fecha = @fechaSacrificio
				and ist.OrganizacionIDDestino = @organizacion
			group by 
				lsl.LoteID
		) t on
			lsl.LoteID = t.LoteID
	where
		lsl.Fecha = @fechaSacrificio
		and ist.OrganizacionIDDestino = @organizacion

GO

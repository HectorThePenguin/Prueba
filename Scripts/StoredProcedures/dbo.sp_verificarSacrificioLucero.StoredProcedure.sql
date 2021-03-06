USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_verificarSacrificioLucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_verificarSacrificioLucero]
GO
/****** Object:  StoredProcedure [dbo].[sp_verificarSacrificioLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_verificarSacrificioLucero]
	@foliosSacrificio varchar(8000)
as
begin
	select 		
		LoteID	
		, am.AnimalID	
		, InterfaceSalidaTraspasoDetalleID	
		, lsd.LoteSacrificioID	
	into		
		#inventario	
	from		
		(select loteid, animalid from AnimalMovimiento where activo = 1 and TipoMovimientoID != 16 and OrganizacionID = 5 group by loteid, animalid) am	
		inner join (select InterfaceSalidaTraspasoDetalleID, AnimalID from InterfaceSalidaTraspasoCosto where facturado = 0 group by InterfaceSalidaTraspasoDetalleID, AnimalID) itc on	
			am.AnimalID = itc.AnimalID
		inner join LoteSacrificioLuceroDetalle lsd on	
			am.AnimalID = lsd.AnimalID
		
	Select		
		a.OrganizacionIDDestino	
		, a.FolioTraspaso	
		, b.LoteID	
		, b.InterfaceSalidaTraspasoDetalleID	
		, c.LoteSacrificioID	
		, isnull(c.Cabezas, 0) as Cabezas	
	from		
		InterfaceSalidaTraspaso a 	
		inner join InterfaceSalidaTraspasoDetalle b on	
			a.InterfaceSalidaTraspasoID = b.InterfaceSalidaTraspasoID
		left outer join (select InterfaceSalidaTraspasoDetalleID, LoteID, LoteSacrificioID, count(1) Cabezas from #inventario group by InterfaceSalidaTraspasoDetalleID, LoteID, LoteSacrificioID) c on	
			b.LoteID = c.LoteID
			and b.InterfaceSalidaTraspasoDetalleID = c.InterfaceSalidaTraspasoDetalleID
	where		
		a.FolioTraspaso in (SELECT Registros FROM [dbo].[FuncionSplit](@foliosSacrificio, ','))		
	order by		
		a.OrganizacionIDDestino	
		, a.FolioTraspaso	
		
	drop table #inventario		

end

GO

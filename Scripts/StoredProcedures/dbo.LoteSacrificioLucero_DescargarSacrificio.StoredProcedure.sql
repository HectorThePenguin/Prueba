USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_DescargarSacrificio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_DescargarSacrificio]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_DescargarSacrificio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_DescargarSacrificio 3, '2015-10-09'
create procedure [dbo].[LoteSacrificioLucero_DescargarSacrificio]
	@organizacion int
	, @fechaSacrificio smalldatetime
as
begin
	create table #Sacrificio (
		LoteSacrificioID						int					--public int LoteSacrificioID { get; set; }
		, LoteID								int					--public int LoteID { get; set; }
		, Corral								varchar(10)			--public string Corral { get; set; }
		, Lote									varchar(20)			--public string Lote { get; set; }
		, ImporteCanal							decimal(18,2)		--public decimal ImporteCanal { get; set; }
		, ImportePiel							decimal(18,2)		--public decimal ImportePiel { get; set; }
		, ImporteVisera							decimal(18,2)		--public decimal ImporteVisera { get; set; }
		, InterfaceSalidaTraspasoDetalleID		int					--public int InterfaceSalidaTraspasoDetalleID { get; set; }
		, PesoCanal								decimal(18,2)		--public decimal PesoCanal { get; set; }
		, PesoPiel								decimal(18,2)		--public decimal PesoPiel { get; set; }
		, PesoVisceras							decimal(18,2)		--public decimal PesoVisceras { get; set; }
		, AnimalID								bigint				--public long AnimalID { get; set; }
		, Total									int					--public int Total { get; set; }
		, Procesado								bit default 0
	)

	insert into #Sacrificio (
		LoteSacrificioID					
		, LoteID							
		, Corral							
		, Lote								
		, ImporteCanal						
		, ImportePiel						
		, ImporteVisera						
		, InterfaceSalidaTraspasoDetalleID	
		, PesoCanal							
		, PesoPiel							
		, PesoVisceras						
		, AnimalID							
		, Total								
	)
	exec LoteSacrificioLucero_SacrificioPorOrganizacionFecha @organizacion, @fechaSacrificio

	DBCC TRACEON (1224)

	DELETE a FROM AnimalCosto a inner join #Sacrificio s on a.AnimalID = s.AnimalID
	DELETE a FROM AnimalConsumo a inner join #Sacrificio s on a.AnimalID = s.AnimalID
	DELETE a FROM AnimalMovimiento a inner join #Sacrificio s on a.AnimalID = s.AnimalID
	DELETE a FROM Animal a inner join #Sacrificio s on a.AnimalID = s.AnimalID

	DBCC TRACEOFF (1224)


end


GO

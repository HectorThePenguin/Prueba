USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverMovimientosPorAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_MoverMovimientosPorAnimal]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverMovimientosPorAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_MoverMovimientosPorAnimal 1013758
create procedure [dbo].[LoteSacrificioLucero_MoverMovimientosPorAnimal]
	@animalID bigint
as
begin
	declare @loteid int
	declare @id bigint
	declare @ids table (
		id bigint
	)

	select 
		@loteid = LoteID
	from
		LoteSacrificioLucero ls
		inner join LoteSacrificioLuceroDetalle lsd on
			ls.LoteSacrificioID = lsd.LoteSacrificioID 
	where
		AnimalID = @animalid

	select
		@id = max(AnimalMovimientoID)
	from
		AnimalMovimiento
	where
		LoteID = @loteid 
		and AnimalID = @animalID
		and Activo = 1
	
	insert into @ids
	select @id

	while @id is not null
	begin
		select 
			@id = max(AnimalMovimientoIDAnterior)
		from
			AnimalMovimiento
		where
			AnimalMovimientoID = @id

		if @id is not null and not exists (select 1 from AnimalMovimientoHistorico where AnimalMovimientoID = @id)
		begin
			insert into @ids
			select @id
		end
	end

	insert into AnimalMovimientoHistorico (
		AnimalID
		, AnimalMovimientoID
		, OrganizacionID
		, CorralID
		, LoteID
		, FechaMovimiento
		, Peso
		, Temperatura
		, TipoMovimientoID
		, TrampaID
		, OperadorID
		, Observaciones
		, LoteIDOrigen
		, AnimalMovimientoIDAnterior
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
		, FechaModificacion
		, UsuarioModificacionID
	)
	select 
		AnimalID
		, AnimalMovimientoID
		, OrganizacionID
		, CorralID
		, LoteID
		, FechaMovimiento
		, Peso
		, Temperatura
		, TipoMovimientoID
		, TrampaID
		, OperadorID
		, Observaciones
		, LoteIDOrigen
		, AnimalMovimientoIDAnterior
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
		, FechaModificacion
		, UsuarioModificacionID
	from 
		AnimalMovimiento
	where
		AnimalMovimientoID in (select id from @ids)
	group by
		AnimalID
		, AnimalMovimientoID
		, OrganizacionID
		, CorralID
		, LoteID
		, FechaMovimiento
		, Peso
		, Temperatura
		, TipoMovimientoID
		, TrampaID
		, OperadorID
		, Observaciones
		, LoteIDOrigen
		, AnimalMovimientoIDAnterior
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
		, FechaModificacion
		, UsuarioModificacionID

end
GO

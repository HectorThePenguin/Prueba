if object_id('dbo.TransferenciaGanadoIndividual_GuardarPorLoteID', 'P') is not null
begin
	drop procedure dbo.TransferenciaGanadoIndividual_GuardarPorLoteID
end
go
create procedure dbo.TransferenciaGanadoIndividual_GuardarPorLoteID
	@AnimalID bigint
	, @LoteID int
as
begin

	select 1 from AnimalMovimiento where AnimalID = @AnimalID and TipoMovimientoID in (8, 11, 16) and Activo = 1

	if @@ROWCOUNT > 0
		THROW 50000, 'El arete no se encuentra disponible para ser transferido.', 1;

	select 1 from Lote where LoteID = @LoteID and Activo = 0

	if @@ROWCOUNT > 0
		THROW 50000, 'El lote destino no está activo.', 1;

	select 
		1
	from 
		AnimalMovimiento 
	where 
		AnimalID = @AnimalID 
		and Activo = 1

	if @@ROWCOUNT > 1
		THROW 50000, 'Existen movimientos activos duplicados.', 1;

	declare @id bigint
	declare @loteIDOrigen int
	declare @corralID int

	select 
		@id = AnimalMovimientoID
		, @loteIDOrigen = LoteID
	from 
		AnimalMovimiento 
	where 
		AnimalID = @AnimalID 
		and Activo = 1

	if @id is null
		THROW 50000, 'No existe el movimiento de origen.', 1;

	select 
		@corralID = CorralID
	from
		Lote
	where
		LoteID = @LoteID

	insert into AnimalMovimiento (
		AnimalID
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
		, OrganizacionID
		, @corralID
		, @LoteID
		, GETDATE()
		, Peso
		, Temperatura
		, 17
		, TrampaID
		, OperadorID
		, Observaciones
		, LoteID
		, AnimalMovimientoID
		, 1
		, GETDATE()
		, 1
		, NULL
		, NULL
	from
		AnimalMovimiento		
	where 
		AnimalMovimientoID = @id
		and Activo = 1

	update 
		AnimalMovimiento
	set
		Activo = 0
	where
		AnimalMovimientoID = @id

	update
		Lote
	set
		Cabezas = Cabezas + 1
	where
		LoteID = @LoteID

	update
		Lote
	set
		Cabezas = Cabezas - 1
	where
		LoteID = @loteIDOrigen

	update
		Lote
	set
		Activo = 0
	where
		LoteID = @loteIDOrigen
		and Cabezas <= 0

end



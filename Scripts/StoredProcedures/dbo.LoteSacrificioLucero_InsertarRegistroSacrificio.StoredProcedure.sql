USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_InsertarRegistroSacrificio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_InsertarRegistroSacrificio]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_InsertarRegistroSacrificio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_InsertarRegistroSacrificio 462032, 3, 0, 0, '2015-10-01', 0, '', 0, 0
create procedure [dbo].[LoteSacrificioLucero_InsertarRegistroSacrificio]
	@AnimalID							bigint
	, @OrganizacionID					int
	, @CorralID							int
	, @LoteID							int
	, @FechaMovimiento					smalldatetime
	, @Peso								int
	, @Observaciones					varchar(255)
	, @LoteIDOrigen						int
	, @AnimalMovimientoIDAnterior		bigint
as
begin	
	
	if not exists (select 1 from AnimalMovimiento where AnimalID = @animalID and Activo = 1 and TipoMovimientoID = 16)
	begin
		INSERT INTO AnimalMovimiento (
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
			, Activo
			, FechaCreacion
			, UsuarioCreacionID
			, LoteIDOrigen
			, AnimalMovimientoIDAnterior
		)
		SELECT	
			@AnimalID					
			, @OrganizacionID			
			, @CorralID					
			, @LoteID					
			, @FechaMovimiento			
			, @Peso						
			, 0
			, 16
			, 1
			, 1
			, @Observaciones			
			, 1
			, GETDATE()
			, 1
			, @LoteIDOrigen				
			, @AnimalMovimientoIDAnterior

		update AnimalMovimiento set Activo = 0 where AnimalMovimientoID = @AnimalMovimientoIDAnterior
	end

	select @@IDENTITY
end
GO

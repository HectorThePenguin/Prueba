USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_EliminarPorAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_EliminarPorAnimal]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_EliminarPorAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_EliminarPorAnimal 1013758
create procedure [dbo].[LoteSacrificioLucero_EliminarPorAnimal]
	@animalID bigint
as
begin
	
	delete
		ac
	from
		AnimalCosto ac
		inner join AnimalCostoHistorico ach on
			ac.AnimalCostoID = ach.AnimalCostoID 
	where
		ac.AnimalID = @animalID

	delete
		ac
	from
		AnimalConsumo ac
		inner join AnimalConsumoHistorico ach on
			ac.AnimalConsumoID = ach.AnimalConsumoID 
	where
		ac.AnimalID = @animalID

	delete
		ac
	from
		AnimalMovimiento ac
		inner join AnimalMovimientoHistorico ach on
			ac.AnimalMovimientoID = ach.AnimalMovimientoID
	where
		ac.AnimalID = @animalID

	delete
		a
	from
		Animal a
		inner join AnimalHistorico ah on
			a.AnimalID = ah.AnimalID
		left outer join (select AnimalID from AnimalCosto group by AnimalID) ac on
			a.AnimalID = ac.AnimalID
		left outer join (select AnimalID from AnimalConsumo group by AnimalID) an on
			a.AnimalID = an.AnimalID
		left outer join (select AnimalID from AnimalMovimiento group by AnimalID) am on
			a.AnimalID = am.AnimalID
	where
		a.AnimalID = @animalID
		and ac.AnimalID is null
		and an.AnimalID is null
		and am.AnimalID is null

end
GO

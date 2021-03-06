USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverConsumosPorAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_MoverConsumosPorAnimal]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverConsumosPorAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_MoverConsumosPorAnimal 1013758
create procedure [dbo].[LoteSacrificioLucero_MoverConsumosPorAnimal]
	@animalID bigint
as
	insert into AnimalConsumoHistorico (
		AnimalConsumoID
		, AnimalID
		, RepartoID
		, FormulaIDServida
		, Cantidad
		, TipoServicioID
		, Fecha
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
		, FechaModificacion
		, UsuarioModificacionID
	)
	select 
		ac.AnimalConsumoID
		, ac.AnimalID
		, ac.RepartoID
		, ac.FormulaIDServida
		, ac.Cantidad
		, ac.TipoServicioID
		, ac.Fecha
		, ac.Activo
		, ac.FechaCreacion
		, ac.UsuarioCreacionID
		, ac.FechaModificacion
		, ac.UsuarioModificacionID
	from 
		AnimalConsumo ac
		left outer join AnimalConsumoHistorico ach on
			ac.AnimalConsumoID = ach.AnimalConsumoID
	where
		ac.AnimalID = @animalID
		and ach.AnimalConsumoID is null





GO

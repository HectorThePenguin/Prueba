USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverCostosPorAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_MoverCostosPorAnimal]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverCostosPorAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_ObtenerCostosPorAnimal 1013758
create procedure [dbo].[LoteSacrificioLucero_MoverCostosPorAnimal]
	@animalID bigint
as
begin
	insert into AnimalCostoHistorico (
		AnimalCostoID
		, AnimalID
		, FechaCosto
		, CostoID
		, TipoReferencia
		, FolioReferencia
		, Importe
		, FechaCreacion
		, UsuarioCreacionID
		, FechaModificacion
		, UsuarioModificacionID 
	)
	select 
		ac.AnimalCostoID
		, ac.AnimalID
		, ac.FechaCosto
		, ac.CostoID
		, ac.TipoReferencia
		, ac.FolioReferencia
		, ac.Importe
		, ac.FechaCreacion
		, ac.UsuarioCreacionID
		, ac.FechaModificacion
		, ac.UsuarioModificacionID 
	from 
		AnimalCosto ac
		left outer join AnimalCostoHistorico ach on
			ac.AnimalCostoID = ach.AnimalCostoID
	where
		ac.AnimalID = @animalID
		and ach.AnimalCostoID is null
end
GO

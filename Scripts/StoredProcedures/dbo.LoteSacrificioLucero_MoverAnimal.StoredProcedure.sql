USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_MoverAnimal]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_MoverAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_MoverAnimal 1013758
create procedure [dbo].[LoteSacrificioLucero_MoverAnimal]
	@animalID bigint
	, @pesoNoqueo int
	, @pesoCanal int
	, @pesoPiel int
as
	insert into AnimalHistorico (
		AnimalID
		, Arete
		, AreteMetalico
		, FechaCompra
		, TipoGanadoID
		, CalidadGanadoID
		, ClasificacionGanadoID
		, PesoCompra
		, OrganizacionIDEntrada
		, FolioEntrada
		, PesoLlegada
		, Paletas
		, CausaRechadoID
		, Venta
		, Cronico
		, PesoNoqueo
		, PesoCanal
		, PesoPiel
		, Activo
		, FechaCreacion
		, UsuarioCreacionID
		, FechaModificacion
		, UsuarioModificacionID
	)
	select 
		ac.AnimalID
		, ac.Arete
		, ac.AreteMetalico
		, ac.FechaCompra
		, ac.TipoGanadoID
		, ac.CalidadGanadoID
		, ac.ClasificacionGanadoID
		, ac.PesoCompra
		, ac.OrganizacionIDEntrada
		, ac.FolioEntrada
		, ac.PesoLlegada
		, ac.Paletas
		, ac.CausaRechadoID
		, ac.Venta
		, ac.Cronico
		, @pesoNoqueo
		, @pesoCanal 
		, @pesoPiel
		, ac.Activo
		, ac.FechaCreacion
		, ac.UsuarioCreacionID
		, ac.FechaModificacion
		, ac.UsuarioModificacionID
	from 
		Animal ac
		left outer join AnimalHistorico ach on
			ac.AnimalID = ach.AnimalID
	where
		ac.AnimalID = @animalID
		and ach.AnimalID is null
	group by
		ac.AnimalID
		, ac.Arete
		, ac.AreteMetalico
		, ac.FechaCompra
		, ac.TipoGanadoID
		, ac.CalidadGanadoID
		, ac.ClasificacionGanadoID
		, ac.PesoCompra
		, ac.OrganizacionIDEntrada
		, ac.FolioEntrada
		, ac.PesoLlegada
		, ac.Paletas
		, ac.CausaRechadoID
		, ac.Venta
		, ac.Cronico
		, ac.Activo
		, ac.FechaCreacion
		, ac.UsuarioCreacionID
		, ac.FechaModificacion
		, ac.UsuarioModificacionID

GO

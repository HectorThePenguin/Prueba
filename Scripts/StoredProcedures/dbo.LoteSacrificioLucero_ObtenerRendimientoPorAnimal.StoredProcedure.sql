USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerRendimientoPorAnimal]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerRendimientoPorAnimal]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerRendimientoPorAnimal]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec LoteSacrificioLucero_ObtenerRendimientoPorAnimal 1013758, 3, '2015-10-02'
create procedure [dbo].[LoteSacrificioLucero_ObtenerRendimientoPorAnimal]
	@animalId bigint
	, @organizacion int
	, @fechaSacrificio smalldatetime
as
begin
	DECLARE @valor decimal

	SELECT 
		@valor = CAST(po.Valor AS decimal) 
	from 	
		Parametro (nolock) p
		INNER JOIN ParametroOrganizacion (nolock) po ON 
			po.ParametroID = p.ParametroID 
	where
		p.Clave = 'AjustePesoSalida'
		AND po.OrganizacionID = @Organizacion

	SET @valor = ISNULL(@valor, 0)

	SELECT 
		los.OrganizacionID
		, a.AnimalID
		, c.Codigo AS Corral
		, l.Lote AS Lote
		, a.AreteBigint AS Arete
		, los.FechaSalida as FechaSacrificio
		, CAST(ROUND(los.PesoNoqueo * (1 + (@valor / 100)), 0) AS int) AS PesoSalida
		, CAST(los.PesoNoqueo AS int) AS PesoNoqueo
		, CAST(ROUND(los.PesoCanal, 0) AS int) AS PesoCanal
		, CAST(ROUND(los.PesoPiel, 0) AS int) AS PesoPiel
		, am.LoteID AS LoteOrigenID
		, c.CorralID
		, am.AnimalMovimientoID AS AnimalMovimientoIDAnterior
		, 16 as TipoMovimientoID
		, a.AreteMetalico
		, a.FechaCompra
		, a.TipoGanadoID
		, a.CalidadGanadoID
		, a.ClasificacionGanadoID
		, a.PesoCompra
		, a.FolioEntrada
		, a.PesoLlegada
		, a.Paletas
		, a.CausaRechadoID
		, a.Venta
		, a.Cronico
		, a.CambioSexo
		, a.Activo
		, a.FechaCreacion
		, a.UsuarioCreacionID
		, a.FechaModificacion
		, a.UsuarioModificacionID
	FROM
		Animal (nolock) a 
		INNER JOIN AnimalMovimiento (nolock) am ON 
			am.AnimalID = a.AnimalID 
			AND am.Activo = 1 
			AND am.TipoMovimientoID not in (8, 11)
		INNER JOIN Lote (nolock) l ON 
			l.LoteID = am.LoteID 
		INNER JOIN Corral (nolock) c ON 
			l.CorralID = c.CorralID
		INNER JOIN LayoutSubidaRendimientos (nolock) los ON 
			a.AreteBigint = cast(los.Arete as bigint)
			AND am.OrganizacionID = los.OrganizacionID
	WHERE 
		a.AnimalID = @animalId
end
GO

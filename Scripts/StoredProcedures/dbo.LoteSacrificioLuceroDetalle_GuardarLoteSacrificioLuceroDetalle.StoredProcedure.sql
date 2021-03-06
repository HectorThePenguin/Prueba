USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioLuceroDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioLuceroDetalle]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioLuceroDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioLuceroDetalle]  
 @fechaSacrificio datetime  
 , @organizacion int  
 , @organizacionSac int  
 , @resultado varchar(800) out  
AS  
BEGIN  
	SET NOCOUNT ON;  
 
	BEGIN TRY  

		-- Obtener Lotes de Sacrificio
		select
			ls.LoteSacrificioID
			, ls.Corral
			, ls.InterfaceSalidaTraspasoDetalleID
		into
			#loteSacrificio
		from
			LoteSacrificioLucero ls (nolock)
			inner join InterfaceSalidaTraspasoDetalle id (nolock) on
				ls.InterfaceSalidaTraspasoDetalleID = id.InterfaceSalidaTraspasoDetalleID
			inner join InterfaceSalidaTraspaso i (nolock) on
				id.InterfaceSalidaTraspasoID = i.InterfaceSalidaTraspasoID
		where
			ls.Fecha = @fechaSacrificio 
			and i.OrganizacionIDDestino = @organizacionSac
			and ls.Activo = 1

		-- Obtener Aretes Sacrificados
		select
			spi.CorralSacrificio
			, l.LoteID
			, cast(spi.Arete as bigint) Arete
			, a.AnimalID
		into
			#spi
		from
			InterfazSPI (nolock) spi
			inner join Lote l (nolock) on
				spi.OrganizacionId = l.OrganizacionID
				and RIGHT('0000' + RTRIM(LTRIM(spi.Lote)), 4) = RIGHT('0000' + RTRIM(LTRIM(l.Lote)), 4)
			inner join Animal a (nolock) on
				a.AreteBigint = cast(spi.Arete as bigint) 
				and spi.OrganizacionId = a.OrganizacionIDEntrada
		where
			spi.OrganizacionId = @organizacion
			and OrganizacionIdSacrificio = @organizacionSac
			and FechaSacrificio = @fechaSacrificio
		
  		select 
			row_number() over (partition by corral order by arete) ndx
			, cast(Arete as bigint) Arete
			, Corral
		into 
			#are 
		from 
			interfazspi
		where 
			fechasacrificio = @fechaSacrificio
			and organizacionid = @organizacion
			and organizacionidsacrificio = @organizacionSac

		select 
			row_number() over (partition by corral order by arete) ndx
			, Corral
			, IDLayoutSubidaRendimientos id 
		into 
			#lr 
		from 
			LayoutSubidaRendimientos (nolock)
		where 
			fechasalida = @fechaSacrificio
			and organizacionid = @organizacion
			and organizacionidsacrificio = @organizacionSac
		
		update
			c
		set 
			Arete = a.Arete
		from
			#are a
			inner join #lr b on
				a.ndx = b.ndx
				and a.Corral = b.Corral
			inner join layoutsubidarendimientos c (nolock) on
				b.id = c.IDLayoutSubidaRendimientos
  
		INSERT INTO   
			LoteSacrificioLuceroDetalle  
		SELECT DISTINCT  
			LoteSacrificioID  
			, AnimalID  
		from   
			#loteSacrificio ls
			inner join #spi a on 
				ls.Corral = a.CorralSacrificio
			  
		SELECT @resultado = 'OK'  
  
	END TRY  
	BEGIN CATCH  
		SELECT @resultado = ERROR_MESSAGE()  
	END CATCH  
END 

GO

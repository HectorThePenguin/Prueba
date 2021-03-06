USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioDetalle_GuardarLoteSacrificioDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioDetalle_GuardarLoteSacrificioDetalle]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioDetalle_GuardarLoteSacrificioDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Victor Sarmiento>
-- Create date: <18 Ago 2014>
-- Description: <Se guarda en la tabla LoteSacrificioDetalle en base a los aretes registrados en la tabla InterfazSPI>
/*
declare @res varchar(800)
exec LoteSacrificioDetalle_GuardarLoteSacrificioDetalle '2015-06-09', 4, @res out
select @res
select * from interfazspi where fechaSacrificio = '2015-03-03' and organizacionid = 2
*/
-- =============================================
CREATE PROCEDURE [dbo].[LoteSacrificioDetalle_GuardarLoteSacrificioDetalle]
	@fechaSacrificio datetime
	, @organizacion int
	, @resultado varchar(800) out
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT 
			ls.LoteSacrificioID
			, ls.LoteID
			, l.OrganizacionID
		INTO 
			#LoteCorral
		FROM 
			LoteSacrificio ls 
			INNER JOIN Lote l ON 
				ls.LoteID = l.LoteID
		WHERE 
			Fecha = @fechaSacrificio
			and l.OrganizacionID =  @organizacion
			and ls.Activo = 1

		select
			spi.Corral
			, l.LoteID
			, cast(spi.Arete as bigint) Arete
			, cast(0 as bit) Ajustado
		into
			#spi
		from
			InterfazSPI spi
			inner join Lote l on
				spi.OrganizacionId = l.OrganizacionID
				and RIGHT('0000' + RTRIM(LTRIM(spi.Lote)), 4) = RIGHT('0000' + RTRIM(LTRIM(l.Lote)), 4)
		where
			spi.OrganizacionId = @organizacion
			and FechaSacrificio = @fechaSacrificio

		select
			a.AnimalID
			, cast(a.Arete as bigint) as Arete
			, am.LoteID
		into
			#Inventario
		from
			AnimalMovimiento am
			inner join Animal a on
				am.AnimalID = a.AnimalID
				and am.Activo = 1
				and am.TipoMovimientoID != 16
				and am.OrganizacionID = @organizacion

		-- Obtener Aretes Ajustados
		select 
			i.AnimalID
			, i.Arete
			, i.LoteID as LoteIDSacrificio
		into
			#Ajustados
		from
			#Inventario i
			inner join #spi s on
				i.Arete = s.Arete
				and i.LoteID = s.LoteID

		INSERT INTO 
			LoteSacrificioDetalle
		SELECT DISTINCT
			LoteSacrificioID
			, AnimalID
		from 
			#LoteCorral ls 
			inner join #Ajustados a on
				ls.LoteID = a.LoteIDSacrificio

		SELECT @resultado = 'OK'

	END TRY
	BEGIN CATCH
		SELECT @resultado = ERROR_MESSAGE()
	END CATCH
END

GO

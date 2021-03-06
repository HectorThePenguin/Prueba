USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ValidaCorrales]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ValidaCorrales]
GO
/****** Object:  StoredProcedure [dbo].[ValidaCorrales]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidaCorrales] @ordenSacrificio INT
AS
BEGIN
	SET NOCOUNT ON;

	

	if exists (select cast(Arete as bigint) from Animal where OrganizacionIDEntrada = 4 group by cast(Arete as bigint) having Count(1) > 1)
	begin
		Select 'Existen Aretes Duplicados.'
	end
	else
	begin
		if exists(select AnimalID from AnimalMovimiento where Activo = 1 and OrganizacionID = 4 group by AnimalID having COUNT(1) > 1)
		begin
			Select 'Existen Movimientos Activos Duplicados.'
		end
		else
		begin
			select	
				osd.LoteID
				, SUM(osd.CabezasSacrificio) CabezasSacrificio
			into
				#osd
			from
				OrdenSacrificio os
				inner join OrdenSacrificioDetalle osd on
					os.OrdenSacrificioID = osd.OrdenSacrificioID
			where
				os.Activo = 1
				and os.OrdenSacrificioID = @ordenSacrificio
				and os.OrganizacionID = 4
				and osd.Activo = 1
			group by
				osd.LoteID

			select
				am.LoteID
				, COUNT(distinct a.Arete) CabezasInventario
			into 	
				#am
			from
				AnimalMovimiento am 
				inner join Animal a on
					am.AnimalID = a.AnimalID
				inner join AnimalCosto ac on
					am.AnimalID = ac.AnimalID
					and ac.CostoID = 1
				inner join #osd osd on
					am.LoteID = osd.LoteID
					and am.Activo = 1
					and am.TipoMovimientoID != 16
			group by
				am.LoteID

			select	
				o.LoteID
				, o.CabezasSacrificio
				, ISNULL(a.CabezasInventario, 0) as CabezasInventario
				, case when ISNULL(a.CabezasInventario, 0) >= o.CabezasSacrificio then 1 else 0 end PuedeCerrar
			from
				#osd o
				left outer join #am a on
					a.LoteID = o.LoteID
	
			drop table #am, #osd
		end
	end
END


GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_AjusteAretes_Intensivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfazSPI_AjusteAretes_Intensivo]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_AjusteAretes_Intensivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InterfazSPI_AjusteAretes_Intensivo] 
	@OrganizacionId int
	, @OrdenSacrificioId int
	, @OrganizacionIdSacrificio int
	, @FechaSacrificio smalldatetime
	, @Folios varchar(4000)
as
begin

	declare @xml xml;
	set @xml = @Folios;
	--set @xml = '<Sacrificio><LotePadre><Istd></Istd><Cabezas></Cabezas></LotePadre></Sacrificio>'

	select
		T.r.value('./Istd[1]', 'int') as Istd
		, T.r.value('./Cabezas[1]', 'int') as Cabezas
	into
		#Relacion
	from
		@xml.nodes('/Sacrificio/LotePadre') T(r)

	create Table #a (
		ndx int
		, InterfazSPIID int
		, Arete Varchar(20)
		, LoteID int
	)

	create table #r (
		CorralSacrificio					varchar(10)
		, LoteSacrificio					varchar(10)
		, CorralIDSacrificio				int
		, LoteIDSacrificio					int
		, CorralSalida						varchar(10)
		, LoteSalida						varchar(10)
		, CorralIDSalida					int
		, LoteIDSalida						int
		, InterfaceSalidaTraspasoDetalleID	int
	)

	insert into #r
	exec ObtenerRelacionCorralSacrificioIntensivo @OrdenSacrificioId

	insert into #a 
	select
		row_number() over (partition by r.LoteIDSalida order by spi.Arete) as ndx
		, spi.InterfazSPIID
		, spi.Arete
		, r.LoteIDSalida
	from
		(select LoteSalida, LoteIDSalida from #r group by LoteSalida, LoteIDSalida) r
		inner join InterfazSPI spi (nolock) on
			right(r.LoteSalida,4) = right(spi.lote,4)
			and spi.FechaSacrificio = @fechaSacrificio
			and spi.organizacionid = @organizacionID 
			and spi.organizacionidsacrificio = @OrganizacionIdSacrificio

	select
		   row_number() over (partition by lsal.LoteID order by a.AnimalID) as ndx
		   , a.Arete
		   , lsal.LoteID
	into #b
	from
		   OrdenSacrificio os (nolock)
		   inner join OrdenSacrificioDetalle osd (nolock) on
				 os.OrdenSacrificioID = @OrdenSacrificioId
				 and os.OrdenSacrificioID = osd.OrdenSacrificioID
		   inner join EntradaGanado eg (nolock) on
				 osd.LoteID = eg.LoteID
		   inner join InterfaceSalidaTraspaso ist (nolock) on
				 eg.FolioOrigen = ist.FolioTraspaso
				 and eg.OrganizacionID = ist.OrganizacionIDDestino
		   inner join InterfaceSalidaTraspasoDetalle istd (nolock) on
				 ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID
		   inner join Lote lsal (nolock) on
				 istd.LoteID = lsal.LoteID
		   inner join Corral csal (nolock) on
				 lsal.CorralID = csal.CorralID
		   inner join AnimalMovimiento am (nolock) on
				 am.LoteID = lsal.LoteID
				 and am.Activo = 1
				 and am.TipoMovimientoID != 16
		   inner join Animal a (nolock) on
				 am.AnimalID = a.AnimalID
		   inner join (select AnimalID, InterfaceSalidaTraspasoDetalleID from InterfaceSalidaTraspasoCosto (nolock) where Facturado = 0 group by AnimalID, InterfaceSalidaTraspasoDetalleID) istc on
				 am.AnimalID = istc.AnimalID 
				 and istd.InterfaceSalidaTraspasoDetalleID = istc.InterfaceSalidaTraspasoDetalleID

	update s
	set    Arete = b.Arete
	from
		   interfazspi s 
		   inner join #a a on s.interfazSPIID = a.interfazSPIID 
		   inner join #b b on a.LoteID = b.LoteID and a.ndx = b.ndx

	drop table #a, #b, #r

	select distinct
		spi.Corral
		, lsal.LoteID
		, cast(spi.Arete as bigint) Arete
		, cast(0 as bit) Ajustado
	into
		#spi
	from
		OrdenSacrificio os (nolock)
		inner join OrdenSacrificioDetalle osd (nolock) on
			os.OrdenSacrificioID = osd.OrdenSacrificioID
		inner join EntradaGanado eg (nolock) on
			osd.LoteID = eg.LoteID
		inner join InterfaceSalidaTraspaso ist (nolock) on
			eg.FolioOrigen = ist.FolioTraspaso
			and eg.OrganizacionID = ist.OrganizacionIDDestino
		inner join InterfaceSalidaTraspasoDetalle istd (nolock) on
			ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID
		inner join Lote lsal (nolock) on
			istd.LoteID = lsal.LoteID
		inner join InterfazSPI spi (nolock) on
			RIGHT('0000' + RTRIM(LTRIM(lsal.Lote)), 4) = RIGHT('0000' + RTRIM(LTRIM(spi.Lote)), 4)
			and lsal.organizacionid = spi.organizacionid
			and dateadd(d,0,datediff(d,0,os.FechaOrden)) = spi.FechaSacrificio
	where
		os.OrdenSacrificioID = @OrdenSacrificioId
		and spi.OrganizacionIdSacrificio = @OrganizacionIdSacrificio 
		
	select
		Corral
		, LoteID
		, COUNT(1) Cabezas
	into
		#sacrificados
	from
		#spi
	group by
		Corral
		, LoteID

	-- Detectar corrales incompletos
	select
		s.Corral
		, am.LoteID
		, ((s.Cabezas) - am.Cabezas) Diferencia
	into
		#incompletos
	from
		#sacrificados s
		left outer join (select LoteID, COUNT(1) Cabezas from AnimalMovimiento where Activo = 1 and TipoMovimientoID != 16 group by LoteID) am on
			am.LoteID = s.LoteID
	where
		((s.Cabezas) - am.Cabezas) > 0

	if exists (select * from #incompletos)
	begin
		declare @result varchar(500)
		SELECT @result = substring((
			SELECT 
				', Al Corral '
				, Corral AS [text()]
				, ' le faltan '
				, cast(Diferencia AS INT) AS [text()]
				, ' Cabeza(s)'
			FROM #incompletos
			FOR XML path('')
		), 3, 4000) + ' en los Corrales de Lucero'
		raiserror (@result, 16, 1)
	end

	-- Obtener inventario de Aretes
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
			and am.OrganizacionID = @OrganizacionId

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

	-- Identificar los Aretes Ajustados
	update s
	set Ajustado = 1
	from
		#spi s
		inner join #Ajustados a
			on s.Arete = a.Arete
	
	-- Obtener Aretes por Ajustar
	select
		ROW_NUMBER() over (order by s.LoteID) as Indice
		, s.LoteID as LoteIDSac
		, s.Arete
		, i.AnimalID as AnimalIDLoc
		, i.LoteID as LoteIDLoc
	into
		#PorAjustar
	from 
		#spi s
		left outer join #inventario i on
			i.Arete = s.Arete
	where
		s.Ajustado = 0

	declare @indice int = 1
	declare @maximo int
	select @maximo = max(Indice) from #PorAjustar

	declare @loteSac int
	declare @arete bigint
	declare @animal int
	declare @lote int

	declare @areteR bigint
	declare @animalR int
	
	declare @ajustados table (indice int)

	while (@indice <= @maximo)
	begin
		if exists (select * from @ajustados where indice = @indice)
		begin
			set @indice = @indice + 1
			continue
		end

		select @loteSac = LoteIDSac, @arete = Arete, @animal = AnimalIDLoc, @lote = LoteIDLoc from #PorAjustar where Indice = @indice and Indice not in (select indice from @ajustados)

		declare @ndx int

		select top 1 @ndx = Indice, @areteR = Arete, @animalR = AnimalIDLoc from #PorAjustar where LoteIDLoc = @loteSac and Indice not in (select indice from @ajustados)
		if @@rowcount = 1 and @animal is not null
		begin
			-- Acomodar aretes entre lotes sacrificados
			update #PorAjustar
			set Arete = @areteR, LoteIDLoc = @loteSac
			where Indice = @indice

			update #PorAjustar
			set Arete = @arete, LoteIDLoc = @lote
			where Indice = @ndx

			update #inventario
			set Arete = @areteR
			where AnimalID = @animal

			update #inventario
			set Arete = @arete
			where AnimalID = @animalR

			insert into @ajustados select @indice union select @ndx
			set @indice = @indice + 1
			continue
		end

		select top 1 @areteR = Arete, @animalR = AnimalID from #inventario where LoteID = @loteSac and Arete not in (select Arete from #spi) 
		if @@rowcount = 1 and @animal is not null
		begin		
			-- Acomodar aretes entre lotes no sacrificados
			update #PorAjustar
			set AnimalIDLoc = @animalR, LoteIDLoc = @loteSac
			where Indice = @indice

			update #inventario
			set Arete = @areteR
			where AnimalID = @animal

			update #inventario
			set Arete = @arete
			where AnimalID = @animalR

			insert into #PorAjustar (Indice, LoteIDSac, Arete, AnimalIDLoc, LoteIDLoc)
			select @indice * -1, @lote, @areteR, @animal, @lote
			
			insert into @ajustados select @indice union select @indice * -1
			set @indice = @indice + 1
			continue
		end

		select top 1 @areteR = Arete, @animalR = AnimalID from #inventario where LoteID = @loteSac and Arete not in (select Arete from #spi) 
		if @@rowcount = 1 and @animal is null
		begin		
			-- Acomodar aretes que no existen en el inventario
			update #PorAjustar
			set AnimalIDLoc = @animalR, LoteIDLoc = @loteSac
			where Indice = @indice

			update #inventario
			set Arete = @arete
			where AnimalID = @animalR

			insert into @ajustados select @indice
		end
		
		set @indice = @indice + 1
	end

	if exists (select * from #PorAjustar)
	begin
		-- Actualizar tabla Animal
		update a
		set Arete = p.Arete
		from Animal a
		inner join #PorAjustar p on
			a.AnimalID = p.AnimalIDLoc
	end

	drop table #spi, #sacrificados, #incompletos, #Inventario, #Ajustados, #PorAjustar
		
	update
		InterfazSPI
	set
		Procesado = 1
	where
		OrganizacionId = @OrganizacionId
		and OrganizacionIdSacrificio = @OrganizacionIdSacrificio
		and FechaSacrificio = @FechaSacrificio		

	-- Generar Calculo de Costos de Sacrificio	
	SELECT cv.Valor + cs.ClaveContable AS CTA_CON
		,cs.CostoID
	INTO #Cuentas
	FROM CuentaValor cv(NOLOCK)
	INNER JOIN Cuenta c(NOLOCK) ON cv.CuentaID = c.CuentaID
	CROSS JOIN Costo cs(NOLOCK)
	WHERE c.ClaveCuenta = 'CTACTOGAN'
		AND cv.OrganizacionID = @OrganizacionID

	declare @Fecha smalldatetime
	set @Fecha = DATEADD(d,0,datediff(d,0,getdate()))
		
	select 
		0 as CostoCorralId
		, cast(egcs.CostoID as varchar(2)) as CodigoCosto
		, '51' AS TipoMovimiento
		, cast(osd.FolioSalida AS VARCHAR(6)) AS FOLIO
		, @FechaSacrificio AS FECHA
		, (egcs.Importe * cast(cast(r.Cabezas as decimal(18,2)) / cast(istd.Cabezas as decimal(18,2)) as decimal(18,2))) * -1 as IMPORTE
		, RIGHT('000' + rtrim(ltrim(c.Codigo)), 3) as Corral
		, RIGHT('0000' + rtrim(ltrim(l.Lote)), 4) AS Proceso 
		, cs.CTA_CON AS CuentaContable
		, '0' AS NUM_LIN
		, (egcs.Importe * cast(cast(r.Cabezas as decimal(18,2)) / cast(istd.Cabezas as decimal(18,2)) as decimal(18,2))) * -1 as IMPORTE_A
		, @Fecha AS FEC_ACT
	from 
		EntradaGanado eg (nolock)
		inner join EntradaGanadoCosteo egc (nolock) on 
			eg.EntradaGanadoID = egc.EntradaGanadoID
		inner join EntradaGanadoCosto egcs (nolock) on
			egc.EntradaGanadoCosteoID = egcs.EntradaGanadoCosteoID
		inner join InterfaceSalidaTraspaso ist (nolock) on
			eg.FolioOrigen = ist.FolioTraspaso
			and eg.OrganizacionID = ist.OrganizacionIDDestino
		inner join InterfaceSalidaTraspasoDetalle istd (nolock) on
			ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID
		inner join OrdenSacrificioDetalle osd (nolock) on
			eg.LoteID = osd.LoteID
		inner join OrdenSacrificio os (nolock) on
			osd.OrdenSacrificioID = os.OrdenSacrificioID
		inner join #Cuentas cs on
			egcs.CostoID = cs.CostoID
		inner join Lote l on
			eg.LoteID = l.LoteID
		inner join Corral c on
			l.CorralID = c.CorralID
		inner join Lote l2 on
			istd.LoteID = l2.LoteID
		inner join Corral c2 on
			l2.CorralID = c2.CorralID
		inner join #Relacion r on
			istd.InterfaceSalidaTraspasoDetalleID = r.Istd
	where 
		os.OrdenSacrificioID = @OrdenSacrificioId
	order by
		c.Codigo
end

GO

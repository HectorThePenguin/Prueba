USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_EjecutarProgramacionSacrificioLucero]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_EjecutarProgramacionSacrificioLucero]
GO
/****** Object:  StoredProcedure [dbo].[sp_EjecutarProgramacionSacrificioLucero]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_EjecutarProgramacionSacrificioLucero]
as
begin

	CREATE TABLE #LoteSacrificio
	(
		LoteID INT
		, FolioOrdenSacrificio INT
		, Corral VARCHAR(3)
		, Lote VARCHAR(4)
		, Fecha VARCHAR(10)
		, ImporteCanal DECIMAL(18,2)
		, ImportePiel DECIMAL(18,2)
		, ImporteVisera DECIMAL(18,2)
		, Serie VARCHAR(10)
		, Folio VARCHAR(10)
		, Observaciones VARCHAR(800)
		, Activo BIT
		, FechaCreacion SMALLDATETIME
		, UsuarioCreacionID INT
		, FechaModificacion SMALLDATETIME
		, UsuarioModificacion INT
		, ClienteID int
		, Cabezas int
		, PesoNoqueo DECIMAL(18,2)
		, PesoCanal DECIMAL(18,2)
		, PesoPiel DECIMAL(18,2)
		, PesoVisceras DECIMAL(18,2)
		, Procesado bit not null default(0)
		, LoteSacrificioId INT
	)

	Create Table #spi
	(
		OrganizacionID int, 
		Corral varchar(3), 
		Lote varchar(4), 
		Arete varchar(20), 
		Fecha smalldatetime, 
		Procesado bit,
		OrganizacionIdSacrificio int,
		CorralSacrificio varchar(3)
	)

	create Table #lsr
	(
		OrganizacionId int
		, Lote varchar (4)
		, Corral varchar(3)
		, Arete varchar(20)
		, Fecha varchar(10)
		, PesoNoqueo decimal(18,2)
		, PesoCanal decimal(18,2)
		, PesoPiel decimal(18,2)
		, OrganizacionIdSacrificio int
	)

	create table #lsd
	(
		LoteSacrificioId int
		, AnimalID int
	)

	MERGE 
		LoteSacrificioLucero as trg
	USING (
			select 
				LoteID, Corral, Lote, Fecha, ISTDID, LoteSacrificioID
			from
				ProgramacionSacrificioLucero
			where
				Sacrificado = 0
		) AS src (LoteID, Corral, Lote, Fecha, ISTDID, LoteSacrificioID) ON (
			trg.Lote = src.Lote 
			AND trg.Corral = src.Corral 
			AND trg.Fecha = src.Fecha 
			AND trg.LoteID = src.LoteID
			AND trg.InterfaceSalidaTraspasoDetalleID = src.ISTDID
			--AND trg.LoteSacrificioID = src.LoteSacrificioID
		) 
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (LoteID,FolioOrdenSacrificio,Corral,Lote,Fecha,ImporteCanal,ImportePiel,ImporteVisera,Serie,Folio,Observaciones,Activo
		,FechaCreacion,UsuarioCreacionID,FechaModificacion,UsuarioModificacionID,ClienteID,InterfaceSalidaTraspasoDetalleID,PolizaGenerada,PesoCanal,PesoPiel,PesoVisceras)		
		VALUES (src.LoteID, 0, src.Corral, src.Lote, src.Fecha, 0, 0, 0, null, null, '', 1
		,getdate(), 1, null, null, null, src.ISTDID, 0, 0, 0, 0)

	OUTPUT $action;

	Select
		OrganizacionID
		, Fecha
		, 0 as Procesado
	into 
		#progs
	from
		ProgramacionSacrificioLucero
	where
		Sacrificado = 0
	group by
		OrganizacionID
		, Fecha

	while exists (select * from #progs where Procesado = 0)
	begin
		declare @fec varchar(10)
		declare @org int

		select top 1 @fec = convert(varchar(10), fecha, 121), @org = organizacionid from #progs where Procesado = 0 order by Fecha
		select @fec, @org

		Insert into #LoteSacrificio (LoteID, FolioOrdenSacrificio, Corral, Lote, Fecha, ImporteCanal, ImportePiel, ImporteVisera, Serie, Folio, Observaciones, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacion, ClienteID, Cabezas, PesoNoqueo, PesoCanal, PesoPiel, PesoVisceras)
		exec sp_ObtenerLoteSacrificioLucero @fec, @org

		while exists (Select * from ProgramacionSacrificioLucero where Sacrificado = 0 and Fecha = @fec and OrganizacionID = @org)
		begin
			declare @top int
			declare @lid int
			declare @ist int
			declare @lot int
			declare @lst int
			declare @tot int
			select top 1 @top = cabezas, @lot = LoteID, @lid = Id, @ist = ISTDID, @lst = LoteSacrificioID from ProgramacionSacrificioLucero where Sacrificado = 0 and Fecha = @fec and OrganizacionID = @org

			update ls
			set
				Fecha = lss.Fecha
				, ImporteCanal = lss.ImporteCanal					* (cast(pp.Cabezas as decimal(18,2)) / cast(lss.Cabezas as decimal(18,2)))
				, ImportePiel = lss.ImportePiel 					* (cast(pp.Cabezas as decimal(18,2)) / cast(lss.Cabezas as decimal(18,2)))
				, ImporteVisera = lss.ImporteVisera 				* (cast(pp.Cabezas as decimal(18,2)) / cast(lss.Cabezas as decimal(18,2)))
				, PesoCanal = lss.PesoCanal 						* (cast(pp.Cabezas as decimal(18,2)) / cast(lss.Cabezas as decimal(18,2)))
				, PesoPiel = lss.PesoPiel 							* (cast(pp.Cabezas as decimal(18,2)) / cast(lss.Cabezas as decimal(18,2)))
				, PesoVisceras = lss.PesoVisceras 					* (cast(pp.Cabezas as decimal(18,2)) / cast(lss.Cabezas as decimal(18,2)))
				, FolioOrdenSacrificio = lss.FolioOrdenSacrificio
			from 
				LoteSacrificioLucero ls
				inner join #LoteSacrificio lss on ls.Corral = lss.Corral and ls.Lote = lss.Lote
				inner join ProgramacionSacrificioLucero pp on ls.InterfaceSalidaTraspasoDetalleID = pp.ISTDID
				--inner join ProgramacionSacrificioLucero pp on ls.LoteSacrificioID = pp.LoteSacrificioID
			Where
				pp.Id = @lid

			declare @sql nvarchar(max)

			insert into #lsd (LoteSacrificioID, AnimalID)
			select top(@top)
				ls.LoteSacrificioID, am.AnimalID
			from 
				LoteSacrificioLucero ls
				inner join AnimalMovimiento am on
					ls.LoteID = am.LoteID and am.Activo = 1 and am.TipoMovimientoID != 16
				inner join (select InterfaceSalidaTraspasoDetalleID, AnimalID from InterfaceSalidaTraspasoCosto where facturado = 0 group by InterfaceSalidaTraspasoDetalleID, AnimalID) itc on
					am.AnimalID = itc.AnimalID
					and ls.InterfaceSalidaTraspasoDetalleID = itc.InterfaceSalidaTraspasoDetalleID
				left outer join LoteSacrificioLuceroDetalle lsd on
					am.AnimalID = lsd.AnimalID
				left outer join #lsd lssd on
					am.AnimalID = lssd.AnimalID
			where 
				FolioOrdenSacrificio is not null and Serie is null
				and ls.LoteID = @lot 
				and ls.InterfaceSalidaTraspasoDetalleID = @ist
				--and ls.LoteSacrificioID = @lst
				and lsd.AnimalID is null
				and lssd.AnimalID is null
			group by
				ls.LoteSacrificioid
				, am.AnimalID

			insert into #spi (OrganizacionID, Corral, Lote, Arete, Fecha, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
			select 
				5 as orgnzacionID, c.Codigo, l.Lote, a.Arete, ls.Fecha, 0 as Procesado, @org, lss.Corral
			from
				LoteSacrificioLucero ls 
				inner join #lsd lsd on ls.LoteSacrificioID = lsd.LoteSacrificioID
				inner join AnimalMovimiento am on lsd.AnimalID = am.AnimalID and am.Activo = 1
				inner join Animal a on lsd.AnimalId = a.AnimalId
				inner join Lote l on am.LoteID = l.LoteID
				inner join Corral c on l.CorralID = c.CorralID
				inner join ProgramacionSacrificioLucero pp on ls.InterfaceSalidaTraspasoDetalleID = pp.ISTDID
				--inner join ProgramacionSacrificioLucero pp on ls.LoteSacrificioID = pp.LoteSacrificioID
				inner join #LoteSacrificio lss on ls.Corral = lss.Corral and ls.Lote = lss.Lote
			where 
				pp.ID = @lid

			insert into #lsr (OrganizacionID, Lote, Corral, Arete, Fecha, PesoNoqueo, PesoCanal, PesoPiel, OrganizacionIdSacrificio)
			select 
				5 as organizacionId, l.Lote, c.Codigo, a.Arete, convert(varchar(10), ls.Fecha, 120) as fecha
				, CAST(lss.PesoNoqueo / cast(lss.Cabezas as decimal(18,2)) as decimal(18,2)) as PesoNoqueo
				, CAST(ls.PesoCanal / pp.Cabezas as decimal(18,2)) as PesoCanal
				, CAST(ls.PesoPiel / pp.Cabezas as decimal(18,2)) as PesoPiel
				, @org
			from
				LoteSacrificioLucero ls 
				inner join #lsd lsd on ls.LoteSacrificioID = lsd.LoteSacrificioID
				inner join AnimalMovimiento am on lsd.AnimalID = am.AnimalID and am.Activo = 1
				inner join Animal a on lsd.AnimalId = a.AnimalId
				inner join Lote l on am.LoteID = l.LoteID
				inner join Corral c on l.CorralID = c.CorralID
				inner join ProgramacionSacrificioLucero pp on ls.InterfaceSalidaTraspasoDetalleID = pp.ISTDID
				--inner join ProgramacionSacrificioLucero pp on ls.LoteSacrificioID = pp.LoteSacrificioID
				inner join #LoteSacrificio lss on ls.Corral = lss.Corral and ls.Lote = lss.Lote
			where 
				pp.ID = @lid
			group by
				l.Lote, c.Codigo, a.Arete, ls.Fecha, lss.PesoNoqueo, ls.PesoCanal, ls.PesoPiel, pp.Cabezas, lss.Cabezas

			update ProgramacionSacrificioLucero set Sacrificado = 1 where Id = @lid
		end
	
		insert into LoteSacrificioLuceroDetalle (LoteSacrificioID, AnimalID)
		select LoteSacrificioID, AnimalID from #lsd

		insert into InterfazSPI (OrganizacionID, Corral, Lote, Arete, FechaSacrificio, Procesado, OrganizacionIdSacrificio, CorralSacrificio)
		select OrganizacionID, Corral, Lote, Arete, Fecha, Procesado, OrganizacionIdSacrificio, CorralSacrificio from #spi

		insert into LayoutSubidaRendimientos (OrganizacionID, Lote, Corral, Arete, FechaSalida, PesoNoqueo, PesoCanal, PesoPiel, OrganizacionIdSacrificio)
		select OrganizacionID, Lote, Corral, Arete, Fecha, PesoNoqueo, PesoCanal, PesoPiel, OrganizacionIdSacrificio from #lsr

		update #progs set Procesado = 1 where Fecha = @fec and OrganizacionID = @org

		update ProgramacionSacrificioLucero set Sacrificado = 1 where Fecha = @fec and OrganizacionID = @org

		delete #LoteSacrificio
		delete #spi
		delete #lsr
		delete #lsd
	end

	drop table #progs, #LoteSacrificio, #lsr, #spi, #lsd
end

-- select count(cast(arete as bigint)), count(distinct cast(arete as bigint)) from animal a inner join #lsd l on a.animalid = l.animalid

-- update ProgramacionSacrificioLucero set Sacrificado = 0 where Fecha = '2015-06-30' and OrganizacionId = 1

-- select distinct animalid from #lsd
--create table ProgramacionSacrificioLucero
--(
--	Id int identity(1, 1) primary key
--	, Folio int not null
--	, Corral varchar(3) not null
--	, Lote varchar(4) not null
--	, Cabezas int not null
--	, ISTDID int not null
--	, LoteID int not null
--	, Fecha datetime not null
--	, OrganizacionID int not null
--	, Sacrificado bit not null default(0)
--)

--insert into ProgramacionSacrificioLucero (Folio,Corral,Lote,Cabezas,ISTDID,LoteID,Fecha,OrganizacionID)
--select 380, 'L01', '3004', 54, 382, 20101, '2015-04-30', 1 union
--select 381, 'L02', '3004', 65, 383, 20102, '2015-04-30', 1 union
--select 382, 'L03', '3004', 65, 384, 20103, '2015-04-30', 1 union
--select 383, 'L04', '3004', 65, 385, 20105, '2015-04-30', 1 union
--select 384, 'L05', '3004', 33, 386, 20107, '2015-04-30', 1

--insert into ProgramacionSacrificioLucero(Folio, Corral, Lote, Cabezas, ISTDID, LoteID, Fecha, OrganizacionID)

-- exec sp_ObtenerLoteSacrificioLucero '2015-05-21', 1


--select
--	FolioTraspaso
--	, InterfaceSalidaTraspasoDetalleID
--	, LoteID
--from
--	InterfaceSalidaTraspaso ist
--	inner join InterfaceSalidaTraspasoDetalle istd on
--		ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID
--where
--	FolioTraspaso in (389,391,390,401,392,393)


GO

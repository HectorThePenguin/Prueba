USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerSacrificios]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[sp_ObtenerSacrificios]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerSacrificios]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ObtenerSacrificios]
	@fechas varchar(400)
	, @organizacionId int
as
begin
	--declare @organizacionId int
	--declare @fechas varchar(400)
	--set @organizacionId = 2
	--set @fechas = '2015-03-25'
	DECLARE @xml xml, @str varchar(100), @delimiter varchar(10)
	SET @delimiter = ','
	SET @xml = cast(('<X>'+replace(@fechas, @delimiter, '</X><X>')+'</X>') as xml)
	SELECT C.value('.', 'varchar(10)') as fecha into #Fechas FROM @xml.nodes('X') as X(C)

	create table #CostosSacrificio
	(
		FechaOperacion smalldatetime
		, tipoGanado VARCHAR(1)
		, Descripcion varchar(50)
		, FolioSalida int
		, lote varchar(10)
		, Cabezas decimal(18,2)
		, Kilos decimal(18,2)
		, Costos decimal(18,2)
	)
	create table #Costos
	(
		FechaOperacion smalldatetime
		, lote varchar(10)
		, Cabezas int
		, PesoNoqueo decimal(18,2)		
		, PesoCanal decimal(18,2)		
		, PesoPiel decimal(18,2)		
		, PesoViscera decimal(18,2)		
		, CostoCanal decimal(18,2)		
		, CostoPiel decimal(18,2)		
		, CostoViscera decimal(18,2)		
	)

	create table #Sacrificio
	(
		NUM_SALI int
		, NUM_CORR VARCHAR(3)
		, NUM_PRO VARCHAR(4)
		, FEC_SACR VARCHAR(10)
		, NUM_CAB int
	)

	if @organizacionId = 1
	begin
		insert into 
			#CostosSacrificio
		SELECT   
			a.FechaOperacion ,  
			tipoGanado,  
			case when indicador=2 then 'Canal'   
			  when indicador=6 then 'Piel'   
			  when indicador=7 then 'Visceras'   
			  when indicador=4 then 'PESO CANAL'   
			end as 'Descripcion',  
			FolioSalida,  
			lote,  
			sum(piezas) Cabezas,  
			sum(kilos) Kilos ,  
			SUM(costo) Costos   
		FROM	
			[srvppisocln].basculas_sl.dbo.CierreSacrificio a  
			inner join [srvppisocln].basculas_sl.dbo.CosteoSacrificio b on 
				a.CierreSacrificioId=b.CierreSacrificioId   
			INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		group by 
			a.FechaOperacion ,tipoGanado,indicador,FolioSalida,lote  
		order by 
			a.FechaOperacion,foliosalida,b.lote  

		insert into 
			#Costos
		select
			canal.FechaOperacion
			, canal.Lote
			, canal.Cabezas
			, noqueo.Peso as PesoNoqueo
			, canal.Kilos as PesoCanal
			, piel.Kilos as PesoPiel
			, isnull(viscera.Kilos,0) as PesoViscera
			, canal.Costos as CostoCanal
			, piel.Costos as CostoPiel
			, isnull(viscera.Costos,0) as CostoViscera
		from
			(select * from #CostosSacrificio where Descripcion = 'Canal') canal inner join
			(select * from #CostosSacrificio where Descripcion = 'Piel') piel on canal.FolioSalida = piel.FolioSalida inner join
			(select * from #CostosSacrificio where Descripcion = 'Visceras') viscera on canal.FolioSalida = viscera.FolioSalida left outer join
			(select Lote_Padre, sum(Cantidad_Primaria) as Peso 
				from [srvppisocln].basculas_sl.dbo.Layout_Subida 
				where Fecha_de_Produccion in (Select Fecha from #Fechas) and Indicador = 1 
				group by Lote_Padre) noqueo on canal.Lote = noqueo.Lote_Padre

		insert into 
			#Sacrificio
		select 
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR, Count(1) as NUM_CAB
		from 
			[srvppisocln].basculas_sl.dbo.salida_sacrificio ss 
			inner join [srvppisocln].basculas_sl.dbo.layout_subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
			inner join #Fechas f on 
				ss.FEC_SACR = f.fecha
		where 
			origen = 'P' 
		group by
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR
			
	end

	if @organizacionId = 2
	begin
		insert into 
			#CostosSacrificio
		SELECT   
			a.FechaOperacion ,  
			tipoGanado,  
			case when indicador=2 then 'Canal'   
			  when indicador=6 then 'Piel'   
			  when indicador=7 then 'Visceras'   
			  when indicador=4 then 'PESO CANAL'   
			end as 'Descripcion',  
			FolioSalida,  
			lote,  
			sum(piezas) Cabezas,  
			sum(kilos) Kilos ,  
			SUM(costo) Costos   
		FROM	
			[srvppisomxli].basculas_sl.dbo.CierreSacrificio a  
			inner join [srvppisomxli].basculas_sl.dbo.CosteoSacrificio b on 
				a.CierreSacrificioId=b.CierreSacrificioId   
			INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		group by 
			a.FechaOperacion ,tipoGanado,indicador,FolioSalida,lote  
		order by 
			a.FechaOperacion,foliosalida,b.lote  

		insert into 
			#Costos
		select
			canal.FechaOperacion
			, canal.Lote
			, canal.Cabezas
			, noqueo.Peso as PesoNoqueo
			, canal.Kilos as PesoCanal
			, piel.Kilos as PesoPiel
			, isnull(viscera.Kilos,0) as PesoViscera
			, canal.Costos as CostoCanal
			, piel.Costos as CostoPiel
			, isnull(viscera.Costos,0) as CostoViscera
		from
			(select * from #CostosSacrificio where Descripcion = 'Canal') canal inner join
			(select * from #CostosSacrificio where Descripcion = 'Piel') piel on canal.FolioSalida = piel.FolioSalida left outer join
			(select * from #CostosSacrificio where Descripcion = 'Visceras') viscera on canal.FolioSalida = viscera.FolioSalida left outer join
			(select Lote_Padre, sum(Cantidad_Primaria) as Peso 
				from [srvppisomxli].basculas_sl.dbo.Layout_Subida 
				where Fecha_de_Produccion in (Select Fecha from #Fechas) and Indicador = 1 
				group by Lote_Padre) noqueo on canal.Lote = noqueo.Lote_Padre

		insert into 
			#Sacrificio
		select 
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR, Count(1) as NUM_CAB
		from 
			[srvppisomxli].basculas_sl.dbo.salida_sacrificio ss 
			inner join [srvppisomxli].basculas_sl.dbo.layout_subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
			inner join #Fechas f on 
				ss.FEC_SACR = f.fecha
		where 
			origen = 'P' 
		group by
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR
	end

	if @organizacionId = 3
	begin
		insert into 
			#CostosSacrificio
		SELECT   
			a.FechaOperacion ,  
			tipoGanado,  
			case when indicador=2 then 'Canal'   
			  when indicador=6 then 'Piel'   
			  when indicador=7 then 'Visceras'   
			  when indicador=4 then 'PESO CANAL'   
			end as 'Descripcion',  
			FolioSalida,  
			lote,  
			sum(piezas) Cabezas,  
			sum(kilos) Kilos ,  
			SUM(costo) Costos   
		FROM	
			[srvppisomty].basculas_sl.dbo.CierreSacrificio a  
			inner join [srvppisomty].basculas_sl.dbo.CosteoSacrificio b on 
				a.CierreSacrificioId=b.CierreSacrificioId   
			INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		group by 
			a.FechaOperacion ,tipoGanado,indicador,FolioSalida,lote  
		order by 
			a.FechaOperacion,foliosalida,b.lote  

		insert into 
			#Costos
		select
			canal.FechaOperacion
			, canal.Lote
			, canal.Cabezas
			, noqueo.Peso as PesoNoqueo
			, canal.Kilos as PesoCanal
			, piel.Kilos as PesoPiel
			, isnull(viscera.Kilos,0) as PesoViscera
			, canal.Costos as CostoCanal
			, piel.Costos as CostoPiel
			, isnull(viscera.Costos,0) as CostoViscera
		from
			(select * from #CostosSacrificio where Descripcion = 'Canal') canal inner join
			(select * from #CostosSacrificio where Descripcion = 'Piel') piel on canal.FolioSalida = piel.FolioSalida inner join
			(select * from #CostosSacrificio where Descripcion = 'Visceras') viscera on canal.FolioSalida = viscera.FolioSalida left outer join
			(select Lote_Padre, sum(Cantidad_Primaria) as Peso 
				from [srvppisomty].basculas_sl.dbo.Layout_Subida 
				where Fecha_de_Produccion in (Select Fecha from #Fechas) and Indicador = 1 
				group by Lote_Padre) noqueo on canal.Lote = noqueo.Lote_Padre

		insert into 
			#Sacrificio
		select 
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR, Count(1) as NUM_CAB
		from 
			[srvppisomty].basculas_sl.dbo.salida_sacrificio ss 
			inner join [srvppisomty].basculas_sl.dbo.layout_subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
			inner join #Fechas f on 
				ss.FEC_SACR = f.fecha
		where 
			origen = 'P' 
		group by
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR
	end

	if @organizacionId = 4
	begin
		insert into 
			#CostosSacrificio
		SELECT   
			a.FechaOperacion,  
			tipoGanado,  
			case when indicador=2 then 'Canal'   
			  when indicador=6 then 'Piel'   
			  when indicador=7 then 'Visceras'   
			  when indicador=4 then 'PESO CANAL'   
			end as 'Descripcion',  
			FolioSalida,  
			lote,  
			sum(piezas) Cabezas,  
			sum(kilos) Kilos ,  
			SUM(costo) Costos   
		FROM	
			[srvppisomon].basculas_sl.dbo.CierreSacrificio a  
			inner join [srvppisomon].basculas_sl.dbo.CosteoSacrificio b on 
				a.CierreSacrificioId=b.CierreSacrificioId   
			INNER JOIN #Fechas F ON a.FechaOperacion = F.fecha
		group by 
			a.FechaOperacion ,tipoGanado,indicador,FolioSalida,lote  
		order by 
			a.FechaOperacion,foliosalida,b.lote  

		insert into 
			#Costos
		select
			canal.FechaOperacion
			, canal.Lote
			, canal.Cabezas
			, noqueo.Peso as PesoNoqueo
			, canal.Kilos as PesoCanal
			, piel.Kilos as PesoPiel
			, isnull(viscera.Kilos,0) as PesoViscera
			, canal.Costos as CostoCanal
			, piel.Costos as CostoPiel
			, isnull(viscera.Costos,0) as CostoViscera
		from
			(select * from #CostosSacrificio where Descripcion = 'Canal') canal inner join
			(select * from #CostosSacrificio where Descripcion = 'Piel') piel on canal.FolioSalida = piel.FolioSalida inner join
			(select * from #CostosSacrificio where Descripcion = 'Visceras') viscera on canal.FolioSalida = viscera.FolioSalida left outer join
			(select Lote_Padre, sum(CAntidad_PRimaria) as Peso 
				from [srvppisomon].basculas_sl.dbo.Layout_Subida 
				where Fecha_de_Produccion in (Select Fecha from #Fechas) and Indicador = 1 
				group by Lote_Padre) noqueo on canal.Lote = noqueo.Lote_Padre

		insert into 
			#Sacrificio
		select 
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR, Count(1) as NUM_CAB
		from 
			[srvppisomon].basculas_sl.dbo.salida_sacrificio ss 
			inner join [srvppisomon].basculas_sl.dbo.layout_subida ls on
				ss.fec_sacr = ls.fecha_de_produccion
				and ss.num_corr + ss.num_pro = ls.lote_padre
				and ls.indicador = 1
			inner join #Fechas f on 
				ss.FEC_SACR = f.fecha
		where 
			origen = 'P' 
		group by
			NUM_SALI, ss.NUM_CORR, NUM_PRO, FEC_SACR
	end

	select 
		0 as LoteID, ss.NUM_SALI as FolioOrdenSacrificio, ss.NUM_CORR as Corral, ss.NUM_PRO as Lote, ss.FEC_SACR as Fecha, cs.CostoCanal as ImporteCanal
		, cs.CostoPiel as ImportePiel, cs.CostoViscera as ImporteVisera, NULL as Serie, NULL as Folio, '' as Observaciones, 1 as Activo, GETDATE() as FechaCreacion
		, 1 as UsuarioCreacionID, NULL as FechaModificacion, NULL as UsuarioModificacion, NULL as ClienteID, ss.NUM_CAB as Cabezas
		, cs.PesoNoqueo, cs.PesoCanal, cs.PesoPiel, cs.PesoViscera
	from 
		#Sacrificio ss 
		inner join #Costos cs on ss.NUM_CORR + ss.NUM_PRO = cs.Lote and ss.FEC_SACR = cs.FechaOperacion

	drop table #Fechas 
	drop table #CostosSacrificio 
	drop table #Costos 
	drop table #Sacrificio

end





GO

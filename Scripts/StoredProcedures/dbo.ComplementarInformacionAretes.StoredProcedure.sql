USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ComplementarInformacionAretes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ComplementarInformacionAretes]
GO
/****** Object:  StoredProcedure [dbo].[ComplementarInformacionAretes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=====================================================================================================================
-- Author     : Ernesto Cardenas LLanes    
-- Create date: 10/04/2015
-- Description: En base a Corral y Cabezas del Sacrificio del SCP se Obtiene informacion de Aretes 
-- SpName     : ComplementarInformacionAretes '<root><Corral><Codigo>L12</Codigo><Cabezas>70</Cabezas></Corral><Corral><Codigo>L13</Codigo><Cabezas>59</Cabezas></Corral><Corral><Codigo>L10</Codigo><Cabezas>61</Cabezas></Corral><Corral><Codigo>L11</Codigo><Cabezas>72</Cabezas></Corral><Corral><Codigo>L14</Codigo><Cabezas>73</Cabezas></Corral></root>'
--=====================================================================================================================
CREATE Procedure [dbo].[ComplementarInformacionAretes]
	@corrales xml
  , @fechaSacrificio datetime
As
Begin

	--declare @corrales xml 
	--set @corrales = '<root><Corral><Codigo>L12</Codigo><Cabezas>70</Cabezas></Corral><Corral><Codigo>L13</Codigo><Cabezas>59</Cabezas></Corral><Corral><Codigo>L10</Codigo><Cabezas>61</Cabezas></Corral><Corral><Codigo>L11</Codigo><Cabezas>72</Cabezas></Corral><Corral><Codigo>L14</Codigo><Cabezas>73</Cabezas></Corral></root>'

	--declare @fechaSacrificio datetime
	--set @fechaSacrificio = '2015-05-15'

	Create --drop
	Table #Corrales
	(	Codigo Varchar(10),
		Cabezas int,
		terminado int 
	)

	Create --drop
	Table #Folio
	(	Corral Varchar(10),
		Folio int,
		Cabezas int,
		Aplicado int,
		FEcha SmallDateTime
	)
	Create --drop
	Table #Resultado
	(	interfaceSalidaTraspasoDetalleId int,
		loteLuceroId int,
		CorralSacrificio Varchar(10),
		Lote Varchar(10),
		Corral Varchar(10),
		LoteSacrificio Varchar(10),
		Cabezas int
	)
	
	Declare @ContCOrrales int, @ContFolio int
	Set @ContCOrrales =0
	Set @ContFolio = 0

	Declare @Codigo Varchar(10), @CorralSacrificio Varchar(5), @Lote Varchar(5), @Corral Varchar(5),@LoteSacrificio Varchar(5)
	Declare @Cabezas int, @CabezasSarificadas int, @terminado Int, @InterfaceSalidaTraspasoDetalleID Int, @LoteLuceroId int,
			@Folio int, @CabezasPorSacrificar int, @cabezasporCorral int
	Declare @Dummy SmallDateTime

	Insert Into #Corrales
	Select l.b.value('Codigo[1]', 'Varchar(5)') As Id
	   , l.b.value('Cabezas[1]', 'INT') as Cabezas
	   , 0
	From @corrales.nodes('/root/Corral') as l(b)    


	select
		ist.FolioTraspaso, istd.InterfaceSalidaTraspasoDetalleID, istd.CabezasPorSacrificar, istd.LoteID
	into --drop table
		#Traspasos
	from
		InterfaceSalidaTraspaso ist
		inner join (select * from InterfaceSalidaTraspasoDetalle where CabezasPorSacrificar is not null And CabezasPorSacrificar > 0)istd on
			ist.InterfaceSalidaTraspasoID = istd.InterfaceSalidaTraspasoID 
		inner join Lote l on
			istd.LoteID = l.LoteID
		inner join Corral c on
			l.CorralID = c.CorralID

	select 
		idCorralEngorda
		, FolioEnvio
	into --drop table
		#Entradas 
	from 
		tblEngEntradas 
	where 
		Fecha = @fechaSacrificio

	While Exists(Select Top 1 Codigo, Cabezas From #Corrales Where terminado=0)
	Begin
		--Set @ContCOrrales = @ContCOrrales+1
		--If @ContCOrrales = 100
		--Begin
		--	Update #Corrales Set Terminado=1
		--	Print 'Terminado por Ciclado #Corrales'
		--End
		
		Select Top 1 @Codigo = Codigo, @Cabezas= Cabezas From #Corrales Where Terminado=0
		Set @cabezasporCorral = 0

		Insert Into #Folio
		Select @Codigo, t.InterfaceSalidaTraspasoDetalleID, t.CabezasPorSacrificar, 0 aplicado, @fechaSacrificio
		From 
			#Entradas ee
			inner join #Traspasos t on
				ee.folioEnvio = t.FolioTraspaso
		where
			ee.idCorralEngorda = @Codigo

		While Exists(Select * From #Folio Where Aplicado=0 and corral = @Codigo)
		Begin
			Select Top 1 @Folio= Folio, @CabezasPorSacrificar = Cabezas, @Dummy= Min(Fecha)
			From #Folio 
			Where Aplicado=0 
			Group By Folio, Cabezas 
			
			--Set @ContFolio = @ContFolio +1
			--If @ContFolio= 100
			--Begin
			--	Update #Folio Set Aplicado = 1
			--	print 'Terminado por Ciclado #Folio'
			--End
			
			Select
				@InterfaceSalidaTraspasoDetalleID = t.InterfaceSalidaTraspasoDetalleID
				, @LoteLuceroId = t.LoteId
				, @CorralSacrificio = ee.idCorralEngorda
				, @Lote= l.Lote
				, @Corral= c.Codigo
				, @LoteSacrificio= RIGHT('00' + CAST(DATEPART(M, @fechaSacrificio) as VARCHAR), 2) +
						RIGHT('00' + CAST(DATEPART(DD, @fechaSacrificio) as VARCHAR), 2)
			From 
				#Entradas ee
				inner join #Traspasos t on
					ee.folioEnvio = t.FolioTraspaso
				inner join Lote l on
					t.LoteID = l.LoteID
				inner join Corral c on
					l.CorralID = c.CorralID
			Where
			 ee.idCorralEngorda = @Codigo
			 And t.InterfaceSalidaTraspasoDetalleID = @Folio
			 
			Insert Into #Resultado Values(@InterfaceSalidaTraspasoDetalleID, @LoteLuceroId, @CorralSacrificio, @Lote, @Corral, @LoteSacrificio, @CabezasPorSacrificar)
			
			Set @cabezasporCorral = @cabezasporCorral + @CabezasPorSacrificar
			
			Update #Folio Set Aplicado = 1 Where Folio = @Folio ANd corral = @Codigo
			Set @Folio= NULL
			Set @CabezasPorSacrificar = NULL
			Set @Dummy= NULL
			
			IF @Cabezas <= @cabezasporCorral
			Begin
				Update #Folio Set Aplicado = 1 Where Corral = @Codigo
				Update #Corrales Set Terminado=1 Where Codigo= @Codigo
			End
		End
		Set @Codigo = Null
		Set @Cabezas= Null
	End

	Select * From #Resultado
End	


/*
Alter Table InterfaceSalidaTraspasoDetalle
Add CabezasPorSacrificar Int
*/


GO

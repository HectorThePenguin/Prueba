USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_AjusteAretesLucero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfazSPI_AjusteAretesLucero]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_AjusteAretesLucero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--====================================================================================================    
-- Author     : Ernesto Cardenas          
-- Create date: 01/08/2014          
-- Description: Procedimiento que realiza el ajuste en el SIAP de los animales sacrificados en el SPI          
--              Replica de InterfazSPI_AjusteAretes      
-- SpName     : Exec [InterfazSPI_AjusteAretesLucero] 2, '2015-03-10'    
--====================================================================================================    
CREATE Procedure [dbo].[InterfazSPI_AjusteAretesLucero]  
 @OrganizacionId Int  
 ,@FechaSacrificio SmallDateTime  
As  
Begin    
 Set NoCount On    
 --declare @fechaSacrificio smalldatetime    
 --declare @organizacionId int    
 --set @fechaSacrificio = '2015-03-11'      
 --set @organizacionId = 1      
 Declare @resultado nVarchar(4000)      
    
 --Identificar aretes duplicados    
 Select    
  a.Arete    
 into    
  #duplicados    
 from    
  Animal a (nolock)          
  inner join AnimalMovimiento am (nolock)          
   on a.AnimalID = am.AnimalID           
   and am.Activo = 1          
  inner join Lote l (nolock)          
   on am.LoteID = l.LoteID           
  inner join Corral c (nolock)          
   on l.CorralID = c.CorralID          
 where          
  am.OrganizacionID = @organizacionId          
  and a.Activo = 1          
 group by          
  a.Arete          
 having           
  Count(1) > 1          
          
 --Obtener Inventario de Aretes          
 select          
  a.AnimalID          
  , cast(a.Arete as bigint) as Arete          
  , am.AnimalMovimientoID          
  , l.CorralID          
  , am.LoteID          
  , am.OrganizacionID          
  , RIGHT('000' + ltrim(rtrim(c.Codigo)), 3) as Corral          
  , RIGHT('0000' + rtrim(ltrim(l.Lote)), 4) as Lote          
  , CASE WHEN d.Arete is null THEN 0 ELSE 1 END as Duplicado          
  , CASE WHEN d.Arete is null THEN 1 ELSE 0 END as ParaAjuste          
 into          
  #inv_aretes          
 from          
  Animal a (nolock)          
  inner join AnimalMovimiento am (nolock)          
   on a.AnimalID = am.AnimalID           
   and am.Activo = 1          
  inner join Lote l (nolock)          
   on am.LoteID = l.LoteID           
  inner join Corral c (nolock)          
   on l.CorralID = c.CorralID          
  left join #duplicados d          
   on a.Arete = d.Arete          
 where          
  am.OrganizacionID = @organizacionId  
  and a.Activo = 1  
 group by          
  a.AnimalID          
  , cast(a.Arete as bigint)          
  , am.AnimalMovimientoID          
  , l.CorralID          
  , am.LoteID          
  , am.OrganizacionID          
  , RIGHT('000' + ltrim(rtrim(c.Codigo)), 3)          
  , RIGHT('0000' + rtrim(ltrim(l.Lote)), 4)          
  , CASE WHEN d.Arete is null THEN 0 ELSE 1 END          
  , CASE WHEN d.Arete is null THEN 1 ELSE 0 END          
    
    
--Select * From #inv_aretes    
--Where LOTE='0007'    
          
 -- Seleccionar de los aretes repetidos cual se tomara para ajuste          
 update          
  #inv_aretes          
 set          
  ParaAjuste = 1          
 where          
  AnimalID in (select min(AnimalID) from #inv_aretes where Duplicado = 1 group by Arete)          
          
 -- Eliminar los aretes repetidos que no se utilizaran para ajuste          
 delete          
  #inv_aretes          
 where          
  ParaAjuste = 0          
          
 -- Obtener los aretes sacrificados          
 Select   
  i.OrganizacionId          
  , RIGHT('000' + ltrim(rtrim(c.Codigo)), 3) as Corral          
  , RIGHT('0000' + ltrim(rtrim(l.Lote)), 4) as Lote          
  , l.LoteId          
  , c.CorralId          
  , Cast(Arete as bigint) as Arete          
 into          
  #interfazSPI          
 from          
  InterfazSPI i (nolock)           
  inner join Lote l (nolock) on           
   RIGHT('0000' + ltrim(rtrim(i.Lote)), 4) = RIGHT('0000' + ltrim(rtrim(l.Lote)), 4) and i.OrganizacionId = l.OrganizacionId          
  inner join Corral c (nolock) on          
   l.CorralId = c.CorralId          
 where          
  i.OrganizacionId = @organizacionId          
  and FechaSacrificio = @fechaSacrificio          
          
 -- Generar conciliacion para validar que los corrales esten completos          
 select          
  c.Codigo as Corral          
  , l.Lote          
  , ISNULL(a.Cabezas, 0) as ControlPiso          
  , ISNULL(b.Cabezas, 0) as SIAP          
  , ISNULL(a.Cabezas, 0) - ISNULL(b.Cabezas, 0) as Faltante          
 into          
  #conciliacion          
 from          
  (select LoteId, Count(1) as Cabezas from #interfazSPI group by LoteId) a          
  left join (select LoteId, Count(1) as Cabezas from #inv_aretes group by LoteId) b on          
   a.LoteId = b.LoteId          
  inner join Lote l on a.LoteId = l.LoteId          
  inner join Corral c on l.CorralId = c.CorralId          
 where           
  case when IsNull(a.Cabezas, 0) > ISNULL(b.Cabezas, 0) then 0 else 1 end = 0          
          
 select @resultado = substring(          
  (select ', Corral ', Corral as [text()], ' con ', cast(Faltante as int) as [text()], ' Cabezas'          
  from #conciliacion          
  for xml path('')), 3, 4000)          
          
 if @resultado is not null          
 begin          
  set @resultado = 'El número de cabezas sacrificadas es mayor al numero de cabezas disponibles en el corral de engorda: ' + @resultado + '.';          
  drop table #duplicados          
  drop table #inv_aretes          
  drop table #interfazSPI          
  drop table #conciliacion          
          
  raiserror (@resultado, 16, 1);          
 end          
 else          
 begin          
  -- Todos los corrales estan completos, se procede al ajuste de aretes          
            
  -- Se obtienen los aretes que estan ubicados en otro corral          
  select          
   a.*          
   , i.CorralID as CorralIDFisico          
   , i.LoteID as LoteIDFisico          
   , i.Corral as CorralFisico          
   , i.Lote as LoteFisico          
  into          
   #ParaSwitch          
  from          
   #inv_aretes a          
   inner join #interfazSPI i           
    on a.Arete = i.Arete           
    and a.LoteId != i.LoteId          
  where          
   a.OrganizacionID = @organizacionId          
          
  declare @animalIdSist int          
  declare @animalIdReal int          
  declare @loteIdSistema int          
  declare @loteIdReal int          
  declare @arete varchar(50)          
  declare @areteImpostor varchar(50)          
          
  print 'Para Ajuste'          
          
  -- Ciclo para ajustar los aretes por sustitucion de uno en uno          
  while exists (select 1 from #ParaSwitch)          
  begin          
   set @animalIdSist = null          
   set @animalIdReal = null          
   set @loteIdSistema = null          
   set @loteIdReal = null          
   set @arete = null          
   set @areteImpostor = null          
          
   -- Obtenemos el arete, el id del animal, su lote en el sistema y el lote real donde deberia estar          
   select top 1            
    @arete = Arete          
    , @animalIdSist = AnimalID          
    , @loteIdSistema = LoteID          
    , @loteIdReal = LoteIDFisico -- select *          
   from          
    #ParaSwitch          
          
   -- Obtener el arete impostor, el id del animal de cualquiera que no deba sacrificarse del lote real          
   select top 1           
    @areteImpostor = inv.Arete          
    , @animalIdReal = inv.AnimalID --select inv.arete, inv.animalid          
   from          
    #inv_aretes inv          
    left outer join #interfazSPI isp on          
     inv.Arete = isp.Arete           
   where          
    inv.LoteID = @loteIdReal          
    and isp.Arete is null          
          
   if @areteImpostor is not null          
   begin          
    --Mover el arete impostor al animal del lote de sistema que tiene el arete          
    update           
     #inv_aretes           
    set           
     Arete = @areteImpostor          
    where           
     AnimalID = @animalIdSist          
          
    --Mover el arete impostor al animal del lote de sistema que tiene el arete          
    update           
     #inv_aretes           
    set           
     Arete = @arete          
    where           
     AnimalID = @animalIdReal          
   end          
   else          
   begin          
          
    select @arete arete, @areteimpostor impostor, @loteIdSistema loteSistema, @loteIdReal loteReal          
          
    select * from #ParaSwitch          
          
    select * from #InterfazSPI           
          
    select * from #inv_aretes          
          
    RAISERROR ('trono', 16, 1)          
   end          
   delete #ParaSwitch where Arete = @arete          
  end          
          
  print 'Para Reemplazo'          
          
  -- Se obtienen todos los aretes que no se sacrificaran para ser reemplazados.          
  select           
   i.Arete          
   , c.CorralID as CorralIDFisico          
   , l.LoteID as LoteIDFisico          
   , i.Corral as CorralFisico          
   , i.Lote as LoteFisico          
  into          
   #ParaReemplazo          
  from          
   #interfazSPI i          
   left outer join #inv_aretes a          
    on i.arete = a.arete           
   left join Lote l (nolock)          
    on RIGHT('0000' + ltrim(rtrim(l.Lote)), 4) = i.Lote           
    and l.OrganizacionID = i.OrganizacionID          
   left join Corral c (nolock)          
    on l.CorralId = c.CorralId          
  where          
   i.OrganizacionID = @organizacionId          
   and a.Arete is null          
           
  -- Ciclo para ejecutar el ajuste por reemplazo de uno en uno          
  while exists (select 1 from #ParaReemplazo)          
  begin          
   set @animalIdSist = null          
   set @animalIdReal = null          
   set @loteIdSistema = null          
   set @loteIdReal = null          
   set @arete = null          
   set @areteImpostor = null          
          
   -- Obtener el arete y el id del lote real          
   select top 1          
    @arete = Arete          
    , @loteIdReal = LoteIDFisico          
   from          
    #ParaReemplazo          
           
   -- Obtener el arete impostor, el id del animal al cual se le reemplazara por el arete real y que pertenezca al lote eral          
   select top 1           
    @animalIdReal = inv.AnimalID          
   from          
    #inv_aretes inv          
    left outer join #interfazSPI spi          
     on inv.Arete = spi.Arete          
   where          
    inv.LoteID = @loteIdReal          
    and spi.Arete is null          
          
   -- Reemplazar el arete elegido por el arete sacrificado          
   update           
    #inv_aretes          
   set           
    Arete = @arete          
   where           
    AnimalID = @animalIdReal          
          
   delete #ParaReemplazo where Arete = @arete          
  end          
   /*      
  drop table #duplicados          
  drop table #ParaSwitch          
  drop table #ParaReemplazo          
  drop table #conciliacion          
    */      
  --Actualizar la informacion en la tabla animal          
  update           
   a          
  set           
   arete = b.Arete          
  from           
   animal a          
   inner join #inv_aretes b on           
    a.AnimalID = b.AnimalID          
    and a.Arete != b.Arete          
          
  -- Marcar aretes como procesados          
  UPDATE spi          
  SET Procesado = 1          
  FROM           
   InterfazSPI spi          
   INNER JOIN #inv_aretes (NOLOCK) a ON           
    spi.Lote = a.Lote          
    and cast(spi.arete as bigint) = a.Arete          
    and spi.OrganizacionId = a.OrganizacionID          
  WHERE           
   spi.Procesado = 0          
   AND spi.OrganizacionId = @OrganizacionId          
   AND spi.FechaSacrificio = @FechaSacrificio          
          
  /*      
  DROP TABLE #interfazSPI          
  DROP TABLE #inv_aretes          
  DROP TABLE #Costeo          
  DROP TABLE #Cuentas          
  */      
 END          
 SET NOCOUNT OFF          
END
GO

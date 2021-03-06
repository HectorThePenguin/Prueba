USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_CostoIntensivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfazSPI_CostoIntensivo]
GO
/****** Object:  StoredProcedure [dbo].[InterfazSPI_CostoIntensivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--========================================================================================================================  
-- Author     : Ernesto Cardenas LLanes        
-- Create date: 12/03/2015        
-- Description: Procedimiento que realiza el Costeo de salidas de sacrificio Intensivas de Lucero        
-- SpName     : InterfazSPI_CostoIntensivo 2, '2015-03-10', '<root><lote><id>7527</id><cabezas>71</cabezas></lote></root>'        
--========================================================================================================================  
CREATE Procedure [dbo].[InterfazSPI_CostoIntensivo]  
  @OrganizacionId  Int  
 ,@FechaSacrificio SmallDateTime  
 ,@ordenesid    xml  
As  
Begin  
        
 Declare @Fecha DateTime      
 Declare @costo Table(CodigoCosto int, FOLIO Varchar(6), IMPORTE Numeric(18,2), Codigo Varchar(3), LoteId int, CuentaContable Varchar(50))        
 Declare @Cuentas Table (CTA_CON Varchar(50), CostoID int)        
 Set @Fecha = DateAdd(d, 0, DateDiff(d, 0, GetDate()))        
         
 Declare @InterfazSPI Table(LoteId Int, FolioSalida Int)        
         
         
 Declare @ordenes Table          
 ( id Int, cabezas INT, ejecutado bit      
 )          
            
 Insert Into @ordenes (id, cabezas, ejecutado)          
 Select        
   l.b.value('id[1]', 'INT') as Id          
   , l.b.value('cabezas[1]', 'INT') as Cabezas      
   , 0      
 From @ordenesid.nodes('/root/lote') as l(b)        
        
        
 --Obteniendo los lotes involucrados en el cierre de sacrigficio para ganado intensivo        
 Insert Into @InterfazSPI        
 Select Distinct ord.id as LoteID, 0        
 From  @ordenes ord        
        
 Insert Into @Cuentas        
 Select cv.Valor + cs.ClaveContable As CTA_CON, cs.CostoID        
 From CuentaValor cv (NoLock)        
 Inner Join Cuenta c (NoLock) On cv.CuentaID = c.CuentaID        
 Cross Join Costo cs (NoLock)        
 Where c.ClaveCuenta = 'CTACTOGAN'        
   And cv.OrganizacionID = @OrganizacionID        
        
create table #Costos (      
 CostoCorralId int      
 , CodigoCosto varchar(2)      
 , TipoMovimiento varchar(2)      
 , Folio varchar(6)      
 , Fecha smalldatetime      
 , Importe decimal(18,2)      
 , Corral varchar(3)      
 , Proceso varchar(4)      
 , CuentaContable varchar(10)      
 , Num_Lin varchar(1)      
 , IMPORTE_A decimal(18,2)      
 , Fec_Act smalldatetime      
)      
      
declare @fechatxt varchar(10)      
declare @hoytxt varchar(10)      
      
set @fechatxt = convert(nvarchar(10), @FechaSacrificio, 120)      
set @hoytxt = convert(nvarchar(10), @Fecha, 120)      
      
 while exists(select top 1 * from @ordenes where ejecutado = 0)      
 begin      
  
 declare @loteid int      
 declare @top int      
      
 select top 1  @loteid = id, @top = cabezas from @ordenes where ejecutado = 0      
      
 declare @sql nvarchar(max)      
      
 set @sql  = '      
  Select top ' + cast(@top as nvarchar) +  '      
   0 As CostoCorralId        
   , cast(acsto.CostoID As VarChar(2)) As CodigoCosto     
   , ''51'' AS TipoMovimiento     
   , ''0'' As Folio        
   , ''' + @fechatxt  + ''' AS Fecha        
   , Sum(acsto.Importe)  As Importe        
   , c.Codigo as Corral        
   , l.Lote as Proceso         
   , acsto.CostoID As CuentaContable        
   , ''0'' AS Num_Lin        
   , Sum(Importe) AS IMPORTE_A        
   , ''' +@hoytxt  + '''  AS Fec_Act        
  From Lote l (NoLock)    
  Inner Join interfazSPI  ispi (Nolock) On ispi.lote = l.Lote And ispi.OrganizacionId= l.OrganizacionID    
  Inner Join Animal          a (NoLock) On a.Arete = ispi.Arete And a.OrganizacionIDEntrada =  ispi.OrganizacionId    
  Inner Join AnimalCosto acsto(NoLock)On acsto.AnimalID = a.AnimalID  and a.Activo = 1      
  Inner Join Corral c (NoLock) On l.CorralID = c.CorralID    
  Where l.LoteID = '+ Cast(@loteid as Varchar(10))+'    
   And ispi.FechaSacrificio = ''' + @fechatxt +'''    
   And ispi.OrganizacionId = ' + Cast(@OrganizacionId as Varchar(3))+'    
  Group By acsto.CostoID, acsto.CostoID, c.Codigo, l.lote    
  ';      
  
  
  
 insert into #Costos      
 exec sp_executesql @sql      
  update @ordenes set ejecutado = 1 where id = @loteid and cabezas = @top      
end       
      
update a      
set       
 a.CuentaContable = b.CTA_CON      
from       
 #Costos a      
 inner join @Cuentas b on a.CuentaContable = b.CostoID      
    
select * from #Costos    
End    
    
GO

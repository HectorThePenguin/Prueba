USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioDetalle]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioDetalle]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioDetalle]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================        
-- Author: Ernesto Cardenas LLanes      
-- Create date: <18 Ago 2014>      
-- Description: <Se guarda en la tabla LoteSacrificioDetalle en base a los aretes registrados en la tabla InterfazSPI>      
--               Replica de LoteSacrificioDetalle_GuardarLoteSacrificioDetalle       
/*        
declare @res varchar(800)        
exec [LoteSacrificioLuceroDetalle_GuardarLoteSacrificioDetalle] '2015-03-10', '<root><loteSacrificio><loteSacrificioID>170</loteSacrificioID></loteSacrificio></root>', 2, @res out    
select @res        
select * from interfazspi where fechaSacrificio = '2015-03-03' and organizacionid = 2        
*/  
-- =============================================    
CREATE Procedure [dbo].[LoteSacrificioLuceroDetalle_GuardarLoteSacrificioDetalle]    
@fechaSacrificio dateTime    
,@sacrificioId xml    
, @organizacion int    
, @resultado varchar(800) out        
As        
Begin    
 Set NoCount On;    
  
 Declare @LoteSacrificio Table    
 ( id Int    
 )    
     
 Insert Into @LoteSacrificio (id)    
 Select    
  l.b.value('loteSacrificioID[1]', 'INT') as Id    
 From    
  @sacrificioId.nodes('/root/loteSacrificio') as l(b)    
      
  --select * from @LoteSacrificio    
      
  Begin Try    
  Select    
   ls.LoteSacrificioID        
   , ls.LoteID        
   , c.CorralID        
   , l.OrganizacionID        
   , RIGHT('000' + LTRIM(RTRIM(c.Codigo)), 3) as Corral        
   , RIGHT('0000' + LTRIM(RTRIM(l.Lote)), 4) as Lote        
  Into #LoteCorral    
  From LoteSacrificioLucero ls    
  Inner Join @LoteSacrificio vls On vls.id = ls.LoteSacrificioID    
  Inner Join Lote l ON ls.LoteID = l.LoteID    
  Inner Join Corral c ON c.CorralID = l.CorralID and l.OrganizacionID = c.OrganizacionID  
  Where 1=1    
    And  Fecha = @fechaSacrificio    
    And l.OrganizacionID = @organizacion    
    And ls.Activo = 1    
    --And ls.LoteSacrificioID = 169  
    
    
  --Select * From #LoteCorral    
    
  Select    
   lc.*    
   , Cast(ispi.Arete As BigInt) AS Arete    
  Into #Aretes    
  From #LoteCorral lc    
  Inner Join InterfazSPI ispi ON lc.Corral = RIGHT('000' + LTRIM(RTRIM(ispi.Corral)), 3)         
    And lc.Lote = RIGHT('0000' + LTRIM(RTRIM(ispi.Lote)), 4)        
    And lc.OrganizacionID = ispi.OrganizacionId        
  Where ispi.FechaSacrificio = @fechaSacrificio  
  
  --Select * From #Aretes  
  
  Select  
   a.LoteSacrificioID        
   , al.AnimalID        
  INTO        
   #Detalle        
  FROM         
   #Aretes a  
   INNER JOIN Animal al (NOLOCK) ON         
    a.Arete = CAST(al.Arete as BIGINT)        
    and a.OrganizacionID = al.OrganizacionIDEntrada        
   INNER JOIN AnimalMovimiento aml (NOLOCK) ON        
    al.AnimalID = aml.AnimalID         
    and a.LoteID = aml.LoteID         
    and a.CorralID = aml.CorralID        
    and a.OrganizacionID = aml.OrganizacionID         
    and aml.Activo = 1         
  GROUP BY        
   a.LoteSacrificioID        
   , al.AnimalID      
     
  --Select  
  --  al.AnimalID  
  --  , al.Arete  
  --From  
  -- Animal al (NoLock)  
  --  --a.Arete = CAST(al.Arete as BIGINT)  
  --  --and a.OrganizacionID = al.OrganizacionIDEntrada  
  -- Inner Join AnimalMovimiento aml (NoLock) On  
  --  al.AnimalID = aml.AnimalID  
  --  and aml.Activo = 1  
  --Where al.OrganizacionIDEntrada = 2  
  --  and aml.LoteID =  7527  
  --  and aml.CorralID= 1453  
  --  and aml.OrganizacionID= 2  
  --Group By  
  -- al.AnimalID  
  --  , al.Arete    
  
   --Select * from #Detalle  
   
  INSERT INTO         
   LoteSacrificioLuceroDetalle        
  SELECT DISTINCT        
   LoteSacrificioID        
   , AnimalID        
  from         
   #Detalle d  
  Where not exists(Select 'S'  
       From LoteSacrificioLuceroDetalle lsld  
       Where lsld.AnimalID = d.AnimalID)  
     
           
  SELECT @resultado = 'OK'        
 END TRY        
 BEGIN CATCH        
  SELECT @resultado = ERROR_MESSAGE()        
 END CATCH        
END
GO

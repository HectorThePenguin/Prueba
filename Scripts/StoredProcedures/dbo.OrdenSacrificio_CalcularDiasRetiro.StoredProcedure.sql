USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CalcularDiasRetiro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_CalcularDiasRetiro]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_CalcularDiasRetiro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[OrdenSacrificio_CalcularDiasRetiro] @LoteId INT                                                          
 ,@TipoFinalizacion INT                                                          
AS                                                          
BEGIN                                                          
CREATE TABLE #DIASFORMULA (                                                          
  FormulaID INT                                                          
  ,Fecha DATE                                                          
  )                                                          
 DECLARE @FechaUltimaFormulaDiferente DATE                                                          
 SET @FechaUltimaFormulaDiferente = (                                                          
   SELECT TOP 1 CAST(ac.Fecha AS DATE) AS Fecha                                                          
   FROM AnimalConsumo ac                                                          
   INNER JOIN AnimalMovimiento am ON ac.AnimalID = am.AnimalID                                                          
   INNER JOIN Formula fo ON ac.FormulaIDServida = fo.FormulaID                                                          
   WHERE am.Activo = 1                                                          
   AND cast(am.FechaMovimiento AS DATE) <= (GETDATE() - 3)                                                          
   AND am.LoteID = @LoteId                                                          
   AND fo.TipoFormulaID <> @TipoFinalizacion                                                          
   ORDER BY ac.Fecha DESC                                                          
   )                                                          
 INSERT INTO #DIASFORMULA                                                          
 SELECT ac.FormulaIDServida                                                          
  ,ac.Fecha                                                          
 FROM AnimalConsumo ac                                                          
 INNER JOIN AnimalMovimiento am ON ac.AnimalID = am.AnimalID                                                          
 INNER JOIN Formula fo ON ac.FormulaIDServida = fo.FormulaID                                                          
 WHERE am.Activo = 1                                                          
  AND am.LoteID = @LoteId                                                          
  AND cast(am.FechaMovimiento AS DATE) <= (GETDATE() - 3)                                                          
  AND fo.TipoFormulaID = @TipoFinalizacion                                                          
  AND CAST(ac.Fecha AS DATE) >= @FechaUltimaFormulaDiferente                                                          
  AND CAST(ac.Fecha AS DATE) <= CAST(getdate() AS DATE)                                                          
 GROUP BY ac.FormulaIDServida                                                          
  ,ac.Fecha                                                          
 IF   @LoteID = 46345              
or @LoteID =46155    
or @LoteID =46160              
or @LoteID =46347              
or @LoteID =48613            
or @LoteID =48615            
or @LoteID =49290            
or @LoteID =49271            
or @LoteID =49270            
or @LoteID =49272            
or @LoteID =49480            
or @LoteID =49348    or @LoteID =49351      
or @LoteID =49086      
or @LoteID =49352      
or @LoteID =49353      
or @LoteID =49356      
or @LoteID =49354      
or @LoteID =49357      
or @LoteID =49359      
or @LoteID =49483      
or @LoteID =49347      
or @LoteID =49355      
or @LoteID =49349      
or @LoteID =49350                 
OR @LoteID = 49695            
OR @LoteID = 49710            
OR @LoteID = 49712      
OR @LoteID = 49696      
OR @LoteID =49697      
OR @LoteID =49698      
OR @LoteID =49699      
OR @LoteID =49705      
OR @LoteID =49706      
OR @LoteID =49707      
OR @LoteID =49709      
OR @LoteID =49960                 
or @LoteID =48523     
or @LoteID =48534    
or @LoteID =49376    
or @LoteID =48492    
or @LoteID =48325    
or @LoteID =48346    
or @LoteID =48878    
or @LoteID =48879    
or @LoteID =49120    
OR @LoteID =49709    
OR @LoteID =49935    
OR @LoteID =49946    
OR @LoteID =49954    
OR @LoteID =49956    
OR @LoteID =49957    
OR @LoteID =49958    
OR @LoteID =49959    
OR @LoteID =49963    
OR @LoteID =49934  

or @LoteID =50531
or @LoteID =49594    
or @LoteID =49604              
or @LoteID =51183              
or @LoteID =49608            
or @LoteID =49609
                 
  BEGIN                                                          
  SELECT 5 AS DiasRetiro                                                          
 END                          
 ELSE                                            
  SELECT Count(*) AS DiasRetiro                               
  FROM #DIASFORMULA                                                          
  GROUP BY FormulaID                                                          
 DROP TABLE #DIASFORMULA                       
END 
GO

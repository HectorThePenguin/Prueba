USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PolizasIncorrectas_S]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PolizasIncorrectas_S]
GO
/****** Object:  StoredProcedure [dbo].[PolizasIncorrectas_S]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[PolizasIncorrectas_S]   
(        
@TipoPolizaID INT        
)        
AS         
BEGIN        
SELECT         
po.OrganizacionID        
,po.FolioMovto        
,po.FechaDocto        
,po.Concepto        
,po.Ref3        
,po.Cargos        
,po.Abonos        
,po.DocumentoSAP        
,po.Procesada        
,po.Mensaje        
,po.PolizaID        
,po.TipoPolizaID    
INTO #TablaPendientes      
FROM PolizasCorrectas pc      
RIGHT JOIN PolizasIncorrectas po       
 on po.FolioMovto = pc.foliomovto       
 and po.cargos = pc.cargos       
 and po.abonos = pc.abonos      
where pc.polizaid is null      
 and  po.TipoPolizaID = @TipoPolizaID      
ORDER BY po.foliomovto      
        
        
  SELECT   
  T.OrganizacionID        
 ,T.FolioMovto        
 ,T.FechaDocto        
 ,T.Concepto        
 ,T.Ref3        
 ,T.Cargos        
 ,T.Abonos        
 ,T.DocumentoSAP        
 ,T.Procesada        
 ,T.Mensaje        
 ,T.PolizaID        
 ,T.TipoPolizaID   
 ,TP.Descripcion as 'TipoPoliza'  
  FROM   
  #TablaPendientes T  
  INNER JOIN TipoPoliza TP  
  ON T.TipoPolizaID = TP.TipoPolizaID   
    
        
END 
GO

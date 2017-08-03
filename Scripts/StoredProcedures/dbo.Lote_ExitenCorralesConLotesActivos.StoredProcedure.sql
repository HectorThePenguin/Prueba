-- =================================================================================================  
-- Autor  : Sergio Gamez Gomez  
-- Fecha  : 2015/09/15  
-- Descripcion : Consular si un corral tiene lotes activos, obteniendo el corral a raiz de un lote  
-- Origen  : SIAP  
-- EXEC Lote_ExitenCorralesConLotesActivos 1,'<ROOT><Lotes><LoteID>51977</LoteID></Lotes></ROOT>'  
-- =================================================================================================  
alter PROCEDURE [dbo].[Lote_ExitenCorralesConLotesActivos]  
    @OrganizacionID INT,  
 @Xml XML   
AS  
BEGIN  
 SET NOCOUNT ON  
 SELECT    
  LoteID = T.item.value('./LoteID[1]', 'INT')  
 INTO #Lotes   
 FROM  @Xml.nodes('ROOT/Lotes') AS T(item)
 
 SELECT   
  DISTINCT C.Codigo  
 FROM #Lotes OSD (NOLOCK)  
 INNER JOIN  Lote L (NOLOCK)  
  ON L.LoteId = OSD.LoteId AND L.OrganizacionId = @OrganizacionID  
 INNER JOIN  Corral C (NOLOCK)  
  ON C.CorralId = L.CorralId  
 INNER JOIN  Lote L2 (NOLOCK)  
  ON L2.CorralId = C.CorralId AND L2.OrganizacionId = @OrganizacionID AND l2.LoteID != OSD.LoteID  
 WHERE L2.Activo = 1 
 
 SET NOCOUNT OFF  
END  
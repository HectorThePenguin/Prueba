IF object_id('[dbo].[ResumenSacrificio_ObtenerPorFecha]','P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[ResumenSacrificio_ObtenerPorFecha]
END
GO
CREATE PROCEDURE [dbo].[ResumenSacrificio_ObtenerPorFecha]        
 @Sacrificio XML
AS
BEGIN
 SET NOCOUNT ON

 SELECT            
  FEC_SACR = t.item.value('./FEC_SACR[1]', 'VARCHAR(30)'),        
  NUM_CORR = t.item.value('./NUM_CORR[1]', 'VARCHAR(3)'),        
  NUM_PRO = t.item.value('./NUM_PRO[1]', 'VARCHAR(4)'),        
  TAG_ARETE = t.item.value('./TAG_ARETE[1]', 'BIGINT'),        
  TIPO_DE_GANADO = t.item.value('./TIPO_DE_GANADO[1]', 'INT'),        
  LOTEID = t.item.value('./LOTEID[1]', 'INT'),        
  ANIMALID = t.item.value('./ANIMALID[1]', 'BIGINT'),        
  NUM_CORR_INNOVA = t.item.value('./NUM_CORR_INNOVA[1]', 'VARCHAR(3)'),        
  PO_INNOVA = t.item.value('./PO_INNOVA[1]', 'VARCHAR(30)'),        
  Consecutivo_Sacrificio = t.item.value('./Consecutivo_Sacrificio[1]', 'INT'),        
  Indicador_Noqueo = t.item.value('./Indicador_Noqueo[1]', 'BIT'),        
  Indicador_Piel_Sangre = t.item.value('./Indicador_Piel_Sangre[1]', 'BIT'),        
  Indicador_Piel_Descarnada = t.item.value('./Indicador_Piel_Descarnada[1]', 'BIT'),        
  Indicador_Viscera = t.item.value('./Indicador_Viscera[1]', 'BIT'),        
  Indicador_Inspeccion = t.item.value('./Indicador_Inspeccion[1]', 'BIT'),        
  Indicador_Canal_Completa = t.item.value('./Indicador_Canal_Completa[1]', 'BIT'),        
  Indicador_Canal_Caliente = t.item.value('./Indicador_Canal_Caliente[1]', 'BIT')
 INTO #Sacrificio        
 FROM @Sacrificio.nodes('ROOT/Sacrificio') AS T(item)          

 DECLARE @ORG INT
 
 SELECT TOP 1
	@ORG = L.OrganizacionID
 FROM
	#Sacrificio S
	INNER JOIN Lote L (NOLOCK) ON
		S.LOTEID = L.LoteID
 WHERE
	S.LOTEID IS NOT NULL
        
 SELECT        
  S.FEC_SACR,        
  S.NUM_CORR,    
  S.NUM_PRO,        
  S.TAG_ARETE,        
  TG.Descripcion TIPO_DE_GANADO,    
  TG.TipoGanadoID,         
  S.LOTEID,        
  S.ANIMALID,        
  S.NUM_CORR_INNOVA,        
  S.PO_INNOVA,        
  S.Consecutivo_Sacrificio,        
  S.Indicador_Noqueo,        
  S.Indicador_Piel_Sangre,        
  S.Indicador_Piel_Descarnada,        
  S.Indicador_Viscera,        
  S.Indicador_Inspeccion,        
  S.Indicador_Canal_Completa,        
  S.Indicador_Canal_Caliente,        
  AM.LoteID LoteIdSiap,        
  AM.Lote,        
  AM.Codigo,    
  am.AnimalID AnimalIdSiap    
 FROM         
  #Sacrificio S (NOLOCK)         
  LEFT OUTER JOIN (        
    select         
     am.AnimalID        
     , am.LoteID        
     , l.Lote        
     , c.Codigo        
    from         
     AnimalMovimiento AM (NOLOCK)
     INNER JOIN Lote L (NOLOCK) ON         
      L.LoteID = AM.LoteID        
     INNER JOIN Corral C (NOLOCK) ON         
      C.CorralID = L.CorralID        
    where        
     AM.Activo = 1        
     AND AM.OrganizacionID = @ORG
   ) AM ON         
   AM.AnimalID = S.AnimalID        
  INNER JOIN TipoGanado TG(NOLOCK) ON         
   TG.TipoGanadoID = S.TIPO_DE_GANADO
  Order By Consecutivo_Sacrificio 
        
 SET NOCOUNT OFF          
END
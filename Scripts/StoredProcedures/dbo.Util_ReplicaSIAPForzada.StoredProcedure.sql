USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Util_ReplicaSIAPForzada]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Util_ReplicaSIAPForzada]
GO
/****** Object:  StoredProcedure [dbo].[Util_ReplicaSIAPForzada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================            
-- Author     : César Augusto Villa Bojórquez            
-- Create date: 06/12/2014            
-- Description: Replica Salidas de Centros a SIAP           
-- [[Util_ReplicaSIAPForzada]]             
--=============================================            
CREATE PROCEDURE [dbo].[Util_ReplicaSIAPForzada]              
AS            
BEGIN            
 SET NOCOUNT ON;           
       
       
 DECLARE @COUNT INT      
 SET @COUNT=(Select Count(*) From dbo.Util_InterfaceSalida_Correcta   Where Valido='OK')      
       
 IF @COUNT>0      
 BEGIN      
       
   BEGIN TRAN      
          
     INSERT INTO InterfaceSalida      
     SELECT IO.OrganizacionID, I.IdSalida, IOG.OrganizacionID, I.FechaHora, I.esRuteo, I.Cabezas, 1, I.FechaRegistro, I.UsuarioRegistro      
     FROM dbo.Util_InterfaceSalida I      
     INNER JOIN InterfaceOrganizacion IO       
      ON I.IdCentro = IO.CodigoRelacion AND IO.TipoSistema = 'C'      
     INNER JOIN InterfaceOrganizacion IOG       
      ON I.IdGanadera = IOG.CodigoRelacion AND IOG.TipoSistema = 'G'      
      
     INSERT INTO InterfaceSalidaDetalle      
     SELECT IO.OrganizacionID, I.IdSalida, ISA.IdTipoGanado, COUNT(ISA.numArete), ROUND(AVG(ISC.Importe/ISA.PesoCompra),0),      
     SUM(ISC.Importe), I.FechaRegistro, I.UsuarioRegistro      
     FROM Util_InterfaceSalida I      
     INNER JOIN Util_InterfaceSalidaAnimal ISA       
      ON I.IdCentro = ISA.IdCentro AND I.IdSalida = ISA.IdSalida       
     INNER JOIN Util_InterfaceSalidaCosto ISC      
      ON ISC.IdCentro = ISA.IdCentro AND ISC.IdSalida = ISA.IdSalida AND ISC.numArete = ISA.numArete AND ISC.FechaCompra = ISA.FechaCompra AND ISC.IdCosto = 26      
     INNER JOIN InterfaceOrganizacion IO       
      ON I.IdCentro = IO.CodigoRelacion AND IO.TipoSistema = 'C'      
     INNER JOIN InterfaceOrganizacion IOG       
      ON I.IdGanadera = IOG.CodigoRelacion AND IOG.TipoSistema = 'G'      
     GROUP BY IO.OrganizacionID, I.IdSalida, ISA.IdTipoGanado, I.FechaRegistro, I.UsuarioRegistro      
      

     INSERT INTO InterfaceSalidaAnimal      
     SELECT IO.OrganizacionID, I.IdSalida, ISA.numArete, ISA.FechaCompra, ISA.PesoCompra, ISA.IdTipoGanado, ISA.pesoEntrada, I.FechaRegistro,       
     I.UsuarioRegistro,Null,0     --validar Arete Metalico  y AnimalID
     FROM Util_InterfaceSalida I      
     INNER JOIN Util_InterfaceSalidaAnimal ISA       
      ON I.IdCentro = ISA.IdCentro AND I.IdSalida = ISA.IdSalida       
     INNER JOIN InterfaceOrganizacion IO       
      ON I.IdCentro = IO.CodigoRelacion AND IO.TipoSistema = 'C'      
     INNER JOIN InterfaceOrganizacion IOG       
      ON I.IdGanadera = IOG.CodigoRelacion AND IOG.TipoSistema = 'G'      
      
     INSERT INTO  InterfaceSalidaCosto      
     SELECT IO.OrganizacionID, I.IdSalida, ISA.numArete, ISA.fechaCompra, IC.CostoID, ISC.Importe, I.FechaRegistro, I.UsuarioRegistro      
     FROM Util_InterfaceSalida I      
     INNER JOIN Util_InterfaceSalidaAnimal ISA       
      ON I.IdCentro = ISA.IdCentro AND I.IdSalida = ISA.IdSalida       
     INNER JOIN Util_InterfaceSalidaCosto ISC      
      ON ISC.IdCentro = ISA.IdCentro AND ISC.IdSalida = ISA.IdSalida AND ISC.numArete = ISA.numArete AND ISC.FechaCompra = ISA.FechaCompra      
     INNER JOIN InterfaceOrganizacion IO       
      ON I.IdCentro = IO.CodigoRelacion AND IO.TipoSistema = 'C'      
     INNER JOIN InterfaceOrganizacion IOG       
      ON I.IdGanadera = IOG.CodigoRelacion AND IOG.TipoSistema = 'G'      
     INNER JOIN InterfaceCosto IC       
      ON ISC.IdCosto = IC.CodigoRelacion AND IC.TipoSistema = 'C'      
                 
   IF @@ERROR <> 0 BEGIN                            
        ROLLBACK TRAN                           
        END       
       ELSE       
        BEGIN                            
         COMMIT TRAN                           
        END                            
      
    Update a    
  Set a.statusReplica=1    
  --Select *     
  From [bld-gviz05].centro_gviz.dbo.SalidaGanado a,      (SELECT I.OrganizacionDestinoID,IO.CodigoRelacion,SalidaID    
    FROM InterfaceSalida I  (nolock)    
    INNER JOIN InterfaceOrganizacion IO    (nolock)    
     ON I.OrganizacionID = IO.OrganizacionID AND IO.TipoSistema = 'C'      
    INNER JOIN InterfaceOrganizacion IOG    (nolock)    
     ON I.OrganizacionDestinoID = IOG.CodigoRelacion AND IOG.TipoSistema = 'G' ) b,Util_InterfaceSalida(nolock) c    
  Where --a.IdGanadera= b.OrganizacionDestinoID and     
  a.IdCentro=b.CodigoRelacion    and a.IdSalida=b.SalidaID and b.CodigoRelacion=c.IdCentro and b.SalidaId=c.IdSalida    
  and a.statusReplica=0 and Convert(varchar(8),a.FechaReg,112)>='20141101'        
      
    If Exists (Select * From sysobjects Where name='Util_InterfaceDesnormalizada')            
    Drop Table Util_InterfaceDesnormalizada              
                     
    If Exists (Select * From sysobjects Where name='Util_InterfaceSalida')            
    Drop Table Util_InterfaceSalida           
                 
    If Exists (Select * From sysobjects Where name='Util_InterfaceSalidaDetalle')            
    Drop Table Util_InterfaceSalidaDetalle           
                 
    If Exists (Select * From sysobjects Where name='Util_InterfaceSalidaAnimal')            
    Drop Table Util_InterfaceSalidaAnimal            
                        
    If Exists (Select * From sysobjects Where name='Util_InterfaceSalidaCosto')            
    Drop Table Util_InterfaceSalidaCosto           
                 
    If Exists (Select * From sysobjects Where name='Util_InterfaceSalida_Correcta')            
    Drop Table Util_InterfaceSalida_Correcta         
          
 END         
        
 SET NOCOUNT OFF;            
END 
GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Util_ReplicaCentroToSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Util_ReplicaCentroToSIAP]
GO
/****** Object:  StoredProcedure [dbo].[Util_ReplicaCentroToSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
--Exec dbo.Util_ReplicaCentroToSIAP 9,909  
  
--sp_helptext Util_ReplicaCentroToSIAP  
  
  
--=============================================      
-- Author     : César Augusto Villa Bojórquez      
-- Create date: 05/12/2014      
-- Description: Replica Salidas de Centros a SIAP     
-- [Util_ReplicaCentroToSIAP]       
--=============================================      
CREATE PROCEDURE [dbo].[Util_ReplicaCentroToSIAP]      
 @Centro char(4),      
 @Salida char(8)    
AS      
BEGIN      
 SET NOCOUNT ON;      
    
    
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
        
      --Declare @Centro char(4)     
      --Declare @Salida char(8)    
          
      --Set @Centro=9    
      --Set @Salida=909    
          
      Declare @Sentencia as varchar(8000)              
      Set @Sentencia='                  
          SELECT     
    SG.IdGanadera, SG.IdCentro, SG.IdSalida, SG.FechaHora, SG.esRuteo, MG.IdTipoGanado AS IdTipoGanadoGrupo,       
    MG.numAnimales, MG.pesoEntrada AS pesoEntradaGrupo, MG.precioKG As precioKGGrupo, MG.Importe AS ImporteGrupo,       
    MG.precioReal, A.numArete, A.fechaCompra, A.IdTipoGanado, A.pesoEntrada, A.precioKG,      
    CI.IdCosto, SUM(CI.Importe) AS Importe    
    INTO dbo.Util_InterfaceDesnormalizada      
    FROM [BLD-GVIZ05]. CENTRO_GVIZ.dbo.SalidaGanado SG (NOLOCK)      
    INNER JOIN [BLD-GVIZ05].CENTRO_GVIZ.dbo.configCentro CC (NOLOCK)       
     ON SG.IdGanadera = CC.IdGanadera AND SG.IdCentro = CC.IdCentro AND CC.IntegrarFox > 0      
    INNER JOIN [BLD-GVIZ05].CENTRO_GVIZ.dbo.CatCentro C (NOLOCK)      
     ON C.IdGanadera = SG.IdGanadera AND C.IdCentro = SG.IdCentro      
    INNER JOIN [BLD-GVIZ05].CENTRO_GVIZ.dbo.CatJaula J (NOLOCK)     
     ON J.IdGanadera = SG.IdGanadera AND J.IdJaula = SG.IdJaula      
    INNER JOIN [BLD-GVIZ05].CENTRO_GVIZ.dbo.MovimientoGanado MG (NOLOCK)      
     ON MG.IdGanadera = SG.IdGanadera AND MG.IdCentro = SG.IdCentro AND MG.IdSalida = SG.IdSalida and MG.idTipoMovimiento=SG.idTipoMovimiento    
    INNER JOIN [BLD-GVIZ05].CENTRO_GVIZ.dbo.Animal A (NOLOCK)      
     ON A.IdGanadera = SG.IdGanadera AND A.IdCentro = SG.IdCentro AND A.IdSalida = SG.IdSalida AND MG.IdTipoGanado = A.IdTipoGanado and MG.IdTipoMovimiento=A.IdTipoSalida    
    INNER JOIN [BLD-GVIZ05].CENTRO_GVIZ.dbo.CostoIndividual CI (NOLOCK)      
     ON CI.IdGanadera = A.IdGanadera AND CI.IdCentro = A.IdCentro AND CI.numArete = A.numArete AND CI.FechaCompra = A.FechaCompra      
    WHERE 1=1    
     AND fechaHora >= ''20141001''    
     AND SG.IdTipoMovimiento = 5     
     AND SG.statusReplica = 0    
     AND SG.IdCentro ='+@Centro+'AND SG.IdSalida ='+@Salida+'    
    GROUP BY SG.IdGanadera, SG.IdCentro, SG.IdSalida, SG.FechaHora, SG.esRuteo, MG.IdTipoGanado, MG.numAnimales,       
    MG.pesoEntrada, MG.precioKG, MG.Importe, MG.precioReal, A.numArete, A.fechaCompra, A.IdTipoGanado, A.pesoEntrada,       
    A.precioKG, CI.IdCosto'              
                
              --print @Sentencia  
          EXEC (@Sentencia)    
    --Select * From Util_InterfaceDesnormalizada    
    
    --Obtenemos Tabla Cabecero    
    SELECT I.IdCentro, I.IdSalida, I.IdGanadera, I.FechaHora, I.esRuteo, COUNT(DISTINCT I.numArete) AS Cabezas, 1 AS Activo, GETDATE() AS FechaRegistro,     
    'usuario.forzado' AS UsuarioRegistro, 'OK' As Valido    
    INTO dbo.Util_InterfaceSalida    
    FROM Util_InterfaceDesnormalizada I    
    GROUP BY I.IdCentro, I.IdSalida, I.IdGanadera, I.FechaHora, I.esRuteo    
    
    --Obtenemos Tabla Detalle    
    SELECT I.IdCentro, I.IdSalida, I.IdTipoGanadoGrupo, numAnimales, pesoEntradaGrupo, I.precioKgGrupo, ImporteGrupo, GETDATE() AS FechaRegistro,     
    'usuario.forzado' AS UsuarioRegistro    
    INTO dbo.Util_InterfaceSalidaDetalle    
    FROM Util_InterfaceDesnormalizada I    
    GROUP BY I.IdCentro, I.IdSalida, I.IdTipoGanadoGrupo, numAnimales, pesoEntradaGrupo, I.precioKgGrupo, ImporteGrupo    
    
    --Obtenemos Tabla Animal    
    SELECT I.IdCentro, I.IdSalida, I.numArete, I.FechaCompra, I.pesoEntrada AS PesoCompra, I.IdTipoGanado, I.pesoEntrada, GETDATE() AS FechaRegistro,     
    'usuario.forzado' AS UsuarioRegistro    
    INTO dbo.Util_InterfaceSalidaAnimal    
    FROM Util_InterfaceDesnormalizada I    
    GROUP BY I.IdCentro, I.IdSalida, I.numArete, I.FechaCompra, I.pesoEntrada, I.IdTipoGanado, I.pesoEntrada    
    
    --Obtenemos Tabla Costo    
    SELECT I.IdCentro, I.IdSalida, I.numArete, I.FechaCompra, I.IdCosto, Importe, GETDATE() AS FechaRegistro, 'usuario.forzado' AS UsuarioRegistro    
    INTO dbo.Util_InterfaceSalidaCosto    
    FROM Util_InterfaceDesnormalizada I    
    WHERE Importe > 0    
    GROUP BY I.IdCentro, I.IdSalida, I.numArete, I.FechaCompra, I.IdCosto, Importe    
    
    --Validamos Integridad Detalle    
    UPDATE Util_InterfaceSalida SET Valido = 'NO'    
    FROM dbo.Util_InterfaceSalida I    
    INNER JOIN (    
    SELECT ISC.IdCentro, ISC.IdSalida, ISC.Cabezas, SUM(numAnimales) As Animales    
    FROM Util_InterfaceSalida AS ISC    
    INNER JOIN Util_InterfaceSalidaDetalle ISD     
     ON ISC.IdCentro = ISD.IdCentro AND ISC.IdSalida = ISD.IdSalida    
    GROUP BY ISC.IdCentro, ISC.IdSalida, ISC.Cabezas    
    HAVING ISC.Cabezas != SUM(numAnimales)) AS TB1     
     ON TB1.IdCentro = I.IdCentro AND TB1.IdSalida = I.IdSalida    
    
    UPDATE Util_InterfaceSalida SET Valido = 'NO'    
    FROM Util_InterfaceSalida I    
    INNER JOIN (    
    SELECT ISD.IdCentro, ISD.IdSalida    
    --, ISD.IdTipoGanadoGrupo, ISD.numAnimales, ISD.pesoEntradaGrupo, ISD.ImporteGrupo, COUNT(ISA.numArete), SUM(ISA.PesoCompra), SUM(ISC.Importe)    
    FROM Util_InterfaceSalidaDetalle ISD    
    INNER JOIN Util_InterfaceSalidaAnimal ISA     
     ON ISD.IdCentro = ISA.IdCentro AND ISD.IdSalida = ISA.IdSalida AND ISD.IdTipoGanadoGrupo = ISA.IdTipoGanado    
    INNER JOIN Util_InterfaceSalidaCosto ISC    
     ON ISC.IdCentro = ISA.IdCentro AND ISC.IdSalida = ISA.IdSalida AND ISC.numArete = ISA.numArete AND ISC.FechaCompra = ISA.FechaCompra AND ISC.IdCosto = 26    
    GROUP BY ISD.IdCentro, ISD.IdSalida, ISD.IdTipoGanadoGrupo, ISD.numAnimales, pesoEntradaGrupo, ISD.ImporteGrupo    
    HAVING (numAnimales != COUNT(ISA.numArete))    
    /*OR (ISD.pesoEntradaGrupo != SUM(ISA.PesoCompra)) OR (ISD.ImporteGrupo != SUM(ISC.Importe))*/)TB2    
     ON TB2.IdCentro = I.IdCentro AND TB2.IdSalida = I.IdSalida    
    
    UPDATE Util_InterfaceSalida SET Util_InterfaceSalida.Valido = 'NO'    
    FROM Util_InterfaceSalida I    
    INNER JOIN InterfaceOrganizacion IO     
     ON I.IdCentro = IO.CodigoRelacion AND IO.TipoSistema = 'C'    
    INNER JOIN InterfaceOrganizacion IOG     
     ON I.IdGanadera = IOG.CodigoRelacion AND IOG.TipoSistema = 'G'    
    INNER JOIN InterfaceSalida ISA    
     ON ISA.OrganizacionID = IO.OrganizacionID AND ISA.SalidaID = I.IdSalida AND ISA.OrganizacionDestinoID = IOG.OrganizacionID            
        
    Select *     
    Into dbo.Util_InterfaceSalida_Correcta    
    From Util_InterfaceSalida    
        
    DELETE FROM Util_InterfaceSalida WHERE Valido != 'OK'    
    
    UPDATE Util_InterfaceSalidaAnimal SET Util_InterfaceSalidaAnimal.IdTipoGanado = TipoGanadoID    
    FROM Util_InterfaceSalidaAnimal   
    INNER JOIN TipoGanado ON PesoCompra BETWEEN PesoMinimo AND PesoMaximo AND CASE WHEN IdTipoGanado <= 4 THEN 'M' ELSE 'H' END = Sexo    
    
 SET NOCOUNT OFF;      
END      
GO

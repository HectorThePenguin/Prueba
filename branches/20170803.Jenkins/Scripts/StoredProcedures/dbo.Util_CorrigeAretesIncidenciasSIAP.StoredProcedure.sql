USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Util_CorrigeAretesIncidenciasSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Util_CorrigeAretesIncidenciasSIAP]
GO
/****** Object:  StoredProcedure [dbo].[Util_CorrigeAretesIncidenciasSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
   
CREATE PROCEDURE [dbo].[Util_CorrigeAretesIncidenciasSIAP]      
 @AreteIncorrecto varchar(15),      
 @AreteCorrectochar varchar(15),
 @EsInicial bit,
 @FolioEntrada int    
AS      
BEGIN      
 SET NOCOUNT ON;      

--Exec dbo.Util_CorrigeAretesIncidenciasSIAP '','',1,2265  --si es 1 es inicial si es 0 no es inicial y Folio de Entrada
  
--sp_helptext Util_CorrigeAretesIncidenciasSIAP  
  
--=============================================      
-- Author     : César Augusto Villa Bojórquez      
-- Create date: 11/02/2015      
-- Description: Corrige Aretes Incidencias   
-- [Util_CorrigeAretesIncidenciasSIAP]       
--=============================================       
    
    --If Exists (Select * From sysobjects Where name='UtilBitacoraDeAretesIncidencias')      
    --Drop Table UtilBitacoraDeAretesIncidencias        
            
 --EXEC sp_ReemplazaAreteInterface @AreteBUENO = '2106963', @AreteMALO = '344033' --Intercambia arete de corte en interface
--EXEC sp_QuitaAreteCargaInicial @Arete = '2194151'--Reemplaza arete de carga inicial

 
 
 
 
 
 SET NOCOUNT OFF;      
END      



GO

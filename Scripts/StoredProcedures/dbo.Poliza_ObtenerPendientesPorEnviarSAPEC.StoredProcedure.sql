USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEC]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEC]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEC]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--PASO (2)
--======================================================    
-- Author     : Ruben Guzman
-- Create date: 04/006/2015    
-- Description:     
-- SpName     : Poliza_ObtenerPendientesPorEnviarSAPEC  Entrada de Ganado TIPO 6  
--======================================================    
CREATE PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEC]    
AS    
BEGIN    
    
 SET NOCOUNT ON    
    
  SELECT TOP 30 CAST(P.XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza    
   ,  P.OrganizacionID    
   ,  P.PolizaID    
   ,  P.TipoPolizaID    
  FROM Poliza P    
  WHERE 
 PolizaID IN (
96407
,96406
,96332
,96331
,96330
,96329
,96328
,96327
,96305
,96304
,96303
,96302
,96301
,96298
,96283
,96282
,96281
,96280
,96279
,96278
,96265
,96218
,96205
,96145
,96095
,96073
,96064
,96050
,95973
,95899
)
     
  ORDER BY PolizaID DESC    
    
 SET NOCOUNT OFF    
    
END    
  
--96218
--,95973
--,95899
--,95898
--,95897
--,95896
--,95869
--,95868
--,95867
--,94594 
--,94514
--,94475
--,94474
--,94473
--,94472
--,94471
--,94470
--,94469
--,94468
--,94467
--,94460
--,94459
--,94458
--,94457
--,94456
--,94449
--,94448
--,94447
--,94446
--,94444
--,94443
--,94442
--,94441
--,94440
--,94436
--,94435
--,94434
--,94433
--,94432
--,94416
GO

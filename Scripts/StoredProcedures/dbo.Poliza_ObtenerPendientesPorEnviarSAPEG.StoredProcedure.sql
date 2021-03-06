USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEG]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEG]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEG]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Gilberto Carranza  
-- Create date: 25/08/2014  
-- Description:   
-- SpName     : Poliza_ObtenerPendientesPorEnviarSAP  Entrada de Ganado TIPO 1
--======================================================  
CREATE PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAPEG]  
AS  
BEGIN  
  
 SET NOCOUNT ON  
  
  SELECT CAST(P.XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza  
   ,  P.OrganizacionID  
   ,  P.PolizaID  
   ,  P.TipoPolizaID  
  FROM Poliza P  
  WHERE Procesada = 0  
   AND LEN(Mensaje) = 0
   --AND CAST(FechaCreacion AS DATE) between CAST(GETDATE()-3 AS DATE) AND CAST(GETDATE()-1 AS DATE) 
   AND TipoPolizaID = 1
   AND PolizaID IN (96684,96683,96682,96681,
96679,96678,
96677,
96676,
96670,
96669)
   
  ORDER BY PolizaID DESC  
  
 SET NOCOUNT OFF  
  
END  
GO

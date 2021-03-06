USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPendientesPorEnviarSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================              
-- Author     : Gilberto Carranza              
-- Create date: 25/08/2014              
-- Description:               
-- SpName     : Poliza_ObtenerPendientesPorEnviarSAP              
--======================================================              
CREATE PROCEDURE [dbo].[Poliza_ObtenerPendientesPorEnviarSAP]              
AS              
BEGIN              
              
 SET NOCOUNT ON              
              
  SELECT TOP 100 CAST(P.XmlPoliza AS VARCHAR(MAX)) AS XmlPoliza              
   ,  P.OrganizacionID              
   ,  P.PolizaID              
   ,  P.TipoPolizaID              
  FROM Poliza P              
  WHERE Procesada = 0              
   AND Estatus = 1              
   AND PolizaID > 140000              
   AND OrganizacionID > 0              
   AND LEN(Mensaje) = 0
   AND Cancelada = 0
  ORDER BY PolizaID DESC              
              
 SET NOCOUNT OFF              
              
END 
GO

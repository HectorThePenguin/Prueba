USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConciliacionDetalle_S]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConciliacionDetalle_S]
GO
/****** Object:  StoredProcedure [dbo].[ConciliacionDetalle_S]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConciliacionDetalle_S]   
(  
@TipoPolizaID INT  
)  
AS  
BEGIN  
 SELECT   
  C.NoRef  
  ,C.FechaDocto  
  ,C.FechaCont  
  ,C.ClaseDoc  
  ,C.Sociedad  
  ,C.Moneda  
  ,C.TipoCambio  
  ,C.TextoDocto  
  ,C.Mes  
  ,C.Cuenta  
  ,C.Proveedor  
  ,C.Cliente  
  ,C.Importe  
  ,C.Concepto  
  ,C.Division  
  ,C.NoLinea  
  ,C.Ref3  
  ,C.ArchivoFolio  
  ,C.DocumentoSAP  
  ,C.DocumentoCancelacionSAP  
  ,C.Segmento  
  ,C.OrganizacionID  
  ,C.Conciliada  
  ,C.Procesada  
  ,C.Cancelada  
  ,C.Mensaje  
  ,C.PolizaID  
  ,C.TipoPolizaID  
  ,TP.Descripcion AS 'TipoPoliza'  
 FROM Conciliacion AS C(NOLOCK)  
 INNER JOIN TipoPoliza  AS TP(NOLOCK)  
 ON  C.TipoPolizaID = TP.TipoPolizaID  
 WHERE C.TipoPolizaID = @TipoPolizaID  
END  
  
GO

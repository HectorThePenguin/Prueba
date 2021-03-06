USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoDetalle_ObtenerPorFleteInternoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInternoDetalle_ObtenerPorFleteInternoID]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoDetalle_ObtenerPorFleteInternoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 22/07/2014
-- Description: Obtiene flete interno detalle por flete interno
-- SpName     : FleteInternoDetalle_ObtenerPorFleteInternoID 4, 1
--======================================================
CREATE PROCEDURE [dbo].[FleteInternoDetalle_ObtenerPorFleteInternoID]  
@FleteInternoID INT,  
@Activo BIT  
AS  
BEGIN  
 SELECT   
  FTD.FleteInternoDetalleID,  
  FTD.FleteInternoID,  
  FTD.ProveedorID,  
  P.Descripcion,  
  P.CodigoSAP,  
  FTD.MermaPermitida,  
  FTD.Observaciones,  
  FTD.Activo  
  ,TipoTarifa = tt.Descripcion  
 FROM FleteInternoDetalle FTD (NOLOCK)  
 INNER JOIN Proveedor P 
	ON P.ProveedorID = FTD.ProveedorID 
 INNER JOIN TipoTarifa tt (NOLOCK)  
	ON FTD.TipoTarifaID = tt.TipoTarifaID 
 WHERE FTD.FleteInternoID = @FleteInternoID  
 AND FTD.Activo = @Activo  
END 

GO

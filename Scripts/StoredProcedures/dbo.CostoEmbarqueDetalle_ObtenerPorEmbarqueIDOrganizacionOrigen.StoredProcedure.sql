USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueIDOrganizacionOrigen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueIDOrganizacionOrigen]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueIDOrganizacionOrigen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Jorge Luis Velazquez Araujo  
-- Create date: 2013/11/03 
-- Description: Sp para obtener los costos de un EmbarqueDetalle  
-- [CostoEmbarqueDetalle_ObtenerPorEmbarqueIDOrganizacionOrigen] 7  
--=============================================  
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueIDOrganizacionOrigen]   
@EmbarqueID INT,
@OrganizacionOrigenID INT
AS  
BEGIN  
 SET NOCOUNT ON;  
 SELECT CostoEmbarqueDetalleID    
  ,co.CostoID  
  ,co.ClaveContable
  ,co.Descripcion [Costo] 
  ,re.RetencionID
  ,re.Descripcion [Retencion] 
  ,pr.ProveedorID
  ,pr.Descripcion [Proveedor]
  ,pr.CodigoSAP
  ,Importe    
 FROM CostoEmbarqueDetalle ced  
 INNER JOIN EmbarqueDetalle ed ON ced.EmbarqueDetalleID = ed.EmbarqueDetalleID 
 INNER JOIN Proveedor pr ON ed.ProveedorID = pr.ProveedorID
 INNER JOIN Embarque e ON e.EmbarqueID = ed.EmbarqueID
 INNER JOIN Costo co on ced.CostoID = co.CostoID  
 LEFT JOIN Retencion re on co.RetencionID = re.RetencionID 
 WHERE e.EmbarqueID = @EmbarqueID  
 --AND ed.OrganizacionOrigenID = @OrganizacionOrigenID
 AND ced.Activo = 1
 SET NOCOUNT OFF;  
END  
GO

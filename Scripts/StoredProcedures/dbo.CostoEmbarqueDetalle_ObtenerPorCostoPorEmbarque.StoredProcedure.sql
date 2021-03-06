USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorCostoPorEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorCostoPorEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorCostoPorEmbarque]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================  
-- Author     : Jorge Luis Velazquez Araujo  
-- Create date: 2013/12/26
-- Description: Sp para obtener los costos de Fletes  
-- [CostoEmbarqueDetalle_ObtenerPorCostoPorEmbarque] 7  
--=============================================  
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorCostoPorEmbarque]   
@EmbarqueID INT
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
 INNER JOIN TipoCosto tc on co.TipoCostoID = tc.TipoCostoID 
 LEFT JOIN Retencion re on co.RetencionID = re.RetencionID 
 WHERE 
 e.EmbarqueID = @EmbarqueID   
 AND ced.Activo = 1
 AND tc.TipoCostoID = 4 
 SET NOCOUNT OFF;  
END  

GO

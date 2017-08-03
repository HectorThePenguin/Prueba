
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerCostosPorProductoId]    ******/
DROP PROCEDURE [dbo].[PremezclaDistribucion_ObtenerCostosPorProductoId]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerCostosPorProductoId]     ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		David Meneodza
-- Create date: 2-12-2015
-- Description:	Otiene un listado de los costos de distribucion para cada producto de premezcla
-- Premezcla_ObtenerCostosPorProductoId 213
-- =============================================
Create PROCEDURE [dbo].[PremezclaDistribucion_ObtenerCostosPorProductoId]
@ProductoId INT,
@Activo BIT = 1
AS
BEGIN
	SELECT
	  PC.CostoID,
		PC.PremezclaDistribucionCostoID,
	  PC.PremezclaDistribucionID,
	  PC.TieneCuenta,
	  PC.ProveedorID,
	  PC.CuentaProvision,
	  PC.Importe,
	  PC.Iva,
	  PC.Retencion,
	  PC.Activo,
PRO.CodigoSap,
PRO.Descripcion As DescripcionProveedor,
CS.Descripcion As DescripcionCuenta
	  FROM PremezclaDistribucionCosto (NOLOCK) PC
INNER JOIN PremezclaDistribucion (NOLOCK) PD ON (PC.PremezclaDistribucionID = PD.PremezclaDistribucionID)
LEFT JOIN Proveedor (NOLOCK) PRO ON (PC.ProveedorID = PRO.ProveedorID) 
LEFT JOIN CuentaSAP (NOLOCK) CS ON (PC.CuentaProvision = CS.CuentaSAP) 
WHERE PD.productoID = @ProductoId
    AND PC.Activo = @Activo;
END

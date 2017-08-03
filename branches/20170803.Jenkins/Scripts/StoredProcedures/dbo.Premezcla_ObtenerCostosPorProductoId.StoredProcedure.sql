
USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Premezcla_ObtenerCostosPorProductoId]    ******/
DROP PROCEDURE [dbo].[Premezcla_ObtenerCostosPorProductoId]
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
CREATE PROCEDURE Premezcla_ObtenerCostosPorProductoId
@ProductoId INT,
@Activo BIT = 1
AS
BEGIN
	SELECT
	  PC.CostoID,
	  PC.PremezclaDistribucionID,
	  PC.TieneCuenta,
	  PC.ProveedorID,
	  PC.CuentaProvision,
	  PC.Importe,
	  PC.Iva,
	  PC.Retencion,
	  PC.Activo
	  FROM PremezclaDistribucionCosto (NOLOCK) PC
INNER JOIN PremezclaDistribucion (NOLOCK) PD ON (PC.PremezclaDistribucionID = PD.PremezclaDistribucionID) 
WHERE PD.productoID = @ProductoId
    AND PC.Activo = @Activo
    END

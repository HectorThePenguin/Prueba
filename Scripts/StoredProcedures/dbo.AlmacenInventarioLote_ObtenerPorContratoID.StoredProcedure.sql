USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 10/12/2014
-- Description: Obtiene el Lote del contrato
-- SpName     : exec AlmacenInventarioLote_ObtenerPorContratoID 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorContratoID]
@ContratoID INT
AS 
BEGIN
	SELECT DISTINCT
	  AIL.[AlmacenInventarioLoteID]
      ,AIL.[AlmacenInventarioID]
      ,AIL.[Lote]
      ,AIL.[Cantidad]
      ,AIL.[PrecioPromedio]
      ,AIL.[Piezas]
      ,AIL.[Importe]
      ,AIL.[FechaInicio]
      ,AIL.[FechaFin]
      ,AIL.[Activo]      
	  ,A.[TipoAlmacenID]
  FROM [dbo].[AlmacenInventarioLote](NOLOCK) AIL
  INNER JOIN [dbo].[AlmacenInventario](NOLOCK) AI ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID)
  INNER JOIN [dbo].[Almacen](NOLOCK) A ON (A.AlmacenID = AI.AlmacenID)
  INNER JOIN AlmacenMovimientoDetalle amd on ail.AlmacenInventarioLoteID = amd.AlmacenInventarioLoteID
  WHERE ail.Activo = 1
  and amd.ContratoID = @ContratoID
END

GO

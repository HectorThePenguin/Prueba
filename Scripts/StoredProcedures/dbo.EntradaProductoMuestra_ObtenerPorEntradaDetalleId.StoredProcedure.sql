USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoMuestra_ObtenerPorEntradaDetalleId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoMuestra_ObtenerPorEntradaDetalleId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoMuestra_ObtenerPorEntradaDetalleId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 20/05/2014
-- Description: Consulta las muestras de cada indicador de la entrada de producto
-- SpName     : exec EntradaProductoMuestra_ObtenerPorEntradaDetalleId 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProductoMuestra_ObtenerPorEntradaDetalleId]
@EntradaProductoDetalleID INT 
AS 
BEGIN
	SELECT [EntradaProductoMuestraID]
      ,[EntradaProductoDetalleID]
      ,[Porcentaje]
      ,[Descuento]
      ,[Rechazo]
      ,[Activo]
      ,[EsOrigen]
  FROM [dbo].[EntradaProductoMuestra]
  WHERE EntradaProductoDetalleID = @EntradaProductoDetalleID AND Activo = 1
END
GO

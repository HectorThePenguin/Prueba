USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_ObtenerPorEntradaId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoDetalle_ObtenerPorEntradaId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_ObtenerPorEntradaId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 20/05/2014
-- Description: Consulta el detalle de indicadores de la entrada de producto
-- SpName     : exec EntradaProductoDetalle_ObtenerPorEntradaId 1
--======================================================
CREATE PROCEDURE [dbo].[EntradaProductoDetalle_ObtenerPorEntradaId]
@EntradaProductoId INT
AS 
BEGIN
	SELECT [EntradaProductoDetalleID]
      ,[EntradaProductoID]
      ,[IndicadorID]
      ,[Activo]
  FROM [dbo].[EntradaProductoDetalle] 
  WHERE EntradaProductoID = @EntradaProductoId AND Activo = 1
END

GO

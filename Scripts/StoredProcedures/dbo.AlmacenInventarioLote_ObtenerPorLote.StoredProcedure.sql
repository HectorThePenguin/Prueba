USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorLote]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 03/07/2014
-- Description: Obtiene el almacen por id
-- SpName     : exec AlmacenInventarioLote_ObtenerPorLote 4,8,80,1,1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorLote]
@OrganizacionID INT,
@TipoAlmacen INT,
@ProductoId INT,
@Lote INT,
@Activo BIT
AS 
BEGIN
	SELECT 
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
      ,AIL.[FechaCreacion]
      ,AIL.[UsuarioCreacionID]
      ,AIL.[FechaModificacion]
      ,AIL.[UsuarioModificacionID]
	  ,A.[TipoAlmacenID]
  FROM [dbo].[AlmacenInventarioLote](NOLOCK) AIL
  INNER JOIN [dbo].[AlmacenInventario](NOLOCK) AI ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID)
  INNER JOIN [dbo].[Almacen](NOLOCK) A ON (A.AlmacenID = AI.AlmacenID)
  WHERE [Lote] = @Lote AND AIL.Activo = 1 
		AND A.OrganizacionID = @OrganizacionID
		AND A.TipoAlmacenID = @TipoAlmacen
		AND AI.ProductoID = @ProductoId
		AND AIL.Activo = @Activo
END

GO

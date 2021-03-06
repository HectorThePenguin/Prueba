USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerAlmacenPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerAlmacenPorId]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerAlmacenPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 14/05/2014
-- Description: Obtiene el almacen por id
-- SpName     : exec AlmacenInventarioLote_ObtenerAlmacenPorId 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerAlmacenPorId]
@AlmacenInventarioLoteId INT
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
	  ,A.[OrganizacionID]
	  ,AI.[ProductoID]
  FROM [dbo].[AlmacenInventarioLote](NOLOCK) AIL
  INNER JOIN [dbo].[AlmacenInventario](NOLOCK) AI ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID)
  INNER JOIN [dbo].[Almacen](NOLOCK) A ON (A.AlmacenID = AI.AlmacenID)
  WHERE [AlmacenInventarioLoteID] = @AlmacenInventarioLoteId AND AIL.Activo = 1
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorFolioLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorFolioLote]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorFolioLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 09/09/2014
-- Description: Obtiene el almacen por id y folio lote
-- SpName     : exec AlmacenInventarioLote_ObtenerPorFolioLote 4, 7, 100, 1, 174
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorFolioLote]
@OrganizacionID INT,
@TipoAlmacen INT,
@ProductoId INT,
@Lote INT,
@AlmacenID INT = 0
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
  INNER JOIN [dbo].[Almacen](NOLOCK) A ON (A.AlmacenID = AI.AlmacenID
											AND @AlmacenID IN (A.AlmacenID, 0))
  WHERE [Lote] = @Lote
		AND A.OrganizacionID = @OrganizacionID
		AND A.TipoAlmacenID = @TipoAlmacen
		AND AI.ProductoID = @ProductoId
END
GO

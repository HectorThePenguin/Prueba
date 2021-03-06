USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenLote]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 22/01/2015
-- Description: Obtiene el Lote del Almacen
-- SpName     : exec AlmacenInventarioLote_ObtenerPorAlmacenLote 1,5,100,1,1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenLote]
@OrganizacionID INT,
@AlmacenID INT,
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
		AND A.AlmacenID = @AlmacenID
		AND AI.ProductoID = @ProductoId
		AND AIL.Activo = @Activo
END

GO

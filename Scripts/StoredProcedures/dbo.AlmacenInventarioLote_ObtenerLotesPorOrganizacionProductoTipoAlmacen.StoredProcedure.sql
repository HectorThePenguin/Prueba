USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 14/05/2014
-- Description: Obtiene el almacen por id
-- SpName     : exec AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacen 1, 6, 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacen]
@OrganizacionID INT,
@TipoAlmacen INT,
@ProductoId INT
AS 
BEGIN
	SELECT ail.[AlmacenInventarioLoteID]
      ,ail.[AlmacenInventarioID]
      ,ail.[Lote]
      ,ail.[Cantidad]
      ,ail.[PrecioPromedio]
      ,ail.[Piezas]
      ,ail.[Importe]
      ,ail.[FechaInicio]
      ,ail.[FechaFin]
      ,ail.[Activo]
      ,ail.[FechaCreacion]
      ,ail.[UsuarioCreacionID]
      ,ail.[FechaModificacion]
      ,ail.[UsuarioModificacionID]
	  ,a.[TipoAlmacenID]
	FROM AlmacenInventarioLote AS ail (NOLOCK)
	INNER JOIN AlmacenInventario AS ai (NOLOCK)
	ON (ail.AlmacenInventarioID = ai.AlmacenInventarioID) 
	INNER JOIN Almacen AS a (NOLOCK) ON (ai.AlmacenID = a.AlmacenID)
	WHERE a.Activo = 1 AND a.OrganizacionID = @OrganizacionID 
		AND (a.TipoAlmacenID = @TipoAlmacen OR @TipoAlmacen = 0) AND ai.ProductoId = @ProductoId
		AND ail.Activo = 1
END

GO

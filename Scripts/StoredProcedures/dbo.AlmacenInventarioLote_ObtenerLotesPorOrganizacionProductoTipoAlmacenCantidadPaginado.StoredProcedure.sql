USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenCantidadPaginado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenCantidadPaginado]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenCantidadPaginado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 02/07/2014 12:00:00 a.m.
-- Description: Obtiene el listado de lotes del producto, organizacion, tipo almacen y cantidad mayor a cero
-- SpName     : AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenCantidadPaginado 1,6,85,0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenCantidadPaginado]
@OrganizacionID INT,
@TipoAlmacen INT,
@ProductoId INT,
@Lote INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	   ROW_NUMBER() OVER (ORDER BY AlmacenInventarioLoteID ASC) AS [RowNum]
	  ,ail.[AlmacenInventarioLoteID]
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
	  ,a.[TipoAlmacenID],
	  a.OrganizacionID,
	  ai.ProductoId AS ProductoID
	INTO #AlmacenInventarioLote
	FROM AlmacenInventarioLote AS ail (NOLOCK)
	INNER JOIN AlmacenInventario AS ai (NOLOCK)
	ON (ail.AlmacenInventarioID = ai.AlmacenInventarioID) 
	INNER JOIN Almacen AS a (NOLOCK) ON (ai.AlmacenID = a.AlmacenID)
	WHERE (ail.Lote LIKE '%' + CAST(@Lote AS VARCHAR(9)) + '%' OR @Lote = 0)
		AND ail.Cantidad > 0
		AND a.OrganizacionID = @OrganizacionID 
		AND (a.TipoAlmacenID = @TipoAlmacen OR @TipoAlmacen = 0) AND ai.ProductoId = @ProductoId
		AND ail.Activo = @Activo
	SELECT
	   [AlmacenInventarioLoteID]
      ,[AlmacenInventarioID]
      ,[Lote]
      ,[Cantidad]
      ,[PrecioPromedio]
      ,[Piezas]
      ,[Importe]
      ,[FechaInicio]
      ,[FechaFin]
      ,[Activo]
      ,[FechaCreacion]
      ,[UsuarioCreacionID]
      ,[FechaModificacion]
      ,[UsuarioModificacionID]
	  ,[TipoAlmacenID]
	  ,[OrganizacionID]
	  ,[ProductoID]
	FROM #AlmacenInventarioLote
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT([AlmacenInventarioLoteID]) AS [TotalReg]
	FROM #AlmacenInventarioLote
	DROP TABLE #AlmacenInventarioLote
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesUso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesUso]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerLotesUso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 28/11/2014 12:00:00 a.m.
-- Description: Obtiene el listado de lotes para la verificacion de lote en uso
-- SpName     : AlmacenInventarioLote_ObtenerLotesUso 1,6,109,1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerLotesUso]
@OrganizacionID INT,
@TipoAlmacen INT,
@ProductoId INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	   ROW_NUMBER() OVER (ORDER BY Lote ASC) AS [RowNum]
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
	FROM AlmacenInventarioLote AS ail (NOLOCK)
	INNER JOIN AlmacenInventario AS ai (NOLOCK)
	ON (ail.AlmacenInventarioID = ai.AlmacenInventarioID) 
	INNER JOIN Almacen AS a (NOLOCK) ON (ai.AlmacenID = a.AlmacenID)
	WHERE ail.Cantidad > 0
		AND a.OrganizacionID = @OrganizacionID 
		AND (a.TipoAlmacenID = @TipoAlmacen OR @TipoAlmacen = 0) AND ai.ProductoId = @ProductoId
		AND ail.Activo = @Activo
	SET NOCOUNT OFF;
END

GO

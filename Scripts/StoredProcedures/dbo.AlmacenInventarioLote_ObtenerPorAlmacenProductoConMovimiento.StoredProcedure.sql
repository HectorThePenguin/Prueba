USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoConMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoConMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoConMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pedro Delgado>
-- Create date: <31/07/2014>
-- Description:	Obtiene los lotes por producto y almacen
-- EXEC AlmacenInventarioLote_ObtenerPorAlmacenProductoConMovimiento 1,1
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoConMovimiento] 
	@ProductoID INT,
	@AlmacenID INT,
	@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT
		  L.AlmacenInventarioLoteID
		  ,L.AlmacenInventarioID
		  ,L.Lote
		  ,L.Cantidad
		  ,L.PrecioPromedio
		  ,L.Piezas
		  ,L.Importe
		  ,L.FechaInicio
		  ,L.FechaFin
		  ,L.Activo
		  ,L.FechaCreacion
		  ,L.UsuarioCreacionID
		  ,L.FechaModificacion
		  ,L.UsuarioModificacionID
  FROM dbo.AlmacenInventarioLote (NOLOCK) L
	   INNER JOIN dbo.AlmacenInventario (NOLOCK) I ON (L.AlmacenInventarioID = I.AlmacenInventarioID)
	   INNER JOIN dbo.Almacen (NOLOCK) A ON (I.AlmacenID = A.AlmacenID)
	   INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AMD ON(L.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID)
  WHERE L.Activo = @Activo 
		AND I.ProductoID = @ProductoID
		AND A.AlmacenID = @AlmacenID;		
  SET NOCOUNT OFF;
END

GO

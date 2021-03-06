USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProducto]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <13/06/2014>
-- Description:	Obtiene los lotes por producto y almacen
-- EXEC AlmacenInventarioLote_ObtenerPorProductoAlmacen 1,1
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProducto] 
	@ProductoID INT,
	@AlmacenID INT,
	@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT L.AlmacenInventarioLoteID
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
  FROM dbo.AlmacenInventarioLote (NOLOCK) L,
	   dbo.AlmacenInventario (NOLOCK) I,
	   dbo.Almacen (NOLOCK) A
  WHERE L.AlmacenInventarioID = I.AlmacenInventarioID
		AND I.AlmacenID = A.AlmacenID
		AND L.Activo = @Activo 
		AND I.ProductoID = @ProductoID
		AND A.AlmacenID = @AlmacenID;		
  SET NOCOUNT OFF;
END

GO

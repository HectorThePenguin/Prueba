USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoEnCeros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoEnCeros]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoEnCeros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 25-09-2014
-- Description:	Obtiene los lotes por producto y almacen
-- EXEC AlmacenInventarioLote_ObtenerPorAlmacenProductoEnCeros 100,6,1
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenProductoEnCeros] 
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
  WHERE L.Activo = @Activo 
		AND I.ProductoID = @ProductoID
		AND A.AlmacenID = @AlmacenID	
		AND L.Cantidad = 0
  SET NOCOUNT OFF;
END

GO

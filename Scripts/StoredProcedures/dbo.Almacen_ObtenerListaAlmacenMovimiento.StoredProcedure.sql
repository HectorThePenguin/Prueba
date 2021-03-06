USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerListaAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerListaAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerListaAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 02/04/2014
-- Description:  Guardar el Almacen Movimiento de cierre inventario
-- Origen: APInterfaces
-- EXEC Almacen_ObtenerListaAlmacenMovimiento 1,14,1
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerListaAlmacenMovimiento]
	@AlmacenID INT,
	@TipoMovimientoID INT,
	@Activo INT
AS
  BEGIN
      SET NOCOUNT ON
			SELECT AM.AlmacenMovimientoID
			,AM.AlmacenID
			,AM.TipoMovimientoID
			,AM.FolioMovimiento
			,AM.FechaCreacion 
			,AM.Observaciones
			,AM.Status
			,AM.UsuarioCreacionID
			,U.Nombre
			,TM.Descripcion
			FROM AlmacenMovimiento AM (NOLOCK)
			INNER JOIN Usuario  (NOLOCK) AS U ON U.UsuarioID=AM.UsuarioCreacionID
			INNER JOIN Almacen  (NOLOCK) AS A ON A.AlmacenID=AM.AlmacenID
			INNER JOIN TipoMovimiento  (NOLOCK)  AS TM ON TM.TipoMovimientoID=AM.TipoMovimientoID 
			WHERE AM.TipoMovimientoID=@TipoMovimientoID
			AND AM.AlmacenID=@AlmacenID
			AND A.Activo=@Activo
	SET NOCOUNT OFF
  END

GO

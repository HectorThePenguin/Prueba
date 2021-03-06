USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AjusteDeInventario_ObtenerDiferenciasInventarios]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AjusteDeInventario_ObtenerDiferenciasInventarios]
GO
/****** Object:  StoredProcedure [dbo].[AjusteDeInventario_ObtenerDiferenciasInventarios]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 25/03/2014
-- Description: Procedimiento que obtiene las diferencias de inventario fisico y teorico
-- SpName     : AjusteDeInventario_ObtenerDiferenciasInventarios 1, 1, 4, 1
--======================================================
CREATE PROCEDURE [dbo].[AjusteDeInventario_ObtenerDiferenciasInventarios]
@AlmacenID INT,
@AlmacenMovimientoID BIGINT,
@OrganizacionID INT,
@Activo INT
AS
BEGIN
		SELECT 
			  P.ProductoID,
			  P.Descripcion,
			  0 AS LoteAlmacenado,
			  UM.ClaveUnidad,
			  AMD.Cantidad,
			  AMD.Precio,
			  AMD.Cantidad * AMD.Precio AS Importe,
			  AI.Cantidad AS CantidadInventarioTeorico,
			  AI.PrecioPromedio AS PrecioInventarioTeorico,
			  AI.AlmacenInventarioID,
			  AMD.AlmacenMovimientoDetalleID
		FROM Almacen A
		INNER JOIN AlmacenMovimiento AM (NOLOCK)  ON AM.AlmacenID = A.AlmacenID
		INNER JOIN AlmacenMovimientoDetalle AMD (NOLOCK)  ON AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID
		INNER JOIN Producto P (NOLOCK)  ON P.ProductoID = AMD.ProductoID
		INNER JOIN UnidadMedicion UM (NOLOCK)  ON UM.UnidadID = P.UnidadID
		INNER JOIN AlmacenInventario AI (NOLOCK) ON AI.ProductoID = AMD.ProductoID
		WHERE A.AlmacenID = @AlmacenID
		AND AI.AlmacenID = @AlmacenID
		AND AM.AlmacenMovimientoID = @AlmacenMovimientoID
		AND A.OrganizacionID = @OrganizacionID
		AND A.Activo = @Activo
		AND P.Activo = @Activo
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerCancelacionPorDescripcionProducto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerCancelacionPorDescripcionProducto]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerCancelacionPorDescripcionProducto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/11/26
-- Description: Procedimiento para obtener los folios a cancelar
/*SalidaProducto_ObtenerCancelacionPorDescripcionProducto
*/
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerCancelacionPorDescripcionProducto]
@Descripcion VARCHAR(50),
@OrganizacionID INT,
@Fecha DATE,
@XMLTipoMovimiento XML,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
				ORDER BY SP.SalidaProductoID ASC
				) AS RowNum,
		SP.SalidaProductoID,
		SP.OrganizacionID,
		SP.OrganizacionIDDestino,
		SP.TipoMovimientoID,
		TM.Descripcion,
		SP.FolioSalida,
		SP.FolioFactura,
		SP.AlmacenID,
		SP.AlmacenInventarioLoteID,
		SP.ClienteID,
		SP.CuentaSAPID,
		SP.Observaciones,
		SP.Precio,
		SP.Importe,
		SP.AlmacenMovimientoID,
		SP.PesoTara,
		SP.PesoBruto,
		SP.Piezas,
		SP.FechaSalida,
		SP.ChoferID,
		SP.CamionID,
		SP.Activo,
		SP.FechaCreacion,
		SP.UsuarioCreacionID
	INTO #SalidaProducto
	FROM SalidaProducto (NOLOCK) SP
	INNER JOIN TipoMovimiento (NOLOCK) TM ON (SP.TipoMovimientoID = TM.TipoMovimientoID)
	INNER JOIN AlmacenInventarioLote (NOLOCK) AML ON (SP.AlmacenInventarioLoteID = AML.AlmacenInventarioLoteID)
	INNER JOIN AlmacenInventario (NOLOCK) AI ON (AML.AlmacenInventarioID = AI.AlmacenInventarioID)
	INNER JOIN AlmacenMovimiento (NOLOCK) AM ON (SP.AlmacenMovimientoID = AM.AlmacenMovimientoID)
	INNER JOIN AlmacenMovimientoDetalle (NOLOCK) AMD ON (AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	INNER JOIN Producto (NOLOCK) P ON (P.ProductoID = AI.ProductoID)
	WHERE 
		AM.TipoMovimientoID IN (SELECT TipoMovimientoID = T.item.value('./TipoMovimientoID[1]', 'INT')
													FROM  @XMLTipoMovimiento.nodes('ROOT/TipoMovimiento') AS T(item))
		AND (P.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
		AND SP.OrganizacionID = @OrganizacionID
		AND SP.Activo = 1
		AND AML.Activo = 1
		AND CAST(SP.FechaSalida AS DATE) >= CAST(@Fecha  AS DATE)
	SELECT 
		SalidaProductoID,
		OrganizacionID,
		OrganizacionIDDestino,
		TipoMovimientoID,
		Descripcion,
		FolioSalida,
		FolioFactura,
		AlmacenID,
		AlmacenInventarioLoteID,
		ClienteID,
		CuentaSAPID,
		Observaciones,
		Precio,
		Importe,
		AlmacenMovimientoID,
		PesoTara,
		PesoBruto,
		Piezas,
		FechaSalida,
		ChoferID,
		CamionID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM #SalidaProducto
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(SalidaProductoID) AS TotalReg
	FROM #SalidaProducto
	DROP TABLE #SalidaProducto
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerFolioPorPaginaReimpresion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerFolioPorPaginaReimpresion]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerFolioPorPaginaReimpresion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/08/21
-- Description: Sp para obtener los folios de las salidas de productos por pagina
-- SalidaProducto_ObtenerFolioPorPaginaReimpresion 1, '', 1, 15
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerFolioPorPaginaReimpresion]
@OrganizacionID INT,
@DescripcionMovimiento VARCHAR(50),
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY SP.FolioSalida ASC
			) AS RowNum, 
		SP.SalidaProductoID,
		SP.OrganizacionID,
		SP.OrganizacionIDDestino,
		SP.TipoMovimientoID,
		SP.FolioSalida,
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
		TM.Descripcion
		, AIL.Lote
		, AMD.ProductoID
		, C.CuentaSAP
		, CL.CodigoSAP
		, CL.Descripcion		AS Cliente
	INTO #DatosSalidaProducto
	FROM SalidaProducto SP
	INNER JOIN TipoMovimiento TM 
		ON (SP.TipoMovimientoID = TM.TipoMovimientoID) 
	INNER JOIN AlmacenInventarioLote AIL(NOLOCK)
		ON (SP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
	INNER JOIN AlmacenMovimientoDetalle AMD
		ON (SP.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID
			AND SP.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	LEFT JOIN Cliente CL
		ON (SP.ClienteID = CL.ClienteID)
	LEFT JOIN CuentaSAP C
		ON (SP.CuentaSAPID = C.CuentaSAPID)
	WHERE (@DescripcionMovimiento = '' OR TM.Descripcion= @DescripcionMovimiento)
		AND @OrganizacionID IN (SP.OrganizacionID, 0)
		AND SP.AlmacenInventarioLoteID > 0
	SELECT SalidaProductoID,
		   OrganizacionID,
		   OrganizacionIDDestino,
		   TipoMovimientoID,
		   FolioSalida,
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
		   Descripcion
		   , Lote
		   , ProductoID
		   , CuentaSAP
		   , CodigoSAP
		   , Cliente
	FROM #DatosSalidaProducto
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT COUNT(FolioSalida) AS TotalReg
	FROM #DatosSalidaProducto
	DROP TABLE #DatosSalidaProducto
	SET NOCOUNT OFF;
END

GO

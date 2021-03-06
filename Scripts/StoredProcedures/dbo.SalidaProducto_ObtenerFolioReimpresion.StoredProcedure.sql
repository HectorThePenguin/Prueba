USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerFolioReimpresion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerFolioReimpresion]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerFolioReimpresion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/08/21
-- Description: Sp para obtener los folios de las salidas de productos por pagina
-- SalidaProducto_ObtenerFolioReimpresion 1, 4
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerFolioReimpresion]
@OrganizacionID INT,
@FolioSalida	INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
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
		, CL.Descripcion AS Cliente
	FROM SalidaProducto SP(NOLOCK)
	INNER JOIN TipoMovimiento TM (NOLOCK)
		ON (SP.TipoMovimientoID = TM.TipoMovimientoID) 
	INNER JOIN AlmacenInventarioLote AIL(NOLOCK)
		ON (SP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
	INNER JOIN AlmacenMovimientoDetalle AMD(NOLOCK)
		ON (SP.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID
			AND SP.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	LEFT JOIN Cliente CL(NOLOCK)
		ON (SP.ClienteID = CL.ClienteID)
	LEFT JOIN CuentaSAP C(NOLOCK)
		ON (SP.CuentaSAPID = C.CuentaSAPID)
	WHERE @OrganizacionID IN (SP.OrganizacionID, 0)
		AND SP.AlmacenInventarioLoteID > 0
		AND SP.AlmacenMovimientoID > 0
		AND SP.FolioSalida = @FolioSalida
	SET NOCOUNT OFF;
END

GO

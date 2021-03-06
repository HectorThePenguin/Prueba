USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerReimpresionSalidaProducto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerReimpresionSalidaProducto]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerReimpresionSalidaProducto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/08/21
-- Description: Sp para obtener los folios de las salidas de productos por pagina
-- SalidaProducto_ObtenerReimpresionSalidaProducto 30
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerReimpresionSalidaProducto]
@SalidaProductoID INT
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
	FROM SalidaProducto SP(NOLOCK)
	INNER JOIN TipoMovimiento TM(NOLOCK)
		ON (SP.TipoMovimientoID = TM.TipoMovimientoID) 
	INNER JOIN AlmacenInventarioLote AIL(NOLOCK)
		ON (SP.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID)
	INNER JOIN AlmacenMovimientoDetalle AMD
		ON (SP.AlmacenInventarioLoteID = AMD.AlmacenInventarioLoteID
			AND SP.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
	WHERE SP.SalidaProductoID = @SalidaProductoID
	SET NOCOUNT OFF;
END

GO

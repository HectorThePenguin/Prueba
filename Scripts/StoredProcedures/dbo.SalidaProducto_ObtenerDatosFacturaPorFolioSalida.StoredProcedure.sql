USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerDatosFacturaPorFolioSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaProducto_ObtenerDatosFacturaPorFolioSalida]
GO
/****** Object:  StoredProcedure [dbo].[SalidaProducto_ObtenerDatosFacturaPorFolioSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/14
-- Description: Sp para obtener los datos para generar la factura.
-- exec SalidaProducto_ObtenerDatosFacturaPorFolioSalida 1,1
--=============================================
CREATE PROCEDURE [dbo].[SalidaProducto_ObtenerDatosFacturaPorFolioSalida] 
@FolioSalida INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT SP.FechaSalida,
	--Datos Cliente
	Cte.CodigoSAP, Cte.Descripcion AS NombreCliente, Cte.RFC, Cte.Calle, Cte.CodigoPostal, Cte.Poblacion, Cte.Estado, Cte.Pais, MP.MetodoPagoID AS MetodoPago, Cte.CondicionPago, Cte.DiasPago,
	--Datos Producto
	P.Descripcion AS DescripcionProducto, (SP.PesoBruto - SP.PesoTara) AS CantidadKilos, SP.Precio, SP.Importe, SP.Piezas
	FROM SalidaProducto (NOLOCK) AS SP
	INNER JOIN Cliente (NOLOCK) AS Cte ON (Cte.ClienteID = SP.ClienteID)
	LEFT JOIN MetodoPago (NOLOCK) AS MP ON (Cte.MetodoPagoID = MP.MetodoPagoID)
	INNER JOIN AlmacenInventarioLote(NOLOCK) AS AIL ON (AIL.AlmacenInventarioLoteID = SP.AlmacenInventarioLoteID)
	INNER JOIN AlmacenInventario(NOLOCK) AS AI ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID)
	INNER JOIN Producto (NOLOCK) AS P ON (P.ProductoID = AI.ProductoID)
	WHERE SP.FolioSalida = @FolioSalida AND SP.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO

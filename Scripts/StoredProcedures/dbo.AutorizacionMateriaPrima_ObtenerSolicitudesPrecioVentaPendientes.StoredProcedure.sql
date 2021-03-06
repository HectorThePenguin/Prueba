USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesPrecioVentaPendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesPrecioVentaPendientes]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesPrecioVentaPendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 04/12/2014
-- Description: Sp para obtener las solicitudes pendientes del tipo movimiento precio de venta
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesPrecioVentaPendientes]
@OrganizacionID INT,
@TipoAutorizacionID INT,
@EstatusID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	/*		Precio de Venta		*/
	SELECT 
	AMP.Folio,
	AMP.AutorizacionMateriaPrimaID,
	P.Descripcion AS DesProducto, 
	A.Descripcion AS DesAlmacen,
	AMP.Lote AS Lote,
	AIL.PrecioPromedio AS CostoUnitario,
	AMP.Precio AS PrecioVenta,
	AMP.Justificacion
	FROM AutorizacionMateriaPrima AS AMP 
	INNER JOIN SalidaProducto AS SP
	ON AMP.AutorizacionMateriaPrimaID = SP.AutorizacionMateriaPrimaID AND AMP.OrganizacionID = @OrganizacionID
	AND AMP.TipoAutorizacionID = @TipoAutorizacionID AND AMP.EstatusID = @EstatusID AND AMP.Activo = @Activo
	INNER JOIN AlmacenInventarioLote AS AIL 
	ON SP.AlmacenInventarioLoteID=AIL.AlmacenInventarioLoteID
	INNER JOIN Producto AS P 
	ON AMP.ProductoID = P.ProductoID 
	INNER JOIN Almacen AS A 
	ON AMP.AlmacenID = A.AlmacenID
	SET NOCOUNT OFF;
END

GO

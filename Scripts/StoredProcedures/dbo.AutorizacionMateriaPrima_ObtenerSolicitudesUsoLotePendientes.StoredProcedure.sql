USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesUsoLotePendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesUsoLotePendientes]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesUsoLotePendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 04/12/2014
-- Description: Sp para obtener las solicitudes pendientes del tipo de uso de lote
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesUsoLotePendientes]
@OrganizacionID INT,
@TipoAutorizacionID INT,
@EstatusID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	/*		Uso de Lote		*/
	SELECT 
	AMP.Folio,
	AMP.AutorizacionMateriaPrimaID,
	P.Descripcion AS DesProducto, 
	A.Descripcion AS DesAlmacen, 
	PMP.InventarioLoteIDOrigen AS Lote,
	AMP.Precio AS CostoUnitario,
	AMP.Lote AS LoteUtilizar,
	AMP.Justificacion
	FROM AutorizacionMateriaPrima AS AMP 
	INNER JOIN Producto AS P 
	ON AMP.OrganizacionID = @OrganizacionID AND AMP.TipoAutorizacionID = @TipoAutorizacionID 
	AND AMP.EstatusID = @EstatusID AND AMP.ProductoID = P.ProductoID AND AMP.Activo = @Activo
	INNER JOIN Almacen AS A 
	ON AMP.AlmacenID = A.AlmacenID 
	INNER JOIN ProgramacionMateriaPrima AS PMP
	ON AMP.AutorizacionMateriaPrimaID = PMP.AutorizacionMateriaPrimaID
	SET NOCOUNT OFF;
END

GO

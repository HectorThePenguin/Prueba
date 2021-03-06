USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Gilberto Carranza
-- Fecha: 2014-08-07
-- Descripci�n:	Obtiene una Recepcion Por Folio
-- RecepcionProducto_ObtenerPorFolioOrganizacion 5, 1
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacion]
@FolioRecepcion INT
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT RP.RecepcionProductoID
			,  RP.FolioRecepcion
			,  RP.FechaSolicitud
			,  RP.FechaRecepcion
			,  RP.AlmacenMovimientoID
			,  RP.Factura
			,  A.AlmacenID
			,  A.Descripcion				AS Almacen
			,  A.CodigoAlmacen
			,  A.CuentaDiferencias
			,  A.CuentaInventario
			,  A.CuentaInventarioTransito
			,  P.ProveedorID
			,  P.CodigoSAP
			,  P.Descripcion				AS Proveedor
		FROM RecepcionProducto RP
		INNER JOIN Almacen A
			ON (RP.AlmacenID = A.AlmacenID
				AND A.OrganizacionID = @OrganizacionID)
		INNER JOIN Proveedor P
			ON (RP.ProveedorID = P.ProveedorID)
		WHERE RP.FolioRecepcion = @FolioRecepcion
	SET NOCOUNT OFF
END

GO

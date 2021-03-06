USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ConciliacionObtenerPorXML]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionProducto_ConciliacionObtenerPorXML]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ConciliacionObtenerPorXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- RecepcionProducto_ConciliacionObtenerPorXML
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionProducto_ConciliacionObtenerPorXML]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON;
		CREATE TABLE #tRecepcionProducto
		(
			RecepcionProductoID		INT
			, AlmacenID				INT
			, FolioRecepcion		INT
			, FolioOrdenCompra		VARCHAR(250)
			, FechaSolicitud		SMALLDATETIME
			, ProveedorID			INT
			, FechaRecepcion		SMALLDATETIME
			, AlmacenMovimientoID	BIGINT
			, Factura				VARCHAR(250)
			, Proveedor				VARCHAR(250)
			, CodigoSAP				VARCHAR(250)
			, OrganizacionID		INT
		)
		INSERT INTO #tRecepcionProducto
		SELECT RP.RecepcionProductoID
			,  RP.AlmacenID
			,  RP.FolioRecepcion
			,  RP.FolioOrdenCompra
			,  RP.FechaSolicitud
			,  RP.ProveedorID
			,  RP.FechaRecepcion
			,  RP.AlmacenMovimientoID
			,  RP.Factura
			,  P.Descripcion
			,  P.CodigoSAP
			,  A.OrganizacionID
		FROM RecepcionProducto RP
		INNER JOIN
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @XmlAlmacenMovimiento.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x	ON (RP.AlmacenMovimientoID = x.AlmacenMovimientoID)
		INNER JOIN Proveedor P
			ON (RP.ProveedorID = P.ProveedorID)
		INNER JOIN Almacen A
			ON (RP.AlmacenID = A.AlmacenID)
		SELECT RecepcionProductoID
			,  AlmacenID
			,  FolioRecepcion
			,  FolioOrdenCompra
			,  FechaSolicitud
			,  ProveedorID
			,  FechaRecepcion
			,  AlmacenMovimientoID
			,  Factura
			,  Proveedor
			,  CodigoSAP
			,  OrganizacionID
		FROM #tRecepcionProducto
		SELECT RPD.RecepcionProductoDetalleID
			,  RPD.RecepcionProductoID
			,  RPD.ProductoID
			,  RPD.Cantidad
			,  RPD.PrecioPromedio
			,  RPD.Importe
			,  P.Descripcion		AS Producto
			,  UM.UnidadID
			,  SF.SubFamiliaID
			,  SF.Descripcion		AS SubFamilia
			,  F.FamiliaID
			,  F.Descripcion		AS Familia
		FROM #tRecepcionProducto RP
		INNER JOIN RecepcionProductoDetalle RPD
			ON (RP.RecepcionProductoID = RPD.RecepcionProductoID)
		INNER JOIN Producto P
			ON (RPD.ProductoID = P.ProductoID)
		INNER JOIN UnidadMedicion UM
			ON (P.UnidadID = UM.UnidadID)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN Familia F
			ON (SF.FamiliaID = F.FamiliaID)
		DROP TABLE #tRecepcionProducto
	SET NOCOUNT OFF;
END

GO

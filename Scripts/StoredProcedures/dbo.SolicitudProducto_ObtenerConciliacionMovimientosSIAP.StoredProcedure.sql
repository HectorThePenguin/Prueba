USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudProducto_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudProducto_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- SolicitudProducto_ObtenerConciliacionMovimientosSIAP
-- =============================================
CREATE PROCEDURE [dbo].[SolicitudProducto_ObtenerConciliacionMovimientosSIAP]
@XmlAlmacenMovimiento XML
AS
BEGIN
	SET NOCOUNT ON;
		CREATE TABLE #tSolicitudProducto
		(
			SolicitudProductoID		INT
			, OrganizacionID		INT
			, FolioSolicitud		INT
			, FechaSolicitud		SMALLDATETIME
			, EstatusID				INT
			, FechaAutorizado		SMALLDATETIME
			, FechaEntrega			SMALLDATETIME
			, CentroCostoID			INT
			, AlmacenID				INT
			, AlmacenMovimientoID	BIGINT
			, Almacen				VARCHAR(255)
			, TipoAlmacenID			INT
		)
		INSERT INTO #tSolicitudProducto
		SELECT SP.SolicitudProductoID
			,  SP.OrganizacionID
			,  SP.FolioSolicitud
			,  SP.FechaSolicitud
			,  SP.EstatusID
			,  SP.FechaAutorizado
			,  SP.FechaEntrega
			,  SP.CentroCostoID
			,  SP.AlmacenID
			,  SP.AlmacenMovimientoID
			,  A.Descripcion			AS Almacen
			,  A.TipoAlmacenID
		FROM 
		(
			SELECT T.N.value('./AlmacenMovimientoID[1]','BIGINT') AS AlmacenMovimientoID
			FROM @XmlAlmacenMovimiento.nodes('/ROOT/AlmacenMovimiento') as T(N)
		) x
		INNER JOIN SolicitudProducto SP
		 ON (X.AlmacenMovimientoID = SP.AlmacenMovimientoID)
		INNER JOIN Almacen A
			ON (SP.AlmacenID = A.AlmacenID)
		GROUP BY SP.SolicitudProductoID
			,  SP.OrganizacionID
			,  SP.FolioSolicitud
			,  SP.FechaSolicitud
			,  SP.EstatusID
			,  SP.FechaAutorizado
			,  SP.FechaEntrega
			,  SP.CentroCostoID
			,  SP.AlmacenID
			,  SP.AlmacenMovimientoID
			,  A.Descripcion
			,  A.TipoAlmacenID
		SELECT SolicitudProductoID
			,  OrganizacionID
			,  FolioSolicitud
			,  FechaSolicitud
			,  EstatusID
			,  FechaAutorizado
			,  FechaEntrega
			,  CentroCostoID
			,  AlmacenID
			,  AlmacenMovimientoID
			,  Almacen
			,  TipoAlmacenID
		FROM #tSolicitudProducto
		SELECT SPD.SolicitudProductoDetalleID
			,  SPD.SolicitudProductoID
			,  SPD.ProductoID
			,  SPD.Cantidad
			,  ISNULL(SPD.CamionRepartoID, 0) AS CamionRepartoID
			,  SPD.EstatusID
			,  P.Descripcion	AS Producto
			,  P.SubFamiliaID
			,  P.UnidadID
			,  AMD.Precio AS Precio
		FROM #tSolicitudProducto SP
		INNER JOIN SolicitudProductoDetalle SPD
			ON (SP.SolicitudProductoID = SPD.SolicitudProductoID)
		INNER JOIN Producto P
			ON (SPD.ProductoID = P.ProductoID)
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (SP.AlmacenMovimientoID = AMD.AlmacenMovimientoID
				AND SPD.ProductoID = AMD.ProductoID)
		GROUP BY SPD.SolicitudProductoDetalleID
			,  SPD.SolicitudProductoID
			,  SPD.ProductoID
			,  SPD.Cantidad
			,  SPD.CamionRepartoID
			,  SPD.EstatusID
			,  P.Descripcion
			,  P.SubFamiliaID
			,  P.UnidadID
			,  AMD.Precio
		DROP TABLE #tSolicitudProducto
	SET NOCOUNT OFF;
END

GO

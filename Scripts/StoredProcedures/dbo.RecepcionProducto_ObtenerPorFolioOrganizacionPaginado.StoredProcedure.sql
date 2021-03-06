USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacionPaginado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacionPaginado]
GO
/****** Object:  StoredProcedure [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacionPaginado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Gilberto Carranza
-- Fecha: 2014-08-07
-- Descripci�n:	Obtiene una Recepcion Por Folio
-- RecepcionProducto_ObtenerPorFolioOrganizacionPaginado 0, 1, '', 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[RecepcionProducto_ObtenerPorFolioOrganizacionPaginado]
@FolioRecepcion		INT
, @OrganizacionID	INT
, @Proveedor		VARCHAR(100)
, @Inicio			INT
, @Limite			INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS [RowNum]
			,  RP.RecepcionProductoID
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
		INTO #tRecepcion
		FROM RecepcionProducto RP
		INNER JOIN Almacen A
			ON (RP.AlmacenID = A.AlmacenID
				AND A.OrganizacionID = @OrganizacionID)
		INNER JOIN Proveedor P
			ON (RP.ProveedorID = P.ProveedorID
				AND (@Proveedor = '' OR P.Descripcion LIKE '%' + @Proveedor + '%'))
		WHERE @FolioRecepcion IN (RP.FolioRecepcion, 0)
		SELECT RecepcionProductoID
			,  FolioRecepcion
			,  FechaSolicitud
			,  FechaRecepcion
			,  AlmacenMovimientoID
			,  Factura
			,  AlmacenID
			,  Almacen
			,  CodigoAlmacen
			,  CuentaDiferencias
			,  CuentaInventario
			,  CuentaInventarioTransito
			,  ProveedorID
			,  CodigoSAP
			,  Proveedor
		FROM #tRecepcion
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT COUNT(RecepcionProductoID) AS [TotalReg]
		FROM #tRecepcion
		DROP TABLE #tRecepcion
	SET NOCOUNT OFF
END

GO

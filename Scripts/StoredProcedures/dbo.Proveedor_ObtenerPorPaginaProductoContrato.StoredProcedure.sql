USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaProductoContrato]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaProductoContrato]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaProductoContrato]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- Proveedor_ObtenerPorPaginaProductoContrato '', 1, 100, 1, 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaProductoContrato]
@Descripcion VARCHAR(50),
@OrganizacionID INT,
@ProductoID INT,
@Activo INT
,@Inicio INT
, @Limite INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS RowNum
			,  P.ProveedorID
			,  P.Descripcion
			,  P.CodigoSAP
			,  TP.TipoProveedorID
			,  TP.Descripcion AS TipoProveedor
		INTO #tProveedor
		FROM Proveedor P
		INNER JOIN TipoProveedor TP
			ON (P.TipoProveedorID = TP.TipoProveedorID)
		LEFT JOIN Contrato C
			ON (P.ProveedorID = C.ProveedorID				
				AND @ProductoID IN (C.ProductoID, 0))
		WHERE (@Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%')
			AND P.Activo = @Activo
			AND @OrganizacionID IN (C.OrganizacionID, 0)
			AND (@OrganizacionID = 0 OR C.Activo = 1)
		GROUP BY P.ProveedorID
			  ,  P.Descripcion
			  ,  P.CodigoSAP
			  ,  TP.TipoProveedorID
			  ,  TP.Descripcion
		SELECT ProveedorID
			,  Descripcion
			,  CodigoSAP
			,  TipoProveedorID
			,  TipoProveedor
		FROM #tProveedor
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT COUNT(ProveedorID) AS TotalReg
		FROM #tProveedor
		DROP TABLE #tProveedor
	SET NOCOUNT OFF;
END

GO

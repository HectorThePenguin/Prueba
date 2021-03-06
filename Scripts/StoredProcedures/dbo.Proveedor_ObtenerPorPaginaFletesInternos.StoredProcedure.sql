USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaFletesInternos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaFletesInternos]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaFletesInternos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz	
-- Create date: 2014/07/31
-- Description: Obtiene los proveedores que tengan fletes internos en la organizacion por producto
-- Proveedor_ObtenerPorPaginaFletesInternos '','',1,1,1,15
--=============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaFletesInternos]
@CodigoSAP VARCHAR(10),
@Descripcion NVARCHAR(50),	
@OrganizacionID INT,
@ProductoId INT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY P.Descripcion ASC
			) AS RowNum, P.ProveedorID, P.Descripcion, P.TipoProveedorID, TP.Descripcion AS TipoProveedor, P.CodigoSAP, P.Activo
	INTO #tmpProveedores
	FROM FleteInterno (NOLOCK) AS FI
	INNER JOIN FleteInternoDetalle (NOLOCK) AS FID ON (FI.FleteInternoID = FID.FleteInternoID)
	INNER JOIN Proveedor (NOLOCK) AS P ON (P.ProveedorID = FID.ProveedorID)
	INNER JOIN TipoProveedor (NOLOCK) AS TP ON (TP.TipoProveedorID = P.TipoProveedorID)
	WHERE FI.OrganizacionID = @OrganizacionId 
	AND FI.ProductoID = @ProductoId
	AND FI.Activo = 1 
	AND FID.Activo = 1 
	AND P.Activo = 1
	AND (@CodigoSAP = 0 OR P.CodigoSAP LIKE '%' + @CodigoSAP + '%')
	AND (@Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%')
	GROUP BY P.ProveedorID, P.Descripcion, P.TipoProveedorID, TP.Descripcion, P.CodigoSAP, P.Activo
	SELECT  ProveedorID, Descripcion, TipoProveedorID, TipoProveedor, CodigoSAP, Activo
	FROM #tmpProveedores
	WHERE RowNum BETWEEN @Inicio
	AND @Limite
	SELECT COUNT(ProveedorID) AS TotalReg
	FROM #tmpProveedores
	DROP TABLE #tmpProveedores
	SET NOCOUNT OFF;
END

GO

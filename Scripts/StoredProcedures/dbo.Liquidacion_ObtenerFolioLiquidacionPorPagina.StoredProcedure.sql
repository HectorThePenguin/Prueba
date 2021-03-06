USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerFolioLiquidacionPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerFolioLiquidacionPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerFolioLiquidacionPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 17/12/2014
-- Description: 
-- SpName     : Liquidacion_ObtenerFolioLiquidacionPorPagina 1, '', 1, 15
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerFolioLiquidacionPorPagina]
@OrganizacionID INT
, @Producto		VARCHAR(255)
, @Inicio		INT
, @Limite		INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT ROW_NUMBER() OVER (ORDER BY Prod.Descripcion ASC) AS [RowNum]
			,  L.ContratoID
			,  L.Folio
			,  L.Fecha
			,  C.ProductoID
			,  C.ProveedorID
			,  Prov.Descripcion		AS Proveedor
			,  Prod.Descripcion		AS Producto
			,  L.OrganizacionID
			,  L.LiquidacionID
			,  O.Descripcion		AS Organizacion
		INTO #tLiquidacion
		FROM Liquidacion L
		INNER JOIN Contrato C
			ON (L.ContratoID = C.ContratoID)
		INNER JOIN Proveedor Prov
			ON (C.ProveedorID = Prov.ProveedorID)
		INNER JOIN Producto Prod
			ON (C.ProductoID = Prod.ProductoID
				AND (@Producto = '' OR Prod.Descripcion LIKE '%' + @Producto + '%'))
		INNER JOIN Organizacion O
			ON (L.OrganizacionID = O.OrganizacionID)
		WHERE CAST(L.Fecha AS DATE) >= CAST(GETDATE() - 6 AS DATE)
			AND L.OrganizacionID = @OrganizacionID
	SELECT ContratoID
		,  Folio
		,  Fecha
		,  ProductoID
		,  ProveedorID
		,  Proveedor
		,  Producto
		,  OrganizacionID
		,  LiquidacionID
		,  Organizacion
	FROM #tLiquidacion
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ContratoID) AS [TotalReg]
	FROM #tLiquidacion
	DROP TABLE #tLiquidacion
	SET NOCOUNT OFF;
END

GO

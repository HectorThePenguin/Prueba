USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerFolioPorPaginaEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ObtenerFolioPorPaginaEstatus]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ObtenerFolioPorPaginaEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz	
-- Create date: 2014/07/24
-- Description: Obtiene los folios de entrada por FolioProducto por estatus, organizacion y familia
-- EntradaProducto_ObtenerFolioPorPaginaEstatus '0','',1,1,26,1,15
--=============================================
CREATE PROCEDURE [dbo].[EntradaProducto_ObtenerFolioPorPaginaEstatus]
@Folio VARCHAR(15),
@DescripcionProducto VARCHAR(50),
@OrganizacionID INT,
@FamiliaID INT,
@EstatusID INT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROW_NUMBER() OVER (
			ORDER BY EP.Folio ASC
			) AS RowNum, 
		EP.EntradaProductoID,
		EP.Folio,
		EP.ProductoID,
		P.Descripcion,
		PV.Descripcion AS Proveedor,
		C.PlacaCamion AS Placas
	INTO #DatosEntradaProducto
	FROM EntradaProducto (NOLOCK) EP
	INNER JOIN Producto (NOLOCK) P ON (EP.ProductoID = P.ProductoID) 
	INNER JOIN SubFamilia (NOLOCK) SF ON (P.SubFamiliaID = SF.SubFamiliaID AND SF.FamiliaID = @FamiliaID)
	INNER JOIN RegistroVigilancia (NOLOCK) RV ON (EP.RegistroVigilanciaID = RV.RegistroVigilanciaID)
	INNER JOIN Proveedor (NOLOCK) PV ON (PV.ProveedorID = RV.ProveedorIDMateriasPrimas)
	INNER JOIN Camion (NOLOCK) C ON (RV.CamionID = C.CamionID)
	WHERE EP.Activo = 1
	AND P.Activo = 1
	AND EP.OrganizacionID = @OrganizacionID
	AND EP.EstatusID = @EstatusID
	AND (@Folio = 0 OR EP.Folio LIKE '%' + @Folio + '%')
	AND (@DescripcionProducto = '' OR P.Descripcion LIKE '%' + @DescripcionProducto + '%')
	SELECT  EntradaProductoID,
			Folio,
			ProductoID,
			Descripcion,
			Proveedor,
			Placas
	FROM #DatosEntradaProducto
	WHERE RowNum BETWEEN @Inicio
	AND @Limite
	SELECT COUNT(Folio) AS TotalReg
	FROM #DatosEntradaProducto
	DROP TABLE #DatosEntradaProducto
	SET NOCOUNT OFF;
END

GO

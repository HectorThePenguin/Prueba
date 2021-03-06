USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorDescripcionLoteExistencia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorDescripcionLoteExistencia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorDescripcionLoteExistencia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alejandro Quiroz
-- Create date: 14/07/2014
-- Description: Obtiene los productos en lote con existencia
-- ============================================= 
-- DROP PROCEDURE [Producto_ObtenerPorDescripcionLoteExistencia]
-- Producto_ObtenerPorDescripcionLoteExistencia '',1,290,0,1,15
CREATE PROCEDURE [dbo].[Producto_ObtenerPorDescripcionLoteExistencia]
@Descripcion VARCHAR(50),
@Activo BIT,
@AlmacenID INT,
@FamiliaID INT = 0,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT ''
			  FROM Almacen A (NOLOCK)
			  INNER JOIN Organizacion O (NOLOCK)
					ON A.OrganizacionID = O.OrganizacionID
			  INNER JOIN TipoOrganizacion T (NOLOCK)
					ON T.TipoOrganizacionID = O.TipoOrganizacionID
			  WHERE AlmacenId = @AlmacenID AND T.TipoOrganizacionID IN(4,6,7))
	BEGIN
		SELECT DISTINCT 
			   P.ProductoID
			,  P.Descripcion
			,  P.UnidadID
		INTO #ProductosTempCentros
		FROM Producto AS P (NOLOCK)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN Sukarne.dbo.CatAlmacenInventario AS AI (NOLOCK) ON P.ProductoID = AI.ProductoID
		--LEFT JOIN Sukarne.dbo.AlmacenInventarioLote AS AIL (NOLOCK) ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID AND AIL.Activo = 1)
		WHERE P.Activo = @Activo
		AND AI.AlmacenID = @AlmacenID  
		AND AI.Cantidad > 0
		AND @FamiliaID IN (SF.FamiliaID, 0)
		AND (@Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%')
		SELECT 
			ROW_NUMBER() OVER (
				ORDER BY Descripcion ASC
				) AS RowNum,
				ProductoID,
				Descripcion
				, UnidadID
		INTO #ProductosCentros
		FROM #ProductosTempCentros
		SELECT DISTINCT ProductoID
			,  Descripcion
			,  UnidadID
		FROM #ProductosCentros
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT COUNT(ProductoID) AS TotalReg
		FROM #ProductosCentros
		DROP TABLE #ProductosCentros
	END
	ELSE
	BEGIN
		SELECT DISTINCT 
			   P.ProductoID
			,  P.Descripcion
			,  P.UnidadID
		INTO #ProductosTemp
		FROM Producto AS P (NOLOCK)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN AlmacenInventario AS AI (NOLOCK) ON P.ProductoID = AI.ProductoID
		LEFT JOIN AlmacenInventarioLote AS AIL (NOLOCK) ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID AND AIL.Activo = 1)
		WHERE P.Activo = @Activo
		AND AI.AlmacenID = @AlmacenID  
		AND AI.Cantidad > 0
		AND @FamiliaID IN (SF.FamiliaID, 0)
		AND (@Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%')
		SELECT 
			ROW_NUMBER() OVER (
				ORDER BY Descripcion ASC
				) AS RowNum,
				ProductoID,
				Descripcion
				, UnidadID
		INTO #Productos
		FROM #ProductosTemp
		SELECT DISTINCT ProductoID
			,  Descripcion
			,  UnidadID
		FROM #Productos
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT COUNT(ProductoID) AS TotalReg
		FROM #Productos
		DROP TABLE #Productos
	END
	SET NOCOUNT OFF;
END

GO

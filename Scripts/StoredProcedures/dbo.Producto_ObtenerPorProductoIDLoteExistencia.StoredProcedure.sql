USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDLoteExistencia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDLoteExistencia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDLoteExistencia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alejandro Quiroz
-- Create date: 15/07/2014
-- Description: Obtiene los productos en lote con existencia por ID
-- ============================================= 
-- DROP PROCEDURE [Producto_ObtenerPorProductoIDLoteExistencia]
-- Producto_ObtenerPorProductoIDLoteExistencia 146,1,290,0
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDLoteExistencia]
@ProductoID INT,
@Activo BIT,
@AlmacenID INT,
@FamiliaID INT = 0
AS
BEGIN
	IF EXISTS(SELECT ''
				  FROM Almacen A (NOLOCK)
				  INNER JOIN Organizacion O (NOLOCK)
						ON A.OrganizacionID = O.OrganizacionID
				  INNER JOIN TipoOrganizacion T (NOLOCK)
						ON T.TipoOrganizacionID = O.TipoOrganizacionID
				  WHERE AlmacenId = @AlmacenID AND T.TipoOrganizacionID IN(4,6,7))
	BEGIN
		SELECT 
			P.ProductoID, 
			P.Descripcion, 
			P.UnidadID
		FROM Producto AS P (NOLOCK)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN Sukarne.dbo.CatAlmacenInventario AS AI (NOLOCK) ON P.ProductoID = AI.ProductoID
		WHERE P.Activo = @Activo
		AND P.ProductoID = @ProductoID
		AND AI.Cantidad > 0
		AND AI.AlmacenID = @AlmacenID
		AND @FamiliaID IN (SF.FamiliaID, 0)
	END
	ELSE
	BEGIN
		SELECT 
			P.ProductoID, 
			P.Descripcion, 
			P.UnidadID
		FROM Producto AS P (NOLOCK)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN AlmacenInventario AS AI (NOLOCK) ON P.ProductoID = AI.ProductoID
		LEFT JOIN AlmacenInventarioLote AS AIL (NOLOCK) ON (AI.AlmacenInventarioID = AIL.AlmacenInventarioID AND AIL.Activo = 1)
		WHERE P.Activo = @Activo
		AND P.ProductoID = @ProductoID
		-- AND AI.Cantidad > 0
		AND AI.AlmacenID = @AlmacenID
		AND @FamiliaID IN (SF.FamiliaID, 0)
	END
END

GO

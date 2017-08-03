USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDSubFamiliaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDSubFamiliaID]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDSubFamiliaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 14/07/2014
-- Description:  Obtener el Producto en base a su ID y su subfamilia
-- Producto_ObtenerPorProductoIDSubFamiliaID 2
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDSubFamiliaID] 
@ProductoID INT,
@SubFamiliaID INT,
@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT pr.ProductoID
		,pr.Descripcion
		,sf.SubFamiliaID
		,sf.Descripcion as [SubFamilia]
		,f.FamiliaID
		,f.Descripcion as [Familia]
		,pr.UnidadID
		,um.Descripcion as [Unidad]
		,um.ClaveUnidad as [ClaveUnidad]
		,pr.ManejaLote
		,pr.Activo
	FROM Producto pr
	INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia f on sf.FamiliaID = f.FamiliaID
	INNER JOIN UnidadMedicion um on um.UnidadID = pr.UnidadID
	WHERE pr.ProductoID = @ProductoID
		AND pr.SubFamiliaID = @SubFamiliaID
	    AND pr.Activo = @Activo
	SET NOCOUNT OFF;
END

GO

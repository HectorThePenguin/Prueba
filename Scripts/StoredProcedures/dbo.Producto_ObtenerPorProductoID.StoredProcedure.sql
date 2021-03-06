USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoID]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 04/03/2014
-- Description:  Obtener el Producto en base a su ID
-- Producto_ObtenerPorProductoID 2
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoID] 
@ProductoID INT
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
		,um.ClaveUnidad
		,pr.ManejaLote
		,pr.Activo
	FROM Producto pr
	INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia f on sf.FamiliaID = f.FamiliaID
	INNER JOIN UnidadMedicion um on um.UnidadID = pr.UnidadID
	WHERE pr.ProductoID = @ProductoID
		AND pr.Activo = 1
	SET NOCOUNT OFF;
END

GO

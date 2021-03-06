USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDSinActivo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDSinActivo]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDSinActivo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Pedro Delgado
-- Create date: 02/07/2014
-- Description:  Obtener producto sin importar el Activo
-- Producto_ObtenerPorProductoIDSinActivo 2
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDSinActivo] 
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
	SET NOCOUNT OFF;
END

GO

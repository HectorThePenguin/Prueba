USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDAlmacenID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDAlmacenID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 17/07/2014
-- Description:  Obtener el Producto en base a su ID
-- Producto_ObtenerPorProductoIDAlmacenID 2, 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDAlmacenID] 
@ProductoID INT,
@AlmacenID INT,
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
		,pr.ManejaLote
		,pr.Activo
	FROM Producto pr
	INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia f on sf.FamiliaID = f.FamiliaID
	INNER JOIN UnidadMedicion um on um.UnidadID = pr.UnidadID
	INNER JOIN AlmacenInventario ai ON ai.ProductoID = pr.ProductoID
	INNER JOIN Almacen Al ON Al.AlmacenID = ai.AlmacenID
	WHERE pr.ProductoID = @ProductoID
	AND pr.Activo = @Activo
	AND Al.AlmacenID = @AlmacenID
	SET NOCOUNT OFF;
END

GO

USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDSubFamilia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDSubFamilia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDSubFamilia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Vel�zquez Araujo
-- Create date: 17/01/2014
-- Description:  Obtener el Producto en base a su ID y SubFamilia
-- Producto_ObtenerPorProductoIDSubFamilia
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDSubFamilia] @ProductoID INT
	,@SubFamiliaId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT pr.ProductoID
		,pr.Descripcion
		,sf.SubFamiliaID
		,sf.Descripcion [SubFamilia]
		,fa.FamiliaID
		,fa.Descripcion [Familia]
		,UnidadID
		,pr.Activo
	FROM Producto pr
	INNER JOIN SubFamilia sf on pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia fa ON sf.FamiliaID = fa.FamiliaID
	WHERE sf.SubFamiliaID = @SubFamiliaId
	AND pr.ProductoID = @ProductoID
	AND pr.Activo = 1
	SET NOCOUNT OFF;
END

GO

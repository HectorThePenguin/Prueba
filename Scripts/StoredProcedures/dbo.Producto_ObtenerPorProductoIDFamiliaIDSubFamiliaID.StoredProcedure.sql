USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDFamiliaIDSubFamiliaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorProductoIDFamiliaIDSubFamiliaID]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorProductoIDFamiliaIDSubFamiliaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Emir Lezama
-- Create date: 05/11/2014
-- Description:  Obtener la descripcion del Producto en base a su ID, de Familia Materia Primas y SubFamilia Granos
-- Producto_ObtenerPorProductoIDFamiliaIDSubFamiliaID
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorProductoIDFamiliaIDSubFamiliaID] 
@ProductoID INT,
@FamiliaID INT,
@SubFamiliaID INT,
@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT pr.ProductoID
	,pr.Descripcion
	,pr.Descripcion
	,pr.Descripcion
	FROM Producto AS pr
	INNER JOIN SubFamilia AS sf 
	ON pr.SubFamiliaID = sf.SubFamiliaID
	WHERE pr.ProductoID = @ProductoID
		AND sf.FamiliaID = @FamiliaID
		AND pr.SubFamiliaID = @SubFamiliaID
	    AND pr.Activo = @Activo
	SET NOCOUNT OFF;
END

GO

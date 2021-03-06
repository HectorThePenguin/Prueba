USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorIndicador]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorIndicador]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorIndicador]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/12/29
-- Description: Procedimiento para obtener los productos por indicador
-- Producto_ObtenerPorIndicador 115, 1
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorIndicador]
@ProductoID		INT
, @IndicadorID	INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT P.ProductoID
			,  P.Descripcion
			,  P.SubFamiliaID
			,  P.UnidadID
			,  SF.Descripcion	AS SubFamilia
			,  F.Descripcion	AS Familia
			,  UM.Descripcion	AS UnidadMedicion
			,  IP.IndicadorID
			,  F.FamiliaID
		FROM Producto P
		INNER JOIN IndicadorProducto IP
			ON (P.ProductoID = IP.ProductoID
				AND IP.IndicadorID = @IndicadorID
				AND IP.ProductoID = @ProductoID
				AND IP.Activo = 1)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN Familia F
			ON (SF.FamiliaID = F.FamiliaID)
		INNER JOIN UnidadMedicion UM
			ON (P.UnidadID = UM.UnidadID)
	SET NOCOUNT OFF;
END

GO

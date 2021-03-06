USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorIndicadorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorIndicadorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorIndicadorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/12/29
-- Description: Procedimiento para obtener los productos por indicador
-- Producto_ObtenerPorIndicadorPagina '', 2, 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorIndicadorPagina]
@Descripcion	VARCHAR(100)
, @IndicadorID	INT
, @Activo		BIT
, @Inicio		INT
, @Limite		INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS [RowNum]
			,  P.ProductoID
			,  P.Descripcion
			,  P.SubFamiliaID
			,  P.UnidadID
			,  SF.Descripcion	AS SubFamilia
			,  F.Descripcion	AS Familia
			,  UM.Descripcion	AS UnidadMedicion
			,  IP.IndicadorID
			,  F.FamiliaID
		INTO #Producto
		FROM Producto P
		INNER JOIN IndicadorProducto IP
			ON (P.ProductoID = IP.ProductoID
				AND IP.IndicadorID = @IndicadorID
				AND IP.Activo = @Activo)
		INNER JOIN SubFamilia SF
			ON (P.SubFamiliaID = SF.SubFamiliaID)
		INNER JOIN Familia F
			ON (SF.FamiliaID = F.FamiliaID)
		INNER JOIN UnidadMedicion UM
			ON (P.UnidadID = UM.UnidadID)
		WHERE @Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%'
		SELECT ProductoID
			,  Descripcion
			,  SubFamiliaID
			,  UnidadID
			,  SubFamilia
			,  Familia
			,  UnidadMedicion
			,  IndicadorID
			,  FamiliaID
		FROM #Producto
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT
		COUNT(ProductoID) AS [TotalReg]
		FROM #Producto
		DROP TABLE #Producto
	SET NOCOUNT OFF;
END

GO
